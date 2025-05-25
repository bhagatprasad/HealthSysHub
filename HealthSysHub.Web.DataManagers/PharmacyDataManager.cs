using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HealthSysHub.Web.DataManagers
{
    public class PharmacyDataManager : IPharmacyManager
    {
        private readonly ApplicationDBContext _dbContext;

        public PharmacyDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Pharmacy> GetPharmacyByIdAsync(Guid pharmacyId)
        {
            return await _dbContext.pharmacies.FindAsync(pharmacyId);
        }

        public async Task<List<Pharmacy>> GetPharmaciesAsync()
        {
            return await _dbContext.pharmacies.ToListAsync();
        }

        public async Task<List<Pharmacy>> GetPharmaciesAsync(string searchString)
        {
            var query = _dbContext.pharmacies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(p =>
                    p.PharmacyName != null && p.PharmacyName.Contains(searchString) ||
                    p.Address != null && p.Address.Contains(searchString) ||
                    p.City != null && p.City.Contains(searchString) ||
                    p.State != null && p.State.Contains(searchString) ||
                    p.Country != null && p.Country.Contains(searchString) ||
                    p.PostalCode != null && p.PostalCode.Contains(searchString) ||
                    p.PhoneNumber != null && p.PhoneNumber.Contains(searchString) ||
                    p.Email != null && p.Email.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task<Pharmacy> InsertOrUpdatePharmacyAsync(Pharmacy pharmacy)
        {
            if (pharmacy.PharmacyId == Guid.Empty)
            {
                pharmacy.PharmacyId = Guid.NewGuid();

                await _dbContext.pharmacies.AddAsync(pharmacy);
                await _dbContext.SaveChangesAsync();

                Subscription subscription = new Subscription()
                {
                    IsActive = true,
                    HospitalId = pharmacy.HospitalId,
                    PharmacyId = pharmacy.PharmacyId,
                    CreatedBy = pharmacy.CreatedBy,
                    CreatedOn = pharmacy.CreatedOn,
                    FromDate = DateTimeOffset.Now,
                    ToDate = DateTimeOffset.Now.AddDays(180),
                    ModifiedBy = pharmacy.ModifiedBy,
                    ModifiedOn = pharmacy.ModifiedOn,
                    Status = "Active",
                    SubscriptionType = "Free",
                };

                await _dbContext.subscriptions.AddAsync(subscription);
                await _dbContext.SaveChangesAsync();

                PharmacyStaff staff = new PharmacyStaff()
                {
                    Designation = "Admin",
                    Email = "admin@" + pharmacy.PharmacyCode + ".com",
                    CreatedBy = pharmacy.CreatedBy,
                    CreatedOn = pharmacy.CreatedOn,
                    ModifiedBy = pharmacy.ModifiedBy,
                    ModifiedOn = pharmacy.ModifiedOn,
                    IsActive = true,
                    FirstName = "Admin",
                    LastName = pharmacy.PharmacyCode,
                    PharmacyId = pharmacy.PharmacyId,
                    HospitalId = pharmacy.HospitalId,
                    PhoneNumber = pharmacy.PhoneNumber,
                    StaffId = Guid.NewGuid(),
                };
                await _dbContext.pharmacyStaff.AddAsync(staff);

                await _dbContext.SaveChangesAsync();
                // Generate a salted hash for the password
                HealthSysHubHashSalt hashSalt = HealthSysHubHashSalt.GenerateSaltedHash("Admin@2025");

                User user = new User()
                {
                    PharmacyId = staff.PharmacyId,
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    Email = staff.Email,
                    Phone = staff.PhoneNumber,
                    StaffId = staff.StaffId,
                    HospitalId = staff.HospitalId,
                    IsActive = true,
                    CreatedBy = staff.CreatedBy,
                    CreatedOn = DateTimeOffset.UtcNow,
                    IsBlocked = false,
                    LastPasswordChangedOn = DateTimeOffset.Now,
                    ModifiedBy = staff.ModifiedBy,
                    ModifiedOn = staff.ModifiedOn,
                    RoleId = staff.Designation == "Admin" ? Guid.Parse("971ECB66-1CFC-43F2-AB4E-0F352A0F9354") : Guid.Parse("F5B853E9-2409-4EDE-ACEC-4EA14C766784"),
                    PasswordHash = hashSalt.Hash,
                    PasswordSalt = hashSalt.Salt
                };

                await _dbContext.users.AddAsync(user);

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                var existingPharmacy = await _dbContext.pharmacies.FindAsync(pharmacy.PharmacyId);

                if (existingPharmacy != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingPharmacy, pharmacy, nameof(Pharmacy.CreatedBy), nameof(Pharmacy.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingPharmacy, pharmacy, nameof(Pharmacy.CreatedBy), nameof(Pharmacy.CreatedOn));
                    }
                }
            }
            await _dbContext.SaveChangesAsync();

            return pharmacy;
        }
    }


}
