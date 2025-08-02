using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("InpatientVitalSigns")]
    public class InpatientVitalSigns
    {
        [Key]
        public Guid VitalSignId { get; set; }
        public Guid? InpatientId { get; set; }
        public Guid? RecordedBy { get; set; }
        public DateTimeOffset? RecordedOn { get; set; }
        public decimal? Temperature { get; set; } // in Celsius
        public string? BloodPressure { get; set; } // e.g., "120/80"
        public int? PulseRate { get; set; } // beats per minute
        public int? RespiratoryRate { get; set; } // breaths per minute
        public decimal? OxygenSaturation { get; set; } // percentage
        public decimal? Height { get; set; } // in cm
        public decimal? Weight { get; set; } // in kg
        public string? Notes { get; set; }
    }
}
