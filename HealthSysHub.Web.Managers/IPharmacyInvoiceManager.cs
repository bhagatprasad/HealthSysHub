using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IPharmacyInvoiceManager
    {
        Task<List<PharmacyInvoiceDetails>> GetPharmacyInvoiceListAsync();
        Task<List<PharmacyInvoiceDetails>> GetPharmacyInvoiceListAsync(Guid pharmacyId);
        Task<List<PharmacyInvoiceDetails>> GetPharmacyInvoiceListAsync(Guid pharmacyId,Guid pharmacyOrderId);
        Task<PharmacyInvoiceDetails> InsertOrUpdatePharmacyInvoiceAsync(PharmacyInvoiceDetails pharmacyInvoiceDetails);
    }
}
