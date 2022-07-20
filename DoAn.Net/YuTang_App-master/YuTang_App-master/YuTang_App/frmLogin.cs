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
using YuTang_App.Src.Database;

namespace YuTang_NET
{
    public partial class frmLogin : Form
    {
        dbConnect conn = new dbConnect();
        String role;
        public frmLogin()
        {
            InitializeComponent();
        }

        public string Role { get => role; set => role = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String tk = txtUser.Text;
                string mk = txtPass.Text;
                if(tk.Trim().Length == 0 || mk.Trim().Length == 0)
                {
                    MessageBox.Show("Tài Khoản hoặc Mật Khẩu không được bỏ trông!");
                    return;
                }
                string sql = "select count(*) from nguoidung where taikhoan = @tk and matkhau = @mk";
                List<SqlParameter> data = new List<SqlParameter>();
                data.Add(new SqlParameter("@tk", tk));
                data.Add(new SqlParameter("@mk", mk));
                int rs = (int)conn.CountData(sql, data);

                string sql2 = "select * from nguoidung where taikhoan = @tk and matkhau = @mk";

                List<SqlParameter> data2 = new List<SqlParameter>();
                data2.Add(new SqlParameter("@tk", tk));
                data2.Add(new SqlParameter("@mk", mk));
                if (rs == 1)
                {
                    DataSet d = conn.getData(sql2, "rs", data2);
                    string r = d.Tables[0].Rows[0]["accRole"].ToString();
                    Role = r;
                    MessageBox.Show("Đăng Nhập thành công !");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Đăng Nhập thất bại !");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERR : " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
