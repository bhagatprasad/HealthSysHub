using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IInpatientManager
    {
        Task<List<Inpatient>> GetInpatientsAsync(); // Retrieve all inpatients
        Task<List<Inpatient>> GetActiveInpatientsAsync(); // Retrieve all active inpatients
        Task<Inpatient> GetInpatientByIdAsync(Guid inpatientId); // Retrieve a specific inpatient by ID
        Task<List<Inpatient>> GetInpatientsByPatientIdAsync(Guid patientId); // Retrieve inpatients by patient ID
        Task<List<Inpatient>> GetInpatientsByHospitalIdAsync(Guid hospitalId); // Retrieve inpatients by hospital ID
        Task<List<Inpatient>> GetInpatientsByWardIdAsync(Guid wardId); // Retrieve inpatients by ward ID
        Task<List<Inpatient>> GetInpatientsByAdmittingDoctorIdAsync(Guid doctorId); // Retrieve inpatients by admitting doctor ID
        Task<List<Inpatient>> GetInpatientsByStatusAsync(string status); // Retrieve inpatients by current status
        Task<List<Inpatient>> GetInpatientsByAdmissionDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate); // Retrieve inpatients by admission date range
        Task<Inpatient> InsertOrUpdateInpatientAsync(Inpatient inpatient); // Insert a new inpatient or update an existing one
        Task<bool> DeleteInpatientAsync(Guid inpatientId); // Delete an inpatient record by ID (soft delete)
        Task<int> GetInpatientCountAsync(); // Get the total count of inpatients
        Task<List<Inpatient>> GetInpatientsByExpectedStayDurationAsync(int duration); // Retrieve inpatients by expected stay duration
        Task<List<Inpatient>> GetInpatientsByDischargeDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate); // Retrieve inpatients by discharge date range
        Task<Inpatient> GetMostRecentInpatientAsync(Guid patientId); // Retrieve the most recent inpatient record for a specific patient
    }

}
