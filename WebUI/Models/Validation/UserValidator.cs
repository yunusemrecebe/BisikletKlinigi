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
                .Length(3, 50).WithMessage("İsim bilgisi en az 2, en fazla 50 karakterden oluşmalıdır!")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ]*$").WithMessage("İsim bilgisi özel karakter içeremez!");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyisim bilgisi boş bırakılamaz")
                .NotNull().WithMessage("Soyisim bilgisi boş bırakılamaz")
                .Length(3, 50).WithMessage("Soyisim bilgisi en az 2, en fazla 50 karakterden oluşmalıdır!")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ]*$").WithMessage("Soyisim bilgisi özel karakter içeremez!");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email bilgisi boş bırakılamaz")
                .NotNull().WithMessage("Email bilgisi boş bırakılamaz")
                .MaximumLength(140).WithMessage("Email bilgisi en fazla 140 karakterden oluşmalıdır!")
                .EmailAddress().WithMessage("Lütfen Email adresi formatlarına uyarak giriş yapınız!");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola bilgisi boş bırakılamaz")
                .NotNull().WithMessage("Parola bilgisi boş bırakılamaz")
                .MinimumLength(8).WithMessage("Parola bilgisi en az 8 karakterden oluşmalıdır!");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon bilgisi boş bırakılamaz")
                .NotNull().WithMessage("Telefon bilgisi boş bırakılamaz");
        }
    }
}
