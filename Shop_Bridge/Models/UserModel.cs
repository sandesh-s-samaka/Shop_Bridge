using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Bridge.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int FK_User_RoleID { get; set; }
        public string Created_By { get; set; }
        public string Created_Date { get; set; }
        public string Modified_By { get; set; }
        public string Modified_Date { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public RoleModel tbl_Role { get; set; }

        public string UserRoleModel { get; set; }
    }
}