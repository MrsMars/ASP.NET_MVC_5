using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerStore.Models
{
    // metadata to class Flower keep in class FlowerMetadata
    [MetadataType(typeof(FlowerMetadata))]              // - atribute
    public partial class Flower
    {
        // without 'FlowerId'
        [Bind(Exclude = "FlowerId")]
        public class FlowerMetadata
        {
            [ScaffoldColumn(false)]
            public int FlowerId { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Flower name is required.")]                              // obligatory
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [StringLength(50)]
            public string Name { get; set; }

            [DisplayName("Price")]
            [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessage = "Flower price is required.")]
            [Range(0.01, 1000, ErrorMessage = "Flower price must be between 0,01 and 1000 $.")]
            public decimal Price { get; set; }

            [DisplayName("Article")]
            [Required(ErrorMessage = "Flower article is required.")]                              
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [StringLength(50)]
            public string Article { get; set; }
        }
    }
}