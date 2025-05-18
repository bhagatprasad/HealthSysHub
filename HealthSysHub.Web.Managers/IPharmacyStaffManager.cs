using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IPharmacyStaffManager
    {
        Task<PharmacyStaff> InsertOrUpdatePharmacyStaffAsync(PharmacyStaff staff);
        Task<PharmacyStaff> GetPharmacyStaffByIdAsync(Guid staffId);
        Task<List<PharmacyStaff>> GetPharmacyStaffAsync(Guid? hospitalId,Guid? pharmacyId);

        Task<List<PharmacyStaff>> GetPharmacyStaffsAsync();
    }
}
