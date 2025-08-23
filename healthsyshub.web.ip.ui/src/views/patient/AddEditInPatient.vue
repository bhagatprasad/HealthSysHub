<template>
    <form @submit.prevent="submitForm">
        <div class="AddEditInPatientFormSidebar sidebar" :class="{ show: isVisible }" id="sidebar">
            <div class="modal-header">
                <h5 class="modal-title">{{ patientData ? 'Edit' : 'Add' }} Patient</h5>
                <button type="button" class="close" @click="$emit('handleClose')" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="patientName">Patient Name</label>
                            <div class="input-group">
                                <input type="text" class="form-control" id="patientName" v-model="formData.patientName"
                                    placeholder="Choose from search" required :disabled="isPatientSelected"
                                    @blur="touchedFields.patientName = true" :class="{
                                        'is-invalid': !isPatientSelected && (touchedFields.patientName || formSubmitted),
                                        'is-valid': isPatientSelected
                                    }">
                                <button type="button" class="btn btn-secondary" @click="openPatientList"
                                    :disabled="isPatientSelected">
                                    <i class="fa fa-search"></i>
                                    {{ isPatientSelected ? 'Selected' : 'Search' }}
                                </button>
                            </div>
                            <div v-if="!isPatientSelected && (touchedFields.patientName || formSubmitted)"
                                class="invalid-feedback">
                                Please select a patient
                            </div>
                            <div v-if="isPatientSelected" class="valid-feedback">
                                Patient selected
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="admittingDoctorId">Admitted Doctor</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-user-md"></i></span>
                                </div>
                                <select class="form-control" id="admittingDoctorId" v-model="formData.admittingDoctorId"
                                    required @blur="touchedFields.admittingDoctorId = true" :class="{
                                        'is-invalid': !formData.admittingDoctorId && (touchedFields.admittingDoctorId || formSubmitted),
                                        'is-valid': formData.admittingDoctorId
                                    }">
                                    <option value="">Select a doctor</option>
                                    <option v-for="doctor in dbDoctorsData" :key="doctor.doctorId"
                                        :value="doctor.doctorId">
                                        {{ doctor.fullName }}
                                    </option>
                                </select>
                                <div v-if="!formData.admittingDoctorId && (touchedFields.admittingDoctorId || formSubmitted)"
                                    class="invalid-feedback">
                                    Please select a doctor
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="patientWard">Select ward</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-home"></i></span>
                                </div>
                                <select class="form-control" id="patientWard" v-model="formData.wardId" required
                                    @change="onWardChange" @blur="touchedFields.wardId = true" :class="{
                                        'is-invalid': !formData.wardId && (touchedFields.wardId || formSubmitted),
                                        'is-valid': formData.wardId
                                    }">
                                    <option value="">Select a ward</option>
                                    <option v-for="ward in dbWardData" :key="ward.wardId" :value="ward.wardId">
                                        {{ ward.wardName }} ({{ ward.wardType }}) ({{ ward.currentOccupancy }} - {{
                                            ward.capacity }})
                                    </option>
                                </select>
                                <div v-if="!formData.wardId && (touchedFields.wardId || formSubmitted)"
                                    class="invalid-feedback">
                                    Please select a ward
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="patientBed">Select Bed</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-bed"></i></span>
                                </div>
                                <select class="form-control" id="patientBed" v-model="formData.bedId" required
                                    @blur="touchedFields.bedId = true" :class="{
                                        'is-invalid': !formData.bedId && (touchedFields.bedId || formSubmitted),
                                        'is-valid': formData.bedId
                                    }">
                                    <option value="">Select a Bed</option>
                                    <option v-for="bed in dbFilteredBeds" :key="bed.bedId" :value="bed.bedId">
                                        {{ bed.bedNumber }} ({{ bed.bedType }})
                                    </option>
                                </select>
                                <div v-if="!formData.bedId && (touchedFields.bedId || formSubmitted)"
                                    class="invalid-feedback">
                                    Please select a bed
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="admissionDate">Admission Date</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-calendar"></i></span>
                                </div>
                                <input type="date" class="form-control" id="admissionDate" required
                                    v-model="formData.admissionDate" @blur="touchedFields.admissionDate = true" :class="{
                                        'is-invalid': !formData.admissionDate && (touchedFields.admissionDate || formSubmitted),
                                        'is-valid': formData.admissionDate
                                    }">
                                <div v-if="!formData.admissionDate && (touchedFields.admissionDate || formSubmitted)"
                                    class="invalid-feedback">
                                    Please select an admission date
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="dischargeDate">Aprox Discharge Date</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-calendar"></i></span>
                                </div>
                                <input type="date" class="form-control" id="dischargeDate" required
                                    v-model="formData.dischargeDate" @blur="touchedFields.dischargeDate = true" :class="{
                                        'is-invalid': !formData.dischargeDate && (touchedFields.dischargeDate || formSubmitted),
                                        'is-valid': formData.dischargeDate
                                    }">
                                <div v-if="!formData.dischargeDate && (touchedFields.dischargeDate || formSubmitted)"
                                    class="invalid-feedback">
                                    Please select a discharge date
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="expectedStayDuration">Aprox Days</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-chair"></i></span>
                                </div>
                                <input type="number" class="form-control" id="expectedStayDuration" required
                                    v-model="formData.expectedStayDuration" min="1"
                                    @blur="touchedFields.expectedStayDuration = true" :class="{
                                        'is-invalid': (!formData.expectedStayDuration || formData.expectedStayDuration < 1) && (touchedFields.expectedStayDuration || formSubmitted),
                                        'is-valid': formData.expectedStayDuration && formData.expectedStayDuration > 0
                                    }">
                                <div v-if="(!formData.expectedStayDuration || formData.expectedStayDuration < 1) && (touchedFields.expectedStayDuration || formSubmitted)"
                                    class="invalid-feedback">
                                    Please enter a valid number of days
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="reasonForAdmission">Reason for admission</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-comment"></i></span>
                                </div>
                                <textarea class="form-control" id="reasonForAdmission" rows="3"
                                    v-model="formData.reasonForAdmission"
                                    @blur="touchedFields.reasonForAdmission = true" :class="{
                                        'is-invalid': !formData.reasonForAdmission && (touchedFields.reasonForAdmission || formSubmitted),
                                        'is-valid': formData.reasonForAdmission
                                    }"></textarea>
                                <div v-if="!formData.reasonForAdmission && (touchedFields.reasonForAdmission || formSubmitted)"
                                    class="invalid-feedback">
                                    Please provide a reason for admission
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="currentStatus">Current Status</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-signal"></i></span>
                                </div>
                                <select class="form-control" id="currentStatus" v-model="formData.currentStatus"
                                    required @blur="touchedFields.currentStatus = true" :class="{
                                        'is-invalid': !formData.currentStatus && (touchedFields.currentStatus || formSubmitted),
                                        'is-valid': formData.currentStatus
                                    }">
                                    <option value="">Select a current status</option>
                                    <option v-for="status in dbStatusData" :key="status.value" :value="status.value">
                                        {{ status.name }}
                                    </option>
                                </select>
                                <div v-if="!formData.currentStatus && (touchedFields.currentStatus || formSubmitted)"
                                    class="invalid-feedback">
                                    Please select a status
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Additional fields go here -->
                <div class="modal-footer">
                    <button type="submit" class="btn btn-primary" :disabled="formSubmitted && !isFormValid">
                        {{ patientData ? 'Update' : 'Submit' }}
                    </button>
                </div>
            </div>
        </div>
    </form>
    <patients-list v-if="isPatientsListVisible" :isPatientsListVisible="isPatientsListVisible"
        @close="closePatientsList" @submit="handleSelectedDoctorAppointment" />
    <div v-if="isPatientsListVisible" class="modal-backdrop fade show" @click="closePatientsList"></div>
</template>

<script>
import PatientsList from './PatientsList.vue';
import wardService from '@/global/API/ward.service';
import wardbedService from '@/global/API/wardbed.service';
import doctorService from '@/global/API/doctor.service';
import appointmentService from '@/global/API/appointment.service';
import consultationService from '@/global/API/consultation.service';
import { useAuthStore } from '@/stores/auth.store';
export default {
    name: "addEditInPatient",
    components: {
        PatientsList
    },
    props: {
        patientData: {
            type: Object,
            default: null
        },
        isVisible: {
            type: Boolean,
            default: false
        }
    },
    data() {
        return {
            dbWardData: [],
            dbWardBedsData: [],
            dbDoctorsData: [],
            dbFilteredBeds: [],
            dbDoctorAppointmentsData: [],
            dbConsultationsData: [],
            dbStatusData: [
                {
                    "id": 1,
                    "name": "Admission Proposed",
                    "value": "admission_proposed"
                },
                {
                    "id": 2,
                    "name": "Admitted",
                    "value": "admitted"
                },
                {
                    "id": 3,
                    "name": "Treatment Ongoing",
                    "value": "treatment_ongoing"
                },
                {
                    "id": 4,
                    "name": "Ready for Discharge",
                    "value": "ready_for_discharge"
                },
                {
                    "id": 5,
                    "name": "Discharged",
                    "value": "discharged"
                },
                {
                    "id": 6,
                    "name": "Transferred",
                    "value": "transferred"
                },
                {
                    "id": 7,
                    "name": "Observation",
                    "value": "observation"
                },
                {
                    "id": 8,
                    "name": "Critical",
                    "value": "critical"
                },
                {
                    "id": 9,
                    "name": "Stable",
                    "value": "stable"
                },
                {
                    "id": 10,
                    "name": "Pre-operative",
                    "value": "pre_operative"
                },
                {
                    "id": 11,
                    "name": "Post-operative",
                    "value": "post_operative"
                },
                {
                    "id": 12,
                    "name": "Emergency Admission",
                    "value": "emergency_admission"
                },
                {
                    "id": 13,
                    "name": "Scheduled Admission",
                    "value": "scheduled_admission"
                },
                {
                    "id": 14,
                    "name": "In Recovery",
                    "value": "in_recovery"
                },
                {
                    "id": 15,
                    "name": "Isolation",
                    "value": "isolation"
                },
                {
                    "id": 16,
                    "name": "Step-down Care",
                    "value": "step_down_care"
                },
                {
                    "id": 17,
                    "name": "Palliative Care",
                    "value": "palliative_care"
                },
                {
                    "id": 18,
                    "name": "Awaiting Test Results",
                    "value": "awaiting_test_results"
                },
                {
                    "id": 19,
                    "name": "Awaiting Consultation",
                    "value": "awaiting_consultation"
                },
                {
                    "id": 20,
                    "name": "Discharge Against Medical Advice",
                    "value": "discharge_against_advice"
                },
                {
                    "id": 21,
                    "name": "Home Care Arranged",
                    "value": "home_care_arranged"
                },
                {
                    "id": 22,
                    "name": "Rehabilitation",
                    "value": "rehabilitation"
                },
                {
                    "id": 23,
                    "name": "Hospice Care",
                    "value": "hospice_care"
                },
                {
                    "id": 24,
                    "name": "Medically Cleared",
                    "value": "medically_cleared"
                },
                {
                    "id": 25,
                    "name": "Insurance Authorization Pending",
                    "value": "insurance_pending"
                }
            ],
            isPatientsListVisible: false,
            currentSelectedDoctorAppointment: Object,
            formSubmitted: false,
            currentConsulatation: Object,
            touchedFields: {
                patientName: false,
                admittingDoctorId: false,
                wardId: false,
                bedId: false,
                admissionDate: false,
                dischargeDate: false,
                expectedStayDuration: false,
                reasonForAdmission: false,
                currentStatus: false
            },
            formData: {
                patientName: this.patientData ? this.patientData.patientName : '',
                patientId: this.patientData ? this.patientData.patientId : '',
                wardId: this.patientData ? this.patientData.wardId : '',
                bedId: this.patientData ? this.patientData.bedId : '',
                hospitalId: this.patientData ? this.patientData.hospitalId : '',
                admissionDate: this.patientData ? this.patientData.admissionDate : '',
                dischargeDate: this.patientData ? this.patientData.dischargeDate : '',
                admittingDoctorId: this.patientData ? this.patientData.admittingDoctorId : '',
                currentStatus: this.patientData ? this.patientData.currentStatus : '',
                reasonForAdmission: this.patientData ? this.patientData.reasonForAdmission : '',
                expectedStayDuration: this.patientData ? this.patientData.expectedStayDuration : ''
            }
        }
    },
    computed: {
        isPatientSelected() {
            return !!this.formData.patientId && !!this.formData.patientName;
        },
        isFormValid() {
            return this.isPatientSelected &&
                !!this.formData.admittingDoctorId &&
                !!this.formData.wardId &&
                !!this.formData.bedId &&
                !!this.formData.admissionDate &&
                !!this.formData.dischargeDate &&
                !!this.formData.expectedStayDuration &&
                this.formData.expectedStayDuration > 0 &&
                !!this.formData.reasonForAdmission &&
                !!this.formData.currentStatus;
        },
        currentuser() {
            return this.authStore.user?.id;
        },
        currentHospital() {
            return this.authStore.hospitalInformation?.hospitalId;
        }
    },
    methods: {
        submitForm() {
            this.formSubmitted = true;
            this.formData.hospitalId = this.currentHospital;
            this.formData.createdBy = this.currentuser;
            this.formData.createdOn = new Date();
            this.formData.modifiedOn = new Date();
            this.formData.modifiedBy = this.currentuser;

            console.log(JSON.stringify(this.formData));
            // Mark all fields as touched to show validation errors
            Object.keys(this.touchedFields).forEach(key => {
                this.touchedFields[key] = true;
                console.log(this.touchedFields[key])
            });

            if (this.isFormValid) {
                this.$emit('handleSave', this.formData);
            }
        },
        openPatientList() {
            this.isPatientsListVisible = true;
        },
        closePatientsList() {
            this.isPatientsListVisible = false;
        },
        handleSelectedDoctorAppointment(selectedDoctorAppointment) {
            this.currentSelectedDoctorAppointment = selectedDoctorAppointment;
            this.formData.patientName = selectedDoctorAppointment.patientName;

            this.formData.admittingDoctorId = selectedDoctorAppointment.doctorId;

            const consultationDetails = this.dbConsultationsData.filter(x => x.appointmentId == selectedDoctorAppointment.appointmentId);

            if (consultationDetails) {
                this.currentConsulatation = consultationDetails[0];
                this.formData.patientId = this.currentConsulatation.patientDetails.patientId;
            }
            // Update the form data with the selected patient's information

            // Mark patient name as touched to show validation state
            this.touchedFields.patientName = true;
            // You can also set other fields if needed
            this.closePatientsList();
        },
        async fetchWardsByHospitalIdAsync() {
            const authStore = useAuthStore();
            const hospitalId = authStore.hospitalInformation?.hospitalId;
            if (hospitalId) {
                const wards = await wardService.GetWardsByHospitalIdAsync(hospitalId);
                this.dbWardData = wards;
            }
        },
        async fetchAllBedsAsync() {
            const wardBeds = await wardbedService.GetAllBeds();
            this.dbWardBedsData = wardBeds;
            this.dbFilteredBeds = wardBeds;
        },
        async fetchDoctorsByHospitalAsync() {
            const authStore = useAuthStore();
            const hospitalId = authStore.hospitalInformation?.hospitalId;
            if (hospitalId) {
                const doctors = await doctorService.GetDoctorsByHospitalAsync(hospitalId);
                this.dbDoctorsData = doctors;
            }
        },
        onWardChange() {
            const dbFilteredBeds = this.dbWardBedsData.filter(bed => bed.wardId === this.formData.wardId);
            this.dbFilteredBeds = dbFilteredBeds;
            // Reset bed selection when ward changes
            this.formData.bedId = '';
            // Mark bed as touched to show validation state
            this.touchedFields.bedId = true;
        },
        async fetchDoctorAppointmentsAsync() {
            const authStore = useAuthStore();
            const hospitalId = authStore.hospitalInformation?.hospitalId;
            if (hospitalId) {
                const appoinemtns = await appointmentService.GetActiveDoctorAppointmentsAsync(hospitalId);
                this.dbDoctorAppointmentsData = appoinemtns;
            }
        },
        async fetchDoctorConsulatationsAsync() {
            const authStore = useAuthStore();
            const hospitalId = authStore.hospitalInformation?.hospitalId;
            if (hospitalId) {
                const consultations = await consultationService.GetConsultationsByHospitalAsync(hospitalId);
                this.dbConsultationsData = consultations;
            }
        }
    },
    watch: {
        patientData: {
            immediate: true,
            handler(newVal) {
                if (newVal) {
                    this.formData = {
                        patientName: newVal.patientName,
                        patientId: newVal.patientId,
                        wardId: newVal.wardId,
                        bedId: newVal.bedId,
                        hospitalId: newVal.hospitalId,
                        admissionDate: newVal.admissionDate,
                        dischargeDate: newVal.dischargeDate,
                        admittingDoctorId: newVal.admittingDoctorId,
                        currentStatus: newVal.currentStatus,
                        reasonForAdmission: newVal.reasonForAdmission,
                        expectedStayDuration: newVal.expectedStayDuration
                    };
                    // Mark all fields as touched when editing existing patient
                    if (newVal) {
                        Object.keys(this.touchedFields).forEach(key => {
                            this.touchedFields[key] = true;
                        });
                    }
                }
            }
        }
    },
    mounted() {
        this.fetchWardsByHospitalIdAsync();
        this.fetchAllBedsAsync();
        this.fetchDoctorsByHospitalAsync();
        this.fetchDoctorAppointmentsAsync();
        this.fetchDoctorConsulatationsAsync();
    }
}
</script>

<style scoped>
/* Add any additional styles here */
.btn:disabled {
    opacity: 0.6;
    cursor: not-allowed;
}

.valid-feedback,
.invalid-feedback {
    display: block;
}
</style>