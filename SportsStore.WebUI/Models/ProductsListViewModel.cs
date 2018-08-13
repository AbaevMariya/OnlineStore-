using System;
using System.Collections.Generic;
using System.Linq;
using SportsStore.Domain.Entities;
using System.Web;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PageInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}