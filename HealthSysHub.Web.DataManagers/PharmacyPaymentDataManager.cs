using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class PharmacyPaymentDataManager : IPharmacyPaymentManager
    {
        private readonly ApplicationDBContext _dbContext;

        private readonly IPharmacyOrderManager _pharmacyOrderManager;
        public PharmacyPaymentDataManager(ApplicationDBContext _dbContext, IPharmacyOrderManager pharmacyOrderManager)
        {
            this._dbContext = _dbContext;
            this._pharmacyOrderManager = pharmacyOrderManager;
        }
        public async Task<PharmacyPayment> ProcessPharmacyOrderPaymentAsync(PharmacyPayment pharmacyPayment)
        {
            var order = await GetPharmacyOrderAsync(pharmacyPayment.PharmacyOrderId);
            if (order == null)
            {
                return pharmacyPayment;
            }

            var orderRequest = await GetPharmacyOrderRequestAsync(order.PharmacyOrderRequestId);
            if (await HasExistingPaymentsAsync(pharmacyPayment.PharmacyOrderId))
            {
                return pharmacyPayment;
            }

            if (IsPaymentValid(order, pharmacyPayment))
            {
                CompleteOrder(order, orderRequest);
                await AddPaymentAsync(pharmacyPayment);
            }

            return pharmacyPayment;
        }

        public async Task<List<PharmacyPaymentDetail>> GetPharmacyPaymentListAsync()
        {
            var orders = await _pharmacyOrderManager.GetPharmacyOrdersAsync();
            var payments = await _dbContext.pharmacyPayments.ToListAsync();

            List<PharmacyPaymentDetail> pharmacyPayments = new List<PharmacyPaymentDetail>();

            foreach (var order in orders)
            {
                var payment = payments.Any() ? payments.Where(x => x.PharmacyOrderId == order.PharmacyOrderId).FirstOrDefault() : new PharmacyPayment();


                pharmacyPayments.Add(new PharmacyPaymentDetail()
                {
                    PharmacyOrderId = order.PharmacyOrderId,
                    BalanceAmount = order.BalanceAmount,
                    CreatedBy = order.CreatedBy,
                    CreatedOn = order.CreatedOn,
                    DiscountAmount = order.DiscountAmount,
                    FinalAmount = order.FinalAmount,
                    GatewayResponse = payment.GatewayResponse,
                    IsActive = payment.IsActive,
                    ItemQty = order.ItemQty,
                    PharmacyOrderRequestId = order.PharmacyOrderRequestId,
                    ModifiedBy = order.ModifiedBy,
                    ModifiedOn = order.ModifiedOn,
                    Notes = order.Notes,
                    PaymentAmount = payment.PaymentAmount,
                    PaymentDate = payment.PaymentDate,
                    PaymentGateway = payment.PaymentGateway,
                    OrderReference = order.OrderReferance,
                });
            }

            return pharmacyPayments;
        }

        public async Task<List<PharmacyPaymentDetail>> GetPharmacyPaymentListAsync(Guid? pharmacyId, DateTimeOffset? paymentsDate)
        {
            var orders = await _pharmacyOrderManager.GetPharmacyOrdersListByPharmacyAsync(pharmacyId.Value);
            var payments = await _dbContext.pharmacyPayments.Where(x => x.PharmacyId == pharmacyId).ToListAsync();

            List<PharmacyPaymentDetail> pharmacyPayments = new List<PharmacyPaymentDetail>();

            foreach (var order in orders)
            {
                var payment = payments.Any() ? payments.Where(x => x.PharmacyOrderId == order.PharmacyOrderId).FirstOrDefault() : new PharmacyPayment();


                pharmacyPayments.Add(new PharmacyPaymentDetail()
                {
                    PharmacyId = order.PharmacyId,
                    OrderReference = order.OrderReference,
                    Name = order.Name,
                    Phone = order.Phone,
                    DoctorName = order.DoctorName,
                    ItemQty = order.ItemQty,
                    TotalAmount = order.TotalAmount,
                    DiscountAmount = order.DiscountAmount,
                    FinalAmount = order.FinalAmount,
                    BalanceAmount = order.BalanceAmount,
                    Notes = order.Notes,
                    Status = order.Status,
                    CreatedBy = order.CreatedBy,
                    CreatedOn = order.CreatedOn,
                    ModifiedBy = order.ModifiedBy,
                    ModifiedOn = order.ModifiedOn,
                    IsActive = order.IsActive,
                    pharmacyOrderItemDetails = order.pharmacyOrderItemDetails,
                    PaymentId = payment?.PaymentId,
                    PaymentNumber = payment?.PaymentNumber,
                    PaymentDate = payment?.PaymentDate,
                    PaymentMethod = payment?.PaymentMethod,
                    PaymentAmount = payment?.PaymentAmount,
                    ReferenceNumber = payment?.ReferenceNumber,
                    PaymentStatus = payment?.Status,
                    PaymentGateway = payment?.PaymentGateway,
                    GatewayResponse = payment?.GatewayResponse,
                    PaymentNotes = payment?.Notes,
                    HospitalId = payment?.HospitalId
                });
            }

            return pharmacyPayments.OrderByDescending(x => x.ModifiedOn).ToList();
        }

        public async Task<PharmacyPaymentDetail> GetPharmacyPaymentDetailAsync(Guid? pharmacyId, Guid? pharmacyOrderId)
        {
            if (!pharmacyId.HasValue || !pharmacyOrderId.HasValue)
            {
                return null;
            }

            // Fetch the order by pharmacyOrderId and pharmacyId (assuming GetPharmacyOrdersListByPharmacyAsync exists and fetching single)
            var orders = await _pharmacyOrderManager.GetPharmacyOrdersListByPharmacyAsync(pharmacyId.Value);
            var order = orders.Find(o => o.PharmacyOrderId == pharmacyOrderId.Value);

            if (order == null)
            {
                return null;
            }

            // Fetch the payment(s) related to that order
            var payment = await _dbContext.pharmacyPayments
                .FirstOrDefaultAsync(p => p.PharmacyOrderId == pharmacyOrderId && p.PharmacyId == pharmacyId);

            // Map order and payment details into PharmacyPaymentDetail
            var paymentDetail = new PharmacyPaymentDetail()
            {
                // Order details
                PharmacyOrderId = order.PharmacyOrderId,
                PharmacyOrderRequestId = order.PharmacyOrderRequestId,
                PharmacyId = order.PharmacyId,
                OrderReference = order.OrderReference,
                Name = order.Name,
                Phone = order.Phone,
                DoctorName = order.DoctorName,
                ItemQty = order.ItemQty,
                TotalAmount = order.TotalAmount,
                DiscountAmount = order.DiscountAmount,
                FinalAmount = order.FinalAmount,
                BalanceAmount = order.BalanceAmount,
                Notes = order.Notes,
                Status = order.Status,
                CreatedBy = order.CreatedBy,
                CreatedOn = order.CreatedOn,
                ModifiedBy = order.ModifiedBy,
                ModifiedOn = order.ModifiedOn,
                IsActive = order.IsActive,
                pharmacyOrderItemDetails = order.pharmacyOrderItemDetails,

                // Payment details (handle null payment)
                PaymentId = payment?.PaymentId,
                PaymentNumber = payment?.PaymentNumber,
                PaymentDate = payment?.PaymentDate,
                PaymentMethod = payment?.PaymentMethod,
                PaymentAmount = payment?.PaymentAmount,
                ReferenceNumber = payment?.ReferenceNumber,
                PaymentStatus = payment?.Status,
                PaymentGateway = payment?.PaymentGateway,
                GatewayResponse = payment?.GatewayResponse,
                PaymentNotes = payment?.Notes,
                HospitalId = payment?.HospitalId
            };

            return paymentDetail;
        }

        private async Task<PharmacyOrder> GetPharmacyOrderAsync(Guid? orderId)
        {
            return await _dbContext.pharmacyOrders.FirstOrDefaultAsync(x => x.PharmacyOrderId == orderId.Value);
        }

        private async Task<PharmacyOrderRequest> GetPharmacyOrderRequestAsync(Guid? orderRequestId)
        {
            return await _dbContext.pharmacyOrderRequests.FirstOrDefaultAsync(x => x.PharmacyOrderRequestId == orderRequestId.Value);
        }

        private async Task<bool> HasExistingPaymentsAsync(Guid? orderId)
        {
            var payments = await _dbContext.pharmacyPayments
                .Where(x => x.PharmacyOrderId == orderId)
                .ToListAsync();
            return payments.Any();
        }

        private bool IsPaymentValid(PharmacyOrder order, PharmacyPayment pharmacyPayment)
        {
            return order.FinalAmount == pharmacyPayment.PaymentAmount;
        }

        private void CompleteOrder(PharmacyOrder order, PharmacyOrderRequest orderRequest)
        {
            order.Status = "Completed";
            orderRequest.Status = "Completed";
        }

        private async Task AddPaymentAsync(PharmacyPayment pharmacyPayment)
        {
            await _dbContext.pharmacyPayments.AddAsync(pharmacyPayment);
            await _dbContext.SaveChangesAsync();
        }

    }
}
