using Shop_Bridge.DAO;
using Shop_Bridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Bridge.Services
{
    public class ProductService
    {
        readonly ProductDAO prodDAO;

        public ProductService()
        {

            if (prodDAO == null)
            {
                prodDAO = new ProductDAO();
            }
        }

        /*'**********************************************************************************************
        * 'Function Name  - AddProductCategory()
        * 'Parameters     - userName, Password, Product Category, Product Category Description
        * 'Description    - Add Product Category to Inventory 
        * '********************************************************************************************/
        public string AddProductCategory(string userName, string password, string prodCat, string prodCatDesc)
        {
            string RetMessage = prodDAO.AddProductCategory(userName, password, prodCat, prodCatDesc);
            return RetMessage;
        }

        /*'**********************************************************************************************
         * 'Function Name  - AddProduct()
         * 'Parameters     - userName, Password, Prod Category ID, Product, Product Description, Price, Quantity, Stock
         * 'Description    - Add Product to Inventory  
         * '********************************************************************************************/
        public string AddProduct(string userName, string password, int prodCatID, string prod, string prodDesc, double price, int qty, string stock)
        {
            string RetMessage = prodDAO.AddProduct(userName, password, prodCatID, prod, prodDesc, price, qty, stock);
            return RetMessage;
        }

        /*'**********************************************************************************************
        * 'Function Name  - EditProductCategory()
        * 'Parameters     - userName, Password, Prod Category ID, Product Category, Product Category Description
        * 'Description    - Edit Product Category
        * '********************************************************************************************/

        public string EdiProductCategory(string userName, string password, int prodCatID, string prodCat, string prodCatDesc)
        {
            string RetMessage = prodDAO.EdiProductCategory(userName, password, prodCatID, prodCat, prodCatDesc);
            return RetMessage;
        }

        /*'**********************************************************************************************
        * 'Function Name  - EditProduct()
        * 'Parameters     - userName, Password, Prod ID, Product, Product Description, Price, Quantity, Stock
        * 'Description    - Edit Product
        * '********************************************************************************************/
        public string EdiProduct(string userName, string password, int prodID, string prod, string prodDesc, double price, int qty, string stock)
        {
            string RetMessage = prodDAO.EdiProduct(userName, password, prodID, prod, prodDesc, price, qty, stock);
            return RetMessage;
        }

        /*'**********************************************************************************************
        * 'Function Name  - DeleteProductCategory()
        * 'Parameters     - userName, Password, Prod Category ID
        * 'Description    - Delete Product Category from Inventory
        * '********************************************************************************************/
        public bool DeleteProductCategory(string userName, string password, int prodCatID, string prodCatIsActive)
        {
            bool delProdCat = prodDAO.DeleteProductCategory(userName, password, prodCatID, prodCatIsActive);
            return delProdCat;
        }

        /*'**********************************************************************************************
        * 'Function Name  - DeleteProduct()
        * 'Parameters     - userName, Password, Prod ID
        * 'Description    - Delete Product from Inventory
        * '********************************************************************************************/
        public bool DeleteProduct(string userName, string password, int prodID, string prodIsActive)
        {
            bool delProd = prodDAO.DeleteProduct(userName, password, prodID, prodIsActive);
            return delProd;
        }

        /*'**********************************************************************************************
         * 'Function Name  - ProductList()
         * 'Parameters     - userName, Password
         * 'Description    - List of Product Items to display
         * '********************************************************************************************/
        public List<ProductModel> ProductList(string userName, string password, int prodID)
        {
            List<ProductModel> RetMessage = prodDAO.ProductList(userName, password, prodID);
            return RetMessage;
        }

    }
}