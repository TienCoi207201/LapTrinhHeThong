using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinFormsApp1
{
   
    public partial class Quanlynhanvien : Form
    {
        SqlConnection conn_str = new SqlConnection(@"Data Source=LAPTOP-TTTBNCOH;Initial Catalog=QuanLyNhaHang;Integrated Security=True");
        public Quanlynhanvien()
        {
            InitializeComponent();
            loadnhanvien();
        }

        private void loadnhanvien()
        {
            try
            {
                conn_str.Open();
                SqlCommand cmd = new SqlCommand("select * from nhanvien", conn_str);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dgvnhanvien.DataSource = dt;
                conn_str.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void cleardulieu()
        {
            txtmanv.Text = txtnhanvien.Text = txtquequan.Text = txtsdt.Text = txtgioitinh.Text = txtchucvu.Text = txthsl.Text = "";

        }
        private void btnthem_Click(object sender, EventArgs e)
        {
            try
            {
                conn_str.Open();
                SqlCommand cmd = new SqlCommand("insert into nhanvien values(@MaNV, @TenNV, @Gioitinh, @Quequan, @SDT, @HSL,@Chucvu)", conn_str);
                cmd.Parameters.Add(new SqlParameter("@MaNV", txtmanv.Text));
                cmd.Parameters.Add(new SqlParameter("@TenNV", txtnhanvien.Text));
                cmd.Parameters.Add(new SqlParameter("@Gioitinh", txtgioitinh.Text));
                cmd.Parameters.Add(new SqlParameter("@Quequan", txtquequan.Text));
                cmd.Parameters.Add(new SqlParameter("@SDT", txtsdt.Text));
                cmd.Parameters.Add(new SqlParameter("@HSL", txthsl.Text));
                cmd.Parameters.Add(new SqlParameter("@Chucvu", txtchucvu.Text));
                cmd.ExecuteNonQuery();
                conn_str.Close();
                cleardulieu();
                loadnhanvien();
                MessageBox.Show("Thêm thành công!");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hãy xem lại thông tin vừa nhập", ex.Message);
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            try
            {
                conn_str.Open();  
                SqlCommand cmd = new SqlCommand("update nhanvien set TenNV=@TenNV,GioiTinh=@GioiTinh,QueQuan=@QueQuan,Sdt=@Sdt,HSL=@HSL,ChucVu=@ChucVu where MaNV=@MaNV", conn_str);
                cmd.Parameters.Add(new SqlParameter("@MaNV", txtmanv.Text));
                cmd.Parameters.Add(new SqlParameter("@TenNV", txtnhanvien.Text));
                cmd.Parameters.Add(new SqlParameter("@GioiTinh", txtgioitinh.Text));
                cmd.Parameters.Add(new SqlParameter("@QueQuan", txtquequan.Text));
                cmd.Parameters.Add(new SqlParameter("@Sdt", txtsdt.Text));
                cmd.Parameters.Add(new SqlParameter("@HSL", txthsl.Text));
                cmd.Parameters.Add(new SqlParameter("@ChucVu", txtchucvu.Text));
                cmd.ExecuteNonQuery();
                conn_str.Close();
                cleardulieu();
                loadnhanvien();
                MessageBox.Show("Sửa thành công!");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Sửa thất bại", ex.Message);
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            try
            {
                conn_str.Open();
                SqlCommand cmd = new SqlCommand("delete from nhanvien where manv=@manv ", conn_str);
                cmd.Parameters.Add(new SqlParameter("@manv", txtmanv.Text));
                cmd.ExecuteNonQuery();
                conn_str.Close();
                cleardulieu();
                loadnhanvien();
                MessageBox.Show("Xóa thành công!");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Xóa thất bại", ex.Message);
            }
        }

        private void dgvnhanvien_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;
            txtmanv.Text = dgvnhanvien.Rows[numrow].Cells[0].Value.ToString();
            txtnhanvien.Text = dgvnhanvien.Rows[numrow].Cells[1].Value.ToString();
            txtgioitinh.Text = dgvnhanvien.Rows[numrow].Cells[2].Value.ToString();
            txtquequan.Text = dgvnhanvien.Rows[numrow].Cells[3].Value.ToString();
            txtsdt.Text = dgvnhanvien.Rows[numrow].Cells[4].Value.ToString();
            txthsl.Text = dgvnhanvien.Rows[numrow].Cells[5].Value.ToString();
            txtchucvu.Text = dgvnhanvien.Rows[numrow].Cells[6].Value.ToString();
        }
    }
}
