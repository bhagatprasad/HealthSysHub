using Microsoft.AspNetCore.Identity;

namespace HealthSysHub.Web.UI.Models
{
    public class PrintAppointmentsReportRequest
    {
        public Guid? HospitalId { get; set; }
        public Guid? DoctorId { get; set; }
        public string? SearchStr { get; set; }
        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset? ToDate { get; set; }
    }
}
