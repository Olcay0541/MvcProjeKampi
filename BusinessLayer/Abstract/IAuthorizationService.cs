using EntityLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAuthorizationService
    {
        void Register(string adminMail, string password);
        bool Login(Login loginDto);
        bool WriterLogin(WriterLogin writerLoginDto);
        void WriterRegister(string mail, string password);
    }
}
