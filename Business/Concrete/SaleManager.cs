using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class SaleManager : ISaleService
    {
        private ISaleDal _saleDal;

        public SaleManager(ISaleDal saleDal)
        {
            _saleDal = saleDal;
        }

        public IDataResult<List<Sale>> GetAll()
        {
            var result = _saleDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<Sale>>(result, Messages.SalesListed);
            }
            
            return new ErrorDataResult<List<Sale>>(Messages.SaleCanNotFound);
        }

        public IDataResult<Sale> GetById(int saleId)
        {
            var result = _saleDal.Get(x => x.Id == saleId);
            if (result != null)
            {
                return new SuccessDataResult<Sale>(result);
            }

            return new ErrorDataResult<Sale>(Messages.SaleCanNotFound);
        }

        public IResult Add(Sale sale)
        {
            try
            {
                _saleDal.Add(sale);
                return new SuccessResult(Messages.SaleAdded);
            }
            catch (Exception e)
            {
                return new ErrorResult(Messages.SaleCanNotAdded);
                throw;
            }
        }

        public IResult Update(Sale sale)
        {
            try
            {
                _saleDal.Update(sale);
                return new SuccessResult(Messages.SaleUpdated);
            }
            catch (Exception e)
            {
                return new ErrorResult(Messages.SaleCanNotUpdated);
                throw;
            }
            
        }

        public IResult Delete(Sale sale)
        {
            try
            {
                _saleDal.Delete(sale);
                return new SuccessResult(Messages.SaleDeleted);
            }
            catch (Exception e)
            {
                return new ErrorResult(Messages.SaleCanNotDeleted);
                throw;
            }
           
        }
    }
}
