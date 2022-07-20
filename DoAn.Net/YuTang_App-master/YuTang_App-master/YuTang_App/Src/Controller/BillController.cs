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
    class BillController
    {
        dbConnect conn = new dbConnect();

        public BillController()
        {
        }
        public DataSet getAll(string table_name)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql = "select MaHD, TenNV, TenKH, NgayHD, TongTien, TrangThai\n" +
                            "from HoaDon, NhanVien, KhachHang\n" +
                            "where	HoaDon.MaKH = KhachHang.MaKH and NhanVien.MaNV = HoaDon.MaNV\n "+
                            "order by TrangThai";
                rs = conn.getData(sql, table_name, null);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }

        public DataSet getBillToDay(string table_name)
        {
            try
            {
                Console.WriteLine(DateTime.Today.ToString("MM/dd/yyyy"));
                DataSet rs = new DataSet();
                string sql = "select MaHD, TenNV, TenKH, NgayHD, TongTien, TrangThai\n" +
                            "from HoaDon, NhanVien, KhachHang\n" +
                            "where	HoaDon.MaKH = KhachHang.MaKH and NhanVien.MaNV = HoaDon.MaNV\n "+
                            " and NgayHD = '" + DateTime.Today.ToString("MM/dd/yyyy") + "'\n" +
                            " order by TrangThai";
                rs = conn.getData(sql, table_name, null);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }

        public int paymentByMaHD(List<SqlParameter> data)
        {
            try
            {
                string sql = "update HoaDon set TrangThai = 'Yes' where MaHD = @MaHD";
                int rs = (int)conn.UpdateData(sql, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
        public DataSet getInfoByMaHD(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                String sql = "select TenNV, NhanVien.MaNV , TenKH, KhachHang.SDT ,NgayHD, TongTien\n" +
                            "from HoaDon, NhanVien, KhachHang\n" +
                            "where	HoaDon.MaKH = KhachHang.MaKH and NhanVien.MaNV = HoaDon.MaNV\n" +
                            "		and HoaDon.MaHD = @MaHD ";
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
        public DataSet getFoodByMaHD(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                String sql = "select CTHoaDon.MaHD,CTHoaDon.MaMon, MonAn.TenMon,MonAn.DonGia, CTHoaDon.SoLuong, Sum(CTHoaDon.SoLuong*MonAn.DonGia) as ThanhTien\n" +
                        "from CTHoaDon, MonAn\n" +
                        "where CTHoaDon.MaMon = MonAn.MaMon and\n" +
                        "		CTHoaDon.MaHD = @MaHD \n" +
                        "group by CTHoaDon.MaHD, CTHoaDon.MaMon, MonAn.TenMon, CTHoaDon.SoLuong,MonAn.DonGia";
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
        public DataSet searchDate(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql = "select MaHD, TenNV, TenKH, NgayHD, TongTien, TrangThai\n" +
                            "from HoaDon, NhanVien, KhachHang\n" +
                            "where	HoaDon.MaKH = KhachHang.MaKH and NhanVien.MaNV = HoaDon.MaNV\n" +
                            "and NgayHD = @NgayHD \n" +
                            "order by TrangThai";
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
        public DataSet searchMaNV(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql = "select MaHD, TenNV, TenKH, NgayHD, TongTien, TrangThai\n" +
                            "from HoaDon, NhanVien, KhachHang\n" +
                            "where	HoaDon.MaKH = KhachHang.MaKH and NhanVien.MaNV = HoaDon.MaNV\n" +
                            "and HoaDon.MaNV = @MaNV \n" +
                            "order by TrangThai ASC, NgayHD DESC";
                rs = conn.getData(sql, table_name, data);
                return rs;
            }
            catch (Exception err)
            {
                throw;
            }
        }
        public DataSet searchMaKH(string table_name, List<SqlParameter> data)
        {
            try
            {
                DataSet rs = new DataSet();
                string sql = "select MaHD, TenNV, TenKH, NgayHD, TongTien, TrangThai\n" +
                            "from HoaDon, NhanVien, KhachHang\n" +
                            "where	HoaDon.MaKH = KhachHang.MaKH and NhanVien.MaNV = HoaDon.MaNV\n" +
                            "and HoaDon.MaKH = @MaKH \n" +
                            "order by  TrangThai ASC, NgayHD DESC";
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
