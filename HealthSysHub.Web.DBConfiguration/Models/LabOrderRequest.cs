using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("LabOrderRequest")]
    public class LabOrderRequest
    {
        [Key]
        public Guid LabOrderRequestId { get; set; }
        public Guid? PatientPrescriptionId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? PatientId { get; set; }
        public string? HospitalName { get; set; }
        public string? DoctorName { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Notes { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
