using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.EntityFrameworkCore;


namespace HealthSysHub.Web.DataManagers
{

    public class InpatientVitalSignsDataManager : IInpatientVitalSignsManager
    {
        private readonly ApplicationDBContext _dbContext;

        public InpatientVitalSignsDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteVitalSignAsync(Guid vitalSignId)
        {
            var vitalSign = await _dbContext.inpatientVitalSigns.FindAsync(vitalSignId);
            if (vitalSign != null)
            {
                _dbContext.inpatientVitalSigns.Remove(vitalSign);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<InpatientVitalSigns>> GetAllVitalSignsAsync()
        {
            return await _dbContext.inpatientVitalSigns.ToListAsync();
        }

        public async Task<List<InpatientVitalSigns>> GetVitalSignsByInpatientIdAsync(Guid inpatientId)
        {
            return await _dbContext.inpatientVitalSigns
                .Where(vs => vs.InpatientId == inpatientId)
                .ToListAsync();
        }

        public async Task<InpatientVitalSigns> GetVitalSignByIdAsync(Guid vitalSignId)
        {
            return await _dbContext.inpatientVitalSigns
                .FirstOrDefaultAsync(vs => vs.VitalSignId == vitalSignId);
        }

        public async Task<InpatientVitalSigns> InsertOrUpdateVitalSignAsync(InpatientVitalSigns vitalSign)
        {
            if (vitalSign.VitalSignId == Guid.Empty)
            {
                // Insert new vital sign
                await _dbContext.inpatientVitalSigns.AddAsync(vitalSign);
            }
            else
            {
                // Update existing vital sign
                var existingVitalSign = await _dbContext.inpatientVitalSigns.FindAsync(vitalSign.VitalSignId);
                if (existingVitalSign != null)
                {
                    // Update properties
                    existingVitalSign.InpatientId = vitalSign.InpatientId;
                    existingVitalSign.RecordedBy = vitalSign.RecordedBy;
                    existingVitalSign.RecordedOn = vitalSign.RecordedOn;
                    existingVitalSign.Temperature = vitalSign.Temperature;
                    existingVitalSign.BloodPressure = vitalSign.BloodPressure;
                    existingVitalSign.PulseRate = vitalSign.PulseRate;
                    existingVitalSign.RespiratoryRate = vitalSign.RespiratoryRate;
                    existingVitalSign.OxygenSaturation = vitalSign.OxygenSaturation;
                    existingVitalSign.Height = vitalSign.Height;
                    existingVitalSign.Weight = vitalSign.Weight;
                    existingVitalSign.Notes = vitalSign.Notes;
                }
            }

            await _dbContext.SaveChangesAsync();
            return vitalSign;
        }

        public async Task<int> GetVitalSignCountAsync()
        {
            return await _dbContext.inpatientVitalSigns.CountAsync();
        }
    }

}
