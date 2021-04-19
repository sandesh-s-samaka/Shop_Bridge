using Shop_Bridge.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Shop_Bridge.DAO
{
    public class ProductDAO
    {
        /*'**********************************************************************************************
        * 'Function Name  - AddProductCategory()
        * 'Parameters     - userName, Password, Product Category, Product Category Description
        * 'Description    - Add Product Category to Inventory 
        * '********************************************************************************************/
        public string AddProductCategory(string userName, string password, string prodCat, string prodCatDesc)
        {
            string flag = string.Empty;
            try
            {
                ShopBridgeEntities loginEntities = ShopBridgeEntities.getEfInstance();
                using (loginEntities)
                {
                    try
                    {
                        var data = loginEntities.tbl_User.Select(x => x.UserID.Equals(userName) && x.Password.Equals(password) && x.IsActive == true).FirstOrDefault();
                    }
                    catch
                    {
                        throw new Exception("User Name or Password Incorrect!! Please Re-enter the Credentials");
                    }
                }

                tbl_Product_Category prodCatTable = new tbl_Product_Category();
                ShopBridgeEntities addProdEntities = ShopBridgeEntities.getEfInstance();
                using (addProdEntities)
                {
                    try
                    {
                        var addProd = addProdEntities.tbl_Product_Category.Where(add => add.Product_Category.Equals(prodCat)).FirstOrDefault();
                        if (addProd == null)
                        {
                            prodCatTable.Product_Category = Convert.ToString(prodCat).Trim();
                            prodCatTable.Product_Category_Desc = Convert.ToString(prodCatDesc).Trim();
                            prodCatTable.Created_By = userName;
                            prodCatTable.Created_Date = DateTime.Now;
                            prodCatTable.Modified_By = userName;
                            prodCatTable.Modified_Date = DateTime.Now;
                            prodCatTable.IsActive = true;

                            addProdEntities.tbl_Product_Category.Add(prodCatTable);
                            addProdEntities.SaveChanges();

                            flag = "Product Category added to Inventory";
                        }
                        else
                        {
                            flag = "Product Category Already Exists!!";
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
                }
                return flag;
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*'**********************************************************************************************
         * 'Function Name  - AddProduct()
         * 'Parameters     - userName, Password, Prod Category, Product, Product Description, Price, Quantity, Stock
         * 'Description    - Add Product to Inventory  
         * '********************************************************************************************/
        public string AddProduct(string userName, string password, int prodCatID, string prod, string prodDesc, double price, int qty, string stock)
        {
            string flag = string.Empty;

            try
            {
                ShopBridgeEntities loginEntities = ShopBridgeEntities.getEfInstance();
                using (loginEntities)
                {
                    try
                    {
                        var data = loginEntities.tbl_User.Where(x => x.UserID.Equals(userName) && x.Password.Equals(password) && x.IsActive == true).FirstOrDefault();
                    }
                    catch
                    {
                        throw new Exception("User Name or Password Incorrect!! Please Re-enter the Credentials");
                    }
                }

                tbl_Product prodTable = new tbl_Product();
                ShopBridgeEntities addProdEntities = ShopBridgeEntities.getEfInstance();
                using (addProdEntities)
                {
                    try
                    {

                        try
                        {
                            var dataCat = addProdEntities.tbl_Product_Category.Where(x => x.PCID == prodCatID && x.IsActive == true).FirstOrDefault();
                        }
                        catch
                        {
                            throw new Exception("Product Category not found!! Please Valid Entries");
                        }

                        var addProd = addProdEntities.tbl_Product.Where(add => add.Product.Equals(prod)).FirstOrDefault();
                        if (addProd == null)
                        {
                            prodTable.Product = Convert.ToString(prod).Trim();
                            prodTable.Product_Description = Convert.ToString(prodDesc).Trim();
                            prodTable.FK_PCID = prodCatID;
                            prodTable.Price = Convert.ToDouble(price);
                            prodTable.Quantity = Convert.ToInt32(qty);
                            prodTable.Stock = Convert.ToString(stock);
                            prodTable.Created_By = userName;
                            prodTable.Created_Date = DateTime.Now;
                            prodTable.Modified_By = userName;
                            prodTable.Modified_Date = DateTime.Now;
                            prodTable.IsActive = true;

                            addProdEntities.tbl_Product.Add(prodTable);
                            addProdEntities.SaveChanges();

                            flag = "Product Details added to Inventory";
                        }
                        else
                        {
                            flag = "Product Details already exists!!";
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
                }
                return flag;
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*'**********************************************************************************************
         * 'Function Name  - EditProductCategory()
         * 'Parameters     - userName, Password, Prod Category ID, Product, Product Description, Price, Quantity
         * 'Description    - Edit Product Category
         * '********************************************************************************************/
        public string EdiProductCategory(string userName, string password, int prodCatID, string prodCat, string prodCatDesc)
        {
            string flag = string.Empty;
            try
            {
                ShopBridgeEntities loginEntities = ShopBridgeEntities.getEfInstance();
                using (loginEntities)
                {
                    try
                    {
                        var data = loginEntities.tbl_User.Where(x => x.UserID.Equals(userName) && x.Password.Equals(password)).FirstOrDefault();
                    }
                    catch
                    {
                        throw new Exception("User Name or Password Incorrect!! Please Re-enter the Credentials");
                    }
                }

                tbl_Product prodTable = new tbl_Product();
                ShopBridgeEntities editProdEntities = ShopBridgeEntities.getEfInstance();
                using (editProdEntities)
                {
                    try
                    {
                        var editProd = editProdEntities.tbl_Product_Category.Where(edit => edit.PCID == prodCatID && edit.IsActive == true).FirstOrDefault();
                        if (editProd != null)
                        {
                            editProd.Product_Category = Convert.ToString(prodCat).Trim();
                            editProd.Product_Category_Desc = Convert.ToString(prodCatDesc).Trim();
                            editProd.Modified_By = userName;
                            editProd.Modified_Date = DateTime.Now;
                            editProd.IsActive = true;

                            editProdEntities.SaveChanges();

                            flag = "Product Category Details updated to Inventory";
                        }
                        else
                        {
                            flag = "Product Category Details Not exists!!";
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
                }
                return flag;
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*'**********************************************************************************************
        * 'Function Name  - EditProduct()
        * 'Parameters     - userName, Password, Prod ID, Product, Product Description, Price, Quantity, Stock
        * 'Description    - Edit Product
        * '********************************************************************************************/
        public string EdiProduct(string userName, string password, int prodID, string prod, string prodDesc, double price, int qty, string stock)
        {
            string flag = string.Empty;
            try
            {
                ShopBridgeEntities loginEntities = ShopBridgeEntities.getEfInstance();
                using (loginEntities)
                {
                    try
                    {
                        var data = loginEntities.tbl_User.Where(x => x.UserID.Equals(userName) && x.Password.Equals(password) && x.IsActive == true).FirstOrDefault();

                    }
                    catch
                    {
                        throw new Exception("User Name or Password Incorrect!! Please Re-enter the Credentials");
                    }
                }

                tbl_Product prodTable = new tbl_Product();
                ShopBridgeEntities editProdEntities = ShopBridgeEntities.getEfInstance();
                using (editProdEntities)
                {
                    try
                    {
                        var editProd = editProdEntities.tbl_Product.Where(edit => edit.PID == prodID && edit.IsActive == true).FirstOrDefault();
                        if (editProd != null)
                        {
                            editProd.Product = Convert.ToString(prod).Trim();
                            editProd.Product_Description = Convert.ToString(prodDesc).Trim();
                            editProd.Price = Convert.ToDouble(price);
                            editProd.Quantity = Convert.ToInt32(qty);
                            editProd.Stock = Convert.ToString(stock);
                            editProd.Modified_By = userName;
                            editProd.Modified_Date = DateTime.Now;
                            editProd.IsActive = true;

                            editProdEntities.SaveChanges();

                            flag = "Product Details updated to Inventory";
                        }
                        else
                        {
                            flag = "Product Details not exists!!";
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
                }
                return flag;
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*'**********************************************************************************************
        * 'Function Name  - DeleteProductCategory()
        * 'Parameters     - userName, Password, Prod Category ID
        * 'Description    - Delete Product Category from Inventory
        * '********************************************************************************************/
        public bool DeleteProductCategory(string userName, string password, int prodCatID, string prodCatIsActive)
        {
            bool flag = false;
            try
            {
                ShopBridgeEntities loginEntities = ShopBridgeEntities.getEfInstance();
                using (loginEntities)
                {
                    try
                    {
                        var data = loginEntities.tbl_User.Where(x => x.UserID.Equals(userName) && x.Password.Equals(password) && x.IsActive == true).FirstOrDefault();
                    }
                    catch
                    {
                        throw new Exception("User Name or Password Incorrect!! Please Re-enter the Credentials");
                    }
                }

                ShopBridgeEntities DelEntities = ShopBridgeEntities.getEfInstance();
                using (DelEntities)
                {
                    var catProd = DelEntities.tbl_Product_Category.Where(x => x.PCID == prodCatID).FirstOrDefault();
                    if (catProd != null)
                    {

                        if (prodCatIsActive == Convert.ToString("true"))
                        {
                            catProd.IsActive = true;
                            flag = true;
                        }

                        else if (prodCatIsActive == Convert.ToString("false"))
                        {
                            catProd.IsActive = false;
                            flag = true;
                        }

                        else
                        {
                            return flag;
                        }

                        DelEntities.SaveChanges();
                    }
                    else
                    {
                        return flag;
                    }
                }
                return flag;
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*'**********************************************************************************************
        * 'Function Name  - DeleteProduct()
        * 'Parameters     - userName, Password, Prod ID
        * 'Description    - Delete Product from Inventory
        * '********************************************************************************************/
        public bool DeleteProduct(string userName, string password, int prodID, string prodIsActive)
        {
            bool flag = false;
            try
            {
                ShopBridgeEntities loginEntities = ShopBridgeEntities.getEfInstance();
                using (loginEntities)
                {
                    try
                    {
                        var data = loginEntities.tbl_User.Where(x => x.UserID.Equals(userName) && x.Password.Equals(password) && x.IsActive == true).FirstOrDefault();
                    }
                    catch
                    {
                        throw new Exception("User Name or Password Incorrect!! Please Re-enter the Credentials");
                    }
                }

                ShopBridgeEntities DelEntities = ShopBridgeEntities.getEfInstance();
                using (DelEntities)
                {
                    var catProd = DelEntities.tbl_Product.Where(x => x.PID == prodID && x.IsActive == true).FirstOrDefault();
                    if (catProd != null)
                    {

                        if (prodIsActive == Convert.ToString("true"))
                        {
                            catProd.IsActive = true;
                            flag = true;
                        }

                        else if (prodIsActive == Convert.ToString("false"))
                        {
                            catProd.IsActive = false;
                            flag = true;
                        }

                        else
                        {
                            return flag;
                        }

                        DelEntities.SaveChanges();
                    }
                    else
                    {
                        return flag;
                    }
                }
                return flag;
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*'**********************************************************************************************
         * 'Function Name  - ProductList()
         * 'Parameters     - userName, Password
         * 'Description    - List of Product Items to display
         * '********************************************************************************************/
        public List<ProductModel> ProductList(string userName, string password, int prodID)
        {
            List<ProductModel> prodModel;
            try
            {

                ShopBridgeEntities loginEntities = ShopBridgeEntities.getEfInstance();
                using (loginEntities)
                {
                    try
                    {
                        var usr = loginEntities.tbl_User.Where(x => x.UserID.Equals(userName) && x.Password.Equals(password) && x.IsActive == true).FirstOrDefault();
                    }
                    catch
                    {
                        throw new Exception("User Name or Password Incorrect!! Please Re-enter the Credentials");
                    }
                }

                ShopBridgeEntities prodEntities = ShopBridgeEntities.getEfInstance();
                using (prodEntities)
                {

                    if (prodID == 0)
                    {
                        var data = (from rows in prodEntities.tbl_Product
                                    where rows.IsActive == true
                                    select rows).ToList();

                        prodModel = data.AsEnumerable().Select(DataRow => new ProductModel
                        {
                            PID = DataRow.PID,
                            Product = DataRow.Product,
                            Product_Description = DataRow.Product_Description,
                            FK_PCID = DataRow.FK_PCID,
                            Price = DataRow.Price,
                            Quantity = DataRow.Quantity,
                            Stock = DataRow.Stock,
                            Created_By = DataRow.Created_By,
                            Created_Date = string.Format("{0: dd/mm/yyyy}", DataRow.Created_Date),
                            Modified_By = DataRow.Modified_By,
                            Modified_Date = string.Format("{0: dd/mm/yyyy}", DataRow.Modified_Date),
                            IsActive = DataRow.IsActive,

                        }).ToList();

                    }
                    else
                    {
                        var data = (from rows in prodEntities.tbl_Product
                                    where rows.PID == prodID && rows.IsActive == true
                                    select rows).ToList();

                        prodModel = data.AsEnumerable().Select(DataRow => new ProductModel
                        {
                            PID = DataRow.PID,
                            Product = DataRow.Product,
                            Product_Description = DataRow.Product_Description,
                            FK_PCID = DataRow.FK_PCID,
                            Price = DataRow.Price,
                            Quantity = DataRow.Quantity,
                            Stock = DataRow.Stock,
                            Created_By = DataRow.Created_By,
                            Created_Date = string.Format("{0: dd/mm/yyyy}", DataRow.Created_Date),
                            Modified_By = DataRow.Modified_By,
                            Modified_Date = string.Format("{0: dd/mm/yyyy}", DataRow.Modified_Date),
                            IsActive = DataRow.IsActive,

                        }).ToList();
                    }
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
            return prodModel;
        }
    }
}