using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.DataManagers
{
    public class PatientDataManager : IPatientManager
    {
        private readonly ApplicationDBContext _dbContext;
        public PatientDataManager(ApplicationDBContext dbContext) 
        {
            this._dbContext = dbContext;
        }
        public async Task<Patient> GetPatientByIdAsync(Guid patientId)
        {
            return await _dbContext.patients.FindAsync(patientId);
        }

        public async Task<List<Patient>> GetPatientByIdHospitalAsync(Guid hospitalId)
        {
            return await _dbContext.patients.Where(x => x.HospitalId == hospitalId && x.IsActive).ToListAsync();
        }

        public async Task<List<Patient>> GetPatientsAsync()
        {
            return await _dbContext.patients.ToListAsync();
        }

        public async Task<Patient> InsertOrUpdatePatientAsync(Patient patient)
        {
            if (patient.PatientId == Guid.Empty)
            {
                await _dbContext.patients.AddAsync(patient);
            }
            else
            {
                var existingPatient = await _dbContext.patients.FindAsync(patient.PatientId);
                if (existingPatient != null)
                {
                    var hasChanges = EntityUpdater.HasChanges(existingPatient, patient, nameof(Patient.CreatedBy), nameof(Patient.CreatedOn));
                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingPatient, patient, nameof(Patient.CreatedBy), nameof(Patient.CreatedOn));
                    }
                }
            }
            await _dbContext.SaveChangesAsync();
            return patient;
        }
    }
}
