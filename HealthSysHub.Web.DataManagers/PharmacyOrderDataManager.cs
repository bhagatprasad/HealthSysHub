using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class PharmacyOrderDataManager : IPharmacyOrderManager
    {
        private readonly ApplicationDBContext _dbContext;
        public PharmacyOrderDataManager(ApplicationDBContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<PharmacyOrder> GetPharmacyOrderByIdAsync(Guid pharmacyOrderId)
        {
            return await _dbContext.pharmacyOrders.FindAsync(pharmacyOrderId);
        }



        public async Task<List<PharmacyOrder>> GetPharmacyOrdersAsync()
        {
            return await _dbContext.pharmacyOrders.ToListAsync();
        }

        public async Task<List<PharmacyOrder>> GetPharmacyOrdersByPharmacyAsync(Guid pharmacyId)
        {
            return await _dbContext.pharmacyOrders.Where(x => x.PharmacyId == pharmacyId).ToListAsync();
        }
        public async Task<PharmacyOrder> InsertOrUpdatePharmacyOrderAsync(PharmacyOrder pharmacyOrder)
        {
            if (pharmacyOrder.PharmacyOrderId == Guid.Empty)
            {
                await _dbContext.pharmacyOrders.AddAsync(pharmacyOrder);
            }
            else
            {
                var existingPharmacyOrder = await _dbContext.pharmacyOrders.FindAsync(pharmacyOrder.PharmacyOrderId);

                if (existingPharmacyOrder != null)
                {
                    var hasChanges = EntityUpdater.HasChanges(existingPharmacyOrder, pharmacyOrder, nameof(PharmacyOrder.CreatedBy), nameof(PharmacyOrder.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingPharmacyOrder, pharmacyOrder, nameof(PharmacyOrder.CreatedBy), nameof(PharmacyOrder.CreatedOn));
                    }
                }
            }
            await _dbContext.SaveChangesAsync();

            return pharmacyOrder;
        }

        public Task<PharmacyOrderDetails> GetPharmacyOrderByIdAsync(Guid pharmacyId, Guid pharmacyOrderId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PharmacyOrderDetails>> GetPharmacyOrdersListByPharmacyAsync(Guid pharmacyId)
        {
            // Load all required data in parallel

            var orders = await _dbContext.pharmacyOrders
                    .Where(o => o.PharmacyId == pharmacyId)
                    .ToListAsync();

            var requests = await _dbContext.pharmacyOrderRequests
                    .Where(r => r.PharmacyId == pharmacyId)
                    .ToListAsync();

            var requestItems = await _dbContext.pharmacyOrderItems
                    .Where(i => i.PharmacyId == pharmacyId)
                    .ToListAsync();

            var medicines = await _dbContext.pharmacyMedicines
                    .Where(m => m.PharmacyId == pharmacyId)
                    .ToListAsync();

            if (!orders.Any())
            {
                return new List<PharmacyOrderDetails>();
            }

            // Create lookup dictionaries for faster access
            var requestsLookup = requests.ToDictionary(r => r.PharmacyOrderRequestId);
            var medicinesLookup = medicines.ToDictionary(m => m.MedicineId);

            return orders.Select(order =>
            {
                requestsLookup.TryGetValue(order.PharmacyOrderRequestId.Value, out var pharmacyRequest);

                return new PharmacyOrderDetails()
                {
                    PharmacyOrderId = order.PharmacyOrderId,
                    PharmacyOrderRequestId = order.PharmacyOrderRequestId,
                    PharmacyId = order.PharmacyId,
                    OrderReference = order.OrderReferance,
                    ItemQty = order.ItemQty,
                    TotalAmount = order.TotalAmount,
                    DiscountAmount = order.DiscountAmount,
                    FinalAmount = order.FinalAmount,
                    BalanceAmount = order.BalanceAmount,
                    Notes = order.Notes,
                    Status = order.Status,
                    DoctorName = pharmacyRequest?.DoctorName,
                    Name = pharmacyRequest?.Name,
                    Phone = pharmacyRequest?.Phone,
                    CreatedOn = order.CreatedOn,
                    CreatedBy = order.CreatedBy,
                    ModifiedOn = order.ModifiedOn,
                    ModifiedBy = order.ModifiedBy,
                    IsActive = order.IsActive,
                    pharmacyOrderItemDetails = MapPharmacyOrderItemDetails(
                        requestItems.Where(i => i.PharmacyOrderId == order.PharmacyOrderId).ToList(),
                        medicinesLookup)
                };
            }).OrderByDescending(c => c.ModifiedOn).ToList();
        }

        private List<PharmacyOrderItemDetails> MapPharmacyOrderItemDetails(
            List<PharmacyOrderItem> items,
            Dictionary<Guid, PharmacyMedicine> medicinesLookup)
        {
            return items.Select(item =>
            {
                medicinesLookup.TryGetValue(item.MedicineId.Value, out var medicine);

                return new PharmacyOrderItemDetails
                {
                    PharmacyOrderItemId = item.PharmacyOrderItemId,
                    MedicineId = item.MedicineId,
                    MedicineName = medicine?.MedicineName,
                    ItemQty = item.ItemQty,
                    UnitPrice = medicine?.PricePerUnit,
                    TotalAmount = item.ItemQty * medicine?.PricePerUnit,
                    CreatedBy = item.CreatedBy,
                    CreatedOn = item.CreatedOn,
                    IsActive = item.IsActive,
                    ModifiedBy = item.ModifiedBy,
                    ModifiedOn = item.ModifiedOn,
                    PharmacyId = item.PharmacyId
                };
            }).ToList();
        }

        public async Task<PharmacyOrdersProcessResponse> ProcessPharmacyOrdersRequestAsync(PharmacyOrdersProcessRequest request)
        {
            var response = new PharmacyOrdersProcessResponse();
            if (request == null)
            {
                response.Success = false;
                response.Message = "Invalid object for process order ,please verify and resend";
                return response;
            }
            var pharmacyOrder = await _dbContext.pharmacyOrders.FindAsync(request.PharmacyOrderId);

            if (pharmacyOrder == null)
            {
                response.Success = false;
                response.Message = "Invalid orderid for process order ,please verify and resend";
                return response;
            }

            pharmacyOrder.Status = request.Status;
            pharmacyOrder.ModifiedBy = request.ModifiedBy;
            pharmacyOrder.ModifiedOn = request.ModifiedOn;
            pharmacyOrder.Notes = request.Notes;
            pharmacyOrder.Notes = !string.IsNullOrEmpty(pharmacyOrder.Notes) ? string.Join(",", pharmacyOrder.Notes, pharmacyOrder.Notes) : request.Notes;
            await _dbContext.SaveChangesAsync();

            response.Success = true;
            response.Message = "Successfully proccessed order";


            return response;
        }

        

    }
}
