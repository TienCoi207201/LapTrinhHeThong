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
    
    public partial class Form1 : Form
    {
        string conn_str = "Data Source=LAPTOP-TTTBNCOH;Initial Catalog=QuanLyNhaHang;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            try
            {
                int rs = 0;
                string tk = txtuser.Text;
                string mk = txtpass.Text;
                string sql = "select count(*) from nguoidung where tk=@tk and mk=@mk";
                SqlConnection conn = new SqlConnection(conn_str);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@tk", tk));
                cmd.Parameters.Add(new SqlParameter("@mk", mk));
                rs = (int)cmd.ExecuteScalar();
                conn.Close();

                if (rs == 1)
                {
                    DialogResult = DialogResult.OK;
                    MessageBox.Show("Đăng nhập thành công!!");
                    Main fm = new Main();
                    fm.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("Đăng nhập thất bại");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
