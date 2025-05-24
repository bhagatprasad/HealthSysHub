using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class PharmacyStaffDataManager : IPharmacyStaffManager
    {
        private readonly ApplicationDBContext _dbContext;

        public PharmacyStaffDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PharmacyStaff> GetPharmacyStaffByIdAsync(Guid staffId)
        {
            return await _dbContext.pharmacyStaff.FindAsync(staffId);
        }

        public async Task<List<PharmacyStaff>> GetPharmacyStaffAsync(Guid? hospitalId, Guid? pharmacyId)
        {
            var query = _dbContext.pharmacyStaff.AsQueryable();

            if (hospitalId.HasValue)
            {
                query = query.Where(ps => ps.HospitalId == hospitalId.Value);
            }

            if (pharmacyId.HasValue)
            {
                query = query.Where(ps => ps.PharmacyId == pharmacyId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<PharmacyStaff>> GetPharmacyStaffsAsync()
        {
            return await _dbContext.pharmacyStaff.ToListAsync();
        }

        public async Task<PharmacyStaff> InsertOrUpdatePharmacyStaffAsync(PharmacyStaff staff)
        {
            if (staff.StaffId == Guid.Empty)
            {
                // Generate a salted hash for the password
                HealthSysHubHashSalt hashSalt = HealthSysHubHashSalt.GenerateSaltedHash("Admin@2025");

                staff.StaffId = Guid.NewGuid(); // Assign a new GUID for the staff member

                await _dbContext.pharmacyStaff.AddAsync(staff);

                // Create a new user associated with the pharmacy staff
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
            }
            else
            {
                var existingStaff = await _dbContext.pharmacyStaff.FindAsync(staff.StaffId);

                if (existingStaff != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingStaff, staff, nameof(PharmacyStaff.CreatedBy), nameof(PharmacyStaff.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingStaff, staff, nameof(PharmacyStaff.CreatedBy), nameof(PharmacyStaff.CreatedOn));
                    }
                }

                var exisitngUser = await _dbContext.users.Where(x => x.StaffId == staff.StaffId).FirstOrDefaultAsync();
                if (exisitngUser != null)
                {
                    exisitngUser.FirstName = staff.FirstName;
                    exisitngUser.LastName = staff.LastName;
                    exisitngUser.Phone = staff.PhoneNumber;
                    exisitngUser.Email = staff.Email;
                    exisitngUser.IsActive = staff.IsActive;
                    exisitngUser.PharmacyId = staff.PharmacyId;
                    exisitngUser.HospitalId = staff.HospitalId;
                    exisitngUser.ModifiedBy = staff.ModifiedBy;
                    exisitngUser.ModifiedOn = staff.ModifiedOn;
                }
            }

            await _dbContext.SaveChangesAsync();

            return staff;
        }
    }

}
