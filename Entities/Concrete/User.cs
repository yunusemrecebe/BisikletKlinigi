using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "İsim")]
        public string Name { get; set; }

        [Display(Name = "Soyisim")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Parola")]
        public string Password { get; set; }

        [Display(Name = "Telefon")]
        public long Phone { get; set; }

        public int Role { get; set; }
    }
}
