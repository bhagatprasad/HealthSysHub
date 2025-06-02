using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IPharmacyPaymentManager
    {

        Task<PharmacyPayment> ProcessPharmacyOrderPaymentAsync(PharmacyPayment pharmacyPayment);

        Task<List<PharmacyPaymentDetail>> GetPharmacyPaymentListAsync();

        Task<List<PharmacyPaymentDetail>> GetPharmacyPaymentListAsync(Guid? pharmacyId,DateTimeOffset? paymentsDate);

        Task<PharmacyPaymentDetail> GetPharmacyPaymentDetailAsync(Guid? pharmacyId, Guid? pharmacyOrderId);

    }
}
