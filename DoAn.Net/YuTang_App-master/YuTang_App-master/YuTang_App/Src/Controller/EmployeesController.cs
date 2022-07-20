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
    class EmployeesController
    {
        dbConnect conn = new dbConnect();

        public EmployeesController()
        {
        }
        public DataSet getAll(string table_name)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql = "select * from NhanVien";
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
                string sql = "insert into NhanVien(TenNV, GioiTinh, NgSinh, CMND, SDT, DiaChi, ChucVu ,hsl) values (@TenNV, @GioiTinh, @NgSinh, @CMND, @SDT, @DiaChi, @ChucVu , @hsl)";
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
                string sql = "update NhanVien set TenNV = @TenNV, GioiTinh = @GioiTinh, NgSinh = @NgSinh, CMND = @CMND, SDT = @SDT, DiaChi = @DiaChi, ChucVu = @ChucVu , hsl = @hsl  where MaNV = @MaNV";
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
                string sql = "delete NhanVien where MaNV = @MaNV";
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
                string sql = "select * from NhanVien where TenNV like '%' + @TenNV + '%'";
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
