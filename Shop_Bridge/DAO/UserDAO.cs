using Shop_Bridge.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Shop_Bridge.DAO
{
    public class UserDAO
    {
        /*'**********************************************************************************************
         * 'Function Name  - Login()
         * 'Parameters     - userName, Password
         * 'Description    - Authenticate the user with username and password 
         * '********************************************************************************************/
        public List<UserModel> Login(string userName, string Password)
        {
            List<UserModel> usrModel;
            try
            {
                ShopBridgeEntities loginEntities = ShopBridgeEntities.getEfInstance();
                using (loginEntities)
                {
                    var data = (from rows in loginEntities.tbl_User
                                where rows.UserID.Equals(userName) && rows.Password.Equals(Password)
                                select rows).ToList();

                    usrModel = data.AsEnumerable().Select(DataRow => new UserModel
                    {
                        UserID = DataRow.UserID,
                        Password = DataRow.Password,
                        Email = DataRow.Email,
                        FK_User_RoleID = DataRow.tbl_Role.RoleID,
                        IsActive = DataRow.IsActive,
                        UserRoleModel = DataRow.tbl_Role.RoleName
                    }).ToList();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return usrModel;
        }

        /*'**********************************************************************************************
         * 'Function Name  - GenerateToken()
         * 'Parameters     - userName, Role
         * 'Description    - Generate Token for Session Handling
         * '********************************************************************************************/
        public string GenerateToken(string userName, string Role)
        {
            ShopBridgeEntities GenTokenEntities = ShopBridgeEntities.getEfInstance();
            string tokenValue = string.Empty;

            try
            {
                using (GenTokenEntities)
                {
                    tokenValue = Guid.NewGuid().ToString();
                    var user = GenTokenEntities.Set<tbl_Token>();
                    user.Add(new tbl_Token
                    {
                        UserID = userName,
                        Role = Role,
                        TokenValue = tokenValue,
                        Created_Date = DateTime.Now,
                        Modified_Date = DateTime.Now,
                        IsActive = true
                    });

                    GenTokenEntities.SaveChanges();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tokenValue;
        } 
    }
}

///*'*********************************************************************************
// * 'Function Name  - AddEditUser()
// * 'Parameters     - User Name, First Name, Last Name, Email ID, Role ID, Edit User ID
// * 'Description    - Add new user or Edit the existing user [Admin Module]
// * '*********************************************************************************/
//public string AddEditUser(string username, string password, string email, int roleid, string userid, int edituserid)
//{
//    string addedituser = string.Empty;
//    ShopBridgeEntities AddEditcontext = ShopBridgeEntities.getEfInstance();
//    tbl_User usr = new tbl_User();
//    int RoleID = 0;
//    int EditUserID = 0;

//    EditUserID = Convert.ToInt16(edituserid);
//    try
//    {
//        using (AddEditcontext)
//        {
//            if (string.IsNullOrEmpty(Convert.ToString(roleid)))
//            {
//                usr.FK_User_RoleID = Convert.ToInt32(RoleID);
//            }
//            else
//            {
//                usr.FK_User_RoleID = roleid;
//            }

//            var edituser = AddEditcontext.tbl_User.FirstOrDefault(add => add.ID == EditUserID);
//            if (edituser != null)
//            {
//                edituser.FK_User_RoleID = Convert.ToInt32(roleid);
//                edituser.Modified_Date = Convert.ToDateTime(DateTime.Now);
//                edituser.Modified_By = Convert.ToString(userid);
//                AddEditcontext.SaveChanges();
//                addedituser = "User Edited Successfully";
//            }
//            else
//            {
//                var usremailid = AddEditcontext.tbl_User.FirstOrDefault(x => x.Email == email || x.UserID == userid || x.Password == password);
//                if (usremailid == null)
//                {
//                    usr.UserID = Convert.ToString(userid);
//                    usr.Password = Convert.ToString(password);
//                    usr.Email = Convert.ToString(email);
//                    usr.IsActive = Convert.ToBoolean(true);
//                    usr.Created_Date = Convert.ToDateTime(DateTime.Now);
//                    usr.Created_By = Convert.ToString(username);
//                    AddEditcontext.tbl_User.Add(usr);
//                    AddEditcontext.SaveChanges();
//                    addedituser = "New User Added Successfully";
//                }
//                else
//                {
//                    addedituser = "User Already Exists";
//                }
//            }
//        }
//        return addedituser;
//    }
//    catch (SqlException sqlex)
//    {
//        throw sqlex;
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//}