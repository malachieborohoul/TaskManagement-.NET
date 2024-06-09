using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using TaskManagement.Domain.DTOs.Response.Task;

namespace TaskManagement.Application.Services.Pdf
{
    public class PdfService:IPdfService
    {
        public Task<byte[]> ExportTasksToPdfAsync(List<GetTaskDTO> tasks)
        {
            using var ms = new MemoryStream();
            var writer = new PdfWriter(ms);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            document.Add(new Paragraph("Liste des Tâches").SetFontSize(18));

            var table = new Table(new float[] { 1, 2, 3 }).UseAllAvailableWidth();
            table.AddHeaderCell("Title");
            table.AddHeaderCell("Priority");
            table.AddHeaderCell("Status");
            table.AddHeaderCell("Added By");
            table.AddHeaderCell("Create At");
            table.AddHeaderCell("Due Date");

            foreach (var task in tasks)
            {
                table.AddCell(task.Title);
                table.AddCell(task.Priority.Name);
                table.AddCell(task.Status.Name);
                table.AddCell(task.User.Name);
                table.AddCell(task.CreatedAt.ToShortDateString());
                table.AddCell(task.DueDate.ToShortDateString());
           
            
            }

            document.Add(table);
            document.Close();
            writer.Close();

            return Task.FromResult(ms.ToArray());
        }
    
        public async Task<byte[]> ConvertHtmlToPdfAsync(string htmlContent)
        {
            using (var stream = new MemoryStream())
            {
                /*
                ConverterProperties converterProperties = new ConverterProperties();
                converterProperties.SetBaseUri("file:///" + Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName);*/
                HtmlConverter.ConvertToPdf(htmlContent, stream);
                return stream.ToArray();
            }
        }
    
    
    }
}