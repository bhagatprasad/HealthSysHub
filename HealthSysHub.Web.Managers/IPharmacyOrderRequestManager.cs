﻿using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IPharmacyOrderRequestManager
    {
        Task<List<PharmacyOrderRequestDetails>> GetPharmacyOrderRequestsAsync();
        Task<List<PharmacyOrderRequestDetails>> GetPharmacyOrderRequestsByPharmacyAsync(Guid pharmacyId);
        Task<List<PharmacyOrderRequestDetails>> GetPharmacyOrderRequestsByHospitalAsync(Guid hospitalId);
        Task<List<PharmacyOrderRequestDetails>> GetPharmacyOrderRequestsByPatientAsync(Guid patientId);
        Task<PharmacyOrderRequestDetails> GetPharmacyOrderRequestDetailAsync(Guid pharmacyOrderRequestId);
        Task<PharmacyOrderRequestDetails> InsertOrUpdatePharmacyOrderRequestDetailsAsync(PharmacyOrderRequestDetails requestDetails);
        Task<ProcessPharmacyOrderRequestResponse> ProcessPharmacyOrderRequestAsync(ProcessPharmacyOrderRequest requestDetails);
    }
}
