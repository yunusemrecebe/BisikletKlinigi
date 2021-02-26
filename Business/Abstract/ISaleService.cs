using System;
using System.Collections.Generic;
using System.Text;
using Business.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ISaleService
    {
        IDataResult<List<Sale>> GetAll();
        IDataResult<Sale> GetById(int saleId);
        IResult Add(Sale sale);
        IResult Update(Sale sale);
        IResult Delete(Sale sale);
    }
}
