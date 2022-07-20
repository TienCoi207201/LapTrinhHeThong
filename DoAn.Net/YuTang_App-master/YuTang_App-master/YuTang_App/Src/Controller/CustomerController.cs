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
    class CustomerController
    {
        dbConnect conn = new dbConnect();

        public CustomerController()
        {
        }
        public DataSet getAll(string table_name)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql = "select * from KhachHang";
                rs = conn.getData(sql, table_name, null);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
 
        public int insertData(List<SqlParameter> data)
        {
            try
            {
                string sql = "insert into KhachHang(TenKH, NgSinh, SDT, DiaChi, Point ,LoaiTV) values (@TenKH, @NgSinh, @SDT, @DiaChi, @Point , @LoaiTV)";
                int rs = (int)conn.UpdateData(sql, data);
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int updateData(List<SqlParameter> data)
        {
            try
            {
                string sql = "update KhachHang set TenKH = @TenKH, SDT = @SDT, NgSinh = @NgSinh , DiaChi = @DiaChi, Point = @Point ,LoaiTV = @LoaiTV where MaKH = @MaKH";
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
                string sql = "delete KhachHang where MaKH = @MaKH";
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
                string sql = "select * from KhachHang where TenKH like '%' + @TenKH + '%'";
                Console.WriteLine(sql);
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
        public DataSet getByID(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql = "select * from KhachHang where MaKH = @MaKH";
                Console.WriteLine(sql);
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
