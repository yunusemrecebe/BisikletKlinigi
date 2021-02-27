using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        IResult IUserService.Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        IResult IUserService.Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeteled);
        }

        IResult IUserService.Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        IDataResult<List<User>> IUserService.GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(),Messages.UsersListed);
        }

        IDataResult<User> IUserService.GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(x=>x.Id == userId));
        }

        public IDataResult<User> Login(string mail, string password)
        {
            return new SuccessDataResult<User>(_userDal.Get(x => x.Email == mail && x.Password == password));
        }
    }
}
