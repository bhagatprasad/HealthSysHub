using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IWardManager
    {
        Task<List<Ward>> GetAllWardsAsync(); // Retrieve all wards
        Task<List<Ward>> GetActiveWardsAsync(); // Retrieve all active wards
        Task<Ward> GetWardByIdAsync(Guid wardId); // Retrieve a specific ward by ID
        Task<List<Ward>> GetWardsByHospitalIdAsync(Guid hospitalId); // Retrieve wards by hospital ID
        Task<Ward> InsertOrUpdateWardAsync(Ward ward); // Insert or update a ward
        Task<bool> DeleteWardAsync(Guid wardId); // Delete a ward by ID (soft delete)
        Task<int> GetWardCountAsync(); // Get the total count of wards
    }

}
