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
            try
            {
                _contactUsDal.Add(contactUs);
                return new SuccessResult(Messages.MessagesAdded);
            }
            catch (Exception e)
            {
                return new ErrorResult(Messages.MessagesCanNotAdded);
                throw;
            }
        }

        public IResult Delete(ContactUs contactUs)
        {
            try
            {
                _contactUsDal.Delete(contactUs);
                return new SuccessResult(Messages.MessageDeleted);
            }
            catch (Exception e)
            {
                return new ErrorResult(Messages.MessageCanNotDeleted);
                throw;
            }
        }

        public IResult Update(ContactUs contactUs)
        {
            try
            {
                _contactUsDal.Update(contactUs);
                return new SuccessResult(Messages.MessageUpdated);
            }
            catch (Exception e)
            {
                return new ErrorResult(Messages.MessageCanNotUpdated);
                throw;
            }
        }

        public IDataResult<List<ContactUs>> GetAll()
        {
            var result = _contactUsDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<ContactUs>>(result, Messages.MessagesListed);
            }
            return new ErrorDataResult<List<ContactUs>>(Messages.MessagesCanNotListed);
        }

        public IDataResult<ContactUs> GetById(int contactId)
        {
            var result = _contactUsDal.Get(x => x.Id == contactId);
            if (result != null)
            {
                return new ErrorDataResult<ContactUs>(result,Messages.MessagesListed);
            }
            return new ErrorDataResult<ContactUs>(Messages.MessagesCanNotListed);
        }
    }
}
