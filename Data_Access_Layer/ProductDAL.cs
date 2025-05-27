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

    public class ProductDAL
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



        public int InsertProduct(int CategoryID,string ProductName, int Quantity, decimal   Price)
        {
            try
            {
                connect();


                using (SqlCommand cmd = new SqlCommand("SP_Product_Insert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductName", ProductName);
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);
                    cmd.Parameters.AddWithValue("@Price", Price);
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

        public int UpdateProduct(int ProductID , string ProductName, int CategoryID, int Quantity, decimal Price)
        {
            try
            {
                connect();

                using (SqlCommand cmd = new SqlCommand("SP_Product_Update", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@ProductName", ProductName);
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    int result = cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating " + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }


        public int DeleteProduct(int ProductID)
        {
            try
            {
                connect();
                using (SqlCommand cmd = new SqlCommand("SP_Product_Delete", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
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

        public DataTable ViewProduct(bool showDeleted)
        {
            try
            {
                connect();
                using (SqlCommand cmd = new SqlCommand("SP_Product_View",con))
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

          

        public bool checkProductExist(string ProductName)
        {
            try  
            {
                connect();    
                using (SqlCommand cmd = new SqlCommand("SP_Product_CheckExists", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductName", ProductName);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while check the data existance " + ex.Message.ToString());
            }


        }

    }

}

