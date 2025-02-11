using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class StaffDataManager : IStaffManager
    {
        private readonly ApplicationDBContext _dbContext;

        public StaffDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<HospitalStaff>> GetAllHospitalStaffAsync(Guid hosptialId)
        {
            return await _dbContext.hospitalStaffs.Where(x => x.HospitalId == hosptialId).ToListAsync();
        }

        public async Task<HospitalStaff> GetHospitalStaffAsync(Guid hosptialId, Guid staffId)
        {
            return await _dbContext.hospitalStaffs.Where(x => x.HospitalId == hosptialId && x.StaffId == staffId).FirstOrDefaultAsync();
        }

        public async Task<HospitalStaff> InsertOrUpdateHospitalStaffAsync(HospitalStaff hospitalStaff)
        {
            if (hospitalStaff.StaffId == Guid.Empty)
            {
                HealthSysHubHashSalt hashSalt = HealthSysHubHashSalt.GenerateSaltedHash("Admin@2025");

                hospitalStaff.StaffId = Guid.NewGuid();

                await _dbContext.hospitalStaffs.AddAsync(hospitalStaff);

                User huser = new User()
                {
                    HospitalId = hospitalStaff.HospitalId,
                    FirstName = hospitalStaff.FirstName,
                    LastName = hospitalStaff.LastName,
                    Email = hospitalStaff.Email,
                    Phone = hospitalStaff.Phone,
                    StaffId = hospitalStaff.StaffId,
                    IsActive = true,
                    CreatedBy = hospitalStaff.CreatedBy,
                    CreatedOn = DateTimeOffset.UtcNow,
                    IsBlocked = false,
                    LastPasswordChangedOn = DateTimeOffset.Now,
                    ModifiedBy = hospitalStaff.ModifiedBy,
                    ModifiedOn = hospitalStaff.ModifiedOn,
                    RoleId = hospitalStaff.RoleId,
                    PasswordHash = hashSalt.Hash,
                    PasswordSalt = hashSalt.Salt
                };
                await _dbContext.users.AddAsync(huser);
            }
            else
            {

                var existingStaff = await _dbContext.hospitalStaffs.FindAsync(hospitalStaff.StaffId);

                if (existingStaff != null)
                {

                    bool hasChanges = EntityUpdater.HasChanges(existingStaff, hospitalStaff, nameof(HospitalStaff.CreatedBy), nameof(HospitalStaff.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingStaff, hospitalStaff, nameof(Hospital.CreatedBy), nameof(HospitalStaff.CreatedOn));
                    }
                }
            }

            await _dbContext.SaveChangesAsync();

            return hospitalStaff;
        }
    }
}
