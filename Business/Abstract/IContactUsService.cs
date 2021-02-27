using System;
using System.Collections.Generic;
using System.Text;
using Business.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IContactUsService
    {
        IDataResult<List<ContactUs>> GetAll();
        IDataResult<ContactUs> GetById(int contactId);
        IResult Add(ContactUs contactUs);
        IResult Update(ContactUs contactUs);
        IResult Delete(ContactUs contactUs);
    }
}
