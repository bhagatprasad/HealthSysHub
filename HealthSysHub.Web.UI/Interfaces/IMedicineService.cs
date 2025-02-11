using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IMedicineService
    {
        Task<Medicine> InsertOrUpdateMedicineAsync(Medicine medicine);
        Task<Medicine> GetMedicineByIdAsync(Guid medicineId);
        Task<List<Medicine>> GetMedicinesAsync();
    }
}
