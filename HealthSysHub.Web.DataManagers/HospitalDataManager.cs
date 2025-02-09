using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HealthSysHub.Web.DataManagers
{
    public class HospitalDataManager : IHospitalManager
    {
        private readonly ApplicationDBContext _dbContext;

        public HospitalDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Hospital> GetHospitalByIdAsync(Guid hospitalId)
        {
            return await _dbContext.hospitals.FindAsync(hospitalId);
        }


        public async Task<List<Hospital>> GetHospitalsAsync()
        {
            return await _dbContext.hospitals.ToListAsync();
        }

        public async Task<Hospital> InsertOrUpdateHospitalAsync(Hospital hospital)
        {
            if (hospital.HospitalId == Guid.Empty)
            {
                await _dbContext.hospitals.AddAsync(hospital);
            }
            else
            {

                var existingHospital = await _dbContext.hospitals.FindAsync(hospital.HospitalId);

                if (existingHospital != null)
                {

                    bool hasChanges = EntityUpdater.HasChanges(existingHospital, hospital, nameof(Hospital.CreatedBy), nameof(Hospital.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingHospital, hospital, nameof(Hospital.CreatedBy), nameof(Hospital.CreatedOn));
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
            return hospital;
        }
        public async Task<HospitalInformation> GetHospitalInformationByIdAsync(Guid hospitalId)
        {
            var hospitalInformation = new HospitalInformation();

            try
            {
                var hospital = await _dbContext.hospitals
                    .FirstOrDefaultAsync(h => h.HospitalId == hospitalId);

                if (hospital == null)
                {
                    return hospitalInformation;
                }

                await MapHospitalInformationAsync(hospitalInformation, hospital);
            }
            catch (Exception ex)
            {
                throw;
            }

            return hospitalInformation;
        }


        public async Task<List<HospitalInformation>> GetHospitalInformationsAsync()
        {
            var hospitalInformations = new List<HospitalInformation>();

            try
            {
                var hospitals = await _dbContext.hospitals.ToListAsync();

                if (hospitals == null || !hospitals.Any())
                {
                    return hospitalInformations;
                }

                var departments = await _dbContext.departments.ToListAsync();
                var specialties = await _dbContext.specializations.ToListAsync();
                var hospitalTypes = await _dbContext.hospitalTypes.ToListAsync();
                var hospitalContacts = await _dbContext.hospitalContacts.ToListAsync();
                var hospitalContents = await _dbContext.hospitalContents.ToListAsync();
                var hospitalDepartments = await _dbContext.hospitalDepartments.ToListAsync();
                var hospitalSpecialties = await _dbContext.hospitalSpecialties.ToListAsync();

                foreach (var hospital in hospitals)
                {
                    var hospitalInformation = new HospitalInformation();
                    await MapHospitalInformationAsync(hospitalInformation, hospital, departments, specialties, hospitalTypes, hospitalContacts, hospitalContents, hospitalDepartments, hospitalSpecialties);
                    hospitalInformations.Add(hospitalInformation);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return hospitalInformations;
        }

        private async Task MapHospitalInformationAsync(
            HospitalInformation hospitalInformation,
            Hospital hospital,
            List<Department> departments = null,
            List<Specialization> specialties = null,
            List<HospitalType> hospitalTypes = null,
            List<HospitalContact> hospitalContacts = null,
            List<HospitalContent> hospitalContents = null,
            List<HospitalDepartment> hospitalDepartments = null,
            List<HospitalSpecialty> hospitalSpecialties = null)
        {

            MapHospitalInformation(hospitalInformation, hospital);

            await MapHospitalTypeInformation(hospitalInformation, hospital.HospitalTypeId, hospitalTypes);
            MapHospitalContactInformation(hospitalInformation, hospital.HospitalId, hospitalContacts);
            MapHospitalContentInformation(hospitalInformation, hospital.HospitalId, hospitalContents);
            MapHospitalDepartmentInformation(hospitalInformation, hospital.HospitalId, hospitalDepartments, departments);
            MapHospitalSpecialtyInformation(hospitalInformation, hospital.HospitalId, hospitalSpecialties, specialties);
        }



        public async Task<List<HospitalContactInformation>> InsertOrUpdateHospitalContactInformationAsync(HospitalContactInformation hospitalContactInformation)
        {
            var hospitalContacts = new List<HospitalContactInformation>();

            try
            {
                if (hospitalContactInformation.HospitalContactId == Guid.Empty)
                {
                    await InsertOrUpdateContactByTypeAndHospitalAsync(hospitalContactInformation);
                }
                else
                {
                    await UpdateContactByIdAsync(hospitalContactInformation);
                }


                await _dbContext.SaveChangesAsync();

                hospitalContacts = await GetContactsByHospitalIdAsync(hospitalContactInformation.HospitalId.Value);
            }
            catch (Exception ex)
            {

                throw;
            }

            return hospitalContacts;
        }
        public async Task<HospitalContentInformation> InsertOrUpdateHospitalContentInformationAsync(HospitalContentInformation hospitalContentInformation)
        {
            try
            {
                if (hospitalContentInformation.HospitalContentId == Guid.Empty)
                {

                    await InsertHospitalContentAsync(hospitalContentInformation);
                }
                else
                {

                    await UpdateHospitalContentAsync(hospitalContentInformation);
                }

                await _dbContext.SaveChangesAsync();


                var updatedContent = await GetHospitalContentByIdAsync(hospitalContentInformation.HospitalContentId.Value);

                return updatedContent;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<HospitalDepartmentInformation> InsertOrUpdateHospitalDepartmentInformationAsync(HospitalDepartmentInformation hospitalDepartmentInformation)
        {
            try
            {
                if (hospitalDepartmentInformation.HospitalDepartmentId == Guid.Empty)
                {
                    await InsertHospitalDepartmentAsync(hospitalDepartmentInformation);
                }
                else
                {
                    await UpdateHospitalDepartmentAsync(hospitalDepartmentInformation);
                }

                await _dbContext.SaveChangesAsync();

                var updatedDepartment = await GetHospitalDepartmentByIdAsync(hospitalDepartmentInformation.HospitalDepartmentId.Value);

                return updatedDepartment;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<HospitalSpecialtyInformation> InsertOrUpdateHospitalSpecialtyInformationAsync(HospitalSpecialtyInformation hospitalSpecialtyInformation)
        {
            try
            {
                if (hospitalSpecialtyInformation.HospitalSpecialtyId == Guid.Empty)
                {
                    await InsertHospitalSpecialtyAsync(hospitalSpecialtyInformation);
                }
                else
                {
                    await UpdateHospitalSpecialtyAsync(hospitalSpecialtyInformation);
                }

                await _dbContext.SaveChangesAsync();

                var updatedSpecialty = await GetHospitalSpecialtyByIdAsync(hospitalSpecialtyInformation.HospitalSpecialtyId.Value);

                return updatedSpecialty;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void MapHospitalInformation(HospitalInformation hospitalInformation, Hospital hospital)
        {
            hospitalInformation.HospitalId = hospital.HospitalId;
            hospitalInformation.HospitalName = hospital.HospitalName;
            hospitalInformation.HospitalCode = hospital.HospitalCode;
            hospitalInformation.RegistrationNumber = hospital.RegistrationNumber;
            hospitalInformation.Address = hospital.Address;
            hospitalInformation.City = hospital.City;
            hospitalInformation.State = hospital.State;
            hospitalInformation.Country = hospital.Country;
            hospitalInformation.PostalCode = hospital.PostalCode;
            hospitalInformation.PhoneNumber = hospital.PhoneNumber;
            hospitalInformation.Email = hospital.Email;
            hospitalInformation.Website = hospital.Website;
            hospitalInformation.LogoUrl = hospital.LogoUrl;
            hospitalInformation.HospitalTypeId = hospital.HospitalTypeId;
            hospitalInformation.CreatedBy = hospital.CreatedBy;
            hospitalInformation.CreatedOn = hospital.CreatedOn;
            hospitalInformation.ModifiedBy = hospital.ModifiedBy;
            hospitalInformation.ModifiedOn = hospital.ModifiedOn;
            hospitalInformation.IsActive = hospital.IsActive;
        }

        private Task MapHospitalTypeInformation(HospitalInformation hospitalInformation, Guid? hospitalTypeId, List<HospitalType> hospitalTypes = null)
        {
            if (hospitalTypeId.HasValue && hospitalTypeId != Guid.Empty)
            {
                var hospitalType = hospitalTypes?.FirstOrDefault(ht => ht.HospitalTypeId == hospitalTypeId)
                                   ?? _dbContext.hospitalTypes.FirstOrDefault(ht => ht.HospitalTypeId == hospitalTypeId);
                hospitalInformation.HospitalTypeName = hospitalType?.HospitalTypeName;
            }

            return Task.CompletedTask;
        }

        private void MapHospitalContactInformation(HospitalInformation hospitalInformation, Guid hospitalId, List<HospitalContact> hospitalContacts = null)
        {
            var contacts = hospitalContacts?.Where(c => c.HospitalContactId == hospitalId).ToList()
                           ?? _dbContext.hospitalContacts.Where(c => c.HospitalContactId == hospitalId).ToList();

            foreach (var contact in contacts)
            {
                hospitalInformation.hospitalContactInformation.Add(new HospitalContactInformation
                {
                    HospitalContactId = contact.HospitalContactId,
                    ContactType = contact.ContactType,
                    Phone = contact.Phone,
                    Email = contact.Email,
                    CreatedBy = contact.CreatedBy,
                    CreatedOn = contact.CreatedOn,
                    ModifiedBy = contact.ModifiedBy,
                    ModifiedOn = contact.ModifiedOn,
                    IsActive = contact.IsActive,
                });
            }
        }

        private void MapHospitalContentInformation(HospitalInformation hospitalInformation, Guid hospitalId, List<HospitalContent> hospitalContents = null)
        {
            var content = hospitalContents?.FirstOrDefault(c => c.HospitalId == hospitalId)
                          ?? _dbContext.hospitalContents.FirstOrDefault(c => c.HospitalId == hospitalId);

            if (content != null)
            {
                hospitalInformation.hospitalContentInformation = new HospitalContentInformation
                {
                    HospitalContentId = content.HospitalContentId,
                    HospitalId = content.HospitalId,
                    Description = content.Description,
                    CreatedBy = content.CreatedBy,
                    CreatedOn = content.CreatedOn,
                    ModifiedBy = content.ModifiedBy,
                    ModifiedOn = content.ModifiedOn,
                    IsActive = content.IsActive,
                };
            }
        }

        private void MapHospitalDepartmentInformation(HospitalInformation hospitalInformation, Guid hospitalId, List<HospitalDepartment> hospitalDepartments = null, List<Department> departments = null)
        {
            var depts = hospitalDepartments?.Where(d => d.HospitalId == hospitalId).ToList()
                        ?? _dbContext.hospitalDepartments.Where(d => d.HospitalId == hospitalId).ToList();

            var deptList = departments ?? _dbContext.departments.ToList();

            foreach (var dept in depts)
            {
                var department = deptList.FirstOrDefault(d => d.DepartmentId == dept.DepartmentId);

                hospitalInformation.hospitalDepartmentInformation.Add(new HospitalDepartmentInformation
                {
                    HospitalDepartmentId = dept.HospitalDepartmentId,
                    HospitalId = dept.HospitalId,
                    DepartmentId = dept.DepartmentId,
                    DepartmentName = department?.DepartmentName,
                    HeadOfDepartment = dept.HeadOfDepartment,
                    CreatedBy = dept.CreatedBy,
                    CreatedOn = dept.CreatedOn,
                    ModifiedBy = dept.ModifiedBy,
                    ModifiedOn = dept.ModifiedOn,
                    IsActive = dept.IsActive,
                });
            }
        }

        private void MapHospitalSpecialtyInformation(HospitalInformation hospitalInformation, Guid hospitalId, List<HospitalSpecialty> hospitalSpecialties = null, List<Specialization> specialties = null)
        {
            var specs = hospitalSpecialties?.Where(s => s.HospitalId == hospitalId).ToList()
                        ?? _dbContext.hospitalSpecialties.Where(s => s.HospitalId == hospitalId).ToList();

            var specList = specialties ?? _dbContext.specializations.ToList();

            foreach (var spec in specs)
            {
                var specialty = specList.FirstOrDefault(s => s.SpecializationId == spec.SpecializationId);

                hospitalInformation.hospitalSpecialtyInformation.Add(new HospitalSpecialtyInformation
                {
                    HospitalSpecialtyId = spec.HospitalSpecialtyId,
                    HospitalId = spec.HospitalId,
                    SpecializationId = spec.SpecializationId,
                    SpecializationName = specialty?.SpecializationName,
                    CreatedBy = spec.CreatedBy,
                    CreatedOn = spec.CreatedOn,
                    ModifiedBy = spec.ModifiedBy,
                    ModifiedOn = spec.ModifiedOn,
                    IsActive = spec.IsActive,
                });
            }
        }
        private async Task InsertOrUpdateContactByTypeAndHospitalAsync(HospitalContactInformation hospitalContactInformation)
        {
            var existingContact = await _dbContext.hospitalContacts
                .FirstOrDefaultAsync(x => x.ContactType == hospitalContactInformation.ContactType &&
                                          x.HospitalId == hospitalContactInformation.HospitalId);

            if (existingContact == null)
            {
                var newContact = new HospitalContact
                {
                    ContactType = hospitalContactInformation.ContactType,
                    Email = hospitalContactInformation.Email,
                    Phone = hospitalContactInformation.Phone,
                    CreatedBy = hospitalContactInformation.CreatedBy,
                    CreatedOn = hospitalContactInformation.CreatedOn,
                    ModifiedBy = hospitalContactInformation.ModifiedBy,
                    ModifiedOn = hospitalContactInformation.ModifiedOn,
                    IsActive = hospitalContactInformation.IsActive,
                    HospitalId = hospitalContactInformation.HospitalId
                };

                await _dbContext.hospitalContacts.AddAsync(newContact);
            }
            else
            {
                UpdateContactProperties(existingContact, hospitalContactInformation);
            }
        }

        private async Task UpdateContactByIdAsync(HospitalContactInformation hospitalContactInformation)
        {
            var existingContact = await _dbContext.hospitalContacts
                .FindAsync(hospitalContactInformation.HospitalContactId);

            if (existingContact != null)
            {
                UpdateContactProperties(existingContact, hospitalContactInformation);
            }
        }

        private void UpdateContactProperties(HospitalContact contact, HospitalContactInformation hospitalContactInformation)
        {
            contact.Email = hospitalContactInformation.Email;
            contact.Phone = hospitalContactInformation.Phone;
            contact.ModifiedBy = hospitalContactInformation.ModifiedBy;
            contact.ModifiedOn = hospitalContactInformation.ModifiedOn;
            contact.IsActive = hospitalContactInformation.IsActive;
        }

        private async Task<List<HospitalContactInformation>> GetContactsByHospitalIdAsync(Guid hospitalId)
        {
            var contacts = await _dbContext.hospitalContacts
                .Where(c => c.HospitalId == hospitalId)
                .Select(c => new HospitalContactInformation
                {
                    HospitalContactId = c.HospitalContactId,
                    ContactType = c.ContactType,
                    Phone = c.Phone,
                    Email = c.Email,
                    CreatedBy = c.CreatedBy,
                    CreatedOn = c.CreatedOn,
                    ModifiedBy = c.ModifiedBy,
                    ModifiedOn = c.ModifiedOn,
                    IsActive = c.IsActive,
                    HospitalId = c.HospitalId
                })
                .ToListAsync();

            return contacts;
        }



        private async Task InsertHospitalContentAsync(HospitalContentInformation hospitalContentInformation)
        {
            var newContent = new HospitalContent
            {
                HospitalId = hospitalContentInformation.HospitalId,
                Description = hospitalContentInformation.Description,
                CreatedBy = hospitalContentInformation.CreatedBy,
                CreatedOn = hospitalContentInformation.CreatedOn,
                ModifiedBy = hospitalContentInformation.ModifiedBy,
                ModifiedOn = hospitalContentInformation.ModifiedOn,
                IsActive = hospitalContentInformation.IsActive
            };

            await _dbContext.hospitalContents.AddAsync(newContent);

            hospitalContentInformation.HospitalContentId = newContent.HospitalContentId;
        }

        private async Task UpdateHospitalContentAsync(HospitalContentInformation hospitalContentInformation)
        {
            var existingContent = await _dbContext.hospitalContents
                .FindAsync(hospitalContentInformation.HospitalContentId);

            if (existingContent != null)
            {
                existingContent.Description = hospitalContentInformation.Description;
                existingContent.ModifiedBy = hospitalContentInformation.ModifiedBy;
                existingContent.ModifiedOn = hospitalContentInformation.ModifiedOn;
                existingContent.IsActive = hospitalContentInformation.IsActive;
            }
            else
            {
                throw new KeyNotFoundException($"Hospital content with ID {hospitalContentInformation.HospitalContentId} not found.");
            }
        }

        private async Task<HospitalContentInformation> GetHospitalContentByIdAsync(Guid hospitalContentId)
        {
            var content = await _dbContext.hospitalContents
                .Where(c => c.HospitalContentId == hospitalContentId)
                .Select(c => new HospitalContentInformation
                {
                    HospitalContentId = c.HospitalContentId,
                    HospitalId = c.HospitalId,
                    Description = c.Description,
                    CreatedBy = c.CreatedBy,
                    CreatedOn = c.CreatedOn,
                    ModifiedBy = c.ModifiedBy,
                    ModifiedOn = c.ModifiedOn,
                    IsActive = c.IsActive
                })
                .FirstOrDefaultAsync();

            if (content == null)
            {
                throw new KeyNotFoundException($"Hospital content with ID {hospitalContentId} not found.");
            }

            return content;
        }


        private async Task InsertHospitalDepartmentAsync(HospitalDepartmentInformation hospitalDepartmentInformation)
        {
            var newDepartment = new HospitalDepartment
            {
                HospitalId = hospitalDepartmentInformation.HospitalId,
                DepartmentId = hospitalDepartmentInformation.DepartmentId,
                HeadOfDepartment = hospitalDepartmentInformation.HeadOfDepartment,
                CreatedBy = hospitalDepartmentInformation.CreatedBy,
                CreatedOn = hospitalDepartmentInformation.CreatedOn,
                ModifiedBy = hospitalDepartmentInformation.ModifiedBy,
                ModifiedOn = hospitalDepartmentInformation.ModifiedOn,
                IsActive = hospitalDepartmentInformation.IsActive
            };

            await _dbContext.hospitalDepartments.AddAsync(newDepartment);


            hospitalDepartmentInformation.HospitalDepartmentId = newDepartment.HospitalDepartmentId;
        }

        private async Task UpdateHospitalDepartmentAsync(HospitalDepartmentInformation hospitalDepartmentInformation)
        {
            var existingDepartment = await _dbContext.hospitalDepartments
                .FindAsync(hospitalDepartmentInformation.HospitalDepartmentId);

            if (existingDepartment != null)
            {
                existingDepartment.DepartmentId = hospitalDepartmentInformation.DepartmentId;
                existingDepartment.HeadOfDepartment = hospitalDepartmentInformation.HeadOfDepartment;
                existingDepartment.ModifiedBy = hospitalDepartmentInformation.ModifiedBy;
                existingDepartment.ModifiedOn = hospitalDepartmentInformation.ModifiedOn;
                existingDepartment.IsActive = hospitalDepartmentInformation.IsActive;
            }
            else
            {
                throw new KeyNotFoundException($"Hospital department with ID {hospitalDepartmentInformation.HospitalDepartmentId} not found.");
            }
        }

        private async Task<HospitalDepartmentInformation> GetHospitalDepartmentByIdAsync(Guid hospitalDepartmentId)
        {
            var department = await _dbContext.hospitalDepartments
                .Where(d => d.HospitalDepartmentId == hospitalDepartmentId)
                .Select(d => new HospitalDepartmentInformation
                {
                    HospitalDepartmentId = d.HospitalDepartmentId,
                    HospitalId = d.HospitalId,
                    DepartmentId = d.DepartmentId,
                    HeadOfDepartment = d.HeadOfDepartment,
                    CreatedBy = d.CreatedBy,
                    CreatedOn = d.CreatedOn,
                    ModifiedBy = d.ModifiedBy,
                    ModifiedOn = d.ModifiedOn,
                    IsActive = d.IsActive
                })
                .FirstOrDefaultAsync();

            if (department == null)
            {
                throw new KeyNotFoundException($"Hospital department with ID {hospitalDepartmentId} not found.");
            }

            return department;
        }



        private async Task InsertHospitalSpecialtyAsync(HospitalSpecialtyInformation hospitalSpecialtyInformation)
        {
            var newSpecialty = new HospitalSpecialty
            {
                HospitalId = hospitalSpecialtyInformation.HospitalId,
                SpecializationId = hospitalSpecialtyInformation.SpecializationId,
                CreatedBy = hospitalSpecialtyInformation.CreatedBy,
                CreatedOn = hospitalSpecialtyInformation.CreatedOn,
                ModifiedBy = hospitalSpecialtyInformation.ModifiedBy,
                ModifiedOn = hospitalSpecialtyInformation.ModifiedOn,
                IsActive = hospitalSpecialtyInformation.IsActive
            };

            await _dbContext.hospitalSpecialties.AddAsync(newSpecialty);


            hospitalSpecialtyInformation.HospitalSpecialtyId = newSpecialty.HospitalSpecialtyId;
        }

        private async Task UpdateHospitalSpecialtyAsync(HospitalSpecialtyInformation hospitalSpecialtyInformation)
        {
            var existingSpecialty = await _dbContext.hospitalSpecialties
                .FindAsync(hospitalSpecialtyInformation.HospitalSpecialtyId);

            if (existingSpecialty != null)
            {
                existingSpecialty.SpecializationId = hospitalSpecialtyInformation.SpecializationId;
                existingSpecialty.ModifiedBy = hospitalSpecialtyInformation.ModifiedBy;
                existingSpecialty.ModifiedOn = hospitalSpecialtyInformation.ModifiedOn;
                existingSpecialty.IsActive = hospitalSpecialtyInformation.IsActive;
            }
            else
            {
                throw new KeyNotFoundException($"Hospital specialty with ID {hospitalSpecialtyInformation.HospitalSpecialtyId} not found.");
            }
        }

        private async Task<HospitalSpecialtyInformation> GetHospitalSpecialtyByIdAsync(Guid hospitalSpecialtyId)
        {
            var specialty = await _dbContext.hospitalSpecialties
                .Where(s => s.HospitalSpecialtyId == hospitalSpecialtyId)
                .Select(s => new HospitalSpecialtyInformation
                {
                    HospitalSpecialtyId = s.HospitalSpecialtyId,
                    HospitalId = s.HospitalId,
                    SpecializationId = s.SpecializationId,
                    CreatedBy = s.CreatedBy,
                    CreatedOn = s.CreatedOn,
                    ModifiedBy = s.ModifiedBy,
                    ModifiedOn = s.ModifiedOn,
                    IsActive = s.IsActive
                })
                .FirstOrDefaultAsync();

            if (specialty == null)
            {
                throw new KeyNotFoundException($"Hospital specialty with ID {hospitalSpecialtyId} not found.");
            }

            return specialty;
        }
    }
}
