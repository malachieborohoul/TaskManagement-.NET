using OfficeOpenXml;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Response.Task;

namespace TaskManagement.Infrastructure.Repos;

public class ExcelRepository:IExcel
{
    public async Task<byte[]> ExportTasksToExcelAsync(List<GetTaskDTO> tasks)
    {
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Tâches");

            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "Title";

            for (int i = 0; i < tasks.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = tasks[i].Id;
                worksheet.Cells[i + 2, 2].Value = tasks[i].Title;
            }

            return await Task.FromResult(package.GetAsByteArray());
        }
    }
}