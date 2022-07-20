using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuTang_App.Src.Database;
namespace YuTang_App.Src.Controller
{
    class FoodController
    {
        dbConnect conn = new dbConnect();

        public FoodController()
        {
        }
        public DataSet getAll(string table_name)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql = "select * from MonAn";
                rs = conn.getData(sql, table_name, null);
                return rs;
            }
            catch(Exception err) {
                throw;
            }
        }

        public int insertData(List<SqlParameter> data)
        {
            try
            {
                string sql = "insert into MonAn(TenMon, GiaGoc, KhuyenMai, Loai) values (@TenMon, @GiaGoc, @KhuyenMai, @Loai)";
                int rs = (int)conn.UpdateData(sql, data);
                return rs;
            }catch(Exception ex)
            {
                throw;
            }
        }
        public int updateData(List<SqlParameter> data)
        {
            try
            {
                string sql = "update MonAn set TenMon = @TenMon, GiaGoc = @GiaGoc, KhuyenMai = @KhuyenMai, Loai = @Loai where MaMon = @MaMon";
                int rs = (int)conn.UpdateData(sql, data);
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int deleteData(List<SqlParameter> data)
        {
            try
            {
                string sql = "delete MonAn where MaMon = @MaMon";
                int rs = (int)conn.UpdateData(sql, data);
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataSet search(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql = "select * from MonAn where TenMon like '%' + @TenMon + '%'";
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
        public DataSet searchLoai(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql = "select * from MonAn where Loai like '%' + @Loai + '%'";
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
    }
}
