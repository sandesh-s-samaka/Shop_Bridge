using Master_Operations_Proj.Models;
using Newtonsoft.Json;
using Shop_Bridge.Models;
using Shop_Bridge.Services;
using Shop_Bridge.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop_Bridge.Controller
{
    public class UserController : ApiController
    {
        //--------log4net File
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //--------log4net File

        readonly Util utility;
        readonly UserService usrSrv;
        readonly JsonFormatResponse format;

        public UserController()
        {
            if (utility == null)
            {
                utility = new Util();
            }
            if (usrSrv == null)
            {
                usrSrv = new UserService();
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
        [HttpPost]
        [Route("api/User/Login")]
        public HttpResponseMessage Login(dynamic jObject)
        {
            string RetMesssage = string.Empty;
            string userName = Convert.ToString(jObject.UserName);
            string password = Convert.ToString(jObject.Password);

            try
            {
                string RetMessage = usrSrv.Login(userName, password);
                return Request.CreateResponse(HttpStatusCode.OK, RetMessage);
            }
            catch (SqlException sqlex)
            {
                log.Error(sqlex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, sqlex.Message);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}

///*'**********************************************************************************************
// * 'Function Name  - AddEditUser()
//* 'Parameters     - User Name, Password, First Name, Last Name, Email ID, Role ID, Edit User ID
//* 'Description    - Authenticate the user with username and password 
//* '********************************************************************************************/
//[HttpPost]
//[Route("api/User/AddEditUser")]
//public HttpResponseMessage AddEditUser(dynamic jObject)
//{
//    string RetMesssage = string.Empty;
//    string userName = Convert.ToString(jObject.UserName);
//    string password = Convert.ToString(jObject.Password);
//    string userid = Convert.ToString(jObject.UserID);
//    string email = Convert.ToString(jObject.Email);
//    string flag = string.Empty;
//    int roleID = Convert.ToInt32(jObject.roleid);
//    int editUserID = Convert.ToInt32(jObject.EditUserID);


//    bool emailIDCheck = utility.emailAdressCheck(email);
//    if (emailIDCheck)
//    {
//        return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Email Address! Please enter a valid Email Address");
//    }

//    try
//    {
//        if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(userid) || !string.IsNullOrEmpty(email) || ((roleID == 0) || string.IsNullOrEmpty(Convert.ToString(roleID))))
//        {

//            string RetMessage = usrSrv.AddEditUser(userName, password, userid, email, roleID, editUserID);
//            return Request.CreateResponse(HttpStatusCode.OK, RetMessage);
//        }
//        else
//        {
//            format.Status = "Failure";
//            format.Message = "User Name, Password, First Name, Address, Email Cannot be Null";
//            format.output_data = "Please Enter the valid entries";
//            return Request.CreateResponse(HttpStatusCode.OK, format);
//        }
//    }

//    catch (SqlException sqlex)
//    {
//        log.Error(sqlex);
//        return Request.CreateResponse(HttpStatusCode.InternalServerError, sqlex.Message);
//    }
//    catch (Exception ex)
//    {
//        log.Error(ex);
//        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
//    }
//}