using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please, enter your name")]
        public string Nmae { get; set; }

        [Required(ErrorMessage ="Your first adress")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Your city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Your country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
