using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly IRepositoryFactory _repository;

        public SpecializationService(IRepositoryFactory repository)
        {
            _repository = repository;
        }

        public async Task<Specialization> GetSpecializationByIdAsync(Guid specializationId)
        {
            var uri = Path.Combine("Specialization/GetSpecializationByIdAsync", specializationId.ToString());
            return await _repository.SendAsync<Specialization>(HttpMethod.Get, uri);
        }

        public async Task<List<Specialization>> GetSpecializationsAsync()
        {
            return await _repository.SendAsync<List<Specialization>>(HttpMethod.Get, "Specialization/GetSpecializationsAsync");
        }

        public async Task<Specialization> InsertOrUpdateSpecializationAsync(Specialization specialization)
        {
            return await _repository.SendAsync<Specialization, Specialization>(HttpMethod.Post, "Specialization/InsertOrUpdateSpecializationAsync", specialization);
        }
    }
}
