using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IPaymentTypeManager
    {
        Task<PaymentType> InsertOrUpdatePaymentTypeAsync(PaymentType paymentType);
        Task<PaymentType> GetPaymentTypeByIdAsync(Guid PaymentTypeId);
        Task<List<PaymentType>> GetPaymentTypesAsync();
    }
}
