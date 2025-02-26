
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IDoctorService
    {
        Task<Doctor> InsertOrUpdateDoctorAsync(Doctor doctor);
        Task<Doctor> GetDoctorByIdAsync(Guid doctorId);
        Task<List<Doctor>> GetDoctorsAsync(Guid hospitalId);
    }
}
