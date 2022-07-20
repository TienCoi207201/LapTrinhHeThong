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
    public partial class pnCustomer : Form
    {
        CustomerController customer = new CustomerController();
        public pnCustomer()
        {
            InitializeComponent();
        }
        private void getData()
        {
            try
            {
                DataSet rs = customer.getAll("customer");
                dgvCustomer.DataSource = rs.Tables["customer"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void clearText(Boolean check)
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            dtNgSinh.Value = DateTime.Today;
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            numPoint.Value = 0;
            cbbLoai.SelectedIndex = 0;
            btnAdd.Enabled = check;
            btnEdit.Enabled = !check;
            btnDel.Enabled = !check;
        }
        private void pnCustomer_Load(object sender, EventArgs e)
        {
            getData();
            clearText(true);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            getData();
            clearText(true);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string TenKH = txtTimKiem.Text.Trim();
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@TenKH", TenKH));
                DataSet rs = customer.search("customer", data);
                dgvCustomer.DataSource = rs.Tables["customer"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                String TenKH = txtTenKH.Text.Trim();
                String NgSinh = dtNgSinh.Value.ToString("MM-dd-yyyy");
                String SDT = txtSDT.Text.Trim();
                String DiaChi = txtDiaChi.Text.Trim();
                int Point = Convert.ToInt32(numPoint.Value);
                String Loai = cbbLoai.SelectedItem.ToString().Equals("Bạc") ? "Bac" : cbbLoai.SelectedItem.ToString().Equals("Vàng") ? "Vang" : "KimCuong";
                if (TenKH.Length <= 0 || NgSinh.Length <= 0 || SDT.Length <= 0 || DiaChi.Length <= 0 || Loai.Length <= 0 || Point < 0)
                {
                    MessageBox.Show("Gía Trị Không Hợp Lệ !!");
                    return;
                }
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@TenKH", TenKH));
                data.Add(new SqlParameter("@NgSinh", NgSinh));
                data.Add(new SqlParameter("@SDT", SDT));
                data.Add(new SqlParameter("@DiaChi", DiaChi));
                data.Add(new SqlParameter("@Point", Point));
                data.Add(new SqlParameter("@LoaiTV", Loai));
                int rs = customer.insertData(data);
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


        private void numPoint_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int point = Convert.ToInt32(numPoint.Value);
                if (point < 1000)
                {
                    cbbLoai.SelectedItem = "Bạc";
                }
                else if (point >= 1000 && point < 3000)
                {
                    cbbLoai.SelectedItem = "Vàng";
                }
                else
                {
                    cbbLoai.SelectedItem = "Kim Cương";
                }
            }
            catch (Exception ex)
            {

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
                String MaKH = txtMaKH.Text.Trim();
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaKH", MaKH));

                int rs = customer.deleteData(data);
                if (rs < 0)
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                String MaKH = txtMaKH.Text;
                String TenKH = txtTenKH.Text.Trim();
                String NgSinh = dtNgSinh.Value.ToString("MM-dd-yyyy");
                String SDT = txtSDT.Text.Trim();
                String DiaChi = txtDiaChi.Text.Trim();
                int Point = Convert.ToInt32(numPoint.Value);
                String Loai = cbbLoai.SelectedItem.ToString().Equals("Bạc") ? "Bac" : cbbLoai.SelectedItem.ToString().Equals("Vàng") ? "Vang" : "KimCuong";
                if (Point < 0 || TenKH.Length == 0 || NgSinh.Length == 0 || SDT.Length == 0 || DiaChi.Length == 0 || Loai.Length == 0) 
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
                data.Add(new SqlParameter("@MaKH", MaKH));
                data.Add(new SqlParameter("@TenKH", TenKH));
                data.Add(new SqlParameter("@NgSinh", NgSinh));
                data.Add(new SqlParameter("@SDT", SDT));
                data.Add(new SqlParameter("@DiaChi", DiaChi));
                data.Add(new SqlParameter("@Point", Point));
                data.Add(new SqlParameter("@LoaiTV", Loai));
                int rs = customer.updateData(data);
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

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int idx = e.RowIndex;
                if (idx >= 0)
                {
                    clearText(false);
                    txtMaKH.Text = dgvCustomer.Rows[idx].Cells["MaKH"].Value.ToString();
                    txtTenKH.Text = dgvCustomer.Rows[idx].Cells["TenKH"].Value.ToString();
                    txtDiaChi.Text = dgvCustomer.Rows[idx].Cells["DiaChi"].Value.ToString();
                    txtSDT.Text = dgvCustomer.Rows[idx].Cells["SDT"].Value.ToString();
                    dtNgSinh.Value = (DateTime)dgvCustomer.Rows[idx].Cells["NgSinh"].Value;
                    numPoint.Value = (int)dgvCustomer.Rows[idx].Cells["Point"].Value;
                    cbbLoai.SelectedItem = dgvCustomer.Rows[idx].Cells["LoaiTV"].Value.ToString().Equals("Bac") ? "Bạc" : dgvCustomer.Rows[idx].Cells["Loai"].Value.ToString().Equals("Vang") ? "Vàng" : "Kim Cương";
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
