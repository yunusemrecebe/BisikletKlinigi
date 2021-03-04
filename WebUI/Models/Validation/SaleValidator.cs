using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebUI.Models.Validation
{
    public class SaleValidator : AbstractValidator<Entities.Concrete.Sale>
    {
        public SaleValidator()
        {
            
        }
    }
}
