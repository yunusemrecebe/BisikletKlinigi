using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebUI.Models.Validation
{
    public class UserValidator : AbstractValidator<Entities.Concrete.User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim bilgisi boş bırakılamaz")
                .NotNull().WithMessage("İsim bilgisi boş bırakılamaz")
                .Length(3, 50).WithMessage("İsim bilgisi en az 3, en fazla 50 karakterden oluşmalıdır!");


        }
    }
}
