using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TalentManager : ITalentCardService
    {
        ITalentDal _talentDal;

        public TalentManager(ITalentDal talentDal)
        {
            _talentDal = talentDal;
        }

        public void Add(TalentCard talentCard)
        {
            _talentDal.Insert(talentCard);
        }

        public void Delete(TalentCard talentCard)
        {
            _talentDal.Delete(talentCard);
        }

        public List<TalentCard> GetAll()
        {
            return _talentDal.List();
        }

        public TalentCard GetById(int id)
        {
            return _talentDal.Get(x => x.TalentID == id);
        }

        public void Update(TalentCard talentCard)
        {
            _talentDal.Update(talentCard);
        }
    }
}
