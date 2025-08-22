<template>
    <form @submit.prevent="submitForm">
        <div class="AddEditInPatientFormSidebar sidebar" :class="{ show: isVisible }" id="sidebar">
            <div class="modal-header">
                <h5 class="modal-title">{{ patientData ? 'Edit' : 'Add' }} Patient</h5>
                <button type="button" class="close" @click="$emit('close')" aria-label="Close">
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
                                    placeholder="Choose from search" required readonly>
                                <button type="button" class="btn btn-secondary" @click="openPatientList">
                                    <i class="fa fa-search"></i> Search
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="admittingDoctorId">Admitted Doctor</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-user"></i></span>
                                </div>
                                <select class="form-control" id="admittingDoctorId" v-model="formData.admittingDoctorId"
                                    required>
                                    <option value="">Select a ward</option>
                                    <option v-for="doctor in dbDoctorsData" :key="doctor.doctorId"
                                        :value="doctor.doctorId">
                                        {{ doctor.fullName }}
                                    </option>
                                </select>
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
                                    @change="onWardChange">
                                    <option value="">Select a ward</option>
                                    <option v-for="ward in dbWardData" :key="ward.wardId" :value="ward.wardId">
                                        {{ ward.wardName }} ({{ ward.wardType }}) ({{ ward.currentOccupancy }} - {{
                                            ward.capacity }})
                                    </option>
                                </select>
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
                                <select class="form-control" id="patientBed" v-model="formData.bedId" required>
                                    <option value="">Select a Bed</option>
                                    <option v-for="bed in dbFilteredBeds" :key="bed.bedId" :value="bed.bedId">
                                        {{ bed.bedNumber }} ({{ bed.bedType }})
                                    </option>
                                </select>
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
                                    v-model="formData.admissionDate">
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
                                    v-model="formData.dischargeDate">
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Additional fields go here -->
                <div class="modal-footer">
                    <button type="submit" class="btn btn-primary">{{ patientData ? 'Update' : 'Submit' }}</button>
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
            isPatientsListVisible: false,
            currentSelectedDoctorAppointment: Object,
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
    methods: {
        submitForm() {
            this.$emit('submit', this.formData);
        },
        openPatientList() {
            this.isPatientsListVisible = true;
        },
        closePatientsList() {
            this.isPatientsListVisible = false;
        },
        handleSelectedDoctorAppointment(selectedDoctorAppointment) {
            this.currentSelectedDoctorAppointment = selectedDoctorAppointment;
            // Update the form data with the selected patient's information
            this.formData.patientName = selectedDoctorAppointment.patientName;
            this.formData.patientId = selectedDoctorAppointment.patientId;
            this.formData.admittingDoctorId = selectedDoctorAppointment.doctorId;
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
            console.log(this.dbFilteredBeds)
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
                }
            }
        }
    },
    mounted() {
        this.fetchWardsByHospitalIdAsync();
        this.fetchAllBedsAsync();
        this.fetchDoctorsByHospitalAsync();
    }
}
</script>

<style scoped>
/* Add any additional styles here */
</style>