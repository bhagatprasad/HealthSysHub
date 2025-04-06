using HealthSysHub.Web.DBConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.Managers
{
    public interface IPharmacistManager
    {
        Task<Pharmacist> InsertOrUpdatePharmacistAsync(Pharmacist pharmacist);
        Task<List<Pharmacist>> GetPharmacistsAsync();
        Task<Pharmacist> GetPharmacistByIdAsync(Guid pharmacistId);
        Task<List<Pharmacist>> GetPharmacistsByHospitalAsync(Guid hospitalId);
    }
}
