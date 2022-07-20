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
    public partial class pnWarehouse : Form
    {
        WarehouseController warehouse = new WarehouseController();
        public pnWarehouse()
        {
            InitializeComponent();
        }
        private void getData()
        {
            try
            {
                DataSet rs = warehouse.getAll("warehouse");
                dgvWarehouse.DataSource = rs.Tables["warehouse"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void clearText(Boolean check)
        {
            txtMaNL.Text = "";
            txtTenNL.Text = "";
            txtNCC.Text = "";
            numSoLuongTon.Value = 0;
            
            btnAdd.Enabled = check;
            btnEdit.Enabled = !check;
            btnDel.Enabled = !check;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string TenNL = txtTimKiem.Text.Trim();
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@TenNL", TenNL));
                DataSet rs = warehouse.search("warehouse", data);
                dgvWarehouse.DataSource = rs.Tables["warehouse"];
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
                String TenNL = txtTenNL.Text.Trim();
                String NCC = txtNCC.Text.Trim();
                int SoLuongTon = Convert.ToInt32( numSoLuongTon.Value);
                if (TenNL.Length <= 0 || SoLuongTon <= 0 || NCC.Length <= 0)
                {
                    MessageBox.Show("Giá Trị Không Hợp Lệ !");
                    return;
                }
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@TenNL", TenNL));
                data.Add(new SqlParameter("@NCC", NCC));
                data.Add(new SqlParameter("@SoLuongTon", SoLuongTon));
                int rs = warehouse.insertData(data);
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                String MaNL = txtMaNL.Text.Trim();
                String TenNL = txtTenNL.Text.Trim();
                String NCC = txtNCC.Text.Trim();
                int SoLuongTon = Convert.ToInt32(numSoLuongTon.Value);
                if (TenNL.Length <= 0 || SoLuongTon <= 0 || NCC.Length <= 0)
                {
                    MessageBox.Show("Giá Trị Không Hợp Lệ !");
                    return;
                }
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaNL", MaNL));
                data.Add(new SqlParameter("@TenNL", TenNL));
                data.Add(new SqlParameter("@NCC", NCC));
                data.Add(new SqlParameter("@SoLuongTon", SoLuongTon));
                int rs = warehouse.updateData(data);
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
                String MaNL = txtMaNL.Text.Trim();
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaNL", MaNL));

                int rs = warehouse.deleteData(data);
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            getData();
            clearText(true);
        }

        private void pnWarehouse_Load(object sender, EventArgs e)
        {
            getData();
            clearText(true);
        }

        private void dgvWarehouse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            if (idx >= 0)
            {
                clearText(false);
                txtMaNL.Text = dgvWarehouse.Rows[idx].Cells["MaNL"].Value.ToString();
                txtTenNL.Text = dgvWarehouse.Rows[idx].Cells["TenNL"].Value.ToString();
                txtNCC.Text = dgvWarehouse.Rows[idx].Cells["NCC"].Value.ToString();
                numSoLuongTon.Value = Convert.ToInt32(dgvWarehouse.Rows[idx].Cells["SoLuongTon"].Value.ToString());
                
            }
        }
    }
}
