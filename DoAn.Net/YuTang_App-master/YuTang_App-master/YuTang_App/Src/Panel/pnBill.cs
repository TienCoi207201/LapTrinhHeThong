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
    public partial class pnBill : Form
    {
        BillController bill = new BillController();
        EmployeesController employees = new EmployeesController();
        CustomerController customer = new CustomerController();
        public pnBill()
        {
            InitializeComponent();
        }
        private void getData()
        {
            try
            {
                DataSet rs = bill.getBillToDay("bill");
                dgvBill.DataSource = rs.Tables["bill"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void clearText()
        {
            txtMaHD.Text = "";
            txtTenNV.Text = "";
            txtTenKH.Text = "";
            dtNgayHD.Value = DateTime.Today;
            txtTongTien.Text = "";
            txtTrangThai.Text = "";
        }
        private void pnBill_Load(object sender, EventArgs e)
        {
            getData();
            cbbLoai.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                String cbb = cbbLoai.SelectedItem.ToString();
                List<SqlParameter> data = new List<SqlParameter>();
                DataSet rs = new DataSet();
                if (cbb.Equals("Ngày"))
                {
                    String NgayHD = dtNgayHD.Value.ToString("MM/dd/yyyy");
                    data.Add(new SqlParameter("@NgayHD", NgayHD));
                    rs = bill.searchDate("bill", data);
                }
                else if (cbb.Equals("Nhân Viên"))
                {
                    String MaNV = cbbSearch.SelectedValue.ToString();
                    data.Add(new SqlParameter("@MaNV", MaNV));
                    rs = bill.searchMaNV("bill", data);
                }
                else if (cbb.Equals("Khách Hàng"))
                {
                    String MaKH = cbbSearch.SelectedValue.ToString();
                    data.Add(new SqlParameter("@MaKH", MaKH));
                    rs = bill.searchMaKH("bill", data);
                }
                
                
                
                dgvBill.DataSource = rs.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string MaHD = txtMaHD.Text;
                if (MaHD.Length <= 0)
                {
                    MessageBox.Show("ERR");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string MaHD = txtMaHD.Text;
                if(MaHD.Length <= 0)
                {
                    MessageBox.Show("ERR");
                    return;
                }
                pnPay pn = new pnPay(MaHD);
                pn.ShowDialog();
                getData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int idx = e.RowIndex;
                if (idx >= 0)
                {
                    clearText();
                    txtMaHD.Text = dgvBill.Rows[idx].Cells["MaHD"].Value.ToString();
                    txtTenNV.Text = dgvBill.Rows[idx].Cells["TenNV"].Value.ToString();
                    txtTenKH.Text = dgvBill.Rows[idx].Cells["TenKH"].Value.ToString();
                    dtNgayHD.Value = (DateTime)dgvBill.Rows[idx].Cells["NgayHD"].Value;
                    txtTongTien.Text = String.Format("{0:#,###,###,###,###}", Convert.ToInt64(dgvBill.Rows[idx].Cells["TongTien"].Value.ToString()));
                    txtTrangThai.Text = dgvBill.Rows[idx].Cells["TrangThai"].Value.ToString();
                    String TrangThai = dgvBill.Rows[idx].Cells["TrangThai"].Value.ToString();
                    if (TrangThai.Equals("No"))
                    {
                        button1.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = false;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Err : " +ex.Message);
            }
        }

        private void cbbLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String cbb = cbbLoai.SelectedItem.ToString();
                if (cbb.Equals("Ngày"))
                {
                    cbbSearch.Hide();
                } else if (cbb.Equals("Nhân Viên"))
                {
                    cbbSearch.Show();
                    DataSet rs = employees.getAll("rs");
                    cbbSearch.DataSource = rs.Tables[0];
                    cbbSearch.DisplayMember = "TenNV";
                    cbbSearch.ValueMember = "MaNV";
                }
                else if (cbb.Equals("Khách Hàng"))
                {
                    cbbSearch.Show();
                    DataSet rs = customer.getAll("rs");
                    cbbSearch.DataSource = rs.Tables[0];
                    cbbSearch.DisplayMember = "TenKH";
                    cbbSearch.ValueMember = "MaKH";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Err : " + ex.Message);
            }
        }
    }
}
