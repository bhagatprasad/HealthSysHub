<template>
    <div class="row">
        <div class="col-xl-12">
            <div class="card">
                <div class="card-header d-flex align-items-center">
                    <div class="row col-xl-12">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="searchInput">Search</label>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text"><i class="fa fa-search"></i></span>
                                    </div>
                                    <input type="text" class="form-control" id="searchInput" placeholder="Search..."
                                        required>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="startDate">Start Date</label>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text"><i class="fa fa-calendar"></i></span>
                                    </div>
                                    <input type="date" class="form-control" id="startDate" required>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="endDate">End Date</label>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text"><i class="fa fa-calendar"></i></span>
                                    </div>
                                    <input type="date" class="form-control" id="endDate" required>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <button type="button" class="btn btn-primary form-control" style="margin-top: 28px;"
                                @click="handleSearchOnGird" id="searchButton">Search</button>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="card border-top-class">
                        <gridheader @add="openSidebar" @edit="openSidebarWithData" />
                        <div class="card-body card-body-padding">
                            <div id="InPatientsGrid" v-if="filteredInPatientsData">
                                <div class="table-responsive">
                                    <table class="table table-striped">
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th>Patient Name</th>
                                                <th>Phone</th>
                                                <th>Age</th>
                                                <th>Gender</th>
                                                <th>Address</th>
                                                <th>Health Issue</th>
                                                <th>Admission Date</th>
                                                <th>Discharge Date</th>
                                                <th>Status</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(patient, index) in filteredInPatientsData"
                                                :key="patient.inpatientId">
                                                <td>{{ index + 1 }}</td>
                                                <td>{{ patient.patientName }}</td>
                                                <td>{{ patient.patientPhone }}</td>
                                                <td>{{ patient.patientAge }}</td>
                                                <td>{{ patient.patientGender }}</td>
                                                <td>{{ patient.patientAddress }}</td>
                                                <td>{{ patient.healthIssue }}</td>
                                                <td>{{ patient.admissionDate }}</td>
                                                <td>{{ patient.dischargeDate }}</td>
                                                <td>{{ patient.currentStatus }}</td>
                                                <td>
                                                    <button @click="openSidebarWithData(patient)"
                                                        class="btn btn-primary">Edit</button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </div>
                            </div>
                            <div v-else class="card-header text-center">
                                no patient founds
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <add-edit-in-patient v-if="isSidebarOpen" :patientData="selectedPatient" :isVisible="isSidebarOpen"
                @handleClose="closeSidebar" @handleSave="handleSubmit" />
            <div v-if="isSidebarOpen" class="modal-backdrop fade show" @click="closeSidebar"></div>
        </div>
    </div>
</template>

<script>
import { showLoader, hideLoader } from '@/components/common/Loader.vue'
import { useAuthStore } from '@/stores/auth.store'
import inpatientService from '@/global/API/inpatient.service'
import consultationService from '@/global/API/consultation.service'
import gridheader from '@/components/common/GridHeader.vue'
import AddEditInPatient from '@/views/patient/AddEditInPatient.vue'

export default {
    name: 'Dashboard',
    components: {
        gridheader,
        AddEditInPatient
    },
    data() {
        return {
            apiData: null,
            isSidebarOpen: false,
            selectedPatient: null,
            dbDoctorConsultationsData: [],
            dbInPatientsData: [],
            combinedPatientData: [],
            searchQuery: '' // Added for search functionality
        }
    },
    computed: {
        applicationUser() {
            return this.authStore.user?.name || 'User ';
        },
        userId() {
            return this.authStore.user?.id;
        },
        currentHospital() {
            return this.authStore.hospitalInformation?.hospitalId;
        },
        filteredInPatientsData() {
            // Implement search and filter logic
            return this.combinedPatientData.filter(patient => {
                return (
                    patient.patientName.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
                    patient.patientPhone.includes(this.searchQuery) ||
                    patient.healthIssue.toLowerCase().includes(this.searchQuery.toLowerCase())
                );
            });
        }
    },
    setup() {
        const authStore = useAuthStore();
        return { authStore };
    },
    async created() {
        await this.fetchData();
    },
    methods: {
        async fetchData() {
            showLoader();
            try {
                const response = await inpatientService.GetInPatientsAsync(this.currentHospital);
                this.dbInPatientsData = response || [];

                if (this.currentHospital) {
                    const consultationResponse = await consultationService.GetConsultationsByHospitalAsync(this.currentHospital);
                    this.dbDoctorConsultationsData = consultationResponse || [];
                }

                this.combineData();
            } catch (error) {
                console.error('Error fetching data:', error);
            } finally {
                hideLoader();
            }
        },
        openSidebar() {
            this.selectedPatient = null; // Reset for adding new patient
            this.isSidebarOpen = true;
        },
        openSidebarWithData(patient) {
            this.selectedPatient = patient; // Set the patient data for editing
            this.isSidebarOpen = true;
        },
        closeSidebar() {
            this.isSidebarOpen = false;
            this.fetchData(); // Refresh the grid after closing the sidebar
        },
        async handleSubmit(patientData) {
            showLoader();
            try {
                await inpatientService.InsertOrUpdateInpatientAsync(patientData);
                this.closeSidebar(); // Close sidebar after submission
            } catch (error) {
                console.error('Error submitting data:', error);
            } finally {
                hideLoader();
            }
        },
        combineData() {
            // Combine the consultation and inpatient data
            this.combinedPatientData = this.dbInPatientsData.map(inpatient => {
                // Find matching consultation data
                const consultation = this.dbDoctorConsultationsData.find(
                    consult => consult.patientDetails.patientId === inpatient.patientId
                );

                // If consultation found, extract patient details
                let healthIssue = null;
                let patientName = null;
                let patientPhone = null;
                let patientAge = null;
                let patientGender = null;
                let patientAddress = null;
                let patientVitals = null;

                if (consultation && consultation.patientDetails) {
                    healthIssue = consultation.patientDetails.healthIssue;
                    patientName = consultation.patientDetails.name;
                    patientPhone = consultation.patientDetails.phone;
                    patientAge = consultation.patientDetails.age;
                    patientGender = consultation.patientDetails.gender;
                    patientAddress = consultation.patientDetails.address;
                    patientVitals = consultation.patientDetails.patientVitalDetails;
                }

                // Return combined data object
                return {
                    inpatientId: inpatient.inpatientId,
                    patientId: inpatient.patientId,
                    hospitalId: inpatient.hospitalId,
                    admissionDate: inpatient.admissionDate,
                    dischargeDate: inpatient.dischargeDate,
                    wardId: inpatient.wardId,
                    bedId: inpatient.bedId,
                    admittingDoctorId: inpatient.admittingDoctorId,
                    currentStatus: inpatient.currentStatus,
                    reasonForAdmission: inpatient.reasonForAdmission,
                    expectedStayDuration: inpatient.expectedStayDuration,
                    createdBy: inpatient.createdBy,
                    createdOn: inpatient.createdOn,
                    modifiedBy: inpatient.modifiedBy,
                    modifiedOn: inpatient.modifiedOn,
                    isActive: inpatient.isActive,
                    healthIssue: healthIssue,
                    patientName: patientName,
                    patientPhone: patientPhone,
                    patientAge: patientAge,
                    patientGender: patientGender,
                    patientAddress: patientAddress,
                    patientVitals: patientVitals
                };
            });

            console.log('combinedPatientData..', JSON.stringify(this.combinedPatientData));
        },
        handleSearchOnGrid() {
            // This method can be used to trigger search/filter if needed
            this.combineData(); // Re-combine data if necessary
        }
    }
}
</script>


<style scoped>
/* Sidebar styles */
.AddEditInPatientFormSidebar {
    position: fixed;
    top: 0;
    right: 0;
    width: 400px;
    /* Adjust width as needed */
    height: 100%;
    background-color: white;
    box-shadow: -2px 0 5px rgba(0, 0, 0, 0.5);
    transform: translateX(100%);
    /* Start off-screen */
    transition: transform 0.3s ease;
    z-index: 1050;
    /* Ensure it is above other content */
}

.AddEditInPatientFormSidebar.show {
    transform: translateX(0);
    /* Slide in */
}

.modal-backdrop {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    z-index: 1040;
    /* Ensure it is below the sidebar */
}
</style>