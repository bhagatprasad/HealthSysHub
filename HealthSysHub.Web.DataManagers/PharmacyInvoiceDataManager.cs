using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class PharmacyInvoiceDataManager : IPharmacyInvoiceManager
    {
        private readonly ApplicationDBContext _dbContext;
        public PharmacyInvoiceDataManager(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<List<PharmacyInvoiceDetails>> GetPharmacyInvoiceListAsync()
        {
            return await MapPharmacyInvoiceDetailsAsync(null, null);
        }

        public async Task<List<PharmacyInvoiceDetails>> GetPharmacyInvoiceListAsync(Guid pharmacyId)
        {
            return await MapPharmacyInvoiceDetailsAsync(pharmacyId, null);
        }

        public async Task<List<PharmacyInvoiceDetails>> GetPharmacyInvoiceListAsync(Guid pharmacyId, Guid pharmacyOrderId)
        {
            return await MapPharmacyInvoiceDetailsAsync(pharmacyId, pharmacyOrderId);
        }

        public async Task<PharmacyInvoiceDetails> InsertOrUpdatePharmacyInvoiceAsync(PharmacyInvoiceDetails pharmacyInvoiceDetails)
        {
            if (pharmacyInvoiceDetails != null)
            {
                if (pharmacyInvoiceDetails.InvoiceId == Guid.Empty)
                {
                    var invoice = MapToPharmacyInvoice(pharmacyInvoiceDetails);


                    await _dbContext.pharmacyInvoices.AddAsync(invoice);
                    await _dbContext.SaveChangesAsync();

                    var invoiceItems = MapToPharmacyInvoiceItems(pharmacyInvoiceDetails, invoice.InvoiceId);


                    pharmacyInvoiceDetails.InvoiceId = invoice.InvoiceId;

                   //update items id as well to the input to retrun 
                }
                else
                {

                }
            }

            return pharmacyInvoiceDetails;
        }



        private async Task<List<PharmacyInvoiceDetails>> MapPharmacyInvoiceDetailsAsync(Guid? pharmacyId, Guid? pharmacyOrderId)
        {
            List<PharmacyInvoiceDetails> pharmacyInvoiceDetails = new List<PharmacyInvoiceDetails>();

            var pharmacyMedicines = pharmacyId.HasValue ? await _dbContext.pharmacyMedicines.Where(x => x.PharmacyId == pharmacyId).ToListAsync() : await _dbContext.pharmacyMedicines.ToListAsync();

            // Retrieve pharmacy invoices based on the provided pharmacyId and pharmacyOrderId
            var pharmacyInvoicesQuery = _dbContext.pharmacyInvoices.AsQueryable();

            if (pharmacyId.HasValue)
            {
                pharmacyInvoicesQuery = pharmacyInvoicesQuery.Where(x => x.PharmacyId == pharmacyId);

                if (pharmacyOrderId.HasValue)
                {
                    pharmacyInvoicesQuery = pharmacyInvoicesQuery.Where(x => x.PharmacyOrderId == pharmacyOrderId);
                }
            }

            // Execute the query and retrieve the list of invoices
            var pharmacyInvoices = await pharmacyInvoicesQuery.ToListAsync();


            if (pharmacyInvoices.Any())
            {
                // Retrieve pharmacy invoice items if there are any invoices
                List<PharmacyInvoiceItem> pharmacyInvoiceItems = new List<PharmacyInvoiceItem>();

                if (pharmacyInvoices.Any())
                {
                    var invoiceIds = pharmacyInvoices.Select(m => m.InvoiceId).ToList();
                    pharmacyInvoiceItems = await _dbContext.pharmacyInvoiceItems
                        .Where(x => invoiceIds.Contains(x.InvoiceId.Value))
                        .ToListAsync();
                }

                foreach (var item in pharmacyInvoices)
                {
                    pharmacyInvoiceDetails.Add(new PharmacyInvoiceDetails()
                    {
                        AmountPaid = item.AmountPaid,
                        BalanceDue = item.BalanceDue,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = item.CreatedOn,
                        DiscountAmount = item.DiscountAmount,
                        HospitalId = item.HospitalId,
                        InvoiceId = item.InvoiceId,
                        InvoiceReference = item.InvoiceReference,
                        IsActive = item.IsActive,
                        ModifiedBy = item.ModifiedBy,
                        ModifiedOn = item.ModifiedOn,
                        Notes = item.Notes,
                        PharmacyId = item.PharmacyId,
                        PharmacyOrderId = item.PharmacyOrderId,
                        PharmacyOrderRequestId = item.PharmacyOrderRequestId,
                        Status = item.Status,
                        SubTotal = item.SubTotal,
                        TaxAmount = item.TaxAmount,
                        TotalAmount = item.TotalAmount,
                        pharmacyInvoiceItemDetails = MapPharmacyInvoiceItemDetails(item, pharmacyInvoiceItems, pharmacyMedicines)
                    });
                }
            }


            return pharmacyInvoiceDetails;
        }

        private List<PharmacyInvoiceItemDetails> MapPharmacyInvoiceItemDetails(PharmacyInvoice pharmacyInvoice, List<PharmacyInvoiceItem> pharmacyInvoiceItems, List<PharmacyMedicine> pharmacyMedicines)
        {
            List<PharmacyInvoiceItemDetails> pharmacyInvoiceItemDetails = new List<PharmacyInvoiceItemDetails>();

            if (pharmacyInvoice != null)
            {
                if (pharmacyInvoiceItems.Any())
                {
                    var pharmacyitems = pharmacyInvoiceItems.Where(x => x.InvoiceId == pharmacyInvoice.InvoiceId).ToList();

                    foreach (var item in pharmacyitems)
                    {
                        var medicine = pharmacyMedicines.Where(x => x.MedicineId == item.MedicineId).FirstOrDefault();

                        pharmacyInvoiceItemDetails.Add(new PharmacyInvoiceItemDetails()
                        {
                            InvoiceId = item.InvoiceId,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = item.CreatedOn,
                            HospitalId = item.HospitalId,
                            InvoiceItemId = item.InvoiceItemId,
                            IsActive = item.IsActive,
                            ItemQty = item.ItemQty,
                            MedicineId = item.MedicineId,
                            ModifiedBy = item.ModifiedBy,
                            ModifiedOn = item.ModifiedOn,
                            PharmacyId = item.PharmacyId,
                            PharmacyOrderId = item.PharmacyOrderId,
                            PharmacyOrderRequestId = item.PharmacyOrderRequestId,
                            TotalAmount = item.TotalAmount,
                            UnitPrice = item.UnitPrice,
                            MedicineName = medicine?.MedicineName
                        });
                    }
                }
            }

            return pharmacyInvoiceItemDetails;
        }


        private PharmacyInvoice MapToPharmacyInvoice(PharmacyInvoiceDetails invoiceDetails)
        {
            if (invoiceDetails == null)
            {
                throw new ArgumentNullException(nameof(invoiceDetails));
            }

            // Map PharmacyInvoice
            var pharmacyInvoice = new PharmacyInvoice
            {
                InvoiceId = invoiceDetails.InvoiceId ?? Guid.NewGuid(), // Generate a new ID if null
                PharmacyOrderId = invoiceDetails.PharmacyOrderId,
                HospitalId = invoiceDetails.HospitalId,
                PharmacyId = invoiceDetails.PharmacyId,
                PharmacyOrderRequestId = invoiceDetails.PharmacyOrderRequestId,
                InvoiceReference = invoiceDetails.InvoiceReference,
                SubTotal = invoiceDetails.SubTotal,
                TaxAmount = invoiceDetails.TaxAmount,
                DiscountAmount = invoiceDetails.DiscountAmount,
                TotalAmount = invoiceDetails.TotalAmount,
                AmountPaid = invoiceDetails.AmountPaid,
                BalanceDue = invoiceDetails.BalanceDue,
                Status = invoiceDetails.Status,
                Notes = invoiceDetails.Notes,
                CreatedBy = invoiceDetails.CreatedBy,
                CreatedOn = invoiceDetails.CreatedOn,
                ModifiedBy = invoiceDetails.ModifiedBy,
                ModifiedOn = invoiceDetails.ModifiedOn,
                IsActive = invoiceDetails.IsActive
            };

            return pharmacyInvoice;
        }

        private async Task<List<PharmacyInvoiceItem>> MapToPharmacyInvoiceItems(PharmacyInvoiceDetails invoiceDetails, Guid invoiceId)
        {
            if (invoiceDetails == null)
            {
                throw new ArgumentNullException(nameof(invoiceDetails));
            }

            // Map PharmacyInvoiceItem list
            var pharmacyInvoiceItems = invoiceDetails.pharmacyInvoiceItemDetails.Select(itemDetail => new PharmacyInvoiceItem
            {
                InvoiceItemId = Guid.NewGuid(), // Generate a new ID for each item
                InvoiceId = invoiceId,
                PharmacyOrderId = invoiceDetails.PharmacyOrderId,
                HospitalId = invoiceDetails.HospitalId,
                PharmacyId = invoiceDetails.PharmacyId,
                PharmacyOrderRequestId = invoiceDetails.PharmacyOrderRequestId,
                MedicineId = itemDetail.MedicineId,
                ItemQty = itemDetail.ItemQty,
                UnitPrice = itemDetail.UnitPrice,
                TotalAmount = itemDetail.TotalAmount,
                CreatedBy = invoiceDetails.CreatedBy,
                CreatedOn = invoiceDetails.CreatedOn,
                ModifiedBy = invoiceDetails.ModifiedBy,
                ModifiedOn = invoiceDetails.ModifiedOn,
                IsActive = true // Assuming items are active by default
            }).ToList();

            foreach (var item in pharmacyInvoiceItems)
            {
                await _dbContext.pharmacyInvoiceItems.AddAsync(item);
            }
            await _dbContext.SaveChangesAsync();
            return pharmacyInvoiceItems;
        }

    }
}
