using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Bridge.Models
{
    public class TokenModel
    {
        public int ID { get; set; }
        public string TokenValue { get; set; }
        public string UserID { get; set; }
        public string Role { get; set; }
        public string Created_Date { get; set; }
        public string Modified_Date { get; set; }
        public bool IsActive { get; set; }
    }
}