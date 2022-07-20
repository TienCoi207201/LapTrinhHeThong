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
    class ReportController
    {
        dbConnect conn = new dbConnect();
        public DataSet DT_month(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql = "select HoaDon.MaHD , TenNV, TenKH, NgayHD, TongTien "+
                            "from HoaDon, NhanVien, KhachHang " +
                            "where HoaDon.MaNV = NhanVien.MaNV and HoaDon.MaKH = KhachHang.MaKH and TrangThai = 'Yes' "+
                            "and MONTH(ngayHd) = @_Month ";
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
        public DataSet DT_date(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql = "select HoaDon.MaHD , TenNV, TenKH, NgayHD, TongTien " +
                            "from HoaDon, NhanVien, KhachHang " +
                            "where HoaDon.MaNV = NhanVien.MaNV and HoaDon.MaKH = KhachHang.MaKH and TrangThai = 'Yes' " +
                            "and ngayHd BETWEEN @NgayBD AND @NgayKT ";
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
        public DataSet DT_Year(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql = "select 'Tháng ' +CONVERT(varchar, MONTH(NgayHD)) as 'Thang', COUNT(*) as 'SoHoaDon', Sum(TongTien) as 'DoanhThu' \n" +
                            "from HoaDon \n" +
                            "where TrangThai = 'Yes' "+
                             "group by MONTH(HoaDon.NgayHD)";
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }

        public DataSet F_month(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql =    "select MonAn.MaMon, TenMon, count(*) as 'SoHD', sum(cthoadon.soluong) as 'SoLuong',  Sum(SoLuong * DonGia) as 'ThanhTien' " +
                                "from MonAn, HoaDon, CTHoaDon " +
                                "where MonAn.MaMon = CTHoaDon.MaMon and CTHoaDon.MaHD = HoaDon.MaHD " +
                                "and HoaDon.TrangThai = 'yes' " +
                                "and MONTH(ngayHd) = @_Month "+
                                "group by MonAn.MaMon, TenMon  ";
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
        public DataSet F_date(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql =    "select MonAn.MaMon, TenMon, count(*) as 'SoHD', sum(cthoadon.soluong) as 'SoLuong',  Sum(SoLuong * DonGia) as 'ThanhTien' " +
                                "from MonAn, HoaDon, CTHoaDon " +
                                "where MonAn.MaMon = CTHoaDon.MaMon and CTHoaDon.MaHD = HoaDon.MaHD " +
                                "and HoaDon.TrangThai = 'yes' " +
                                "and ngayHd BETWEEN @NgayBD AND @NgayKT " +
                                "group by MonAn.MaMon, TenMon  ";
                            
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
        public DataSet F_Year(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql =    "select MonAn.MaMon, TenMon, count(*) as 'SoHD', sum(cthoadon.soluong) as 'SoLuong',  Sum(SoLuong * DonGia) as 'ThanhTien' " +
                                "from MonAn, HoaDon, CTHoaDon " +
                                "where MonAn.MaMon = CTHoaDon.MaMon and CTHoaDon.MaHD = HoaDon.MaHD " +
                                "and HoaDon.TrangThai = 'yes' " +
                                "and YEAR(ngayhd) = @_Year " + 
                                "group by MonAn.MaMon, TenMon  ";
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
        public DataSet NV_month(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql =    "select NhanVien.MaNV, NhanVien.TenNV, COUNT(*) as 'SoHD', SUM(HoaDon.TongTien) as 'ThanhTien' "+
                                "from NhanVien, HoaDon, CTHoaDon " +
                                "where NhanVien.MaNV = HoaDon.MaNV and CTHoaDon.MaHD = HoaDon.MaHD "+
                                "and HoaDon.TrangThai = 'Yes' " +
                                "and MONTH(ngayHd) = @_Month " +
                                "group by NhanVien.MaNV, NhanVien.TenNV";
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
        public DataSet NV_date(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql =    "select NhanVien.MaNV, NhanVien.TenNV, COUNT(*) as 'SoHD', SUM(HoaDon.TongTien) as 'ThanhTien' " +
                                "from NhanVien, HoaDon, CTHoaDon " +
                                "where NhanVien.MaNV = HoaDon.MaNV and CTHoaDon.MaHD = HoaDon.MaHD " +
                                "and HoaDon.TrangThai = 'Yes' " +
                                "and ngayHd BETWEEN @NgayBD AND @NgayKT " +
                                "group by NhanVien.MaNV, NhanVien.TenNV";
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
        public DataSet NV_Year(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql =    "select NhanVien.MaNV, NhanVien.TenNV, COUNT(*) as 'SoHD', SUM(HoaDon.TongTien) as 'ThanhTien' " +
                                "from NhanVien, HoaDon, CTHoaDon " +
                                "where NhanVien.MaNV = HoaDon.MaNV and CTHoaDon.MaHD = HoaDon.MaHD " +
                                "and HoaDon.TrangThai = 'Yes' " +
                                "and YEAR(ngayhd) = @_Year " +
                                "group by NhanVien.MaNV, NhanVien.TenNV";
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
