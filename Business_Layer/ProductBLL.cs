using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Data_Access_Layer;
using Property_Layer;
using System.Data;
namespace Business_Layer
{
    public class ProductBLL
    {
        ProductDAL dal = new ProductDAL();


        public bool dataexist(string ProductName)
        {
            try
            {
                return dal.checkProductExist(ProductName);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in dataexist BLL " + ex.Message.ToString());
            }
        }

        public int InsertProductBLL(ProductFormProperties pf)
        {
            try
            {
                if (!dataexist(pf.ProductName))
                {
                    return dal.InsertProduct(pf.CategoryId,pf.ProductName, pf.Quantity,pf.Price);
                }
                else 
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while inserting " + ex.Message.ToString());
            }
        }

        public int UpdateProductBLL(ProductFormProperties pf)
        {
            try
            {
                return dal.UpdateProduct(pf.ProductId,pf.ProductName, pf.CategoryId, pf.Quantity, pf.Price);
            }

            catch (Exception ex)
            {
                throw new Exception("Error while updating " + ex.Message.ToString());
            }
        }


        public int DeleteProductBLL(ProductFormProperties pf)
        {
            try
            {
                return dal.DeleteProduct(pf.ProductId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while Deleting " + ex.Message.ToString());
            }
        }


        public DataTable ViewProductBLL(bool showDeleted)
        {
            try
            {
                return dal.ViewProduct(showDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while Fetching" + ex.Message.ToString());
            }
        }

    }
}
