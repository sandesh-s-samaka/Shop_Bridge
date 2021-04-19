using log4net;
using Master_Operations_Proj.Models;
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

namespace Shop_Bridge.Controllers
{
    public class ProductController : ApiController
    {
        //--------log4net File
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //--------log4net File

        readonly Util utility;
        readonly ProductService prodSrv;
        readonly JsonFormatResponse format;

        public ProductController()
        {
            if (utility == null)
            {
                utility = new Util();
            }
            if (prodSrv == null)
            {
                prodSrv = new ProductService();
            }
            if (format == null)
            {
                format = new JsonFormatResponse();
            }
        }



        /*'**********************************************************************************************
        * 'Function Name  - AddProductCategory()
        * 'Parameters     - userName, Password, Product Category, Product Category Description
        * 'Description    - Add Product Category to Inventory 
        * '********************************************************************************************/
        [HttpPost]
        [Route("api/Product/AddProductCategory")]
        public HttpResponseMessage AddProductCategory(dynamic jObject)
        {
            string RetMessage = string.Empty;
            string userName = Convert.ToString(jObject.UserName);
            string password = Convert.ToString(jObject.Password);
            string prodCat = Convert.ToString(jObject.Product_Category);
            string prodCatDesc = Convert.ToString(jObject.Product_Category_Desc);

            bool spcChar = utility.SpecialCharCheck(jObject);
            if (spcChar)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Input. Input should not contain special characters");
            }

            try
            {
                if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(prodCat) || !string.IsNullOrEmpty(prodCatDesc))
                {

                    RetMessage = prodSrv.AddProductCategory(userName, password, prodCat, prodCatDesc);
                    format.Status = "Success";
                    format.Message = RetMessage;
                    return Request.CreateResponse(HttpStatusCode.OK, format);
                }
                else
                {
                    format.Status = "Failure";
                    format.Message = "Unable to Add the Product Category";
                    format.output_data = "Please Enter the valid entries";
                    return Request.CreateResponse(HttpStatusCode.OK, format);
                }
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

        /*'**********************************************************************************************
        * 'Function Name  - AddProduct()
        * 'Parameters     - userName, Password, Prod Category, Product, Product Description, Price, Quantity, Stock
        * 'Description    - Add Product to Inventory  
        * '********************************************************************************************/
        [HttpPost]
        [Route("api/Product/AddProduct")]
        public HttpResponseMessage AddProduct(dynamic jObject)
        {
            string RetMessage = string.Empty;
            string userName = Convert.ToString(jObject.UserName);
            string password = Convert.ToString(jObject.Password);
            int prodCatID = Convert.ToInt32(jObject.Product_CategoryID);
            string prod = Convert.ToString(jObject.Product);
            string prodDesc = Convert.ToString(jObject.Product_Desc);
            double price = Convert.ToDouble(jObject.Price);
            int qty = Convert.ToInt32(jObject.Quantity);
            string stock = Convert.ToString(jObject.Stock);

            bool spcChar = utility.SpecialCharCheck(jObject);
            if (spcChar)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Input. Input should not contain special characters");
            }



            try
            {
                if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(prod) || !string.IsNullOrEmpty(prodDesc) || !(qty <= 0) || !(price <= 0))
                {

                    RetMessage = prodSrv.AddProduct(userName, password, prodCatID, prod, prodDesc, price, qty, stock);
                    format.Status = "Success";
                    format.Message = RetMessage;
                    return Request.CreateResponse(HttpStatusCode.OK, format);
                }
                else
                {
                    format.Status = "Failure";
                    format.Message = "Unable to Add the Product Category";
                    format.output_data = "Please Enter the valid entries";
                    return Request.CreateResponse(HttpStatusCode.OK, format);
                }
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

        /*'**********************************************************************************************
        * 'Function Name  - EditProductCategory()
        * 'Parameters     - userName, Password, Prod Category ID, Product Category, Product Category Description
        * 'Description    - Edit Product Category
        * '********************************************************************************************/
        [HttpPost]
        [Route("api/Product/EditProductCategory")]
        public HttpResponseMessage EditProductCategory(dynamic jObject)
        {
            string RetMessage = string.Empty;
            string userName = Convert.ToString(jObject.UserName);
            string password = Convert.ToString(jObject.Password);
            int prodCatID = Convert.ToInt32(jObject.Product_CategoryID);
            string prodCat = Convert.ToString(jObject.Product_Category);
            string prodCatDesc = Convert.ToString(jObject.Product_Category_Desc);

            bool spcChar = utility.SpecialCharCheck(jObject);
            if (spcChar)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Input. Input should not contain special characters");
            }

            try
            {
                if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(prodCat) || !string.IsNullOrEmpty(prodCatDesc))
                {

                    RetMessage = prodSrv.EdiProductCategory(userName, password, prodCatID, prodCat, prodCatDesc);
                    format.Status = "Success";
                    format.Message = RetMessage;
                    return Request.CreateResponse(HttpStatusCode.OK, format);
                }
                else
                {
                    format.Status = "Failure";
                    format.Message = "Unable to Edit the Product Category";
                    format.output_data = "Please Enter the valid entries";
                    return Request.CreateResponse(HttpStatusCode.OK, format);
                }
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

        /*'**********************************************************************************************
        * 'Function Name  - EditProduct()
        * 'Parameters     - userName, Password, Prod ID, Product, Product Description, Price, Quantuty, Stock
        * 'Description    - Edit Product
        * '********************************************************************************************/
        [HttpPost]
        [Route("api/Product/EditProduct")]
        public HttpResponseMessage EditProduct(dynamic jObject)
        {
            string RetMessage = string.Empty;
            string userName = Convert.ToString(jObject.UserName);
            string password = Convert.ToString(jObject.Password);
            int prodID = Convert.ToInt32(jObject.ProductID);
            string prod = Convert.ToString(jObject.Product);
            string prodDesc = Convert.ToString(jObject.Product_Desc);
            double price = Convert.ToDouble(jObject.Price);
            int qty = Convert.ToInt32(jObject.Quantity);
            string stock = Convert.ToString(jObject.Stock);

            bool spcChar = utility.SpecialCharCheck(jObject);
            if (spcChar)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Input. Input should not contain special characters");
            }

            try
            {
                if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(prod) || !string.IsNullOrEmpty(prodDesc) || !(qty <= 0) || !(price <= 0))
                {
                    RetMessage = prodSrv.EdiProduct(userName, password, prodID, prod, prodDesc, price, qty, stock);
                    format.Status = "Success";
                    format.Message = RetMessage;
                    return Request.CreateResponse(HttpStatusCode.OK, format);
                }
                else
                {
                    format.Status = "Failure";
                    format.Message = "Unable to Edit the Product";
                    format.output_data = "Please Enter the valid entries";
                    return Request.CreateResponse(HttpStatusCode.OK, format);
                }
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

        /*'**********************************************************************************************
        * 'Function Name  - DeleteProductCategory()
        * 'Parameters     - userName, Password, Prod Category ID
        * 'Description    - Delete Product Category from Inventory
        * '********************************************************************************************/
        [HttpPost]
        [Route("api/Product/DeleteProductCategory")]
        public HttpResponseMessage DeleteProductCategory(dynamic jObject)
        {
            bool RetMessage = false;
            string userName = Convert.ToString(jObject.UserName);
            string password = Convert.ToString(jObject.Password);
            int prodCatID = Convert.ToInt32(jObject.Product_CategoryID);
            string prodCatIsActive = Convert.ToString(jObject.IsActive);

            bool spcChar = utility.SpecialCharCheck(jObject);
            if (spcChar)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Input. Input should not contain special characters");
            }

            try
            {
                if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(prodCatIsActive))
                {

                    RetMessage = prodSrv.DeleteProductCategory(userName, password, prodCatID, prodCatIsActive);
                    if (RetMessage == true)
                    {
                        format.Status = "Success";
                        format.Message = "Product Category Deleted from Inventory!!";
                        format.output_data = RetMessage;
                        return Request.CreateResponse(HttpStatusCode.OK, format);
                    }
                    else
                    {
                        format.Status = "Failure";
                        format.Message = "Unable to Delete the Product Category";
                        format.output_data = RetMessage + "Please Enter the valid entries";
                        return Request.CreateResponse(HttpStatusCode.OK, format);
                    }
                }
                else
                {
                    format.Status = "Failure";
                    format.Message = "Unable to Delete the Product Category";
                    format.output_data = "Please Enter the valid entries";
                    return Request.CreateResponse(HttpStatusCode.OK, format);
                }
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

        /*'**********************************************************************************************
        * 'Function Name  - DeleteProduct()
        * 'Parameters     - userName, Password, Prod ID
        * 'Description    - Delete Product from Inventory
        * '********************************************************************************************/
        [HttpPost]
        [Route("api/Product/DeleteProduct")]
        public HttpResponseMessage DeleteProduct(dynamic jObject)
        {
            bool RetMessage = false;
            string userName = Convert.ToString(jObject.UserName);
            string password = Convert.ToString(jObject.Password);
            int prodID = Convert.ToInt32(jObject.ProductID);
            string prodIsActive = Convert.ToString(jObject.IsActive);

            bool spcChar = utility.SpecialCharCheck(jObject);
            if (spcChar)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Input. Input should not contain special characters");
            }

            try
            {
                if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(password) || prodID == 0 || !string.IsNullOrEmpty(prodIsActive))
                {

                    RetMessage = prodSrv.DeleteProduct(userName, password, prodID, prodIsActive);
                    if (RetMessage == true)
                    {
                        format.Status = "Success";
                        format.Message = "Product Deleted from Inventory!!";
                        format.output_data = RetMessage;
                        return Request.CreateResponse(HttpStatusCode.OK, format);
                    }
                    else
                    {
                        format.Status = "Failure";
                        format.Message = "Unable to Delete the Product";
                        format.output_data = RetMessage + "Please Enter the valid entries";
                        return Request.CreateResponse(HttpStatusCode.OK, format);
                    }
                }
                else
                {
                    format.Status = "Failure";
                    format.Message = "Unable to Delete the Product";
                    format.output_data = "User Name, Password and Product ID Cannot be Empty! Please Enter the valid entries";
                    return Request.CreateResponse(HttpStatusCode.OK, format);
                }
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

        /*'*****************************************************
         * 'Function Name  - ProductList()
         * 'Parameters     - userName, Password
         * 'Description    - List of Product Items to display
         * '*****************************************************/
        public HttpResponseMessage ProductList(dynamic jObject)
        {
            string userName = jObject.UserName;
            string password = jObject.Password;
            int prodID = 0;
            prodID = Convert.ToInt32(jObject.ProductID);

            bool spcChar = utility.SpecialCharCheck(jObject);
            if (spcChar)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Input. Input should not contain special characters");
            }

            try
            {
                if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(password))
                {
                    List<ProductModel> RetMessage = prodSrv.ProductList(userName, password, prodID);
                    if (RetMessage.Count != 0)
                    {
                        format.Status = "Success";
                        format.Message = "List of Product Items";
                        format.output_data = RetMessage;
                        return Request.CreateResponse(HttpStatusCode.OK, format);
                    }
                    else
                    {
                        format.Status = "Failure";
                        format.Message = "Unable to Display List of Product Items";
                        format.output_data = "List is empty!";
                        return Request.CreateResponse(HttpStatusCode.OK, format);
                    }
                }
                else
                {
                    format.Status = "Failure";
                    format.Message = "Unable Display List of Product Items";
                    format.output_data = "User Name and Password Cannot be Empty! Please Enter the valid entries";
                    return Request.CreateResponse(HttpStatusCode.OK, format);
                }
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
