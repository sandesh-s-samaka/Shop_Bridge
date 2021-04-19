using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Bridge.Models
{
    public class ProductModel
    {
        public int PID { get; set; }
        public string Product { get; set; }
        public string Product_Description { get; set; }
        public Nullable<int> FK_PCID { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Stock { get; set; }
        public string Created_By { get; set; }
        public string Created_Date { get; set; }
        public string Modified_By { get; set; }
        public string Modified_Date { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public Product_Category_Model tbl_Product_Category { get; set; }
    }
}