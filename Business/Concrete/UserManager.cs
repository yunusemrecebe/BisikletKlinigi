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
            try
            {
                _userDal.Add(user);
                return new SuccessResult(Messages.UserAdded);
            }
            catch (Exception e)
            {
                return new ErrorResult(Messages.UserCanNotAdded);
                throw;
            }
        }

        IResult IUserService.Delete(User user)
        {
            try
            {
                _userDal.Delete(user);
                return new SuccessResult(Messages.UserDeteled);
            }
            catch (Exception e)
            {
                return new ErrorResult(Messages.UserCanNotDeteled);
                throw;
            }
        }

        IResult IUserService.Update(User user)
        {
            try
            {
                _userDal.Update(user);
                return new SuccessResult(Messages.UserUpdated);
            }
            catch (Exception e)
            {
                return new ErrorResult(Messages.UserCanNotUpdated);
                throw;
            }
        }

        IDataResult<List<User>> IUserService.GetAll()
        {
            var result = _userDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<User>>(result, Messages.UsersListed);
            }
            return new ErrorDataResult<List<User>>(Messages.UsersCanNotListed);
        }

        IDataResult<User> IUserService.GetById(int userId)
        {
            var result = _userDal.Get(x => x.Id == userId);
            if (result != null)
            {
                return new SuccessDataResult<User>(result,Messages.UsersListed);
            }
            return new ErrorDataResult<User>(Messages.UsersCanNotListed);
        }

        public IDataResult<User> Login(string mail, string password)
        {
            var result = _userDal.Get(x => x.Email == mail && x.Password == password);
            if (result != null)
            {
                return new SuccessDataResult<User>(result,Messages.LoginSucces);
            }
            return new ErrorDataResult<User>(Messages.LoginIsNotSucces);
        }
    }
}
