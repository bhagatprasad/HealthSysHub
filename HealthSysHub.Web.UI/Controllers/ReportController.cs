using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using HealthSysHub.Web.UI.Reports;
using HealthSysHub.Web.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Reflection.Metadata;

namespace HealthSysHub.Web.UI.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly PdfLayoutService _pdfLayoutService;
        private readonly IHospitalService _hospitalService;
        private readonly IDoctorAppointmentService _doctorAppointmentService;
        private readonly INotyfService _notyfService;
        private readonly IDoctorService _doctorService;

        public ReportController(PdfLayoutService pdfLayoutService,
            IHospitalService hospitalService,
            IDoctorAppointmentService doctorAppointmentService,
            INotyfService notyfService,
            IDoctorService doctorService)
        {
            _pdfLayoutService = pdfLayoutService;
            _hospitalService = hospitalService;
            _doctorAppointmentService = doctorAppointmentService;
            _notyfService = notyfService;
            _doctorService = doctorService;
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

        [HttpGet]
        public async Task<IActionResult> PrintAppointmentReceipt(string appointmentId)
        {
            try
            {
                var appointmentDetails = await _doctorAppointmentService.GetDoctorAppointmentByIdAsync(Guid.Parse(appointmentId));

                if (appointmentDetails == null)
                {
                    return NotFound("Appointment not found");
                }

                var doctorDetails = await _doctorService.GetDoctorByIdAsync(appointmentDetails.DoctorId.Value);

                var hospitalDetails = await _hospitalService.GetHospitalByIdAsync(appointmentDetails.HospitalId.Value);

                if (doctorDetails == null || hospitalDetails == null)
                {
                    return NotFound("Required information not found");
                }

                var document = QuestPDF.Fluent.Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        // Set page with border and reduced font size
                        page.Size(PageSizes.A4);
                        page.Margin(1.5f, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(10));
                        //page.Border(1).BorderColor(Colors.Grey.Lighten1).Padding(10);

                        // Header - Hospital Details (smaller font)
                        page.Header()
                            .BorderBottom(1)
                            .BorderColor(Colors.Grey.Lighten1)
                            .PaddingBottom(5)
                            .Row(row =>
                            {
                                row.RelativeItem().Column(column =>
                                {
                                    column.Item().AlignCenter().Text(hospitalDetails.HospitalName).Bold().FontSize(14);
                                    column.Item().AlignCenter().Text($"{hospitalDetails.Address}, {hospitalDetails.City}, {hospitalDetails.Country}").FontSize(10);
                                    column.Item().AlignCenter().Text($"Phone: {hospitalDetails.PhoneNumber} | Email: {hospitalDetails.Email}").FontSize(8);
                                });
                            });

                        // Single Content definition
                        page.Content()
                            .Column(column =>
                            {
                                // Title row with Appointment ID left and Date right
                                column.Item().Row(row =>
                                {
                                    row.RelativeItem().AlignLeft().Text($"Appointment ID: {appointmentId}").FontSize(10);
                                    row.RelativeItem().AlignRight().Text($"Date: {DateTime.Now.ToString("dd MMMM yyyy")}").FontSize(10);
                                });

                                column.Item().PaddingVertical(5);

                                // 1. Patient and Doctor Information Table
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(2); // Left column (40%)
                                        columns.RelativeColumn(3); // Right column (60%)
                                    });

                                    // Helper function for bordered rows
                                    void AddBorderedRow(string leftText, string rightText)
                                    {
                                        table.Cell()
                                            .Border(1)
                                            .BorderColor(Colors.Grey.Lighten1)
                                            .Padding(3)
                                            .Text(leftText)
                                            .FontSize(9);

                                        table.Cell()
                                            .Border(1)
                                            .BorderColor(Colors.Grey.Lighten1)
                                            .Padding(3)
                                            .Text(rightText)
                                            .FontSize(9);
                                    }

                                    // Section Headers
                                    AddBorderedRow("PATIENT INFORMATION", "DOCTOR INFORMATION");

                                    // Patient Details | Doctor Details
                                    AddBorderedRow($"Patient Name: {appointmentDetails.PatientName ?? "N/A"}",
                                                 $"Doctor Name: {doctorDetails.FullName ?? "N/A"}");

                                    AddBorderedRow($"Coming From: {appointmentDetails.ComingFrom ?? "N/A"}",
                                                 $"Specialization: {doctorDetails.Education ?? "N/A"}");

                                    AddBorderedRow($"Phone: {appointmentDetails.PatientPhone ?? "N/A"}",
                                                 $"Designation: {doctorDetails.Experience ?? "Consultant"}");
                                });

                                column.Item().PaddingVertical(5);

                                // 2. Appointment Details Table
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn(3);
                                    });

                                    void AddBorderedRow(string label, string value)
                                    {
                                        table.Cell()
                                            .Border(1)
                                            .BorderColor(Colors.Grey.Lighten1)
                                            .Padding(3)
                                            .Text(label)
                                            .FontSize(9);

                                        table.Cell()
                                            .Border(1)
                                            .BorderColor(Colors.Grey.Lighten1)
                                            .Padding(3)
                                            .Text(value)
                                            .FontSize(9);
                                    }

                                    AddBorderedRow("Appointment Date:", appointmentDetails.AppointmentDate.ToString());
                                    AddBorderedRow("Appointment Type:", "Consultation");
                                    AddBorderedRow("Status:", appointmentDetails.Status.ToString());
                                    AddBorderedRow("Department:", doctorDetails.Experience ?? "General");
                                });

                                column.Item().PaddingVertical(5);

                                // 3. Payment Details Table
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(30);  // Sr No
                                        columns.RelativeColumn(3);  // Name
                                        columns.RelativeColumn();   // Amount
                                        columns.RelativeColumn(2); // Payment Method
                                        columns.RelativeColumn(2); // Reference
                                    });

                                    // Header Row
                                    table.Header(header =>
                                    {
                                        void AddHeaderCell(string text)
                                        {
                                            header.Cell()
                                                .Border(1)
                                                .BorderColor(Colors.Grey.Lighten1)
                                                .Background(Colors.Grey.Lighten3)
                                                .Padding(3)
                                                .Text(text)
                                                .FontSize(9)
                                                .Bold();
                                        }

                                        AddHeaderCell("Sr No");
                                        AddHeaderCell("Name");
                                        AddHeaderCell("Amount");
                                        AddHeaderCell("Payment Method");
                                        AddHeaderCell("Reference");
                                    });

                                    // Data Row
                                    void AddDataCell(string text)
                                    {
                                        table.Cell()
                                            .Border(1)
                                            .BorderColor(Colors.Grey.Lighten1)
                                            .Padding(3)
                                            .Text(text)
                                            .FontSize(9);
                                    }

                                    AddDataCell("1");
                                    AddDataCell("Appointment - Consultation Fee");
                                    AddDataCell($"{appointmentDetails.Amount:C}");
                                    AddDataCell(appointmentDetails.PaymentType ?? "N/A");
                                    AddDataCell(appointmentDetails.PaymentReference ?? "N/A");
                                });

                                // Footer Signature Section
                                column.Item().PaddingVertical(10);
                                column.Item().Row(row =>
                                {
                                    row.RelativeItem(); // Empty space on left

                                    row.ConstantItem(150).Column(signatureCol =>
                                    {
                                        signatureCol.Item().PaddingTop(10).BorderTop(1).AlignCenter().Text("Authorized Signature").FontSize(9);
                                        signatureCol.Item().AlignCenter().Text("Hospital Representative").FontSize(8);
                                        signatureCol.Item().AlignCenter().Text($"Date: {DateTime.Now.ToString("dd/MM/yyyy")}").FontSize(8);
                                    });
                                });
                            });

                        // Page Footer
                        page.Footer()
                            .BorderTop(1)
                            .BorderColor(Colors.Grey.Lighten1)
                            .PaddingTop(3)
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.Span("Page ").FontSize(8);
                                text.CurrentPageNumber().FontSize(8);
                                text.Span(" of ").FontSize(8);
                                text.TotalPages().FontSize(8);
                            });
                    });
                });

                var pdfBytes = document.GeneratePdf();
                return File(pdfBytes, "application/pdf", $"AppointmentReceipt_{appointmentId}.pdf");
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return StatusCode(500, "An error occurred while generating the receipt");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PrintAppointmentsReport([FromBody] PrintAppointmentsReportRequest request)
        {

            try
            {
                var hospitalDetails = await _hospitalService.GetHospitalByIdAsync(request.HospitalId.Value);

                var appointmentsReport = await _doctorAppointmentService.GetAppointmentsReportAsync(request);

                var document = QuestPDF.Fluent.Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(1.5f, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(10));

                        // Header
                        page.Header()
                            .BorderBottom(1)
                            .BorderColor(Colors.Grey.Lighten1)
                            .PaddingBottom(5)
                            .Row(row =>
                            {
                                row.RelativeItem().Column(column =>
                                {
                                    column.Item().AlignCenter().Text(hospitalDetails.HospitalName).Bold().FontSize(14);
                                    column.Item().AlignCenter().Text($"{hospitalDetails.Address}, {hospitalDetails.City}, {hospitalDetails.Country}").FontSize(10);
                                    column.Item().AlignCenter().Text($"Phone: {hospitalDetails.PhoneNumber} | Email: {hospitalDetails.Email}").FontSize(8);
                                    column.Item().AlignCenter().Text("Appointments Report").Bold().FontSize(14);
                                    column.Item().AlignCenter().Text($"Generated on: {DateTime.Now.ToString("dd MMMM yyyy HH:mm")}").FontSize(9);
                                });
                            });

                        // Content - Main Table
                        page.Content()
                            .Table(table =>
                            {
                                // Define 6 columns
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(25);  // Sr No
                                    columns.RelativeColumn(2);   // Patient Info
                                    columns.RelativeColumn(2);   // Doctor Info
                                    columns.RelativeColumn();    // Appointment Date/Time
                                    columns.RelativeColumn();    // Amount
                                    columns.RelativeColumn();    // Payment Info
                                });

                                // Table Header
                                table.Header(header =>
                                {
                                    void AddHeaderCell(string text)
                                    {
                                        header.Cell()
                                            .Border(1)
                                            .Background(Colors.Grey.Lighten3)
                                            .Padding(3)
                                            .Text(text)
                                            .FontSize(9)
                                            .Bold();
                                    }

                                    AddHeaderCell("Sr No");
                                    AddHeaderCell("Patient Information");
                                    AddHeaderCell("Doctor Information");
                                    AddHeaderCell("Appointment");
                                    AddHeaderCell("Amount");
                                    AddHeaderCell("Payment");
                                });

                                // Table Rows
                                for (int i = 0; i < appointmentsReport.Count; i++)
                                {
                                    var appointment = appointmentsReport[i];
                                    var index = i + 1;

                                    // Row with alternating background color
                                    table.Cell()
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Lighten1)
                                        .Background(index % 2 == 0 ? Colors.White : Colors.Grey.Lighten4)
                                        .Padding(3)
                                        .Text(index.ToString())
                                        .FontSize(9);

                                    // Patient Information Cell
                                    table.Cell()
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Lighten1)
                                        .Background(index % 2 == 0 ? Colors.White : Colors.Grey.Lighten4)
                                        .Padding(3)
                                        .Column(column =>
                                        {
                                            column.Item().Text($"Name: {appointment.PatientName ?? "N/A"}").FontSize(9);
                                            column.Item().Text($"Phone: {appointment.PatientPhone ?? "N/A"}").FontSize(8);
                                            column.Item().Text($"From: {appointment.ComingFrom ?? "N/A"}").FontSize(8);
                                        });

                                    // Doctor Information Cell
                                    table.Cell()
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Lighten1)
                                        .Background(index % 2 == 0 ? Colors.White : Colors.Grey.Lighten4)
                                        .Padding(3)
                                        .Column(column =>
                                        {
                                            column.Item().Text($"Dr. {appointment.DoctorName ?? "N/A"}").FontSize(9);
                                            column.Item().Text($"Token: {appointment.TokenNo}").FontSize(8);
                                        });

                                    // Appointment Date/Time Cell
                                    table.Cell()
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Lighten1)
                                        .Background(index % 2 == 0 ? Colors.White : Colors.Grey.Lighten4)
                                        .Padding(3)
                                        .Column(column =>
                                        {
                                            column.Item().Text(appointment.AppointmentDate?.ToString("dd MMM yyyy") ?? "N/A").FontSize(9);
                                            column.Item().Text(appointment.AppointmentTime?.ToString(@"hh\:mm") ?? "N/A").FontSize(8);
                                        });

                                    // Amount Cell
                                    table.Cell()
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Lighten1)
                                        .Background(index % 2 == 0 ? Colors.White : Colors.Grey.Lighten4)
                                        .Padding(3)
                                        .AlignRight()
                                        .Text(appointment.Amount?.ToString("C") ?? "N/A")
                                        .FontSize(9);

                                    // Payment Info Cell
                                    table.Cell()
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Lighten1)
                                        .Background(index % 2 == 0 ? Colors.White : Colors.Grey.Lighten4)
                                        .Padding(3)
                                        .Column(column =>
                                        {
                                            column.Item().Text($"Type: {appointment.PaymentType ?? "N/A"}").FontSize(8);
                                            column.Item().Text($"Ref: {appointment.PaymentReference ?? "N/A"}").FontSize(8);
                                        });
                                }
                            });

                        // Footer
                        page.Footer()
                            .BorderTop(1)
                            .BorderColor(Colors.Grey.Lighten1)
                            .PaddingTop(3)
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.Span("Page ").FontSize(8);
                                text.CurrentPageNumber().FontSize(8);
                                text.Span(" of ").FontSize(8);
                                text.TotalPages().FontSize(8);
                            });
                    });
                });
                var pdfBytes = document.GeneratePdf();
                return File(pdfBytes, "application/pdf", $"AppointmentsReport_{DateTimeOffset.Now.ToString()}.pdf");
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return StatusCode(500, "An error occurred while generating the Appointments Report");
            }
        }
    }
}
