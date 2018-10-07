using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MixMixMVC.Models
{
    public class DrinkNameViewModel
    {
        [DisplayName("Drink navn")]
        public string DrinkName { get; set; }
    }
}