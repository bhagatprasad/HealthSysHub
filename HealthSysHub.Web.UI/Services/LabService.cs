using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class LabService : ILabService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public LabService(IRepositoryFactory repositoryFactory) { _repositoryFactory = repositoryFactory; }

        public async Task<Lab> GetLabByIdAsync(Guid labId)
        {
            string url = Path.Combine("GetLabByIdAsync", labId.ToString());
            return await _repositoryFactory.SendAsync<Lab>(HttpMethod.Get, url);
        }

        public async Task<List<Lab>> GetLabsAsync(string searchString)
        {
            string url = Path.Combine("GetLabsAsync", searchString);
            return await _repositoryFactory.SendAsync<List<Lab>>(HttpMethod.Get, url);
        }

        public async Task<List<Lab>> GetLabsAsync()
        {
            return await _repositoryFactory.SendAsync<List<Lab>>(HttpMethod.Get, "GetLabsAsync");
        }

        public async Task<Lab> InsertOrUpdateLabAsync(Lab lab)
        {
            return await _repositoryFactory.SendAsync<Lab, Lab>(HttpMethod.Post, "InsertOrUpdateLabAsync", lab);
        }
    }
}
