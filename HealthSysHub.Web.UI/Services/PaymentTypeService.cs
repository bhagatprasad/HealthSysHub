using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IRepositoryFactory _repository;

        public PaymentTypeService(IRepositoryFactory repository)
        {
            _repository = repository;
        }
        public async Task<PaymentType> GetPaymentTypeByIdAsync(Guid paymentTypeId)
        {
            var uri = Path.Combine("PaymentType/GetPaymentTypeByIdAsync", paymentTypeId.ToString());
            return await _repository.SendAsync<PaymentType>(HttpMethod.Get, uri);
        }

        public async Task<List<PaymentType>> GetPaymentTypesAsync()
        {
            return await _repository.SendAsync<List<PaymentType>>(HttpMethod.Get, "PaymentType/GetPaymentTypesAsync");
        }

        public async Task<PaymentType> InsertOrUpdatePaymentTypeAsync(PaymentType paymentType)
        {
            return await _repository.SendAsync<PaymentType, PaymentType>(HttpMethod.Post, "PaymentType/InsertOrUpdatePaymentTypeAsync", paymentType);
        }
    }
}
