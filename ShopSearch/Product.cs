using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSearch
{
    class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Package { get; set; }
        public int SupplierId { get; set; }
        public bool IsDiscounted { get; set; }
        public override string ToString()
        {
            return $"{ProductName}";
        }
    }
}
