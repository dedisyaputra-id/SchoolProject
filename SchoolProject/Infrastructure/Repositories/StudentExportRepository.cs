using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrastructure.Persistance;
using System.Runtime.CompilerServices;

namespace SchoolProject.Infrastructure.Repositories
{
    public class StudentExportRepository
    {
        private readonly AppDbContext _context;
        public StudentExportRepository(AppDbContext context)
        {
            _context = context;
        }

        public void ExportStudentToExcel(Guid tenantId)
        {
            try
            {
                var query = _context.Students
                .Where(x => x.TenantId == tenantId)
                .AsNoTracking();

                using var workbook = new XLWorkbook();
                var ws = workbook.Worksheets.Add("Students");

                ws.Cell(1, 1).Value = "Id";
                ws.Cell(1, 2).Value = "Name";
                ws.Cell(1, 3).Value = "Email";

                int row = 2;
                foreach (var s in query)
                {
                    ws.Cell(row, 1).Value = s.Id.ToString();
                    ws.Cell(row, 2).Value = s.Name;
                    ws.Cell(row, 3).Value = s.Email;
                    row++;
                }
                var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                var filePath = Path.Combine(
                    webRootPath,
                    "exports",
                    $"students_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
                );

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                workbook.SaveAs(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while exporting students to CSV: {ex.Message}");
                throw;
            }
        }
    }
}
