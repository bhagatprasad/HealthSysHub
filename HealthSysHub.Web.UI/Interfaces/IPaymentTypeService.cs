using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IPaymentTypeService
    {
        Task<PaymentType> InsertOrUpdatePaymentTypeAsync(PaymentType paymentType);
        Task<PaymentType> GetPaymentTypeByIdAsync(Guid PaymentTypeId);
        Task<List<PaymentType>> GetPaymentTypesAsync();
    }
}
