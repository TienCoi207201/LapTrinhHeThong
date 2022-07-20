using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YuTang_App.Src.Controller;
namespace YuTang_App.Src.Panel
{
    public partial class pnFood : Form
    {
        FoodController food = new FoodController();
        public pnFood()
        {
            InitializeComponent();
        }
        private void getData()
        {
            try
            {
                DataSet rs = food.getAll("foods");
                dgvFood.DataSource = rs.Tables["foods"];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void clearText(Boolean check)
        {
            txtMaMon.Text = "";
            txtTenMon.Text = "";
            txtGiaGoc.Text = "0";
            numKhuyenMai.Value = 0;
            txtDonGia.Text = "0";
            cbbLoai.SelectedIndex = 0;
            btnAdd.Enabled = check;
            btnEdit.Enabled = !check;
            btnDel.Enabled = !check;
        }

        private void pnFood_Load(object sender, EventArgs e)
        {
            getData();
            clearText(true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                String TenMon = txtTenMon.Text.Trim();
                int GiaGoc = Convert.ToInt32(txtGiaGoc.Text.Trim());
                int KhuyenMai = Convert.ToInt32(numKhuyenMai.Text.Trim());
                String Loai = cbbLoai.SelectedItem.ToString().Equals("Đồ Ăn") ? "DoAn" : "DoUong";
                if (KhuyenMai < 0 || KhuyenMai >= 100 || GiaGoc <= 0 || TenMon.Length <= 0 || Loai.Length <= 0)
                {
                    MessageBox.Show("Giá Trị Không Hợp Lệ !");
                    return;
                }
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@TenMon", TenMon));
                data.Add(new SqlParameter("@GiaGoc", GiaGoc));
                data.Add(new SqlParameter("@KhuyenMai", KhuyenMai));
                data.Add(new SqlParameter("@Loai", Loai));
                int rs = food.insertData(data);
                if (rs <= 0)
                {
                    MessageBox.Show("Không Thành Công !");
                }
                else
                {
                    getData();
                    clearText(true);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("ERR : " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                
                String MaMon = txtMaMon.Text.Trim();
                String TenMon = txtTenMon.Text.Trim();
                int GiaGoc = Convert.ToInt32(txtGiaGoc.Text.Trim());
                int KhuyenMai = Convert.ToInt32(numKhuyenMai.Text.Trim());
                String Loai = cbbLoai.SelectedItem.ToString().Equals("Đồ Ăn") ? "DoAn" : "DoUong";
                if (KhuyenMai < 0 || KhuyenMai >= 100 || GiaGoc <= 0 || TenMon.Length <= 0 || Loai.Length <= 0)
                {
                    MessageBox.Show("Giá Trị Không Hợp Lệ !");
                    return;
                }
                DialogResult res = MessageBox.Show("Are you sure you want to Update", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    return;
                }
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaMon", MaMon));
                data.Add(new SqlParameter("@TenMon", TenMon));
                data.Add(new SqlParameter("@GiaGoc", GiaGoc));
                data.Add(new SqlParameter("@KhuyenMai", KhuyenMai));
                data.Add(new SqlParameter("@Loai", Loai));
                int rs = food.updateData(data);
                if (rs <= 0)
                {
                    MessageBox.Show("Không Thành Công !");
                }
                else
                {
                    getData();
                    clearText(true);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERR : " + ex.Message);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = MessageBox.Show("Are you sure you want to Delete", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    return;
                }
                String MaMon = txtMaMon.Text.Trim();
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaMon", MaMon));

                int rs = food.deleteData(data);
                if (rs <= 0)
                {
                    MessageBox.Show("Không Thành Công !");
                }
                else
                {
                    getData();
                    clearText(true);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERR : " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            getData();
            clearText(true);
        }

        private void dgvFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            if (idx >= 0)
            {
                clearText(false);
                txtMaMon.Text = dgvFood.Rows[idx].Cells["MaMon"].Value.ToString();
                txtTenMon.Text = dgvFood.Rows[idx].Cells["TenMon"].Value.ToString();
                txtGiaGoc.Text = dgvFood.Rows[idx].Cells["GiaGoc"].Value.ToString();
                numKhuyenMai.Text = dgvFood.Rows[idx].Cells["KhuyenMai"].Value.ToString();
                txtDonGia.Text = dgvFood.Rows[idx].Cells["DonGia"].Value.ToString();
                cbbLoai.SelectedItem = dgvFood.Rows[idx].Cells["Loai"].Value.ToString().Equals("DoAn") ? "Đồ Ăn" : "Đồ Uống";
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string TenMon = txtTimKiem.Text.Trim();
                if (TenMon.Length <= 0)
                {
                    MessageBox.Show("Giá Trị Không Hợp Lệ !");
                    return;
                }
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@TenMon", TenMon));
                DataSet rs = food.search("foods", data);
                dgvFood.DataSource = rs.Tables["foods"];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
