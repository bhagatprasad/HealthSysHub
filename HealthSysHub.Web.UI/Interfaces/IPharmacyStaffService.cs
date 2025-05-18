using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IPharmacyStaffService
    {
        Task<PharmacyStaff> InsertOrUpdatePharmacyStaffAsync(PharmacyStaff staff);
        Task<PharmacyStaff> GetPharmacyStaffByIdAsync(Guid staffId);
        Task<List<PharmacyStaff>> GetPharmacyStaffAsync(Guid? hospitalId, Guid? pharmacyId);
        Task<List<PharmacyStaff>> GetPharmacyStaffsAsync();
    }
}
