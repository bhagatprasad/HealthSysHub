using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class PaymentTypeDataManager : IPaymentTypeManager
    {
        private readonly ApplicationDBContext _dbContext;

        public PaymentTypeDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaymentType> GetPaymentTypeByIdAsync(Guid paymentTypeId)
        {
            return await _dbContext.paymentTypes.FindAsync(paymentTypeId);
        }

        public async Task<List<PaymentType>> GetPaymentTypesAsync()
        {
            return await _dbContext.paymentTypes.ToListAsync();
        }

        public async Task<PaymentType> InsertOrUpdatePaymentTypeAsync(PaymentType paymentType)
        {
            if (paymentType.PaymentTypeId == Guid.Empty)
            {
                // Insert new PaymentType
                await _dbContext.paymentTypes.AddAsync(paymentType);
            }
            else
            {
                // Update existing PaymentType
                var existingPaymentType = await _dbContext.paymentTypes.FindAsync(paymentType.PaymentTypeId);

                if (existingPaymentType != null)
                {
                    // Check for changes and update properties
                    bool hasChanges = EntityUpdater.HasChanges(existingPaymentType, paymentType, nameof(PaymentType.CreatedBy), nameof(PaymentType.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingPaymentType, paymentType, nameof(PaymentType.CreatedBy), nameof(PaymentType.CreatedOn));
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
            return paymentType;
        }
    }
}
