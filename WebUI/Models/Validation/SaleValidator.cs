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
            RuleFor(x => x.Topic)
                .NotEmpty().WithMessage("Başlık bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Başlık bilgisi boş bırakılamaz!");

            RuleFor(x => x.Usage)
                .NotEmpty().WithMessage("Kullanım durumu boş bırakılamaz!")
                .NotNull().WithMessage("Kullanım durumu boş bırakılamaz!");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Fiyat bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Fiyat bilgisi boş bırakılamaz!")
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır!");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Açıklama bilgisi boş bırakılamaz!");

            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Tür bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Tür bilgisi boş bırakılamaz!");

            RuleFor(x => x.Shifters)
                .NotEmpty().WithMessage("Vites Kolları bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Vites Kolları bilgisi boş bırakılamaz!");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Açıklama bilgisi boş bırakılamaz!");

            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("Marka bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Marka bilgisi boş bırakılamaz!");

            RuleFor(x => x.FrameSize)
                .NotEmpty().WithMessage("Kadro Boyu bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Kadro boyu bilgisi boş bırakılamaz!");

            RuleFor(x => x.WheelSize)
                .NotEmpty().WithMessage("Teker Çapı bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Teker Çapı bilgisi boş bırakılamaz!");

            RuleFor(x => x.BrakeSystem)
                .NotEmpty().WithMessage("Fren Sistemi bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Fren Sistemi bilgisi boş bırakılamaz!");

            RuleFor(x => x.ForkType)
                .NotEmpty().WithMessage("Maşa Tipi bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Maşa Tipi bilgisi boş bırakılamaz!");

            RuleFor(x => x.Speed)
                .NotEmpty().WithMessage("Vites Sayısı bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Vites Sayısı bilgisi boş bırakılamaz!");

            RuleFor(x => x.Warranty)
                .NotEmpty().WithMessage("Garanti Durumu boş bırakılamaz!")
                .NotNull().WithMessage("Garanti Durumu boş bırakılamaz!");

            RuleFor(x => x.RearDerailleur)
                .NotEmpty().WithMessage("Arka Aktarıcı bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Arka Aktarıcı bilgisi boş bırakılamaz!");

            RuleFor(x => x.FrontDerailleurs)
                .NotEmpty().WithMessage("Ön Aktarıcı bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Ön Aktarıcı bilgisi boş bırakılamaz!");

            RuleFor(x => x.Crankset)
                .NotEmpty().WithMessage("Aynakol bilgisi boş bırakılamaz!")
                .NotNull().WithMessage("Aynakol bilgisi boş bırakılamaz!");
        }
    }
}
