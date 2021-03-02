using System;
using System.Collections.Generic;
using System.Text;
using Entities.Abstract;
using Microsoft.AspNetCore.Http;

namespace Entities.Concrete
{
    public class Sale : IEntity
    {
        public int Id { get; set; }
        public int Owner { get; set; }
        public string Genre { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string FrameSize { get; set; }
        public string WheelSize { get; set; }
        public string BrakeSystem { get; set; }
        public string ForkType { get; set; }
        public string Speed { get; set; }
        public string Warranty { get; set; }
        public string RearDerailleur { get; set; }
        public string FrontDerailleurs { get; set; }
        public string Crankset { get; set; }
        public string Shifters { get; set; }
        public string Usage { get; set; }
        public string Image { get; set; }
    }
}
