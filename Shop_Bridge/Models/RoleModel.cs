using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Bridge.Models
{
    public class RoleModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Created_By { get; set; }
        public string Created_Date { get; set; }
        public string Modified_By { get; set; }
        public string Modified_Date { get; set; }
        public bool IsActive { get; set; }
    }
}