using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Bridge.Models
{
    public class Product_Category_Model
    {
        public int PCID { get; set; }
        public string Product_Category { get; set; }
        public string Product_Category_Desc { get; set; }
        public byte[] Created_By { get; set; }
        public string Created_Date { get; set; }
        public string Modified_By { get; set; }
        public string Modified_Date { get; set; }
        public bool IsActive { get; set; }
    }
}