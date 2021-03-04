using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Entities.Abstract;
using Microsoft.AspNetCore.Http;

namespace Entities.Concrete
{
    public class Sale : IEntity
    {
        public int Id { get; set; }

        public int Owner { get; set; }

        [Display(Name = "Tür")]
        public string Genre { get; set; }

        [Display(Name = "Başlık")]
        public string Topic { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        [Display(Name = "Marka")]
        public string Brand { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Kadro Boyu")]
        public string FrameSize { get; set; }

        [Display(Name = "Teker Çapı")]
        public string WheelSize { get; set; }

        [Display(Name = "Fren Sistemi")]
        public string BrakeSystem { get; set; }

        [Display(Name = "Maşa Tipi")]
        public string ForkType { get; set; }

        [Display(Name = "Vites Sayısı")]
        public string Speed { get; set; }

        [Display(Name = "Garanti Durumu")]
        public string Warranty { get; set; }

        [Display(Name = "Arka Aktarıcı")]
        public string RearDerailleur { get; set; }

        [Display(Name = "Ön Aktarıcı")]
        public string FrontDerailleurs { get; set; }

        [Display(Name = "Aynakol")]
        public string Crankset { get; set; }

        [Display(Name = "Vites Kolları")]
        public string Shifters { get; set; }

        [Display(Name = "Kullanım Durumu")]
        public string Usage { get; set; }

        [Display(Name = "Görsel")]
        public string Image { get; set; }
    }
}
