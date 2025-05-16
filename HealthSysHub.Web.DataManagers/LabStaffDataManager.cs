using Azure.Core;
using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class LabStaffDataManager : ILabStaffManager
    {
        private readonly ApplicationDBContext _dbContext;
        public LabStaffDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<LabStaff> GetLabStaffByIdAsync(Guid staffId)
        {
            return await _dbContext.labStaff.FindAsync(staffId);
        }

        public async Task<List<LabStaff>> GetLabStaffAsync(Guid? hospitalId, Guid? labId)
        {
            var query = _dbContext.labStaff.AsQueryable();

            if (hospitalId.HasValue)
            {
                query = query.Where(ls => ls.HospitalId == hospitalId.Value);
            }

            if (labId.HasValue)
            {
                query = query.Where(ls => ls.LabId == labId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<LabStaff>> GetLabStaffsAsync()
        {
            return await _dbContext.labStaff.ToListAsync();
        }

        public async Task<LabStaff> InsertOrUpdateLabStaffAsync(LabStaff staff)
        {
            if (staff.StaffId == Guid.Empty)
            {
                // Generate a salted hash for the password
                HealthSysHubHashSalt hashSalt = HealthSysHubHashSalt.GenerateSaltedHash("Admin@2025");

                staff.StaffId = Guid.NewGuid(); // Assign a new GUID for the staff member

                await _dbContext.labStaff.AddAsync(staff);

                // Create a new user associated with the lab staff
                User user = new User()
                {
                    LabId = staff.LabId,
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
                    RoleId = staff.Designation == "Admin" ? Guid.Parse("971ECB66-1CFC-43F2-AB4E-0F352A0F9354") : Guid.Parse("0653CD6C-D536-4E03-B056-49B354775A48"),
                    PasswordHash = hashSalt.Hash,
                    PasswordSalt = hashSalt.Salt
                };

                await _dbContext.users.AddAsync(user);
            }

            else
            {
                var existingStaff = await _dbContext.labStaff.FindAsync(staff.StaffId);

                if (existingStaff != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingStaff, staff, nameof(LabStaff.CreatedBy), nameof(LabStaff.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingStaff, staff, nameof(LabStaff.CreatedBy), nameof(LabStaff.CreatedOn));
                    }
                }
            }

            await _dbContext.SaveChangesAsync();

            return staff;
        }

    }
}
