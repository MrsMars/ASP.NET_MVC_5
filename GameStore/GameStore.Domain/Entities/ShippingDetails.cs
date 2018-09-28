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
        public string Name { get; set; }

        [Required(ErrorMessage ="Your first adress")]
        [Display(Name = "First adress")]
        public string Line1 { get; set; }

        [Display(Name = "Second adress")]
        public string Line2 { get; set; }

        [Display(Name = "Third adress")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Your city")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Your country")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
