using HealthSysHub.Web.DBConfiguration.Models;
using System.Threading.Tasks;


namespace HealthSysHub.Web.Managers
{
    public interface IStaffManager
    {
        Task<List<HospitalStaff>> GetAllHospitalStaffAsync(Guid hosptialId);
        Task<HospitalStaff> GetHospitalStaffAsync(Guid hosptialId,Guid staffId);
        Task<HospitalStaff> InsertOrUpdateHospitalStaffAsync(HospitalStaff hospitalStaff);

        Task<List<Doctor>> GetDoctorsAsync();
        Task<List<Doctor>> GetDoctorsAsync(Guid hospitalId);
        Task<Doctor> GetDoctorsAsync(Guid hospitalId,Guid doctorId);
        Task<Doctor> InsertOrUpdateDoctorsAsync(Doctor doctor);

        Task<List<Nurse>> GetNursesAsync();
        Task<List<Nurse>> GetNursesAsync(Guid hospitalId);
        Task<Nurse> GetNurseAsync(Guid hospitalId, Guid nurseId);
        Task<Nurse> InsertOrUpdateNurseAsync(Nurse nurse);

        Task<List<Pharmacist>> GetPharmacistsAsync();
        Task<List<Pharmacist>> GetPharmacistsAsync(Guid hospitalId);
        Task<Pharmacist> GetPharmacistAsync(Guid hospitalId, Guid pharmacistId);
        Task<Pharmacist> InsertOrUpdatePharmacistAsync(Pharmacist pharmacist);

    }
}
