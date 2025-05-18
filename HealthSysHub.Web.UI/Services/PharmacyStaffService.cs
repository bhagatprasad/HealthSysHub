using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class PharmacyStaffService : IPharmacyStaffService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public PharmacyStaffService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<List<PharmacyStaff>> GetPharmacyStaffAsync(Guid? hospitalId, Guid? pharmacyId)
        {
            string query = string.Empty;

            if (hospitalId.HasValue && pharmacyId.HasValue)
            {
                query = "hospitalId=" + hospitalId.ToString() + "/" + "pharmacyId=" + pharmacyId.ToString();
            }
            else if (hospitalId.HasValue)
            {
                query = "hospitalId=" + hospitalId.ToString();
            }
            else if (pharmacyId.HasValue)
            {
                query = "pharmacyId=" + pharmacyId.ToString();
            }

            string url = Path.Combine("GetPharmacyStaffAsync", query);

            return await _repositoryFactory.SendAsync<List<PharmacyStaff>>(HttpMethod.Get, url);
        }

        public async Task<PharmacyStaff> GetPharmacyStaffByIdAsync(Guid staffId)
        {
            string url = Path.Combine("GetPharmacyStaffByIdAsync", staffId.ToString());

            return await _repositoryFactory.SendAsync<PharmacyStaff>(HttpMethod.Get, url);
        }

        public async Task<List<PharmacyStaff>> GetPharmacyStaffsAsync()
        {
            return await _repositoryFactory.SendAsync<List<PharmacyStaff>>(HttpMethod.Get, "GetPharmacyStaffsAsync");
        }

        public async Task<PharmacyStaff> InsertOrUpdatePharmacyStaffAsync(PharmacyStaff staff)
        {
            return await _repositoryFactory.SendAsync<PharmacyStaff, PharmacyStaff>(HttpMethod.Post, "InsertOrUpdatePharmacyStaffAsync", staff);
        }
    }
}
