using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.Utility.Models
{
    public class PatientDetails
    {
        public PatientDetails()
        {
            patientPrescriptionDetails = new PatientPrescriptionDetails();
            patientVitalDetails = new PatientVitalDetails();
        }
        public Guid? PatientId { get; set; }
        public Guid? PatientTypeId { get; set; }
        public string? HealthIssue { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? AttenderPhone { get; set; }
        public string? Age { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public PatientPrescriptionDetails patientPrescriptionDetails { get; set; }
        public PatientVitalDetails patientVitalDetails { get; set; }
    }
}
