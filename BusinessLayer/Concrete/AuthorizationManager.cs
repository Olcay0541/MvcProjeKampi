using BusinessLayer.Abstract;
using BusinessLayer.Utilities.Hashing;
using EntityLayer.Concrete;
using EntityLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AuthorizationManager : IAuthorizationService
    {
        IAdminService _adminService;
        IWriterService _writerService;
        public AuthorizationManager(IAdminService adminService, IWriterService writerService)
        {
            _adminService = adminService;
            _writerService = writerService;
        }
        public bool Login(Login login)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var userNameHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.AdminUserName));
                var user = _adminService.GetList();
                foreach (var item in user)
                {
                    if (HashingHelper.VerifyPasswordHash(login.AdminUserName, login.AdminPassword, item.AdminUserNamee, item.AdminPasswordHash, item.AdminPasswordSalt))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public void Register(string userName, string password)
        {
            byte[] userNameHash, passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userName, password, out userNameHash, out passwordHash, out passwordSalt);
            var admin = new Admin
            {
                AdminUserNamee = userNameHash,
                AdminPasswordHash = passwordHash,
                AdminPasswordSalt = passwordSalt,
                RoleId = 2
            };
            _adminService.AdminAddBL(admin);
        }

        public bool WriterLogin(WriterLogin writerLoginDto)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var writer = _writerService.GetList();
                foreach (var item in writer)
                {
                    if (HashingHelper.WriterVerifyPasswordHash(writerLoginDto.WriterPassword, item.WriterPasswordHash, item.WriterPasswordSalt))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public void WriterRegister(string mail, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.WriterCreatePasswordHash(password, out passwordHash, out passwordSalt);
            var writer = new Writer
            {
                WriterMail = mail,
                WriterPasswordHash = passwordHash,
                WriterPasswordSalt = passwordSalt,
            };
            _writerService.WriterAdd(writer);
        }
    }
}
