using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class CategoryDAL
    {
        public SqlConnection con;

        public void connect()
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);

                con.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while connecting " + ex.Message.ToString());
            }
        }

        public int InsertCategory(string CategoryName, string Description)
        {
            try
            {
                connect();

               
                using (SqlCommand cmd = new SqlCommand("SP_Categories_Insert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Categoryname", CategoryName);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    SqlParameter returnParam = new SqlParameter();
                    returnParam.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnParam);
                    cmd.ExecuteNonQuery();
                    int result = (int)returnParam.Value;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while inserting " + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }


        }

        public int UpdateCategory(string CategoryName, string Description, int Categoryid, int Isactive)
        {
            try
            {
                connect();

                using (SqlCommand cmd = new SqlCommand("Sp_categories_Update", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Categoryname", CategoryName);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Categoryid", Categoryid);
                    int result = cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        public int DeleteCategory(int Categoryid)
        {
            try
            {
                connect();
                using (SqlCommand cmd = new SqlCommand("Sp_categories_Delete", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Categoryid", Categoryid);
                    int result = cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while Deleting " + ex.Message.ToString());
            }
            finally
            {
                    con.Close();
            }


        }


        public bool checkCategoryExist(string categoryName)
        {
            try
            {
                connect();
                using (SqlCommand cmd = new SqlCommand("Sp_Categories_CheckExists", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while check the data existance " + ex.Message.ToString());
            }


        }

        public DataTable ViewCategory(bool showDeleted)
        {
            try
            {
                connect();
                using (SqlCommand cmd = new SqlCommand("SP_Categories_ViewAllCategories", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ShowDeleted", showDeleted);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while Fetching" + ex.Message.ToString());
            }

            finally
            {
                con.Close();
            }


        }

        public DataTable getactivecategories()
        {
            connect();
            using(SqlCommand cmd = new SqlCommand("Select CategoryId , CategoryName from Categories where IsActive=1 order by CategoryName", con))
            {
                using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
                
            }
        }

        
            
    }



}
