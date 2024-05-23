using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Response.Task;
using TaskManagement.Domain.Entity.Tasks;

namespace TaskManagement.Infrastructure.Repos;

public class PdfRepository:IPdf
{
    public Task<byte[]> ExportTasksToPdfAsync(List<GetTaskDTO> tasks)
    {
        using var ms = new MemoryStream();
        var writer = new PdfWriter(ms);
        var pdf = new PdfDocument(writer);
        var document = new Document(pdf);

        document.Add(new Paragraph("Liste des Tâches").SetFontSize(18));

        var table = new Table(new float[] { 1, 2, 3 }).UseAllAvailableWidth();
        table.AddHeaderCell("ID");
        table.AddHeaderCell("Title");
        table.AddHeaderCell("Title");

        foreach (var task in tasks)
        {
            table.AddCell(task.Id.ToString());
            table.AddCell(task.Title);
            table.AddCell(task.Title);
            
        }

        document.Add(table);
        document.Close();
        writer.Close();

        return Task.FromResult(ms.ToArray());
    }
    
    
}