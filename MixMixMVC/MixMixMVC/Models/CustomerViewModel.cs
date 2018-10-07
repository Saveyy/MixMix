using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MixMixMVC.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [DisplayName("Fulde navn")]
        public string Name { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Brugernavn")]
        public string Username { get; set; }
        [DisplayName("Tlf. nr.")]
        public string Phonenumber { get; set; }
        [DisplayName("Kodeord")]
        public string Password { get; set; }
    }
}