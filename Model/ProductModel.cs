using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ProductModel
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string category_id { get; set; }
        public string brand_id { get; set; }
        public string product_desc { get; set; }
        public string product_Ram { get; set; }
        public string product_CPU { get; set; }
        public string product_VGA { get; set; }
        public int product_price { get; set; }
        public string product_image { get; set; }
        public long? total { get; set; }

        public BrandModel Brand { get; set; }
        public CategoryModel Category { get; set; }
        public int product_quantity { get; set; }
    }
}
