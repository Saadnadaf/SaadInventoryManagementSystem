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
    public class CategoryBLL
    {
        CategoryDAL dal = new CategoryDAL();

        public bool dataexist(string categoryname)
        {
            try
            {
                return dal.checkCategoryExist(categoryname);
            }
            catch(Exception ex)
            {
                throw new Exception ("Error in dataexist BLL "+ ex.Message.ToString());
            }
        }
        
        public int InsertCategoryBLL(CategoryfrmProperties cf)
        {
            try
            {
                if (!dataexist(cf.CategoryName))
                {
                    return dal.InsertCategory(cf.CategoryName, cf.Description);
                }
                else
                {
                    return -2; //If data exist it will return -2 , showing that data exist
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error while inserting " + ex.Message.ToString());
            }
        }

        public int UpdateCategoryBLL(CategoryfrmProperties cf)
        {
            try
            {
                return dal.UpdateCategory( cf.CategoryName,cf.Description, cf.CategoryID,cf.IsActive);
            }
            
            catch(Exception ex)
            {
                throw new Exception("Error while updating " + ex.Message.ToString());
            }
        }


        public int DeleteCategoryBLL(CategoryfrmProperties cf)
        {
            try
            {
                return dal.DeleteCategory(cf.CategoryID);
            }
            catch(Exception ex)
            {
                throw new Exception("Error while Deleting " + ex.Message.ToString());
            }
        }

        public DataTable ViewCategoriesBLL(bool showDeleted)
        {
            try
            {
                return dal.ViewCategory(showDeleted);
            }
            catch(Exception ex)
            {
                throw new Exception("Error while Fetching" + ex.Message.ToString());
            }
        }


    }
}
