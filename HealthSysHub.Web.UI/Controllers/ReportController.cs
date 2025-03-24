using HealthSysHub.Web.UI.Reports;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace HealthSysHub.Web.UI.Controllers
{
    public class ReportController : Controller
    {
        private readonly PdfLayoutService _pdfLayoutService;

        public ReportController(PdfLayoutService pdfLayoutService)
        {
            _pdfLayoutService = pdfLayoutService;
        }

        // Action for Consultation Bill
        public IActionResult ConsultationBill()
        {
            var pdfBytes = _pdfLayoutService.GeneratePdf(
                header =>
                {
                    header.Row(row =>
                    {
                        row.RelativeItem().Column(column =>
                        {
                            column.Item().Text("My Clinic").Bold().FontSize(20);
                            column.Item().Text("123 Health Street, City, Country").FontSize(12);
                            column.Item().Text("Phone: +123 456 7890 | Email: clinic@example.com").FontSize(10);
                        });

                        row.ConstantItem(100).Image(Placeholders.Image); // Add a logo if needed
                    });
                },
                body =>
                {
                    body.Column(column =>
                    {
                        column.Item().Text("Consultation Bill").Bold().FontSize(18);
                        column.Item().Text("Patient Name: John Doe");
                        column.Item().Text("Consultation Fee: $100");
                        column.Item().Text("Date: " + DateTime.Now.ToShortDateString());
                    });
                },
                footer =>
                {
                    footer.AlignCenter().Text(text =>
                    {
                        text.Span("Page ").FontSize(10);
                        text.CurrentPageNumber().FontSize(10);
                        text.Span(" of ").FontSize(10);
                        text.TotalPages().FontSize(10);
                    });
                }
            );

            return File(pdfBytes, "application/pdf", "ConsultationBill.pdf");
        }

        // Action for Daily Patient Report
        public IActionResult DailyPatientReport()
        {
            var pdfBytes = _pdfLayoutService.GeneratePdf(
                header =>
                {
                    header.Row(row =>
                    {
                        row.RelativeItem().Column(column =>
                        {
                            column.Item().Text("My Clinic").Bold().FontSize(20);
                            column.Item().Text("123 Health Street, City, Country").FontSize(12);
                            column.Item().Text("Phone: +123 456 7890 | Email: clinic@example.com").FontSize(10);
                        });

                        row.ConstantItem(100).Image(Placeholders.Image); // Add a logo if needed
                    });
                },
                body =>
                {
                    body.Column(column =>
                    {
                        column.Item().Text("Daily Patient Report").Bold().FontSize(18);
                        column.Item().Text("Date: " + DateTime.Now.ToShortDateString());

                        column.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Patient Name");
                                header.Cell().Text("Diagnosis");
                                header.Cell().Text("Fee");
                            });

                            // Sample data
                            var patients = new[]
                            {
                            new { Name = "John Doe", Diagnosis = "Fever", Fee = "$50" },
                            new { Name = "Jane Smith", Diagnosis = "Cough", Fee = "$70" }
                            };

                            foreach (var patient in patients)
                            {
                                table.Cell().Text(patient.Name);
                                table.Cell().Text(patient.Diagnosis);
                                table.Cell().Text(patient.Fee);
                            }
                        });
                    });
                },
                footer =>
                {
                    footer.AlignCenter().Text(text =>
                    {
                        text.Span("Page ").FontSize(10);
                        text.CurrentPageNumber().FontSize(10);
                        text.Span(" of ").FontSize(10);
                        text.TotalPages().FontSize(10);
                    });
                }
            );

            return File(pdfBytes, "application/pdf", "DailyPatientReport.pdf");
        }
    }
}
