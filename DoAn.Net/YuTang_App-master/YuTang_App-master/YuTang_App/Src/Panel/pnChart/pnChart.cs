using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuTang_App.Src.Panel.pnChart
{
    public partial class pnChart : Form
    {
        DataSet _data;
        List<String> _ValueName;
        List<String> _ValueNamePie;
        public DataSet Data { get => _data; set => _data = value; }
        public List<string> ValueName { get => _ValueName; set => _ValueName = value; }
        public List<string> ValueNamePie { get => _ValueNamePie; set => _ValueNamePie = value; }

        public pnChart()
        {
            InitializeComponent();
        }
        public pnChart(DataSet _data, List<String>_ValueNameCol, List<String> _ValueNamePie)
        {
            InitializeComponent();
            Data = _data;
            ValueName = _ValueNameCol;
            ValueNamePie = _ValueNamePie;
        }

        
        private void loadChartCol()
        {
            try
            {
                chart1.DataSource = Data.Tables[0];
                chart1.Series[0].Name = ValueName[0];
                chart1.Series[0].XValueMember = ValueName[1];
                chart1.Series[0].YValueMembers = ValueName[2];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadChartPie()
        {
            try
            {
                //chart2.Series[0].Points.Clear();
                int sum = 0;
                chart2.Titles[0].Text = ValueNamePie[0];
                for (int i = 0; i < Data.Tables[0].Rows.Count; i++)
                {
                    sum += Convert.ToInt32(Data.Tables[0].Rows[i][ValueNamePie[2]].ToString());
                }
                Console.WriteLine(sum);
                for (int i = 0; i < Data.Tables[0].Rows.Count; i++)
                {
                    chart2.Series[0].Points.AddXY(Data.Tables[0].Rows[i][ValueNamePie[1]], Data.Tables[0].Rows[i][ValueNamePie[2]]);
                    String lbPoint = Math.Round(Convert.ToDouble(Data.Tables[0].Rows[i][ValueNamePie[2]].ToString()) * 100 / sum, 2) + "%";
                    chart2.Series[0].Points[i].Label = lbPoint;
                    chart2.Series[0].Points[i].LegendText = (string)Data.Tables[0].Rows[i][ValueNamePie[1]];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void pnChart_Load(object sender, EventArgs e)
        {
            loadChartCol();
            loadChartPie();
        }
    }
    
}
