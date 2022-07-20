using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using YuTang_App.Src.Database;

namespace YuTang_App.Src.Controller
{
    class SellController
    {
        dbConnect conn = new dbConnect();

        public SellController()
        {
        }
        public int insertData(List<SqlParameter> data)
        {
            try
            {
                string sql = "insert into HoaDon(MaNV, MaKH, NgayHD, TongTien) values (@MaNV, @MaKH, @NgayHD, 0)";
                int rs = (int)conn.UpdateData(sql, data);
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int insertFoodinMaHD(List<SqlParameter> data)
        {
            try
            {
                string sql = "insert into CTHoaDon (MaHD, MaMon, SoLuong) values (@MaHD, @MaMon, @SoLuong)";
                int rs = (int)conn.UpdateData(sql, data);
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int delFoodinMaHD(List<SqlParameter> data)
        {
            try
            {
                string sql = "delete CTHoaDon Where MaHD = @MaHD and MaMon = @MaMon";
                int rs = (int)conn.UpdateData(sql, data);
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public String getMaHDMAX()
        {
            DataSet rs = new DataSet();
            string sql = "select mahd from hoadon where mahd >= all (select mahd from hoadon)";
            rs = conn.getData(sql, "MaHD", null);
            return rs.Tables["MaHD"].Rows[0]["mahd"].ToString();
        }
        public DataSet getFoodbyHD(String table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                String sql = "select MonAn.MaMon, MonAn.TenMon, MonAn.DonGia, CTHoaDon.SoLuong, Sum(CTHoaDon.SoLuong*MonAn.DonGia) as 'ThanhTien'\n" +
                                "from HoaDon, CTHoaDon, MonAn\n" +
                                "where HoaDon.MaHD = CTHoaDon.MaHD and MonAn.MaMon = CTHoaDon.MaMon and HoaDon.MaHD = @MaHD \n" +
                                "group by HoaDon.MaHD, MonAn.MaMon, MonAn.TenMon, MonAn.DonGia, CTHoaDon.SoLuong";

                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataSet getInfoByHD(String table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                String sql = "select TenNV, TenKH, NgayHD, TongTien\n" +
                            "from HoaDon, NhanVien, KhachHang\n" +
                            "where	HoaDon.MaKH = KhachHang.MaKH and NhanVien.MaNV = HoaDon.MaNV\n" +
                            "		and HoaDon.MaHD = @MaHD ";

                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
