using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.Models.Entities;

namespace WebStore.UI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Products> Products { get; set; }
        public PagingInfo PaginInfo { get; set; }
        public int? CurrentCategory { get; set; }
    }
}