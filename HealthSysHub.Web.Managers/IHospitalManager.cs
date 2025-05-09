﻿using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IHospitalManager
    {
        Task<Hospital> GetHospitalByIdAsync(Guid hospitalId);
        Task<List<Hospital>> GetHospitalsAsync();
        Task<Hospital> InsertOrUpdateHospitalAsync(Hospital hospital);
        Task<List<HospitalInformation>> GetHospitalInformationsAsync();
        Task<HospitalInformation> GetHospitalInformationByIdAsync(Guid hospitalId);

        Task<List<HospitalContactInformation>> InsertOrUpdateHospitalContactInformationAsync(HospitalContactInformation hospitalContactInformation);
        Task<HospitalContentInformation> InsertOrUpdateHospitalContentInformationAsync(HospitalContentInformation hospitalContentInformation);
        Task<List<HospitalDepartmentInformation>> InsertOrUpdateHospitalDepartmentInformationAsync(HospitalDepartmentInformation hospitalDepartmentInformation);
        Task<List<HospitalSpecialtyInformation>> InsertOrUpdateHospitalSpecialtyInformationAsync(HospitalSpecialtyInformation hospitalSpecialtyInformation);

    }
}
