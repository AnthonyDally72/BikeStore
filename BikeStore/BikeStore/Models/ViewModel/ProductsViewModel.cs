using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models.ViewModel
{
    public class ProductsViewModel
    {
        public Products Products { get; set; }

        public IEnumerable<ProductTypes> ProductTypes { get; set; }

        public IEnumerable<Stores> Stores { get; set; }
    }
}
