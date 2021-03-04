using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class ContactUs : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "İsim")]
        public string FirstName { get; set; }

        [Display(Name = "Soyisim")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Mesaj")]
        public string Message { get; set; }
    }
}
