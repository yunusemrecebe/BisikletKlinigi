using System;
using System.Collections.Generic;
using System.Text;
using Business.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int userId);
        IDataResult<User> Login(string mail, string password);
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
    }
}
