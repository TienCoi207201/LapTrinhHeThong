using System;
using System.Drawing;
using System.Windows.Forms;
using YuTang_App.Src.Panel;
using YuTang_NET;

namespace YuTang_App
{
    public partial class App : Form
    {
        Button currentButton;
        Form activeForm;
        String role;

        public string Role { get => role; set => role = value; }

        public App()
        {
            InitializeComponent();
        }
        private void ActivateButton(object btnSender)
        {
            try
            {
                if (btnSender != null)
                {
                    if (currentButton != (Button)btnSender)
                    {
                        DisableButton();
                        currentButton = (Button)btnSender;
                        currentButton.BackColor = Color.LightSkyBlue;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void DisableButton()
        {
            foreach (Control btn in panelMenu.Controls)
            {
                if(btn.GetType() == typeof(Button))
                {
                    btn.BackColor = Color.RoyalBlue;
                }
            }
        }
        private void OpenChildFrom(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelController.Controls.Add(childForm);
            this.panelController.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void btnSell_Click(object sender, EventArgs e)
        {
            OpenChildFrom(new pnSell(), sender);
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            OpenChildFrom(new pnBill(), sender);
        }

        private void btnFood_Click(object sender, EventArgs e)
        {
            OpenChildFrom(new pnFood(), sender);
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            OpenChildFrom(new pnCustomer(), sender);
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            OpenChildFrom(new pnEmployees(), sender);
        }

        private void btnWarehouse_Click(object sender, EventArgs e)
        {
            OpenChildFrom(new pnWarehouse(), sender);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            OpenChildFrom(new pnReport(), sender);
        }

        private void App_Load(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            DialogResult rs = frm.ShowDialog();
            if (rs != DialogResult.OK)
            {
                Application.Exit();
                return;
            }
            Role = frm.Role;
            AAA();
            OpenChildFrom(new pnHome(), null);
        }
        private void AAA()
        {
            try
            {
                if (!Role.Equals("admin"))
                {
                    btnStaff.Dispose();
                    btnWarehouse.Dispose();
                    btnReport.Dispose();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

    }
}
