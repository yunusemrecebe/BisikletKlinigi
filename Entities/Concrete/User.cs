﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public BigInteger Phone { get; set; }
        public int Role { get; set; }
    }
}
