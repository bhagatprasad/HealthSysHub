namespace HealthSysHub.Web.Utility.Models
{
    public class PrintAppointmentsReportRequest
    {
        public Guid? HospitalId { get; set; }
        public string? SearchStr { get; set; }
        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset? ToDate { get; set; }
    }
}
