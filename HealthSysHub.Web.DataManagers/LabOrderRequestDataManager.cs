using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class LabOrderRequestDataManager : ILabOrderRequestManager
    {
        private readonly ApplicationDBContext _dbContext;
        public LabOrderRequestDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<LabOrderRequestDetails> InsertOrUpdateLabOrderRequestAsync(LabOrderRequestDetails labOrderRequestDetails)
        {
            if (labOrderRequestDetails.LabOrderRequestId == null || labOrderRequestDetails.LabOrderRequestId == Guid.Empty)
            {
                // Create new lab order request
                LabOrderRequest labOrderRequest = new LabOrderRequest()
                {
                    LabOrderRequestId = Guid.NewGuid(),
                    CreatedBy = labOrderRequestDetails.CreatedBy,
                    CreatedOn = labOrderRequestDetails.CreatedOn,
                    DoctorName = labOrderRequestDetails.DoctorName,
                    HospitalId = labOrderRequestDetails.HospitalId,
                    HospitalName = labOrderRequestDetails.HospitalName,
                    IsActive = labOrderRequestDetails.IsActive,
                    ModifiedBy = labOrderRequestDetails.ModifiedBy,
                    ModifiedOn = labOrderRequestDetails.ModifiedOn,
                    Name = labOrderRequestDetails.Name,
                    Notes = labOrderRequestDetails.Notes,
                    PatientId = labOrderRequestDetails.PatientId,
                    PatientPrescriptionId = labOrderRequestDetails.PatientPrescriptionId,
                    Phone = labOrderRequestDetails.Phone,
                    Status = labOrderRequestDetails.Status
                };

                await _dbContext.labOrderRequests.AddAsync(labOrderRequest);
                await _dbContext.SaveChangesAsync();

                // Add lab order request items
                if (labOrderRequestDetails.labOrderRequestItemDetails?.Any() == true)
                {
                    foreach (var item in labOrderRequestDetails.labOrderRequestItemDetails)
                    {
                        LabOrderRequestItem labOrderRequestItem = new LabOrderRequestItem()
                        {
                            LabOrderRequestId = labOrderRequest.LabOrderRequestId,
                            CreatedBy = labOrderRequest.CreatedBy,
                            CreatedOn = labOrderRequest.CreatedOn,
                            HospitalId = labOrderRequest.HospitalId,
                            IsActive = labOrderRequest.IsActive,
                            ItemQty = item.ItemQty,
                            LabOrderRequestItemId = Guid.NewGuid(),
                            ModifiedBy = labOrderRequest.ModifiedBy,
                            ModifiedOn = labOrderRequest.ModifiedOn,
                            TestId = item.TestId,
                        };
                        await _dbContext.labOrderRequestItems.AddAsync(labOrderRequestItem);
                    }
                    await _dbContext.SaveChangesAsync();
                }

                // Return the newly created request with updated IDs
                labOrderRequestDetails.LabOrderRequestId = labOrderRequest.LabOrderRequestId;
                return await GetLabOrderRequestDetailsAsync(labOrderRequest.LabOrderRequestId);
            }
            else
            {
                // Update existing lab order request
                var dbLabOrderRequest = await _dbContext.labOrderRequests
                    .FindAsync(labOrderRequestDetails.LabOrderRequestId);

                if (dbLabOrderRequest != null)
                {
                    // Update main request details
                    dbLabOrderRequest.ModifiedBy = labOrderRequestDetails.ModifiedBy;
                    dbLabOrderRequest.ModifiedOn = labOrderRequestDetails.ModifiedOn;
                    dbLabOrderRequest.Status = labOrderRequestDetails.Status;
                    dbLabOrderRequest.Notes = labOrderRequestDetails.Notes;

                    // Get existing items
                    var existingItems = await _dbContext.labOrderRequestItems
                        .Where(x => x.LabOrderRequestId == dbLabOrderRequest.LabOrderRequestId)
                        .ToListAsync();

                    // Process new/updated items
                    if (labOrderRequestDetails.labOrderRequestItemDetails?.Any() == true)
                    {
                        foreach (var item in labOrderRequestDetails.labOrderRequestItemDetails)
                        {
                            var existingItem = existingItems.FirstOrDefault(x => x.TestId == item.TestId);

                            if (existingItem != null)
                            {
                                // Update existing item
                                existingItem.ItemQty = item.ItemQty;
                                existingItem.ModifiedBy = labOrderRequestDetails.ModifiedBy;
                                existingItem.ModifiedOn = labOrderRequestDetails.ModifiedOn;
                                existingItem.IsActive = item.IsActive;
                                _dbContext.labOrderRequestItems.Update(existingItem);
                            }
                            else
                            {
                                // Add new item
                                LabOrderRequestItem newItem = new LabOrderRequestItem()
                                {
                                    LabOrderRequestId = dbLabOrderRequest.LabOrderRequestId,
                                    CreatedBy = labOrderRequestDetails.ModifiedBy, // Using ModifiedBy since this is an update
                                    CreatedOn = DateTime.UtcNow,
                                    HospitalId = dbLabOrderRequest.HospitalId,
                                    IsActive = true,
                                    ItemQty = item.ItemQty,
                                    LabOrderRequestItemId = Guid.NewGuid(),
                                    ModifiedBy = labOrderRequestDetails.ModifiedBy,
                                    ModifiedOn = labOrderRequestDetails.ModifiedOn,
                                    TestId = item.TestId,
                                };
                                await _dbContext.labOrderRequestItems.AddAsync(newItem);
                            }
                        }

                        // Handle items that were removed
                        var itemsToRemove = existingItems
                            .Where(existing => !labOrderRequestDetails.labOrderRequestItemDetails
                                .Any(newItem => newItem.TestId == existing.TestId))
                            .ToList();

                        if (itemsToRemove.Any())
                        {
                            _dbContext.labOrderRequestItems.RemoveRange(itemsToRemove);
                        }
                    }

                    await _dbContext.SaveChangesAsync();
                }

                return await GetLabOrderRequestDetailsAsync(labOrderRequestDetails.LabOrderRequestId.Value);
            }
        }

        private async Task<LabOrderRequestDetails> GetLabOrderRequestDetailsAsync(Guid labOrderRequestId)
        {
            LabOrderRequestDetails labOrderRequestDetails = new LabOrderRequestDetails();

            // Get the main request with its items in a single query
            var labRequest = await _dbContext.labOrderRequests.FirstOrDefaultAsync(r => r.LabOrderRequestId == labOrderRequestId);


            if (labRequest != null)
            {
                // Map all properties from LabOrderRequest to LabOrderRequestDetails
                labOrderRequestDetails.LabOrderRequestId = labRequest.LabOrderRequestId;
                labOrderRequestDetails.PatientPrescriptionId = labRequest.PatientPrescriptionId;
                labOrderRequestDetails.HospitalId = labRequest.HospitalId;
                labOrderRequestDetails.PatientId = labRequest.PatientId;
                labOrderRequestDetails.HospitalName = labRequest.HospitalName;
                labOrderRequestDetails.DoctorName = labRequest.DoctorName;
                labOrderRequestDetails.Name = labRequest.Name;
                labOrderRequestDetails.Phone = labRequest.Phone;
                labOrderRequestDetails.Notes = labRequest.Notes;
                labOrderRequestDetails.Status = labRequest.Status;
                labOrderRequestDetails.CreatedBy = labRequest.CreatedBy;
                labOrderRequestDetails.CreatedOn = labRequest.CreatedOn;
                labOrderRequestDetails.ModifiedBy = labRequest.ModifiedBy;
                labOrderRequestDetails.ModifiedOn = labRequest.ModifiedOn;
                labOrderRequestDetails.IsActive = labRequest.IsActive;

                var labRequestItems = await _dbContext.labOrderRequestItems.Where(x => x.LabOrderRequestId == labRequest.LabOrderRequestId).ToListAsync();
                // Map the related items if they exist
                if (labRequestItems?.Any() == true)
                {
                    // First get all test IDs we need to look up
                    var testIds = labRequestItems.Select(x => x.TestId).Distinct().ToList();

                    // Fetch all needed tests in one query
                    var tests = await _dbContext.labTests
                        .Where(t => testIds.Contains(t.TestId))
                        .ToDictionaryAsync(t => t.TestId, t => t.TestName);

                    labOrderRequestDetails.labOrderRequestItemDetails = labRequestItems.Select(item =>
                    {
                        // Try to get test name, fallback to null if not found
                        tests.TryGetValue(item.TestId.Value, out var testName);

                        return new LabOrderRequestItemDetails
                        {
                            LabOrderRequestItemId = item.LabOrderRequestItemId,
                            TestName = testName,
                            TestId = item.TestId,
                            ItemQty = item.ItemQty,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = item.CreatedOn,
                            ModifiedBy = item.ModifiedBy,
                            ModifiedOn = item.ModifiedOn,
                            IsActive = item.IsActive,
                        };
                    }).ToList();
                }
            }

            return labOrderRequestDetails;
        }
    }
}
