using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Entities.Abstract;
using Microsoft.AspNetCore.Http;

namespace Entities.Concrete
{
    public class Sale : IEntity
    {
        public int Id { get; set; }
        public int Owner { get; set; }
        public int Genre { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Brand { get; set; }
        public int Model { get; set; }
        public int FrameSize { get; set; }
        public int WheelSize { get; set; }
        public int BrakeSystem { get; set; }
        public int ForkType { get; set; }
        public int Speed { get; set; }
        public int Warranty { get; set; }
        public int RearDerailleur { get; set; }
        public int FrontDerailleurs { get; set; }
        public int Crankset { get; set; }
        public int Shifters { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImagePath { get; set; }
    }
}
