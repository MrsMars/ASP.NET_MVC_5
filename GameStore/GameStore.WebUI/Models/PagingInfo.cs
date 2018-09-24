using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class PagingInfo
    {
        // items quantity
        public int TotalItems { get; set; }

        // items quantity on one page
        public int ItemsPerPage { get; set; }

        // number of this page
        public int CurrentPage { get; set; }

        // quantity of pages
        public int TotalPages { get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); } }
    }
}