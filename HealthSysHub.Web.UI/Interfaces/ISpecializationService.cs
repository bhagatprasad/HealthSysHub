using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface ISpecializationService
    {
        Task<Specialization> InsertOrUpdateSpecializationAsync(Specialization specialization);
        Task<Specialization> GetSpecializationByIdAsync(Guid specializationId);
        Task<List<Specialization>> GetSpecializationsAsync();
    }
}
