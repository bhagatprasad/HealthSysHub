using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.Utility.Models
{
    public class DoctorAppointmentDetails
    {
        public Guid? AppointmentId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public TimeSpan? AppointmentTime { get; set; }
        public string? PatientName { get; set; }
        public string? PatientPhone { get; set; }
        public string? ComingFrom { get; set; }
        public string? HealthIssue { get; set; }
        public int TokenNo { get; set; }
        public decimal? Amount { get; set; }
        public string? PaymentType { get; set; }
        public string? PaymentReference { get; set; }
        public string? Status { get; set; }
        public string? StatusMessage { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
