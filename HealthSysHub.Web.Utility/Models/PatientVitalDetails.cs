namespace HealthSysHub.Web.Utility.Models
{
    public class PatientVitalDetails
    {
        public Guid? VitalId { get; set; }
        public string? BodyTemperature { get; set; }
        public string? HeartRate { get; set; }
        public string? BloodPressure { get; set; }
        public string? RespiratoryRate { get; set; }
        public string? OxygenSaturation { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? BMI { get; set; }
        public string? Notes { get; set; }
    }
}
