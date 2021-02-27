using System;
using System.Collections.Generic;
using System.Text;
using BisikletKlinigi.DataAccess.Concrete;
using Business.Abstract;
using Business.Constants;
using Business.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete
{
    class ContactUsManager : IContactUsService
    {
        private IContactUsDal _contactUsDal;

        public ContactUsManager(IContactUsDal contactUsDal)
        {
            _contactUsDal = contactUsDal;
        }

        public IResult Add(ContactUs contactUs)
        {
            _contactUsDal.Add(contactUs);
            return new SuccessResult(Messages.MessagesAdded);
        }

        public IResult Delete(ContactUs contactUs)
        {
            _contactUsDal.Delete(contactUs);
            return new SuccessResult(Messages.MessageDeleted);
        }

        public IDataResult<List<ContactUs>> GetAll()
        {
            return new SuccessDataResult<List<ContactUs>>(_contactUsDal.GetAll(),Messages.MessagesListed);
        }

        public IDataResult<ContactUs> GetById(int contactId)
        {
            return new ErrorDataResult<ContactUs>(_contactUsDal.Get(x=>x.Id == contactId));
        }

        public IResult Update(ContactUs contactUs)
        {
            _contactUsDal.Update(contactUs);
            return new SuccessResult(Messages.MessageUpdated);
        }
    }
}
