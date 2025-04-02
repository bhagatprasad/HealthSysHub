using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;
namespace HealthSysHub.Web.DataManagers
{
    public class DoctorDataManager : IDoctorManager
    {
        private readonly ApplicationDBContext _dbContext;

        public DoctorDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Doctor> GetDoctorByIdAsync(Guid doctorId)
        {
            return await _dbContext.doctors.FindAsync(doctorId);
        }

        public async Task<List<Doctor>> GetDoctorsAsync(Guid hospitalId)
        {
            return await _dbContext.doctors.Where(x => x.HospitalId == hospitalId).ToListAsync();
        }

        public async Task<Doctor> InsertOrUpdateDoctorAsync(Doctor doctor)
        {
            if (doctor.DoctorId == Guid.Empty)
            {
                await _dbContext.doctors.AddAsync(doctor);
            }
            else
            {
                var existingDoctor = await _dbContext.doctors.FindAsync(doctor.DoctorId);

                if (existingDoctor != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingDoctor, doctor, nameof(Doctor.CreatedBy), nameof(Doctor.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingDoctor, doctor, nameof(Doctor.CreatedBy), nameof(Doctor.CreatedOn));
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
            return doctor;
        }
    }
}
