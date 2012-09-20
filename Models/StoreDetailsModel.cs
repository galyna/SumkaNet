using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Data.Entities;

namespace SumkaWeb.Models
{
    public class StoreDetailsModel
    {
        public IList<Product> Products { get; set; }
        public Store Store { get; set; }
    }
}