using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface ISpecializationManager
    {
        Task<Specialization> InsertOrUpdateSpecializationAsync(Specialization specialization);
        Task<Specialization> GetSpecializationByIdAsync(Guid specializationId);
        Task<List<Specialization>> GetSpecializationsAsync();
    }
}