using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.Utility.Models;
using System;

namespace HealthSysHub.Web.UI.Services
{
    public class PharmacyOrderRequestService : IPharmacyOrderRequestService
    {
        private readonly IRepositoryFactory _repository;

        public PharmacyOrderRequestService(IRepositoryFactory repository)
        {
            _repository = repository;
        }

        public async Task<PharmacyOrderRequestDetails> GetPharmacyOrderRequestDetailAsync(Guid pharmacyOrderRequestId)
        {
            string url = Path.Combine("GetPharmacyOrderRequestDetailAsync", pharmacyOrderRequestId.ToString());

            return await _repository.SendAsync<PharmacyOrderRequestDetails>(HttpMethod.Get, url);
        }

        public async Task<List<PharmacyOrderRequestDetails>> GetPharmacyOrderRequestsAsync()
        {
            return await _repository.SendAsync<List<PharmacyOrderRequestDetails>>(HttpMethod.Get, "GetPharmacyOrderRequestsAsync");
        }

        public async Task<List<PharmacyOrderRequestDetails>> GetPharmacyOrderRequestsByHospitalAsync(Guid hospitalId)
        {
            string url = Path.Combine("GetPharmacyOrderRequestsByHospitalAsync", hospitalId.ToString());

            return await _repository.SendAsync<List<PharmacyOrderRequestDetails>>(HttpMethod.Get, url);
        }

        public async Task<List<PharmacyOrderRequestDetails>> GetPharmacyOrderRequestsByPatientAsync(Guid patientId)
        {
            string url = Path.Combine("GetPharmacyOrderRequestsByPatientAsync", patientId.ToString());

            return await _repository.SendAsync<List<PharmacyOrderRequestDetails>>(HttpMethod.Get, url);
        }

        public async Task<List<PharmacyOrderRequestDetails>> GetPharmacyOrderRequestsByPharmacyAsync(Guid pharmacyId)
        {
            string url = Path.Combine("GetPharmacyOrderRequestsByPharmacyAsync", pharmacyId.ToString());

            return await _repository.SendAsync<List<PharmacyOrderRequestDetails>>(HttpMethod.Get, url);
        }

        public async Task<PharmacyOrderRequestDetails> InsertOrUpdatePharmacyOrderRequestDetailsAsync(PharmacyOrderRequestDetails requestDetails)
        {
            return await _repository.SendAsync<PharmacyOrderRequestDetails, PharmacyOrderRequestDetails>(HttpMethod.Post, "PharmacyOrderRequest/InsertOrUpdatePharmacyOrderRequestAsync", requestDetails);

        }
    }
}
