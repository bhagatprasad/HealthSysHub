using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IInpatientAdmissionManager
    {
        Task<List<InpatientAdmission>> GetAllAdmissionsAsync(); // Retrieve all inpatient admissions
        Task<List<InpatientAdmission>> GetActiveAdmissionsAsync(); // Retrieve all active inpatient admissions
        Task<InpatientAdmission> GetAdmissionByIdAsync(Guid admissionId); // Retrieve a specific admission by ID
        Task<List<InpatientAdmission>> GetAdmissionsByPatientIdAsync(Guid patientId); // Retrieve admissions by patient ID
        Task<List<InpatientAdmission>> GetAdmissionsByHospitalIdAsync(Guid hospitalId); // Retrieve admissions by hospital ID
        Task<List<InpatientAdmission>> GetAdmissionsByAdmittingDoctorIdAsync(Guid doctorId); // Retrieve admissions by admitting doctor ID
        Task<List<InpatientAdmission>> GetAdmissionsByAdmissionDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate); // Retrieve admissions by admission date range
        Task<List<InpatientAdmission>> GetAdmissionsByDischargeDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate); // Retrieve admissions by discharge date range
        Task<List<InpatientAdmission>> GetAdmissionsByStatusAsync(string status); // Retrieve admissions by status
        Task<InpatientAdmission> InsertOrUpdateAdmissionAsync(InpatientAdmission admission); // Insert a new admission or update an existing one
        Task<bool> DeleteAdmissionAsync(Guid admissionId); // Delete an admission record by ID (soft delete)
        Task<int> GetAdmissionCountAsync(); // Get the total count of admissions
        Task<List<InpatientAdmission>> GetAdmissionsByExpectedStayDurationAsync(int duration); // Retrieve admissions by expected stay duration
        Task<InpatientAdmission> GetMostRecentAdmissionAsync(Guid patientId); // Retrieve the most recent admission for a specific patient
    }

}
