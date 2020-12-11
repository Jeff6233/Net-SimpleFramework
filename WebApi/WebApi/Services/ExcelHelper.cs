using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using WebApi.Data;

namespace WebApi.Services
{
    public class ExcelHelper
    {
        private IWorkbook workbook;
        public ExcelHelper(string fileName,Stream stream)
        {
            if (Path.GetExtension(fileName) == ".xls")
                workbook = new HSSFWorkbook(stream);
            else if (Path.GetExtension(fileName) == ".xlsx")
                workbook = new XSSFWorkbook(stream);
            else throw new ApplicationException("文件无效");
           
        }

        public List<T_InputVGM> ReadExcel(int beginRow=0)
        {
            List<T_InputVGM> t_InputVGMs = new List<T_InputVGM>();
            ISheet sheet= workbook.GetSheetAt(0);
            var headRow = sheet.GetRow(0);
            List<string> headers=headRow.Cells.Select(i => i.StringCellValue).ToList();
            for (int i = beginRow; i <= sheet.LastRowNum; i++)
            {
                
                var currentRow=sheet.GetRow(i);
                t_InputVGMs.Add(new T_InputVGM() { 
                    DoNo= "HD1908070001",
                    vgmCtnNo=this.GetCellValue(currentRow.Cells[headers.IndexOf("箱号")]).ToString(),
                    vgmCompany=this.GetCellValue(currentRow.Cells[headers.IndexOf("VGM负责方")]).ToString(),
                    vgmCtnType=this.GetCellValue(currentRow.Cells[headers.IndexOf("箱型")]).ToString(),
                    vgmWeight=decimal.Parse(this.GetCellValue(currentRow.Cells[headers.IndexOf("VGM重量")]).ToString()),
                    vgmDirector=this.GetCellValue(currentRow.Cells[headers.IndexOf("负责人签名")]).ToString(),
                    vgmEmail=this.GetCellValue(currentRow.Cells[headers.IndexOf("称重地点")]).ToString(),
                    vgmPhone=this.GetCellValue(currentRow.Cells[headers.IndexOf("称重时间")]).ToString(),
                    vgmMethod=this.GetCellValue(currentRow.Cells[headers.IndexOf("称重方式")]).ToString(),
                    vgmSealNo=this.GetCellValue(currentRow.Cells[headers.IndexOf("封号")]).ToString(),
                });
            }
            return t_InputVGMs;
        }

        private object GetCellValue(ICell cell)
        {
            if (cell.CellType == CellType.String)
                return cell.StringCellValue;
            if (cell.CellType == CellType.Numeric)
                return cell.NumericCellValue;
            if (cell.CellType == CellType.Formula)
                return cell.CellFormula;
            if (cell.CellType == CellType.Boolean)
                return cell.BooleanCellValue;
            if(cell.CellType==CellType.Error)
                return cell.ErrorCellValue;
            if (cell.CellType == CellType.Blank)
                return null;
            if (cell.CellType == CellType.Unknown)
                return cell.StringCellValue;
            throw new ApplicationException("未找到值");
        }
    }
}