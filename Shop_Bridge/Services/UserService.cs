using Master_Operations_Proj.Models;
using Newtonsoft.Json;
using Shop_Bridge.DAO;
using Shop_Bridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Bridge.Services
{
    public class UserService
    {
        readonly UserDAO usrDAO;
        readonly JsonFormatResponse format;

        public UserService()
        {
            if (usrDAO == null)
            {
                usrDAO = new UserDAO();
            }
            if (format == null)
            {
                format = new JsonFormatResponse();
            }
        }

        /*'**********************************************************************************************
        * 'Function Name  - Login()
        * 'Parameters     - userName, Password
        * 'Description    - Authenticate the user with username and password 
        * '********************************************************************************************/
        public string Login(string userName, string Password)
        {
            string flag = string.Empty;
            List<UserModel> dataset = usrDAO.Login(userName, Password);

            if (dataset.Count != 0)
            {
                string userName_db = dataset[0].UserID.ToString();
                string password_db = dataset[0].Password.ToString();
                string role_db = dataset[0].UserRoleModel.ToString();
                string isactive_db = dataset[0].IsActive.ToString();

                if (isactive_db.ToLower() == "true")
                {
                    if (userName.Trim() == userName_db.Trim() && Password.Trim() == password_db.Trim())
                    {
                        format.Status = "Success";
                        format.Message = "Login Successful..";

                        string token = string.Empty;
                        token = usrDAO.GenerateToken(userName, role_db);

                        TokenModel objToken = new TokenModel();
                        objToken.UserID = userName;
                        objToken.Role = role_db;
                        objToken.Created_Date = string.Format("{0} : dd/mm/yyyy", DateTime.Now);
                        objToken.Modified_Date = string.Format("{0} : dd/mm/yyyy", DateTime.Now);
                        objToken.IsActive = true;
                        format.output_data = objToken;

                        flag = JsonConvert.SerializeObject(format);
                    }
                    else
                    {
                        format.Status = "Failure";
                        format.Message = "Incorrect User Name or Password. Please Re-enter";
                        flag = JsonConvert.SerializeObject(format);
                    }

                }
                else
                {
                    format.Status = "Failure";
                    format.Message = "User is not Active. Please contact your Administrator";
                    flag = JsonConvert.SerializeObject(format);
                }
            }
            else
            {
                format.Status = "Failure";
                format.Message = "Incorrect User Name or Password. Please Re-enter";
                flag = JsonConvert.SerializeObject(format);
            }

            return flag;
        }
    }
}

///*'*********************************************************************************
//* 'Function Name  - AddEditUser()
//* 'Parameters     - User Name, Password, First Name, Last Name, Email ID, Role ID, Edit User ID
//* 'Description    - Add new user or Edit the existing user [Admin Module]
//* '*********************************************************************************/
//public string AddEditUser(string username, string password, string userid, string email, int roleid, int edituserid)
//{
//    string adduser = string.Empty;
//    return adduser = usrDAO.AddEditUser(username, password, email, roleid, userid, edituserid);
//}