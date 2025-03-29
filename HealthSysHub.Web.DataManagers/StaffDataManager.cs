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

        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            return await _dbContext.doctors.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<List<Doctor>> GetDoctorsAsync(Guid hospitalId)
        {
            return await _dbContext.doctors.Where(x => x.IsActive == true && x.HospitalId == hospitalId).ToListAsync();
        }

        public async Task<Doctor> GetDoctorsAsync(Guid hospitalId, Guid doctorId)
        {
            return await _dbContext.doctors.Where(x => x.IsActive == true && x.HospitalId == hospitalId && x.DoctorId == doctorId).FirstOrDefaultAsync();
        }

        public async Task<HospitalStaff> GetHospitalStaffAsync(Guid hosptialId, Guid staffId)
        {
            return await _dbContext.hospitalStaffs.Where(x => x.HospitalId == hosptialId && x.StaffId == staffId).FirstOrDefaultAsync();
        }

        public async Task<List<Nurse>> GetNursesAsync()
        {
            return await _dbContext.nurses.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<List<Nurse>> GetNursesAsync(Guid hospitalId)
        {
            return await _dbContext.nurses.Where(x => x.IsActive == true && x.HospitalId == hospitalId).ToListAsync();
        }

        public async Task<Nurse> GetNurseAsync(Guid hospitalId, Guid nurseId)
        {
            return await _dbContext.nurses.Where(x => x.IsActive == true && x.HospitalId == hospitalId && x.NurseId == nurseId).FirstOrDefaultAsync();
        }

        public async Task<List<Pharmacist>> GetPharmacistsAsync()
        {
            return await _dbContext.pharmacists.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<List<Pharmacist>> GetPharmacistsAsync(Guid hospitalId)
        {
            return await _dbContext.pharmacists.Where(x => x.IsActive == true && x.HospitalId == hospitalId).ToListAsync();
        }

        public async Task<Pharmacist> GetPharmacistAsync(Guid hospitalId, Guid pharmacistId)
        {
            return await _dbContext.pharmacists.Where(x => x.IsActive == true && x.HospitalId == hospitalId && x.PharmacistId == pharmacistId).FirstOrDefaultAsync();
        }

        public async Task<Doctor> InsertOrUpdateDoctorsAsync(Doctor doctor)
        {
            if (doctor.DoctorId == Guid.Empty || doctor.DoctorId == null)
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
                else
                {
                    await _dbContext.doctors.AddAsync(doctor);
                }
            }
            await _dbContext.SaveChangesAsync();

            return doctor;
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

                //handling adding to doctors .nurses, pharmasist etc 
                switch (hospitalStaff.Designation)
                {
                    case "Doctor":
                        Doctor doctor = new Doctor()
                        {
                            HospitalId = hospitalStaff.HospitalId,
                            FullName = hospitalStaff.FirstName + " " + hospitalStaff.LastName,
                            Email = hospitalStaff.Email,
                            PhoneNumber = hospitalStaff.Phone,
                            StaffId = hospitalStaff.StaffId,
                            IsActive = true,
                            CreatedBy = hospitalStaff.CreatedBy,
                            CreatedOn = DateTimeOffset.UtcNow,
                        };
                        await _dbContext.doctors.AddAsync(doctor);
                        break;
                    case "Nurse":
                        Nurse nurse = new Nurse()
                        {
                            FullName = hospitalStaff.FirstName + " " + hospitalStaff.LastName,
                            Email = hospitalStaff.Email,
                            HospitalId = hospitalStaff.HospitalId,
                            PhoneNumber = hospitalStaff.Phone,
                            StaffId = hospitalStaff.StaffId,
                            IsActive = true,
                            CreatedBy = hospitalStaff.CreatedBy,
                            CreatedOn = DateTimeOffset.UtcNow,
                        };
                        await _dbContext.nurses.AddAsync(nurse);
                        break;
                    case "Pharmacist":
                        Pharmacist pharmacist = new Pharmacist()
                        {
                            FullName = hospitalStaff.FirstName + " " + hospitalStaff.LastName,
                            Email = hospitalStaff.Email,
                            PhoneNumber = hospitalStaff.Phone,
                            StaffId = hospitalStaff.StaffId,
                            HospitalId = hospitalStaff.HospitalId,
                            IsActive = true,
                            CreatedBy = hospitalStaff.CreatedBy,
                            CreatedOn = DateTimeOffset.UtcNow,
                        };
                        await _dbContext.pharmacists.AddAsync(pharmacist);
                        break;
                    case "Receptionist":
                        Receptionist receptionist = new Receptionist()
                        {
                            HospitalId = hospitalStaff.HospitalId,
                            FullName = hospitalStaff.FirstName + " " + hospitalStaff.LastName,
                            Email = hospitalStaff.Email,
                            PhoneNumber = hospitalStaff.Phone,
                            StaffId = hospitalStaff.StaffId,
                            IsActive = true,
                            CreatedBy = hospitalStaff.CreatedBy,
                            CreatedOn = DateTimeOffset.UtcNow,
                        };
                        await _dbContext.receptionists.AddAsync(receptionist);
                        break;

                    case "PatientCare":
                        PatientCare patientCare = new PatientCare()
                        {
                            HospitalId = hospitalStaff.HospitalId,
                            FullName = hospitalStaff.FirstName + " " + hospitalStaff.LastName,
                            Email = hospitalStaff.Email,
                            PhoneNumber = hospitalStaff.Phone,
                            StaffId = hospitalStaff.StaffId,
                            IsActive = true,
                            CreatedBy = hospitalStaff.CreatedBy,
                            CreatedOn = DateTimeOffset.UtcNow,
                        };
                        await _dbContext.patientCares.AddAsync(patientCare);
                        break;
                }
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

        public async Task<Nurse> InsertOrUpdateNurseAsync(Nurse nurse)
        {
            if (nurse.NurseId == Guid.Empty || nurse.NurseId == null)
            {
                await _dbContext.nurses.AddAsync(nurse);
            }
            else
            {
                var existingNurse = await _dbContext.nurses.FindAsync(nurse.NurseId);

                if (existingNurse != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingNurse, nurse, nameof(Nurse.CreatedBy), nameof(Nurse.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingNurse, nurse, nameof(Nurse.CreatedBy), nameof(Nurse.CreatedOn));
                    }
                }
                else
                {
                    await _dbContext.nurses.AddAsync(nurse);
                }
            }
            await _dbContext.SaveChangesAsync();

            return nurse;
        }

        public async Task<Pharmacist> InsertOrUpdatePharmacistAsync(Pharmacist pharmacist)
        {
            if (pharmacist.PharmacistId == Guid.Empty || pharmacist.PharmacistId == null)
            {
                await _dbContext.pharmacists.AddAsync(pharmacist);
            }
            else
            {
                var existingPharmacist = await _dbContext.pharmacists.FindAsync(pharmacist.PharmacistId);

                if (existingPharmacist != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingPharmacist, pharmacist, nameof(Pharmacist.CreatedBy), nameof(Pharmacist.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingPharmacist, pharmacist, nameof(Pharmacist.CreatedBy), nameof(Pharmacist.CreatedOn));
                    }
                }
                else
                {
                    await _dbContext.pharmacists.AddAsync(pharmacist);
                }
            }
            await _dbContext.SaveChangesAsync();

            return pharmacist;
        }
    }
}
