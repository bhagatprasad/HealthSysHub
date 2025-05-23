﻿using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace HealthSysHub.Web.DataManagers
{
    public class PharmacyOrderRequestDataManager : IPharmacyOrderRequestManager
    {
        private readonly ApplicationDBContext _dbContext;
        public PharmacyOrderRequestDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PharmacyOrderRequestDetails>> GetPharmacyOrderRequestsByPharmacyAsync(Guid pharmacyId)
        {
            return await GetPharmacyOrderRequestsAsync(x => x.PharmacyId == pharmacyId);
        }
        public async Task<PharmacyOrderRequestDetails> GetPharmacyOrderRequestDetailAsync(Guid pharmacyOrderRequestId)
        {
            var result = await GetPharmacyOrderRequestsAsync(x => x.PharmacyOrderRequestId == pharmacyOrderRequestId);
            return result.FirstOrDefault();
        }

        public async Task<List<PharmacyOrderRequestDetails>> GetPharmacyOrderRequestsAsync()
        {
            return await GetPharmacyOrderRequestsAsync(); // no filter
        }
        public async Task<List<PharmacyOrderRequestDetails>> GetPharmacyOrderRequestsByHospitalAsync(Guid hospitalId)
        {
            return await GetPharmacyOrderRequestsAsync(x => x.HospitalId == hospitalId);
        }

        public async Task<List<PharmacyOrderRequestDetails>> GetPharmacyOrderRequestsByPatientAsync(Guid patientId)
        {
            return await GetPharmacyOrderRequestsAsync(x => x.PatientId == patientId);
        }
        public async Task<PharmacyOrderRequestDetails> InsertOrUpdatePharmacyOrderRequestDetailsAsync(PharmacyOrderRequestDetails requestDetails)
        {
            if (requestDetails.PharmacyOrderRequestId == null || requestDetails.PharmacyOrderRequestId == Guid.Empty)
            {
                // Create new pharmacy order request
                PharmacyOrderRequest pharmacyOrderRequest = new PharmacyOrderRequest()
                {
                    PharmacyOrderRequestId = Guid.NewGuid(),
                    CreatedBy = requestDetails.CreatedBy,
                    CreatedOn = requestDetails.CreatedOn,
                    DoctorName = requestDetails.DoctorName,
                    HospitalId = requestDetails.HospitalId,
                    HospitalName = requestDetails.HospitalName,
                    IsActive = requestDetails.IsActive,
                    ModifiedBy = requestDetails.ModifiedBy,
                    ModifiedOn = requestDetails.ModifiedOn,
                    PatientId = requestDetails.PatientId,
                    PatientPrescriptionId = requestDetails.PatientPrescriptionId,
                    Phone = requestDetails.Phone,
                    Status = requestDetails.Status,
                    Notes = requestDetails.Notes
                };

                await _dbContext.pharmacyOrderRequests.AddAsync(pharmacyOrderRequest);
                await _dbContext.SaveChangesAsync();

                // Add pharmacy order request items
                if (requestDetails.pharmacyOrderRequestItemDetails?.Any() == true)
                {
                    foreach (var item in requestDetails.pharmacyOrderRequestItemDetails)
                    {
                        PharmacyOrderRequestItem pharmacyOrderRequestItem = new PharmacyOrderRequestItem()
                        {
                            PharmacyOrderRequestId = pharmacyOrderRequest.PharmacyOrderRequestId,
                            CreatedBy = pharmacyOrderRequest.CreatedBy,
                            CreatedOn = pharmacyOrderRequest.CreatedOn,
                            HospitalId = pharmacyOrderRequest.HospitalId,
                            IsActive = pharmacyOrderRequest.IsActive,
                            ItemQty = item.ItemQty,
                            PharmacyOrderRequestItemId = Guid.NewGuid(),
                            ModifiedBy = pharmacyOrderRequest.ModifiedBy,
                            ModifiedOn = pharmacyOrderRequest.ModifiedOn,
                            MedicineId = item.MedicineId,
                            Usage = item.Usage,
                        };
                        await _dbContext.pharmacyOrderRequestItems.AddAsync(pharmacyOrderRequestItem);
                    }
                    await _dbContext.SaveChangesAsync();
                }

                // Return the newly created request with full details
                return await GetPharmacyOrderRequestDetailsAsync(pharmacyOrderRequest.PharmacyOrderRequestId);
            }
            else
            {
                // Update existing pharmacy order request
                var dbPharmacyOrderRequest = await _dbContext.pharmacyOrderRequests
                    .FindAsync(requestDetails.PharmacyOrderRequestId);

                if (dbPharmacyOrderRequest != null)
                {
                    // Update main request details
                    dbPharmacyOrderRequest.ModifiedBy = requestDetails.ModifiedBy;
                    dbPharmacyOrderRequest.ModifiedOn = requestDetails.ModifiedOn;
                    dbPharmacyOrderRequest.Status = requestDetails.Status;
                    dbPharmacyOrderRequest.Notes = requestDetails.Notes;
                    dbPharmacyOrderRequest.DoctorName = requestDetails.DoctorName;
                    // Get existing items
                    var existingItems = await _dbContext.pharmacyOrderRequestItems
                        .Where(x => x.PharmacyOrderRequestId == dbPharmacyOrderRequest.PharmacyOrderRequestId)
                        .ToListAsync();

                    // Process new/updated items
                    if (requestDetails.pharmacyOrderRequestItemDetails?.Any() == true)
                    {
                        foreach (var item in requestDetails.pharmacyOrderRequestItemDetails)
                        {
                            var existingItem = existingItems.FirstOrDefault(x => x.MedicineId == item.MedicineId);

                            if (existingItem != null)
                            {
                                // Update existing item
                                existingItem.ItemQty = item.ItemQty;
                                existingItem.ModifiedBy = requestDetails.ModifiedBy;
                                existingItem.ModifiedOn = requestDetails.ModifiedOn;
                                existingItem.IsActive = item.IsActive;
                                existingItem.Usage = item.Usage;
                                _dbContext.pharmacyOrderRequestItems.Update(existingItem);
                            }
                            else
                            {
                                // Add new item
                                PharmacyOrderRequestItem newItem = new PharmacyOrderRequestItem()
                                {
                                    PharmacyOrderRequestId = dbPharmacyOrderRequest.PharmacyOrderRequestId,
                                    CreatedBy = requestDetails.ModifiedBy,
                                    CreatedOn = DateTime.UtcNow,
                                    HospitalId = dbPharmacyOrderRequest.HospitalId,
                                    IsActive = true,
                                    ItemQty = item.ItemQty,
                                    PharmacyOrderRequestItemId = Guid.NewGuid(),
                                    ModifiedBy = requestDetails.ModifiedBy,
                                    ModifiedOn = requestDetails.ModifiedOn,
                                    MedicineId = item.MedicineId,
                                    Usage = item.Usage
                                };
                                await _dbContext.pharmacyOrderRequestItems.AddAsync(newItem);
                            }
                        }

                        // Handle items that were removed
                        var itemsToRemove = existingItems
                            .Where(existing => !requestDetails.pharmacyOrderRequestItemDetails
                                .Any(newItem => newItem.MedicineId == existing.MedicineId))
                            .ToList();

                        if (itemsToRemove.Any())
                        {
                            _dbContext.pharmacyOrderRequestItems.RemoveRange(itemsToRemove);
                        }
                    }

                    await _dbContext.SaveChangesAsync();
                }

                return await GetPharmacyOrderRequestDetailsAsync(requestDetails.PharmacyOrderRequestId.Value);
            }
        }

        private async Task<PharmacyOrderRequestDetails> GetPharmacyOrderRequestDetailsAsync(Guid pharmacyOrderRequestId)
        {
            PharmacyOrderRequestDetails requestDetails = new PharmacyOrderRequestDetails();

            // Get the main request
            var pharmacyRequest = await _dbContext.pharmacyOrderRequests
                .FirstOrDefaultAsync(r => r.PharmacyOrderRequestId == pharmacyOrderRequestId);

            if (pharmacyRequest != null)
            {
                // Map all properties from PharmacyOrderRequest to PharmacyOrderRequestDetails
                requestDetails.PharmacyOrderRequestId = pharmacyRequest.PharmacyOrderRequestId;
                requestDetails.PatientPrescriptionId = pharmacyRequest.PatientPrescriptionId;
                requestDetails.HospitalId = pharmacyRequest.HospitalId;
                requestDetails.PatientId = pharmacyRequest.PatientId;
                requestDetails.HospitalName = pharmacyRequest.HospitalName;
                requestDetails.DoctorName = pharmacyRequest.DoctorName;
                requestDetails.Phone = pharmacyRequest.Phone;
                requestDetails.Notes = pharmacyRequest.Notes;
                requestDetails.Status = pharmacyRequest.Status;
                requestDetails.CreatedBy = pharmacyRequest.CreatedBy;
                requestDetails.CreatedOn = pharmacyRequest.CreatedOn;
                requestDetails.ModifiedBy = pharmacyRequest.ModifiedBy;
                requestDetails.ModifiedOn = pharmacyRequest.ModifiedOn;
                requestDetails.IsActive = pharmacyRequest.IsActive;

                // Get and map items
                var pharmacyRequestItems = await _dbContext.pharmacyOrderRequestItems
                    .Where(x => x.PharmacyOrderRequestId == pharmacyRequest.PharmacyOrderRequestId)
                    .ToListAsync();

                if (pharmacyRequestItems?.Any() == true)
                {
                    // Get all medicine IDs for lookup
                    var medicineIds = pharmacyRequestItems.Select(x => x.MedicineId).Distinct().ToList();

                    // Fetch all needed medicines in one query
                    var medicines = await _dbContext.medicines
                        .Where(m => medicineIds.Contains(m.MedicineId))
                        .ToDictionaryAsync(m => m.MedicineId, m => m.MedicineName);

                    requestDetails.pharmacyOrderRequestItemDetails = pharmacyRequestItems.Select(item =>
                    {
                        // Try to get medicine name
                        medicines.TryGetValue(item.MedicineId.Value, out var medicineName);

                        return new PharmacyOrderRequestItemDetails
                        {
                            PharmacyOrderRequestItemId = item.PharmacyOrderRequestItemId,
                            MedicineName = medicineName,
                            MedicineId = item.MedicineId,
                            ItemQty = item.ItemQty,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = item.CreatedOn,
                            ModifiedBy = item.ModifiedBy,
                            ModifiedOn = item.ModifiedOn,
                            IsActive = item.IsActive,
                            Usage = item.Usage,
                        };
                    }).ToList();
                }
            }

            return requestDetails;
        }

        private async Task<List<PharmacyOrderRequestDetails>> GetPharmacyOrderRequestsAsync(Expression<Func<PharmacyOrderRequest, bool>> filter = null)
        {
            var pharmacyOrderRequestDetails = new List<PharmacyOrderRequestDetails>();

            try
            {
                var medicines = await _dbContext.pharmacyMedicines.ToListAsync();

                var requestsQuery = _dbContext.pharmacyOrderRequests.AsQueryable();

                if (filter != null)
                {
                    requestsQuery = requestsQuery.Where(filter);
                }

                var requests = await requestsQuery.ToListAsync();

                if (!requests.Any())
                    return pharmacyOrderRequestDetails;

                var requestIds = requests.Select(r => r.PharmacyOrderRequestId).ToList();

                var requestItems = await _dbContext.pharmacyOrderRequestItems
                    .Where(x => x.PharmacyOrderRequestId != null && requestIds.Contains(x.PharmacyOrderRequestId.Value))
                    .ToListAsync();

                foreach (var request in requests)
                {
                    var items = requestItems
                        .Where(x => x.PharmacyOrderRequestId == request.PharmacyOrderRequestId)
                        .Select(item => new PharmacyOrderRequestItemDetails
                        {
                            CreatedBy = item.CreatedBy,
                            CreatedOn = item.CreatedOn,
                            HospitalId = item.HospitalId,
                            IsActive = item.IsActive,
                            ItemQty = item.ItemQty,
                            MedicineId = item.MedicineId,
                            MedicineName = medicines.FirstOrDefault(m => m.MedicineId == item.MedicineId)?.MedicineName,
                            ModifiedBy = item.ModifiedBy,
                            ModifiedOn = item.ModifiedOn,
                            PharmacyId = item.PharmacyId,
                            PharmacyOrderRequestItemId = item.PharmacyOrderRequestItemId,
                            Usage = item.Usage
                        })
                        .ToList();

                    pharmacyOrderRequestDetails.Add(new PharmacyOrderRequestDetails
                    {
                        CreatedBy = request.CreatedBy,
                        CreatedOn = request.CreatedOn,
                        DoctorName = request.DoctorName,
                        HospitalId = request.HospitalId,
                        HospitalName = request.HospitalName,
                        IsActive = request.IsActive,
                        ModifiedBy = request.ModifiedBy,
                        ModifiedOn = request.ModifiedOn,
                        Name = request.Name,
                        Notes = request.Notes,
                        PatientId = request.PatientId,
                        PatientPrescriptionId = request.PatientPrescriptionId,
                        PharmacyId = request.PharmacyId,
                        PharmacyOrderRequestId = request.PharmacyOrderRequestId,
                        Phone = request.Phone,
                        Status = request.Status,
                        pharmacyOrderRequestItemDetails = items
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return pharmacyOrderRequestDetails;
        }

    }
}
