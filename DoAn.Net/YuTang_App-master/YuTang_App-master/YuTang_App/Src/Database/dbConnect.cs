using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuTang_App.Src.Database
{
    class dbConnect
    {
        String conn_str = @"Data Source=LAPTOP-TTTBNCOH;Initial Catalog=llong_CNPM;Integrated Security=True";
        SqlConnection conn = null;

        public dbConnect()
        {
            conn = new SqlConnection(conn_str);
        }
        public int CountData(String sql, List<SqlParameter> data)
        {
            int rs = 0;
            try
            {
                if(conn == null)
                {
                    conn = new SqlConnection(conn_str);
                }
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (data != null)
                {
                    cmd.Parameters.AddRange(data.ToArray());
                }
                rs = (int)cmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rs;
        }

        public DataSet getData(String sql, string table_name, List<SqlParameter> data)
        {
            if (conn == null)
            {
                conn = new SqlConnection(conn_str);
            }
            DataSet rs = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                if (data != null)
                {
                    da.SelectCommand.Parameters.AddRange(data.ToArray());
                }
                da.Fill(rs, table_name);
            }
            catch (Exception ex)
            {
                throw;
            }

            return rs;
        }
        public int UpdateData(String sql, List<SqlParameter> data)
        {
            if (conn == null)
            {
                conn = new SqlConnection(conn_str);
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (data != null)
                {
                    cmd.Parameters.AddRange(data.ToArray());
                }
                int rs = (int)cmd.ExecuteNonQuery();
                conn.Close();
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
