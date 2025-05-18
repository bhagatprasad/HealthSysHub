using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using System;

namespace HealthSysHub.Web.UI.Services
{
    public class LabStaffService : ILabStaffService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public LabStaffService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<List<LabStaff>> GetLabStaffAsync(Guid? hospitalId, Guid? labId)
        {

            string query = string.Empty;

            if (hospitalId.HasValue && labId.HasValue)
            {
                query = "hospitalId=" + hospitalId.ToString() + "/" + "labId=" + labId.ToString();
            }
            else if (hospitalId.HasValue)
            {
                query = "hospitalId=" + hospitalId.ToString();
            }
            else if (labId.HasValue)
            {
                query = "labId=" + labId.ToString();
            }

            string url = Path.Combine("GetLabStaffAsync", query);

            return await _repositoryFactory.SendAsync<List<LabStaff>>(HttpMethod.Get, url);
        }

        public async Task<LabStaff> GetLabStaffByIdAsync(Guid staffId)
        {
            string url = Path.Combine("GetLabStaffByIdAsync", staffId.ToString());

            return await _repositoryFactory.SendAsync<LabStaff>(HttpMethod.Get, url);
        }

        public async Task<List<LabStaff>> GetLabStaffsAsync()
        {
            return await _repositoryFactory.SendAsync<List<LabStaff>>(HttpMethod.Get, "GetLabStaffsAsync");
        }

        public async Task<LabStaff> InsertOrUpdateLabStaffAsync(LabStaff staff)
        {
            return await _repositoryFactory.SendAsync<LabStaff, LabStaff>(HttpMethod.Post, "InsertOrUpdateLabStaffAsync", staff);
        }
    }
}
