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
using YuTang_App.Src.Test;
using YuTang_App.Src.Model;
using YuTang_App.Src.Controller;


namespace YuTang_App.Src.Panel
{
    public partial class pnReport : Form
    {
        String tabName;
        ExportExcel excel = new ExportExcel();
        ReportController report = new ReportController();
        public pnReport()
        {
            InitializeComponent();
        }

        public string TabName { get => tabName; set => tabName = value; }
        private void initialCbb()
        {
            int MM = Convert.ToInt32(DateTime.Today.ToString("MM"));
            List<Month> str = new List<Month>();
            for(int i = MM; i >= 1; i--)
            {
                str.Add(new Month(("Tháng " + i) , i));
            }
            cbbMonth_DT.DataSource = str;
            cbbMonth_DT.DisplayMember = "MM";
            cbbMonth_DT.ValueMember = "value";
            cbbMonth_DT.SelectedIndex = 0;

            cbbMonth_F.DataSource = str;
            cbbMonth_F.DisplayMember = "MM";
            cbbMonth_F.ValueMember = "value";
            cbbMonth_F.SelectedIndex = 0;

            cbbMonth_NV.DataSource = str;
            cbbMonth_NV.DisplayMember = "MM";
            cbbMonth_NV.ValueMember = "value";
            cbbMonth_NV.SelectedIndex = 0;
        }
        private void setColHeaderDT(DataGridView dgv, int index)
        {
            try
            {
                List<String> colHeader = new List<string>();
                if(index == 2)
                {
                    colHeader.Add("Tháng");
                    colHeader.Add("Số Hóa Đơn");
                    colHeader.Add("Doanh Thu");
                }
                else
                {
                    colHeader.Add("Mã Hóa Đơn");
                    colHeader.Add("Tên Nhân Viên");
                    colHeader.Add("Tên Khách Hàng");
                    colHeader.Add("Ngày Hóa Đơn");
                    colHeader.Add("Tổng Tiền");
                }
                for(int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].HeaderText = colHeader[i];
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadDataDT(int cbbSelectedIndex)
        {
            try
            {
                List<SqlParameter> data = new List<SqlParameter>();
                DataSet rs = new DataSet();
                if (cbbSelectedIndex == 0)
                {
                    dgvDoanhThu.Columns.Clear();
                    int month = Convert.ToInt32(cbbMonth_DT.SelectedValue.ToString());
                    data.Add(new SqlParameter("@_Month", month));
                    rs = report.DT_month("rp", data);
                    if(rs != null)
                    {
                        long total = 0;
                        dgvDoanhThu.DataSource = rs.Tables[0];
                        for (int i = 0; i < rs.Tables[0].Rows.Count; i++)
                        {
                            total += Convert.ToInt64(rs.Tables[0].Rows[i]["TongTien"].ToString());
                        }
                        lbTotal_DT.Text = String.Format("{0:#,###,###,###,###}", total);

                    }
                    setColHeaderDT(dgvDoanhThu, 0);
                }
                else if (cbbSelectedIndex == 1)
                {
                    dgvDoanhThu.Columns.Clear();
                    String NgayBD = dtStart_DT.Value.ToString("MM/dd/yyyy");
                    String NgayKT = dtEnd_DT.Value.ToString("MM/dd/yyyy");
                    data.Add(new SqlParameter("@NgayBD", NgayBD));
                    data.Add(new SqlParameter("@NgayKT", NgayKT));
                    rs = report.DT_date("rp", data);
                    dgvDoanhThu.DataSource = rs.Tables[0];
                    setColHeaderDT(dgvDoanhThu, 1);
                }
                else
                {
                    dgvDoanhThu.Columns.Clear();
                    String Year = Convert.ToString(DateTime.Today.ToString("yyyy"));
                    data.Add(new SqlParameter("@_Year", Year));
                    rs = report.DT_Year("rp", data);
                    dgvDoanhThu.DataSource = rs.Tables[0];
                    setColHeaderDT(dgvDoanhThu, 2);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void pnReport_Load(object sender, EventArgs e)
        {
            initialCbb();
            cbbLoai_DT.SelectedIndex = 0;
            cbbLoai_F.SelectedIndex = 0;
            cbbLoai_NV.SelectedIndex = 0;
            loadDataDT(cbbLoai_DT.SelectedIndex);
            loadDataF(cbbLoai_F.SelectedIndex);
            loadDataNV(cbbLoai_NV.SelectedIndex);
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabName = (sender as TabControl).SelectedTab.Name;
            Console.WriteLine("TabName : {0}", tabName);
        }


        private void btnExcelDT_Click(object sender, EventArgs e)
        {
            try
            {

                List<String> ValueName = new List<string>();
                DataTable dataTable = (DataTable)(dgvDoanhThu.DataSource);
                DataSet rs = new DataSet();
                rs.Merge(dataTable);
                dgvDoanhThu.DataSource = rs.Tables[0];
                for (int i = 0; i < dgvDoanhThu.Columns.Count; i++)
                {
                    ValueName.Add(dgvDoanhThu.Columns[i].HeaderText);
                }

                excel.CreateExcelFile(ValueName, rs);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbbDTLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(cbbLoai_DT.SelectedItem.ToString().Equals("Theo Tháng"))
                {
                    cbbMonth_DT.Show();
                    dtStart_DT.Hide();
                    dtEnd_DT.Hide();
                    lbDT1.Hide();
                    lbDT2.Hide();
                    btnChart_DT.Hide();
                    
                }
                else if(cbbLoai_DT.SelectedItem.ToString().Equals("Tùy Chọn"))
                {
                    cbbMonth_DT.Hide();
                    dtStart_DT.Show();
                    dtEnd_DT.Show();
                    lbDT1.Show();
                    lbDT2.Show();
                    btnChart_DT.Hide();
                }
                else
                {
                    cbbMonth_DT.Hide();
                    dtStart_DT.Hide();
                    dtEnd_DT.Hide();
                    lbDT1.Hide();
                    lbDT2.Hide();
                    btnChart_DT.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnRef_DT_Click(object sender, EventArgs e)
        {
            try
            {
                loadDataDT(cbbLoai_DT.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChartDT_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = (DataTable)(dgvDoanhThu.DataSource);
                DataSet ds = new DataSet();
                ds.Merge(data);
                List<String> headerText = new List<string>();
                for (int i = 0; i < dgvDoanhThu.Columns.Count; i++)
                {
                    headerText.Add(dgvDoanhThu.Columns[i].HeaderText);
                }
                foreach (string item in headerText)
                    Console.WriteLine(item);
                List<String> value1 = new List<string>();
                value1.Add("Số Hóa Đơn");
                value1.Add("Thang");
                value1.Add("SoHoaDon");
                List<String> value2 = new List<string>();
                value2.Add("Doanh Thu");
                value2.Add("Thang");
                value2.Add("DoanhThu");
                pnChart.pnChart frmChart = new pnChart.pnChart(ds, value1, value2);
                frmChart.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // Food
        private void setColHeaderF(DataGridView dgv, int index)
        {
            try
            {
                List<String> colHeader = new List<string>();
                if (index == 0 || index == 1 || index == 2)
                {
                    colHeader.Add("Mã Món");
                    colHeader.Add("Tên Món");
                    colHeader.Add("Số Hóa Đơn");
                    colHeader.Add("Số Lượng");
                    colHeader.Add("Tổng Tiền");
                }
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].HeaderText = colHeader[i];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadDataF(int cbbSelectedIndex)
        {
            try
            {
                List<SqlParameter> data = new List<SqlParameter>();
                DataSet rs = new DataSet();
                if (cbbSelectedIndex == 0)
                {
                    dgvFood.Columns.Clear();
                    int month = Convert.ToInt32(cbbMonth_F.SelectedValue.ToString());
                    data.Add(new SqlParameter("@_Month", month));
                    rs = report.F_month("rp", data);
                    if (rs != null)
                    {
                        long total = 0;
                        dgvFood.DataSource = rs.Tables[0];
                        
                    }
                    setColHeaderF(dgvFood, 0);
                }
                else if (cbbSelectedIndex == 1)
                {
                    dgvFood.Columns.Clear();
                    String NgayBD = dtStart_F.Value.ToString("MM/dd/yyyy");
                    String NgayKT = dtEnd_F.Value.ToString("MM/dd/yyyy");
                    data.Add(new SqlParameter("@NgayBD", NgayBD));
                    data.Add(new SqlParameter("@NgayKT", NgayKT));
                    rs = report.F_date("rp", data);
                    dgvFood.DataSource = rs.Tables[0];
                    setColHeaderF(dgvFood, 1);
                }
                else
                {
                    dgvFood.Columns.Clear();
                    String Year = Convert.ToString(DateTime.Today.ToString("yyyy"));
                    data.Add(new SqlParameter("@_Year", Year));
                    rs = report.F_Year("rp", data);
                    dgvFood.DataSource = rs.Tables[0];
                    setColHeaderF(dgvFood, 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRef_F_Click(object sender, EventArgs e)
        {
            try
            {
                loadDataF(cbbLoai_F.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbbLoai_F_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbLoai_F.SelectedItem.ToString().Equals("Theo Tháng"))
                {
                    cbbMonth_F.Show();
                    dtStart_F.Hide();
                    dtEnd_F.Hide();
                    lbF1.Hide();
                    lbF2.Hide();
                    btnChart_F.Show();

                }
                else if (cbbLoai_F.SelectedItem.ToString().Equals("Tùy Chọn"))
                {
                    cbbMonth_F.Hide();
                    dtStart_F.Show();
                    dtEnd_F.Show();
                    lbF1.Show();
                    lbF2.Show();
                    btnChart_F.Show();
                }
                else
                {
                    cbbMonth_F.Hide();
                    dtStart_F.Hide();
                    dtEnd_F.Hide();
                    lbF1.Hide();
                    lbF2.Hide();
                    btnChart_F.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExcel_F_Click(object sender, EventArgs e)
        {
            try
            {

                List<String> ValueName = new List<string>();
                DataTable dataTable = (DataTable)(dgvFood.DataSource);
                DataSet rs = new DataSet();
                rs.Merge(dataTable);
                dgvFood.DataSource = rs.Tables[0];
                for (int i = 0; i < dgvFood.Columns.Count; i++)
                {
                    ValueName.Add(dgvFood.Columns[i].HeaderText);
                }

                excel.CreateExcelFile(ValueName, rs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChart_F_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = (DataTable)(dgvFood.DataSource);
                DataSet ds = new DataSet();
                ds.Merge(data);
                List<String> headerText = new List<string>();
                for (int i = 0; i < dgvFood.Columns.Count; i++)
                {
                    headerText.Add(dgvFood.Columns[i].HeaderText);
                }
                foreach (string item in headerText)
                    Console.WriteLine(item);
                List<String> value1 = new List<string>();
                value1.Add("Số Hóa Đơn");
                value1.Add("TenMon");
                value1.Add("SoHD");
                List<String> value2 = new List<string>();
                value2.Add("Số Lượn Đã Bán");
                value2.Add("TenMon");
                value2.Add("SoLuong");
                pnChart.pnChart frmChart = new pnChart.pnChart(ds, value1, value2);
                frmChart.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbbLoai_NV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbLoai_NV.SelectedItem.ToString().Equals("Theo Tháng"))
                {
                    cbbMonth_NV.Show();
                    dtStart_NV.Hide();
                    dtEnd_NV.Hide();
                    lbNV1.Hide();
                    lbNV2.Hide();
                    btnChart_NV.Show();

                }
                else if (cbbLoai_NV.SelectedItem.ToString().Equals("Tùy Chọn"))
                {
                    cbbMonth_NV.Hide();
                    dtStart_NV.Show();
                    dtEnd_NV.Show();
                    lbNV1.Show();
                    lbNV2.Show();
                    btnChart_NV.Show();
                }
                else
                {
                    cbbMonth_NV.Hide();
                    dtStart_NV.Hide();
                    dtEnd_NV.Hide();
                    lbNV1.Hide();
                    lbNV2.Hide();
                    btnChart_NV.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRef_NV_Click(object sender, EventArgs e)
        {
            try
            {
                loadDataNV(cbbLoai_NV.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void setColHeaderNV(DataGridView dgv, int index)
        {
            try
            {
                List<String> colHeader = new List<string>();
                if (index == 0 || index == 1 || index == 2)
                {
                    colHeader.Add("Mã NV");
                    colHeader.Add("Tên NV");
                    colHeader.Add("Số Hóa Đơn");
                    colHeader.Add("Tổng Tiền");
                }
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].HeaderText = colHeader[i];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadDataNV(int cbbSelectedIndex)
        {
            try
            {
                List<SqlParameter> data = new List<SqlParameter>();
                DataSet rs = new DataSet();
                if (cbbSelectedIndex == 0)
                {
                    dgvDoanhThu.Columns.Clear();
                    int month = Convert.ToInt32(cbbMonth_NV.SelectedValue.ToString());
                    data.Add(new SqlParameter("@_Month", month));
                    rs = report.NV_month("rp", data);
                    if (rs != null)
                    {
                        dgvNV.DataSource = rs.Tables[0];
                    }
                    setColHeaderNV(dgvNV, 0);
                }
                else if (cbbSelectedIndex == 1)
                {
                    dgvDoanhThu.Columns.Clear();
                    String NgayBD = dtStart_NV.Value.ToString("MM/dd/yyyy");
                    String NgayKT = dtEnd_NV.Value.ToString("MM/dd/yyyy");
                    data.Add(new SqlParameter("@NgayBD", NgayBD));
                    data.Add(new SqlParameter("@NgayKT", NgayKT));
                    rs = report.NV_date("rp", data);
                    dgvNV.DataSource = rs.Tables[0];
                    setColHeaderNV(dgvNV, 1);
                }
                else
                {
                    dgvDoanhThu.Columns.Clear();
                    String Year = Convert.ToString(DateTime.Today.ToString("yyyy"));
                    data.Add(new SqlParameter("@_Year", Year));
                    rs = report.NV_Year("rp", data);
                    dgvNV.DataSource = rs.Tables[0];
                    setColHeaderNV(dgvNV, 2);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExcel_NV_Click(object sender, EventArgs e)
        {
            try
            {

                List<String> ValueName = new List<string>();
                DataTable dataTable = (DataTable)(dgvNV.DataSource);
                DataSet rs = new DataSet();
                rs.Merge(dataTable);
                dgvNV.DataSource = rs.Tables[0];
                for (int i = 0; i < dgvNV.Columns.Count; i++)
                {
                    ValueName.Add(dgvNV.Columns[i].HeaderText);
                }

                excel.CreateExcelFile(ValueName, rs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChart_NV_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = (DataTable)(dgvNV.DataSource);
                DataSet ds = new DataSet();
                ds.Merge(data);
                List<String> headerText = new List<string>();
                for (int i = 0; i < dgvNV.Columns.Count; i++)
                {
                    headerText.Add(dgvNV.Columns[i].HeaderText);
                }
                foreach (string item in headerText)
                    Console.WriteLine(item);
                List<String> value1 = new List<string>();
                value1.Add("Số Hóa Đơn");
                value1.Add("TenNV");
                value1.Add("SoHD");
                List<String> value2 = new List<string>();
                value2.Add("Doanh Thu");
                value2.Add("TenNV");
                value2.Add("ThanhTien");
                pnChart.pnChart frmChart = new pnChart.pnChart(ds, value1, value2);
                frmChart.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
