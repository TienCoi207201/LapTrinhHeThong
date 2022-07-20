using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuTang_App.Src.Test
{
    class ExportExcel
    {
        public void CreateExcelFile(List<String> ValueName ,DataSet data)
        {
            string filePath = "";
            // create SaveFileDialog save filePath excel
            SaveFileDialog dialog = new SaveFileDialog();

            // only file Excel
            dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

            // open dialog + save filePath
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
            }

            //filePath null -> return ;
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                return;
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage p = new ExcelPackage())
                {
                    // Author
                    p.Workbook.Properties.Author = "ndlong";

                    // Title
                    p.Workbook.Properties.Title = "Báo cáo thống kê";

                    //Create sheet
                    p.Workbook.Worksheets.Add("Thống kê");

                    // work on sheet
                    ExcelWorksheet ws = p.Workbook.Worksheets["Thống kê"];

                    // create sheet name
                    ws.Name = "Thống kê";
                    // fontsize on sheet
                    ws.Cells.Style.Font.Size = 11;
                    // font family
                    ws.Cells.Style.Font.Name = "Calibri";
                    // write data in excel
                    BindingFormatForExcel(ws, ValueName , data);
                    //save file
                    Byte[] bin = p.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);
                }
                MessageBox.Show("Xuất excel thành công!");
            }
            catch (Exception EE)
            {
                MessageBox.Show("Có lỗi khi lưu file!" + EE.Message);
            }
        }
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<String> ValueName , DataSet data)
        {
            try
            {
                // Set default width cho tất cả column
                worksheet.DefaultColWidth = 20;
                
                // Tự động xuống hàng khi text quá dài
                worksheet.Cells.Style.WrapText = true;
                // Tạo header
                for (int i = 0; i < ValueName.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = ValueName[i];
                }


                // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
                using (var range = worksheet.Cells["A1:" + ((char)(ValueName.Count + 65 - 1)).ToString() + "1"])
                {
                    // Set PatternType
                    range.Style.Fill.PatternType = ExcelFillStyle.DarkGray;
                    // Set Màu cho Background
                    range.Style.Fill.BackgroundColor.SetColor(Color.Aqua);
                    // Canh giữa cho các text
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    // Set Font cho text  trong Range hiện tại
                    range.Style.Font.SetFromFont(new Font("Arial", 10));
                    // Set Border
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                    // Set màu ch Border
                    range.Style.Border.Bottom.Color.SetColor(Color.Blue);
                }

                // Đỗ dữ liệu từ list vào 
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    var item = data.Tables[0].Rows[i];
                    for (int idx = 0; idx < ValueName.Count; idx++)
                    {
                        worksheet.Cells[i + 2, idx + 1].Value = item[idx].ToString();
                    }
                }
                worksheet.Cells.AutoFitColumns();
            }
            catch(Exception ex)
            {
                throw;
            }
            // Format lại định dạng xuất ra ở cột Money
            //worksheet.Cells[2, 4, listItems.Count + 4, 4].Style.Numberformat.Format = "$#,##.00";


            // Thực hiện tính theo formula trong excel
            // Hàm Sum 
            //worksheet.Cells[listItems.Count + 3, 3].Value = "Total is :";
            //worksheet.Cells[listItems.Count + 3, 4].Formula = "SUM(D2:D" + (listItems.Count + 1) + ")";
            // Tinh theo %
            //worksheet.Cells[listItems.Count + 5, 3].Value = "Percentatge: ";
            //worksheet.Cells[listItems.Count + 5, 4].Style.Numberformat.Format = "0.00%";
            // Dòng này có nghĩa là ở column hiện tại lấy với địa chỉ (Row hiện tại - 1)/ (Row hiện tại - 2) Cùng một colum
            //worksheet.Cells[listItems.Count + 5, 4].FormulaR1C1 = "(R[-1]C/R[-2]C)";
        }
    }
}
