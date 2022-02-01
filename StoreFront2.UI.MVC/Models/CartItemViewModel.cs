using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFront2.DATA.EF;
using System.ComponentModel.DataAnnotations;

namespace StoreFront2.Models
{
    public class CartItemViewModel
    {
        [Range(1, int.MaxValue)]
        public int Qty { get; set; }
        public Product Product { get; set; }

        public CartItemViewModel(int qty, Product product)
        {
            Qty = qty;
            Product = product;
        }
    }
}