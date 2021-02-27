using System;
using System.Collections.Generic;
using System.Text;
using BisikletKlinigi.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Concrete
{
    public class EfContactUsDal : EfEntityRepositoryBase<ContactUs,BisikletKlinigiContext>, IContactUsDal
    {
    }
}
