using HealthSysHub.Web.DBConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.Managers
{
    public interface IPharmacyOrderManager
    {
        Task<PharmacyOrder> InsertOrUpdatePharmacyOrderAsync(PharmacyOrder pharmacyOrder);
        Task<PharmacyOrder> GetPharmacyOrderByIdAsync(Guid pharmacyOrderId);
        Task<List<PharmacyOrder>> GetPharmacyOrdersAsync();
    }
}
