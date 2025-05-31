using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Layer
{
    public class ProductFormProperties
    {
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public int IsActive { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
