using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebUI.Models.Validation
{
    public class MessageValidator : AbstractValidator<Entities.Concrete.ContactUs>
    {
        public MessageValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("İsim bilgisi boş bırakılamaz")
                .NotNull().WithMessage("İsim bilgisi boş bırakılamaz")
                .Length(2, 50).WithMessage("İsim bilgisi en az 2, en fazla 50 karakterden oluşmalıdır!")
                .Matches(@"^[a-zA-Z_ğüşıöçĞÜŞİÖÇ]*$").WithMessage("İsim bilgisi özel karakter içeremez!");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyisim bilgisi boş bırakılamaz")
                .NotNull().WithMessage("Soyisim bilgisi boş bırakılamaz")
                .Length(2, 50).WithMessage("Soyisim bilgisi en az 2, en fazla 50 karakterden oluşmalıdır!")
                .Matches(@"^[a-zA-Z_ğüşıöçĞÜŞİÖÇ]*$").WithMessage("Soyisim bilgisi özel karakter içeremez!");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Mail bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Mail bilgisi boş bırakılamaz!")
                .MaximumLength(140).WithMessage("Email bilgisi en fazla 140 karakterden oluşmalıdır!")
                .EmailAddress().WithMessage("Lütfen Email adresi formatlarına uyarak giriş yapınız!");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Mesaj bilgisi boş bırakılamaz")
                .NotNull().WithMessage("Mesaj bilgisi boş bırakılamaz");
        }
    }
}
