using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using YuTang_App.Src.Controller;

namespace YuTang_App.Src.Panel
{
    public partial class pnEmployees : Form
    {
        public pnEmployees()
        {
            InitializeComponent();
        }
        EmployeesController employees = new EmployeesController();
        private void getData()
        {
            try
            {
                DataSet rs = employees.getAll("employees");
                dgvCustomer.DataSource = rs.Tables["employees"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void clearText(Boolean check)
        {
            txtMaNV.Text = "";
            txtTenKH.Text = "";
            rbNam.Checked = true;
            dtNgSinh.Value = DateTime.Today;
            txtCMND.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            txtCMND.Text = "";
            cbbLoai.SelectedIndex = 0;
            
            btnAdd.Enabled = check;
            btnEdit.Enabled = !check;
            btnDel.Enabled = !check;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string TenNV = txtTimKiem.Text.Trim();
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@TenNV", TenNV));
                DataSet rs = employees.search("employees", data);
                dgvCustomer.DataSource = rs.Tables["employees"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pnEmployees_Load(object sender, EventArgs e)
        {
            getData();
            clearText(true);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            getData();
            clearText(true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                String TenNV = txtTenKH.Text.Trim();
                String GioiTinh = rbNam.Checked ? "Nam" : "Nu";
                String NgSinh = dtNgSinh.Value.ToString("MM-dd-yyyy");
                String CMND = txtCMND.Text.Trim();
                String SDT = txtSDT.Text.Trim();
                String DiaChi = txtDiaChi.Text.Trim();
                String ChucVu = cbbLoai.SelectedItem.ToString().Equals("Nhân Viên") ? "NhanVien" : "QuanLy";
                double hsl = Convert.ToDouble( txtHsl.Text);
                if (TenNV.Length <= 0 || GioiTinh.Length <= 0 || NgSinh.Length <= 0 || CMND.Length <= 0 || SDT.Length <= 0 || DiaChi.Length <= 0 || ChucVu.Length <= 0 )
                {
                    MessageBox.Show("Gía Trị Không Hợp Lệ !!");
                    return;
                }
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@TenNV", TenNV));
                data.Add(new SqlParameter("@GioiTinh", GioiTinh));
                data.Add(new SqlParameter("@NgSinh", NgSinh));
                data.Add(new SqlParameter("@CMND", CMND));
                data.Add(new SqlParameter("@SDT", SDT));
                data.Add(new SqlParameter("@DiaChi", DiaChi));
                data.Add(new SqlParameter("@ChucVu", ChucVu));
                data.Add(new SqlParameter("@hsl", hsl));
                
                int rs = employees.insertData(data);
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

        private void cbbLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtHsl.Text = cbbLoai.SelectedItem.ToString().Equals("Nhân Viên") ? "1.0" : "1.25";
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
      
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                String MaNV = txtMaNV.Text.Trim();
                String TenNV = txtTenKH.Text.Trim();
                String GioiTinh = rbNam.Checked ? "Nam" : "Nu";
                String NgSinh = dtNgSinh.Value.ToString("MM-dd-yyyy");
                String CMND = txtCMND.Text.Trim();
                String SDT = txtSDT.Text.Trim();
                String DiaChi = txtDiaChi.Text.Trim();
                String ChucVu = cbbLoai.SelectedItem.ToString().Equals("Nhân Viên") ? "NhanVien" : "QuanLy";
                double hsl = Convert.ToDouble(txtHsl.Text);
                if (TenNV.Length <= 0 || GioiTinh.Length <= 0 || NgSinh.Length <= 0 || CMND.Length <= 0 || SDT.Length <= 0 || DiaChi.Length <= 0 || ChucVu.Length <= 0)
                {
                    MessageBox.Show("Gía Trị Không Hợp Lệ !!");
                    return;
                }
                DialogResult res = MessageBox.Show("Are you sure you want to Update", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    return;
                }
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaNV", MaNV));
                data.Add(new SqlParameter("@TenNV", TenNV));
                data.Add(new SqlParameter("@GioiTinh", GioiTinh));
                data.Add(new SqlParameter("@NgSinh", NgSinh));
                data.Add(new SqlParameter("@CMND", CMND));
                data.Add(new SqlParameter("@SDT", SDT));
                data.Add(new SqlParameter("@DiaChi", DiaChi));
                data.Add(new SqlParameter("@ChucVu", ChucVu));
                data.Add(new SqlParameter("@hsl", hsl));

                int rs = employees.updateData(data);
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
                String MaNV = txtMaNV.Text.Trim();
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaNV", MaNV));

                int rs = employees.deleteData(data);
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

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int idx = e.RowIndex;
                if (idx >= 0)
                {
                    clearText(false);
                    txtMaNV.Text = dgvCustomer.Rows[idx].Cells["MaNV"].Value.ToString();
                    txtTenKH.Text = dgvCustomer.Rows[idx].Cells["TenNV"].Value.ToString();
                    rbNam.Checked = dgvCustomer.Rows[idx].Cells["GioiTinh"].Value.ToString().Equals("Nam") ? true : false;
                    dtNgSinh.Value = (DateTime)dgvCustomer.Rows[idx].Cells["NgSinh"].Value;
                    txtCMND.Text = dgvCustomer.Rows[idx].Cells["CMND"].Value.ToString();
                    txtSDT.Text = dgvCustomer.Rows[idx].Cells["SDT"].Value.ToString();
                    txtDiaChi.Text = dgvCustomer.Rows[idx].Cells["DiaChi"].Value.ToString();
                    cbbLoai.SelectedItem = dgvCustomer.Rows[idx].Cells["ChucVu"].Value.ToString().Equals("NhanVien") ? "Nhân Viên" : "Quản Lí";
                    txtHsl.Text = dgvCustomer.Rows[idx].Cells["Hsl"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
