using HealthSysHub.Web.DBConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.Managers
{
    public interface IMedicineManager
    {
        Task<Medicine> InsertOrUpdateMedicineAsync(Medicine medicines);
        Task<Medicine> GetMedicineByIdAsync(Guid MedicineId);
        Task<List<Medicine>> GetMedicinesAsync(string searchString);
        Task<List<Medicine>> GetMedicinesAsync();
    }
}
