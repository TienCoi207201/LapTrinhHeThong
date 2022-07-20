using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        //Kiểm tra form con có đang hiển thị trong from cha ko
        private bool CheckExitsForm(string name)
        {
            bool check = false;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == name)
                {
                    check = true;
                    break;
                }
            }
            return check;
        }

        //Kiểm tra from con có đang được mở trong form cha k nếu đc mở sẽ kích hoạt lại bằn (Activate())
        private void ActiveChildForm(string name)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == name)
                {
                    frm.Activate();
                    break;
                }
            }
        }


        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExitsForm("Quanlynhanvien"))
            {
                Quanlynhanvien frm = new Quanlynhanvien();
                frm.MdiParent = this;
                frm.Name = "Quanlynhanvien";
                frm.Show();
            }
            else
            {
                ActiveChildForm("Quanlynhanvien");
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
        }

        private void chươngTrìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExitsForm("QuanLyKhachHang"))
            {
                QuanLyKhachHang frm = new QuanLyKhachHang();
                frm.MdiParent = this;
                frm.Name = "QuanLyKhachHang";
                frm.Show();
            }
            else
            {
                ActiveChildForm("QuanLyKhachHang"); 
            }

        }
    }
}
