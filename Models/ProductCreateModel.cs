using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Data.Entities;

namespace SumkaWeb.Models
{
    public class ProductCreateModel
    {
        public IList<WebTemplate> ProductTemplates { get; set; }
        public Product Product { get; set; }
        public int StoreID { get; set; }
        public string BannerShortDescription { get; set; }
    }
}