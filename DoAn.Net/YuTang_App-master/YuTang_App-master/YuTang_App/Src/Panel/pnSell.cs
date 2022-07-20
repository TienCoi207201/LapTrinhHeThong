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
    public partial class pnSell : Form
    {
        FoodController food = new FoodController();
        EmployeesController employees = new EmployeesController();
        CustomerController customer = new CustomerController();
        SellController sell = new SellController();
        String MaHD;

        public string MaHD1 { get => MaHD; set => MaHD = value; }

        public pnSell()
        {
            InitializeComponent();
        }
        private void ShowcbbEmployees()
        {
            try
            {
                DataSet rs = employees.getAll("employees");
                cbbTenNV.DataSource = rs.Tables["employees"];
                cbbTenNV.DisplayMember = "TenNV";
                cbbTenNV.ValueMember = "MaNV";
            }catch(Exception ex)
            {
                MessageBox.Show("ERR : " + ex.Message);
            }
        }
        private void ShowcbbCustomer()
        {
            try
            {
                DataSet rs = customer.getAll("customer");
                cbbTenKH.DataSource = rs.Tables["customer"];
                cbbTenKH.DisplayMember = "TenKH";
                cbbTenKH.ValueMember = "MaKH";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERR : " + ex.Message);
            }
        }
        private void showDataFood()
        {
            try
            {
                DataSet rs = food.getAll("foods");
                dgvFood.DataSource = rs.Tables["foods"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void showDataFoodBill()
        {
            try
            {
                String MaHD = txtMaHD.Text;
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaHD", MaHD));
                DataSet rs = sell.getFoodbyHD("bill", data);
                dgvBill.DataSource = rs.Tables["bill"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void clearText()
        {
            txtMaMon.Text = "";
            txtTenMon.Text = "";
            txtDonGia.Text = "0";
            numSoLuong.Value = 0;
            txtSearch.Text = "";
        }
        private void getinfoBill()
        {
            try
            {
                DataSet rs = new DataSet();
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaHD", MaHD));
                rs = sell.getInfoByHD("HD", data);
                lbQty.Text = String.Format("{0:#,###,###,###,###}", Convert.ToInt64(rs.Tables["HD"].Rows[0]["TongTien"].ToString()));
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void pnSell_Load(object sender, EventArgs e)
        {
            clearText();
            ShowcbbEmployees();
            ShowcbbCustomer();
            cbbTenKH.SelectedIndex = 1;
            cbbTenNV.SelectedIndex = 1;
            
            txtNgayHD.Text = DateTime.Today.ToString("MM/dd/yyyy");
        }

        private void cbbTenNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String MaNV = cbbTenNV.SelectedValue.ToString();
                lbMaNV.Text = MaNV;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbbTenKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String MaKH = cbbTenKH.SelectedValue.ToString();
                lbMaKH.Text = MaKH;
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaKH", MaKH));
                DataSet rs = customer.getByID("KH", data);
                txtSDT.Text = rs.Tables["KH"].Rows[0]["SDT"].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("cbbTenKH : " + ex.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                string MaNV = lbMaNV.Text;
                string MaKH = lbMaKH.Text;
                string NgayHD = txtNgayHD.Text;
                
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaNV", MaNV));
                data.Add(new SqlParameter("@MaKH", MaKH));
                data.Add(new SqlParameter("@NgayHD", NgayHD));
                int rs = (int)sell.insertData(data);

                String MaHD = sell.getMaHDMAX();
                this.MaHD1 = MaHD;
                txtMaHD.Text = MaHD;
                btnNew.Enabled = false;
                cbbTenKH.Enabled = false;
                cbbTenNV.Enabled = false;
                showDataFood();
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            if (idx >= 0)
            {
                clearText();
                txtMaMon.Text = dgvFood.Rows[idx].Cells["MaMon"].Value.ToString();
                txtTenMon.Text = dgvFood.Rows[idx].Cells["TenMon"].Value.ToString();
                txtDonGia.Text = dgvFood.Rows[idx].Cells["DonGia"].Value.ToString();
                cbbLoai.SelectedItem = dgvFood.Rows[idx].Cells["Loai"].Value.ToString().Equals("DoAn") ? "Đồ Ăn" : "Đồ Uống";
                btnAdd.Enabled = true;
                btnDel.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                String MaHD = txtMaHD.Text;
                String MaMon = txtMaMon.Text;
                int SoLuong = Convert.ToInt32(numSoLuong.Value) ;
                if(MaMon.Length <= 0 || SoLuong <= 0)
                {
                    MessageBox.Show("Giá Trị Không Hợp Lệ!");
                    return;
                }
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaHD", MaHD));
                data.Add(new SqlParameter("@MaMon", MaMon));
                data.Add(new SqlParameter("@SoLuong", SoLuong));
                int rs = (int)sell.insertFoodinMaHD(data);
                showDataFoodBill();
                getinfoBill();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            if (idx >= 0)
            {
                clearText();
                txtMaMon.Text = dgvBill.Rows[idx].Cells["MaMon"].Value.ToString();
                txtTenMon.Text = dgvBill.Rows[idx].Cells["TenMon"].Value.ToString();
                txtDonGia.Text = dgvBill.Rows[idx].Cells["DonGia"].Value.ToString();
                //cbbLoai.SelectedItem = dgvBill.Rows[idx].Cells["Loai"].Value.ToString().Equals("DoAn") ? "Đồ Ăn" : "Đồ Uống";
                btnAdd.Enabled = false;
                btnDel.Enabled = true;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                String MaHD = txtMaHD.Text;
                String MaMon = txtMaMon.Text;
                Console.WriteLine(MaHD + "-" + MaMon);
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaHD", MaHD));
                data.Add(new SqlParameter("@MaMon", MaMon));
                int rs = (int)sell.delFoodinMaHD(data);
                if (rs <= 0)
                {
                    MessageBox.Show("Không Thành Công !");
                }
                else
                {
                    showDataFoodBill();
                    getinfoBill();
                    clearText();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                String TenMon = txtSearch.Text.Trim();
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@TenMon", TenMon));
                DataSet rs = food.search("food", data);
                dgvFood.DataSource = rs.Tables["food"];
                cbbSearchLoai.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbbSearchLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String txtcbbLoai = cbbSearchLoai.SelectedItem.ToString();
                String Loai = txtcbbLoai.Equals("Tất Cả") ? "" : txtcbbLoai.Equals("Đồ Ăn") ? "DoAn" : "DoUong";
                DataSet rs = new DataSet();
                if (Loai.Equals(""))
                {
                    rs = food.getAll("food");
                }
                else
                {
                    List<SqlParameter> data = new List<SqlParameter>();
                    data.Add(new SqlParameter("@Loai", Loai));
                    rs = food.searchLoai("food", data);
                }
                
                dgvFood.DataSource = rs.Tables["food"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
