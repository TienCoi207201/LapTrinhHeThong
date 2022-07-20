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
    public partial class pnPay : Form
    {
        String _MaHD;
        BillController bill = new BillController();
        public pnPay()
        {
            InitializeComponent();
        }
        public pnPay(String MaHD)
        {
            InitializeComponent();
            this.MaHD = MaHD;
        }

        public string MaHD { get => _MaHD; set => _MaHD = value; }
        private void getFood()
        {
            try
            {
                DataSet rs = new DataSet();
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaHD", MaHD));
                rs = bill.getFoodByMaHD("bill", data);
                dgv.DataSource = rs.Tables["bill"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void getText()
        {
            try
            {
                DataSet rs = new DataSet();
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaHD", MaHD));
                rs = bill.getInfoByMaHD("bill", data);


                lbTenKH.Text = rs.Tables["bill"].Rows[0]["TenKH"].ToString();
                lbTenNV.Text = rs.Tables["bill"].Rows[0]["TenNV"].ToString();
                lbMaNV.Text = rs.Tables["bill"].Rows[0]["MaNV"].ToString();
                lbSDT.Text = rs.Tables["bill"].Rows[0]["SDT"].ToString();
                lbNgayHD.Text = DateTime.Parse(rs.Tables["bill"].Rows[0]["NgayHD"].ToString()).ToString("MM-dd-yyyy");
                lbTongTien.Text = String.Format("{0:#,###,###,###,###}", Convert.ToInt64(rs.Tables["bill"].Rows[0]["TongTien"].ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void pnPay_Load(object sender, EventArgs e)
        {
            getFood();
            getText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = MessageBox.Show("Bạn Chắc Chắn Muốn Thanh Toán ?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    return;
                }
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@MaHD", MaHD));
                int rs = (int)bill.paymentByMaHD(data);
                if(rs <= 0)
                {
                    MessageBox.Show("Có Lỗi Xảy Ra !!!");
                    DialogResult resErr = MessageBox.Show("Are you sure you want to Close", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (resErr == DialogResult.Cancel)
                    {
                        return;
                    }
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    MessageBox.Show("Thanh Toán Thành Công !!!");
                    DialogResult = DialogResult.OK;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
