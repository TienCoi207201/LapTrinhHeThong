using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinFormsApp1
{
    
    public partial class QuanLyKhachHang : Form
    {
        SqlConnection conn_str = new SqlConnection(@"Data Source=LAPTOP-TTTBNCOH;Initial Catalog=QuanLyNhaHang;Integrated Security=True");
        public QuanLyKhachHang()
        {
            InitializeComponent();
            loadkhachhang(); 
        }

        private void loadkhachhang()
        {
            try
            {
                conn_str.Open();
                SqlCommand cmd = new SqlCommand("select * from KhachHang", conn_str);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                conn_str.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }
        private void cleardulieu()
        {
            txtmakh.Text = txttenkh.Text = txtquequan.Text = txtsdt.Text = txtgt.Text = "";
            
        }
       

        private void btnthem_Click(object sender, EventArgs e)
        {
            try
            {
                conn_str.Open();
                SqlCommand cmd = new SqlCommand("insert into KhachHang values(@MaKH, @TenKH, @SDT, @Ngaysinh, @Quequan, @Gioitinh)", conn_str);
                cmd.Parameters.Add(new SqlParameter("@MaKH", txtmakh.Text));
                cmd.Parameters.Add(new SqlParameter("@TenKH", txttenkh.Text));
                cmd.Parameters.Add(new SqlParameter("@SDT", txtsdt.Text));
                cmd.Parameters.Add(new SqlParameter("@Ngaysinh", dateTimePicker1.Text));
                cmd.Parameters.Add(new SqlParameter("@Quequan", txtquequan.Text));
                cmd.Parameters.Add(new SqlParameter("@Gioitinh", txtgt.Text));
                cmd.ExecuteNonQuery();
                conn_str.Close();
                cleardulieu(); 
                loadkhachhang();
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
                SqlCommand cmd = new SqlCommand("update khachhang set makh=@makh,tenkh=@tenkh,sdt=@sdt,ngaysinh=@ngaysinh,quequan=@quequan,gioitinh=@gioitinh where makh=@makh", conn_str);
                cmd.Parameters.Add(new SqlParameter("@MaKH", txtmakh.Text));
                cmd.Parameters.Add(new SqlParameter("@TenKH", txttenkh.Text));
                cmd.Parameters.Add(new SqlParameter("@SDT", txtsdt.Text));
                cmd.Parameters.Add(new SqlParameter("@Ngaysinh", dateTimePicker1.Text));
                cmd.Parameters.Add(new SqlParameter("@Quequan", txtquequan.Text));
                cmd.Parameters.Add(new SqlParameter("@Gioitinh", txtgt.Text));
                cmd.ExecuteNonQuery();
                conn_str.Close();
                cleardulieu();
                loadkhachhang();
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
                SqlCommand cmd = new SqlCommand("delete from khachhang where makh=@makh ", conn_str);
                cmd.Parameters.Add(new SqlParameter("@MaKH", txtmakh.Text));
                cmd.ExecuteNonQuery();
                conn_str.Close();
                cleardulieu();
                loadkhachhang();
                MessageBox.Show("Xóa thành công!");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Xóa thất bại", ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;
            txtmakh.Text = dataGridView1.Rows[numrow].Cells[0].Value.ToString();
            txttenkh.Text = dataGridView1.Rows[numrow].Cells[1].Value.ToString();
            txtgt.Text = dataGridView1.Rows[numrow].Cells[2].Value.ToString();
            txtquequan.Text = dataGridView1.Rows[numrow].Cells[3].Value.ToString();
            txtsdt.Text = dataGridView1.Rows[numrow].Cells[4].Value.ToString();
            //dateTimePicker1.Text = dataGridView1.Rows[numrow].Cells[1].Value.ToString();
        }
    }
}
