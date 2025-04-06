using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class ConsultationDataManager : IConsultationManager
    {
        private readonly ApplicationDBContext _dbContext;
        public ConsultationDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Specific methods that use the main method with filters
        public Task<List<ConsultationDetails>> GetConsultationDetailsByAppointmentIdAsync(Guid appointmentId)
            => ConsultationDetailsAsync(appointmentId: appointmentId);

        public Task<List<ConsultationDetails>> GetConsultationDetailsByConsultationIdAsync(Guid consultationId)
            => ConsultationDetailsAsync(consultationId: consultationId);

        public Task<List<ConsultationDetails>> GetConsultationDetailsByDoctorAsync(Guid doctorId)
            => ConsultationDetailsAsync(doctorId: doctorId);

        public Task<List<ConsultationDetails>> GetConsultationDetailsByHospitalAsync(Guid hospitalId)
            => ConsultationDetailsAsync(hospitalId: hospitalId);

        // Helper methods for data retrieval
        private async Task<List<DoctorAppointment>> GetAppointmentsAsync()
            => await _dbContext.doctorAppointments.ToListAsync();

        private async Task<List<Patient>> GetPatientsAsync()
            => await _dbContext.patients.ToListAsync();

        private async Task<List<PatientVital>> GetPatientVitalsAsync()
            => await _dbContext.patientVitals.ToListAsync();

        private async Task<List<PatientPrescription>> GetPatientPrescriptionsAsync()
            => await _dbContext.patientPrescriptions.ToListAsync();

        private async Task<List<PharmacyOrderRequest>> GetPharmacyRequestsAsync()
            => await _dbContext.pharmacyOrderRequests.ToListAsync();

        private async Task<List<PharmacyOrderRequestItem>> GetPharmacyRequestItemsAsync()
            => await _dbContext.pharmacyOrderRequestItems.ToListAsync();

        private async Task<List<LabOrderRequest>> GetLabOrderRequestsAsync()
            => await _dbContext.labOrderRequests.ToListAsync();

        private async Task<List<LabOrderRequestItem>> GetLabOrderRequestItemsAsync()
            => await _dbContext.labOrderRequestItems.ToListAsync();

        private async Task<List<Medicine>> GetMedicinesAsync()
            => await _dbContext.medicines.ToListAsync();

        private async Task<List<LabTest>> GetLabTestsAsync()
            => await _dbContext.labTests.ToListAsync();

        // Helper method to create consultation details
        private ConsultationDetails CreateConsultationDetails(
            Consultation consultation,
            List<DoctorAppointment> appointments,
            List<Patient> patients,
            List<PatientVital> patientVitals,
            List<PatientPrescription> patientPrescriptions,
            List<PharmacyOrderRequest> pharmacyRequests,
            List<PharmacyOrderRequestItem> pharmacyRequestItems,
            List<LabOrderRequest> labOrderRequests,
            List<LabOrderRequestItem> labOrderRequestItems,
            List<Medicine> medicines,
            List<LabTest> labTests)
        {
            var appointment = appointments.FirstOrDefault(a => a.AppointmentId == consultation.AppointmentId);

            var consultationDetails = new ConsultationDetails
            {
                AppointmentId = consultation.AppointmentId,
                ConsultationId = consultation.ConsultationId,
                CreatedBy = consultation.CreatedBy,
                CreatedOn = consultation.CreatedOn,
                DoctorId = consultation.DoctorId,
                HospitalId = consultation.HospitalId,
                IsActive = consultation.IsActive,
                ModifiedBy = consultation.ModifiedBy,
                ModifiedOn = consultation.ModifiedOn,
                Status = consultation.Status
            };

            var patient = patients.FirstOrDefault(p => p.ConsultationId == consultation.ConsultationId);
            if (patient != null)
            {
                consultationDetails.patientDetails = new PatientDetails
                {
                    Address = patient.Address,
                    Age = patient.Age,
                    AttenderPhone = patient.AttenderPhone,
                    HealthIssue = patient.HealthIssue,
                    Gender = patient.Gender,
                    Name = patient.Name,
                    PatientId = patient.PatientId,
                    PatientTypeId = patient.PatientTypeId,
                    Phone = patient.Phone,
                    patientVitalDetails = new PatientVitalDetails(),
                    patientPrescriptionDetails = new PatientPrescriptionDetails()
                };

                // Add patient vitals
                var patientVital = patientVitals.FirstOrDefault(pt => pt.PatientId == patient.PatientId);
                if (patientVital != null)
                {
                    consultationDetails.patientDetails.patientVitalDetails = new PatientVitalDetails
                    {
                        VitalId = patientVital.VitalId,
                        BloodPressure = patientVital.BloodPressure,
                        BodyTemperature = patientVital.BodyTemperature,
                        Height = patientVital.Height,
                        BMI = patientVital.BMI,
                        HeartRate = patientVital.HeartRate,
                        Notes = patientVital.Notes,
                        OxygenSaturation = patientVital.OxygenSaturation,
                        RespiratoryRate = patientVital.RespiratoryRate,
                        Weight = patientVital.Weight
                    };
                }

                // Add prescriptions and related data
                var patientPrescription = patientPrescriptions.FirstOrDefault(m => m.PatientId == patient.PatientId);
                if (patientPrescription != null)
                {
                    consultationDetails.patientDetails.patientPrescriptionDetails = new PatientPrescriptionDetails
                    {
                        Advice = patientPrescription.Advice,
                        PatientPrescriptionId = patientPrescription.PatientPrescriptionId,
                        Treatment = patientPrescription.Treatment,
                        Diagnosis = patientPrescription.Diagnosis,
                        FollowUpOn = patientPrescription.FollowUpOn,
                        Notes = patientPrescription.Notes,
                        pharmacyOrderRequestDetails = new PharmacyOrderRequestDetails(),
                        labOrderRequestDetails = new LabOrderRequestDetails()
                    };

                    // Add pharmacy requests
                    var pharmacyRequest = pharmacyRequests.FirstOrDefault(pr => pr.PatientPrescriptionId == patientPrescription.PatientPrescriptionId);
                    if (pharmacyRequest != null)
                    {
                        consultationDetails.patientDetails.patientPrescriptionDetails.pharmacyOrderRequestDetails = new PharmacyOrderRequestDetails
                        {
                            PatientPrescriptionId = pharmacyRequest.PatientPrescriptionId,
                            DoctorName = pharmacyRequest.DoctorName,
                            HospitalName = pharmacyRequest.HospitalName,
                            HospitalId = pharmacyRequest.HospitalId,
                            PatientId = pharmacyRequest.PatientId,
                            Name = pharmacyRequest.Name,
                            Phone = pharmacyRequest.Phone,
                            Notes = pharmacyRequest.Notes,
                            PharmacyOrderRequestId = pharmacyRequest.PharmacyOrderRequestId,
                            pharmacyOrderRequestItemDetails = new List<PharmacyOrderRequestItemDetails>()
                        };

                        // Add pharmacy request items
                        var items = pharmacyRequestItems.Where(pri => pri.PharmacyOrderRequestId == pharmacyRequest.PharmacyOrderRequestId);
                        foreach (var item in items)
                        {
                            var medicine = medicines.FirstOrDefault(m => m.MedicineId == item.MedicineId);
                            consultationDetails.patientDetails.patientPrescriptionDetails.pharmacyOrderRequestDetails
                                .pharmacyOrderRequestItemDetails.Add(new PharmacyOrderRequestItemDetails
                                {
                                    HospitalId = item.HospitalId,
                                    ItemQty = item.ItemQty,
                                    MedicineId = item.MedicineId,
                                    MedicineName = medicine?.MedicineName,
                                    Usage = item.Usage
                                });
                        }
                    }

                    // Add lab requests
                    var labRequest = labOrderRequests.FirstOrDefault(lr => lr.PatientPrescriptionId == patientPrescription.PatientPrescriptionId);
                    if (labRequest != null)
                    {
                        consultationDetails.patientDetails.patientPrescriptionDetails.labOrderRequestDetails = new LabOrderRequestDetails
                        {
                            LabOrderRequestId = labRequest.LabOrderRequestId,
                            DoctorName = labRequest.DoctorName,
                            HospitalName = labRequest.HospitalName,
                            Notes = labRequest.Notes,
                            Phone = labRequest.Phone,
                            Status = labRequest.Status,
                            Name = labRequest.Name,
                            labOrderRequestItemDetails = new List<LabOrderRequestItemDetails>()
                        };

                        // Add lab request items
                        var labItems = labOrderRequestItems.Where(lori => lori.LabOrderRequestId == labRequest.LabOrderRequestId);
                        foreach (var item in labItems)
                        {
                            var labTest = labTests.FirstOrDefault(lt => lt.TestId == item.TestId);
                            consultationDetails.patientDetails.patientPrescriptionDetails.labOrderRequestDetails
                                .labOrderRequestItemDetails.Add(new LabOrderRequestItemDetails
                                {
                                    ItemQty = item.ItemQty,
                                    LabOrderRequestItemId = item.LabOrderRequestItemId,
                                    TestId = item.TestId,
                                    TestName = labTest?.TestName
                                });
                        }
                    }
                }
            }

            return consultationDetails;
        }

        public async Task<List<Consultation>> GetConsultationsAsync()
        {
            return await _dbContext.consultations.ToListAsync();
        }

        public async Task<List<Consultation>> GetConsultationsByDoctorAsync(Guid doctorId)
        {
            return await _dbContext.consultations.Where(x => x.DoctorId == doctorId && x.IsActive).ToListAsync();
        }

        public async Task<List<Consultation>> GetConsultationsByHospitalAsync(Guid hospitalId)
        {
            return await _dbContext.consultations.Where(x => x.HospitalId == hospitalId && x.IsActive).ToListAsync();
        }

        public async Task<Consultation> InsertOrUpdateConsultationAsync(Consultation consultation)
        {
            if (consultation.ConsultationId == Guid.Empty)
            {
                await _dbContext.consultations.AddAsync(consultation);
            }
            else
            {
                var existingConsultation = await _dbContext.consultations.FindAsync(consultation.ConsultationId);

                if (existingConsultation != null)
                {
                    var hasChanges = EntityUpdater.HasChanges(existingConsultation, consultation, nameof(Consultation.CreatedBy), nameof(Consultation.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingConsultation, consultation, nameof(Consultation.CreatedBy), nameof(Consultation.CreatedOn));
                    }
                }
            }
            await _dbContext.SaveChangesAsync();

            return consultation;
        }

        public async Task<ConsultationDetails> InsertOrUpdateConsultationDetailsAsync(ConsultationDetails consultationDetails)
        {
            if (consultationDetails == null)
            {
                throw new ArgumentNullException(nameof(consultationDetails));
            }

            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();

                if (consultationDetails.ConsultationId == Guid.Empty)
                {
                    await CreateNewConsultation(consultationDetails);
                }
                else
                {
                    await UpdateExistingConsultation(consultationDetails);
                }

                await transaction.CommitAsync();
                return consultationDetails;
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new ApplicationException("An error occurred while saving consultation details.", ex);
            }
        }

        private async Task CreateNewConsultation(ConsultationDetails consultationDetails)
        {
            // Create and save consultation
            var consultation = new Consultation
            {
                AppointmentId = consultationDetails.AppointmentId,
                ConsultationId = Guid.NewGuid(),
                CreatedBy = consultationDetails.CreatedBy,
                CreatedOn = consultationDetails.CreatedOn,
                DoctorId = consultationDetails.DoctorId,
                HospitalId = consultationDetails.HospitalId,
                IsActive = consultationDetails.IsActive,
                ModifiedBy = consultationDetails.ModifiedBy,
                ModifiedOn = consultationDetails.ModifiedOn,
                Status = consultationDetails.Status
            };

            await _dbContext.consultations.AddAsync(consultation);
            await _dbContext.SaveChangesAsync();

            consultationDetails.ConsultationId = consultation.ConsultationId;

            // Create and save patient
            var patient = await CreatePatient(consultationDetails, consultation.ConsultationId);
            await _dbContext.patients.AddAsync(patient);
            await _dbContext.SaveChangesAsync();

            // Create and save vital
            var vital = await CreatePatientVital(consultationDetails, patient.PatientId, consultation.ConsultationId);
            await _dbContext.patientVitals.AddAsync(vital);
            await _dbContext.SaveChangesAsync();

            // Create and save prescription
            var prescription = await CreatePatientPrescription(consultationDetails, patient.PatientId, consultation.ConsultationId);
            await _dbContext.patientPrescriptions.AddAsync(prescription);
            await _dbContext.SaveChangesAsync();
        }

        private async Task UpdateExistingConsultation(ConsultationDetails consultationDetails)
        {
            var dbConsultation = await _dbContext.consultations.FindAsync(consultationDetails.ConsultationId);
            if (dbConsultation == null)
            {
                throw new KeyNotFoundException($"Consultation with ID {consultationDetails.ConsultationId} not found.");
            }

            // Update consultation
            var updatedConsultation = new Consultation
            {
                AppointmentId = consultationDetails.AppointmentId,
                ConsultationId = dbConsultation.ConsultationId,
                CreatedBy = consultationDetails.CreatedBy,
                CreatedOn = consultationDetails.CreatedOn,
                DoctorId = consultationDetails.DoctorId,
                HospitalId = consultationDetails.HospitalId,
                IsActive = consultationDetails.IsActive,
                ModifiedBy = consultationDetails.ModifiedBy,
                ModifiedOn = consultationDetails.ModifiedOn,
                Status = consultationDetails.Status
            };

            if (EntityUpdater.HasChanges(dbConsultation, updatedConsultation, nameof(Consultation.CreatedBy), nameof(Consultation.CreatedOn)))
            {
                EntityUpdater.UpdateProperties(dbConsultation, updatedConsultation, nameof(Consultation.CreatedBy), nameof(Consultation.CreatedOn));
            }

            // Update patient
            await UpdatePatient(consultationDetails, dbConsultation.ConsultationId);

            // Update vital
            await UpdatePatientVital(consultationDetails);

            // Update prescription
            await UpdatePatientPrescription(consultationDetails);

            await _dbContext.SaveChangesAsync();
        }

        private async Task<Patient> CreatePatient(ConsultationDetails consultationDetails, Guid consultationId)
        {
            return new Patient
            {
                Phone = consultationDetails.patientDetails.Phone,
                Address = consultationDetails.patientDetails.Address,
                Age = consultationDetails.patientDetails.Age,
                AttenderPhone = consultationDetails.patientDetails.AttenderPhone,
                ConsultationId = consultationId,
                CreatedBy = consultationDetails.CreatedBy,
                CreatedOn = consultationDetails.CreatedOn,
                Gender = consultationDetails.patientDetails.Gender,
                HealthIssue = consultationDetails.patientDetails.HealthIssue,
                HospitalId = consultationDetails.HospitalId,
                IsActive = consultationDetails.IsActive,
                ModifiedBy = consultationDetails.ModifiedBy,
                ModifiedOn = consultationDetails.ModifiedOn,
                Name = consultationDetails.patientDetails.Name,
                PatientId = Guid.NewGuid(),
                PatientTypeId = consultationDetails.patientDetails.PatientTypeId
            };
        }

        private async Task UpdatePatient(ConsultationDetails consultationDetails, Guid consultationId)
        {
            if (consultationDetails.patientDetails?.PatientId == null)
            {
                throw new ArgumentException("Patient details must include PatientId for update.");
            }

            var patient = new Patient
            {
                Phone = consultationDetails.patientDetails.Phone,
                Address = consultationDetails.patientDetails.Address,
                Age = consultationDetails.patientDetails.Age,
                AttenderPhone = consultationDetails.patientDetails.AttenderPhone,
                ConsultationId = consultationId,
                CreatedBy = consultationDetails.CreatedBy,
                CreatedOn = consultationDetails.CreatedOn,
                Gender = consultationDetails.patientDetails.Gender,
                HealthIssue = consultationDetails.patientDetails.HealthIssue,
                HospitalId = consultationDetails.HospitalId,
                IsActive = consultationDetails.IsActive,
                ModifiedBy = consultationDetails.ModifiedBy,
                ModifiedOn = consultationDetails.ModifiedOn,
                Name = consultationDetails.patientDetails.Name,
                PatientId = consultationDetails.patientDetails.PatientId.Value,
                PatientTypeId = consultationDetails.patientDetails.PatientTypeId
            };

            var dbPatient = await _dbContext.patients.FindAsync(patient.PatientId);
            if (dbPatient == null)
            {
                throw new KeyNotFoundException($"Patient with ID {patient.PatientId} not found.");
            }

            if (EntityUpdater.HasChanges(dbPatient, patient, nameof(Patient.CreatedBy), nameof(Patient.CreatedOn), nameof(Patient.ModifiedBy), nameof(Patient.ModifiedOn)))
            {
                EntityUpdater.UpdateProperties(dbPatient, patient, nameof(Patient.CreatedBy), nameof(Patient.CreatedOn));
            }
        }

        private async Task<PatientVital> CreatePatientVital(ConsultationDetails consultationDetails, Guid patientId, Guid consultationId)
        {
            return new PatientVital
            {
                ModifiedBy = consultationDetails.ModifiedBy,
                PatientId = patientId,
                ModifiedOn = consultationDetails.ModifiedOn,
                BloodPressure = consultationDetails.patientDetails.patientVitalDetails.BloodPressure,
                BMI = consultationDetails.patientDetails.patientVitalDetails.BMI,
                BodyTemperature = consultationDetails.patientDetails.patientVitalDetails.BodyTemperature,
                ConsultationId = consultationId,
                CreatedBy = consultationDetails.CreatedBy,
                CreatedOn = consultationDetails.CreatedOn,
                HeartRate = consultationDetails.patientDetails.patientVitalDetails.HeartRate,
                Height = consultationDetails.patientDetails.patientVitalDetails.Height,
                HospitalId = consultationDetails.HospitalId,
                IsActive = consultationDetails.IsActive,
                Notes = consultationDetails.patientDetails.patientVitalDetails.Notes,
                OxygenSaturation = consultationDetails.patientDetails.patientVitalDetails.OxygenSaturation,
                RespiratoryRate = consultationDetails.patientDetails.patientVitalDetails.RespiratoryRate,
                VitalId = Guid.NewGuid(),
                Weight = consultationDetails.patientDetails.patientVitalDetails.Weight
            };
        }

        private async Task UpdatePatientVital(ConsultationDetails consultationDetails)
        {
            if (consultationDetails.patientDetails?.patientVitalDetails?.VitalId == null)
            {
                throw new ArgumentException("Vital details must include VitalId for update.");
            }

            var vital = new PatientVital
            {
                ModifiedBy = consultationDetails.ModifiedBy,
                PatientId = consultationDetails.patientDetails.PatientId.Value,
                ModifiedOn = consultationDetails.ModifiedOn,
                BloodPressure = consultationDetails.patientDetails.patientVitalDetails.BloodPressure,
                BMI = consultationDetails.patientDetails.patientVitalDetails.BMI,
                BodyTemperature = consultationDetails.patientDetails.patientVitalDetails.BodyTemperature,
                ConsultationId = consultationDetails.ConsultationId,
                CreatedBy = consultationDetails.CreatedBy,
                CreatedOn = consultationDetails.CreatedOn,
                HeartRate = consultationDetails.patientDetails.patientVitalDetails.HeartRate,
                Height = consultationDetails.patientDetails.patientVitalDetails.Height,
                HospitalId = consultationDetails.HospitalId,
                IsActive = consultationDetails.IsActive,
                Notes = consultationDetails.patientDetails.patientVitalDetails.Notes,
                OxygenSaturation = consultationDetails.patientDetails.patientVitalDetails.OxygenSaturation,
                RespiratoryRate = consultationDetails.patientDetails.patientVitalDetails.RespiratoryRate,
                VitalId = consultationDetails.patientDetails.patientVitalDetails.VitalId.Value,
                Weight = consultationDetails.patientDetails.patientVitalDetails.Weight
            };

            var dbVital = await _dbContext.patientVitals.FindAsync(vital.VitalId);
            if (dbVital == null)
            {
                throw new KeyNotFoundException($"Vital with ID {vital.VitalId} not found.");
            }

            if (EntityUpdater.HasChanges(dbVital, vital, nameof(PatientVital.CreatedBy), nameof(PatientVital.CreatedOn), nameof(PatientVital.ModifiedBy), nameof(PatientVital.ModifiedOn)))
            {
                EntityUpdater.UpdateProperties(dbVital, vital, nameof(PatientVital.CreatedBy), nameof(PatientVital.CreatedOn));
            }
        }

        private async Task<PatientPrescription> CreatePatientPrescription(ConsultationDetails consultationDetails, Guid patientId, Guid consultationId)
        {
            return new PatientPrescription
            {
                Advice = consultationDetails.patientDetails.patientPrescriptionDetails.Advice,
                ConsultationId = consultationId,
                CreatedBy = consultationDetails.CreatedBy,
                CreatedOn = consultationDetails.CreatedOn,
                Diagnosis = consultationDetails.patientDetails.patientPrescriptionDetails.Diagnosis,
                FollowUpOn = consultationDetails.patientDetails.patientPrescriptionDetails.FollowUpOn,
                HospitalId = consultationDetails.HospitalId,
                IsActive = consultationDetails.IsActive,
                ModifiedBy = consultationDetails.ModifiedBy,
                ModifiedOn = consultationDetails.ModifiedOn,
                Notes = consultationDetails.patientDetails.patientPrescriptionDetails.Notes,
                PatientId = patientId,
                Treatment = consultationDetails.patientDetails.patientPrescriptionDetails.Treatment,
                PatientPrescriptionId = Guid.NewGuid(),
            };
        }

        private async Task UpdatePatientPrescription(ConsultationDetails consultationDetails)
        {
            if (consultationDetails.patientDetails?.patientPrescriptionDetails?.PatientPrescriptionId == null)
            {
                throw new ArgumentException("Prescription details must include PatientPrescriptionId for update.");
            }

            var prescription = new PatientPrescription
            {
                Advice = consultationDetails.patientDetails.patientPrescriptionDetails.Advice,
                ConsultationId = consultationDetails.ConsultationId,
                CreatedBy = consultationDetails.CreatedBy,
                CreatedOn = consultationDetails.CreatedOn,
                Diagnosis = consultationDetails.patientDetails.patientPrescriptionDetails.Diagnosis,
                FollowUpOn = consultationDetails.patientDetails.patientPrescriptionDetails.FollowUpOn,
                HospitalId = consultationDetails.HospitalId,
                IsActive = consultationDetails.IsActive,
                ModifiedBy = consultationDetails.ModifiedBy,
                ModifiedOn = consultationDetails.ModifiedOn,
                Notes = consultationDetails.patientDetails.patientPrescriptionDetails.Notes,
                PatientId = consultationDetails.patientDetails.PatientId.Value,
                Treatment = consultationDetails.patientDetails.patientPrescriptionDetails.Treatment,
                PatientPrescriptionId = consultationDetails.patientDetails.patientPrescriptionDetails.PatientPrescriptionId.Value,
            };

            var dbPrescription = await _dbContext.patientPrescriptions.FindAsync(prescription.PatientPrescriptionId);
            if (dbPrescription == null)
            {
                throw new KeyNotFoundException($"Prescription with ID {prescription.PatientPrescriptionId} not found.");
            }

            if (EntityUpdater.HasChanges(dbPrescription, prescription, nameof(PatientPrescription.CreatedBy), nameof(PatientPrescription.CreatedOn), nameof(PatientPrescription.ModifiedBy), nameof(PatientPrescription.ModifiedOn)))
            {
                EntityUpdater.UpdateProperties(dbPrescription, prescription, nameof(PatientPrescription.CreatedBy), nameof(PatientPrescription.CreatedOn));
            }
        }

        public async Task<List<ConsultationDetails>> GetConsultationDetailsAsync()
        {
            return await ConsultationDetailsAsync();
        }
        private async Task<List<ConsultationDetails>> ConsultationDetailsAsync(Guid? appointmentId = null, Guid? consultationId = null, Guid? doctorId = null, Guid? hospitalId = null)
        {
            // Base query with optional filters
            var consultationsQuery = _dbContext.consultations.AsQueryable();

            if (appointmentId.HasValue)
                consultationsQuery = consultationsQuery.Where(c => c.AppointmentId == appointmentId.Value);

            if (consultationId.HasValue)
                consultationsQuery = consultationsQuery.Where(c => c.ConsultationId == consultationId.Value);

            if (doctorId.HasValue)
                consultationsQuery = consultationsQuery.Where(c => c.DoctorId == doctorId.Value);

            if (hospitalId.HasValue)
                consultationsQuery = consultationsQuery.Where(c => c.HospitalId == hospitalId.Value);

            // Get filtered consultations
            var consultations = await consultationsQuery.ToListAsync();

            if (!consultations.Any())
                return new List<ConsultationDetails>();

            // Start all data retrieval tasks
            var appointmentsTask = GetAppointmentsAsync();
            var patientsTask = GetPatientsAsync();
            var patientVitalsTask = GetPatientVitalsAsync();
            var patientPrescriptionsTask = GetPatientPrescriptionsAsync();
            var pharmacyRequestsTask = GetPharmacyRequestsAsync();
            var pharmacyRequestItemsTask = GetPharmacyRequestItemsAsync();
            var labOrderRequestsTask = GetLabOrderRequestsAsync();
            var labOrderRequestItemsTask = GetLabOrderRequestItemsAsync();
            var medicinesTask = GetMedicinesAsync();
            var labTestsTask = GetLabTestsAsync();

            // Wait for all tasks to complete
            await Task.WhenAll(
                appointmentsTask, patientsTask, patientVitalsTask, patientPrescriptionsTask,
                pharmacyRequestsTask, pharmacyRequestItemsTask, labOrderRequestsTask,
                labOrderRequestItemsTask, medicinesTask, labTestsTask);

            // Get the results from each task
            var appointments = await appointmentsTask;
            var patients = await patientsTask;
            var patientVitals = await patientVitalsTask;
            var patientPrescriptions = await patientPrescriptionsTask;
            var pharmacyRequests = await pharmacyRequestsTask;
            var pharmacyRequestItems = await pharmacyRequestItemsTask;
            var labOrderRequests = await labOrderRequestsTask;
            var labOrderRequestItems = await labOrderRequestItemsTask;
            var medicines = await medicinesTask;
            var labTests = await labTestsTask;

            // Create consultation details
            var consultationDetails = consultations.Select(consultation =>
                CreateConsultationDetails(
                    consultation,
                    appointments,
                    patients,
                    patientVitals,
                    patientPrescriptions,
                    pharmacyRequests,
                    pharmacyRequestItems,
                    labOrderRequests,
                    labOrderRequestItems,
                    medicines,
                    labTests
                )).ToList();

            return consultationDetails;
        }

    }
}
