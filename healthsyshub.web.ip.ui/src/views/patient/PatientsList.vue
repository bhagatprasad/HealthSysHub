<template>
    <div v-if="isPatientsListVisible" class="modal-overlay">
        <div class="modal-container">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Select Patient</h5>
                    <button type="button" class="close" @click="closePatientsList" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label for="searchInput">Search Patient</label>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text"><i class="fa fa-search"></i></span>
                                    </div>
                                    <input type="text" class="form-control" id="searchInput" v-model="searchQuery" placeholder="Search by patient name or phone..." @input="filterPatients">
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="table-container">
                        <table class="table table-striped table-hover">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Patient Name</th>
                                    <th>Appointment Date</th>
                                    <th>Health Issue</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(appointment, index) in filteredAppointments" :key="appointment.appointmentId">
                                    <td>{{ index + 1 }}</td>
                                    <td>{{ appointment.patientName }}<br/>{{ appointment.patientPhone }}</td>
                                    <td>{{ appointment.appointmentDate ? formatDate(appointment.appointmentDate) : 'N/A' }}</td>
                                    <td>{{ appointment.status || 'N/A' }}</td>
                                    <td>
                                        <button @click="selectAppointment(appointment)" class="btn btn-primary btn-sm">
                                            Select
                                        </button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <div v-if="filteredAppointments.length === 0" class="text-center py-4">
                            <p>No patients found</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div v-if="isPatientsListVisible" class="modal-backdrop" @click="closePatientsList"></div>
</template>

<script>
import { useAuthStore } from '@/stores/auth.store';
import { showLoader,hideLoader } from '@/components/common/Loader.vue';
import doctorAppointmentService from '@/global/API/appointment.service';

export default {
    name: "PatientsList",
    props: {
        isPatientsListVisible: {
            type: Boolean,
            default: false
        }
    },
    data() {
        return {
            doctorAppointments: [],
            searchQuery: ''
        }
    },
    computed: {
        filteredAppointments() {
            if (!this.searchQuery) {
                return this.doctorAppointments;
            }
            
            const query = this.searchQuery.toLowerCase();
            return this.doctorAppointments.filter(appointment => 
                appointment.PatientName?.toLowerCase().includes(query) ||
                appointment.PatientPhone?.includes(query)
            );
        }
    },
    methods: {
        async fetchDoctorAppointments() {
            showLoader();
            const authStore = useAuthStore();
            const hospitalId = authStore.hospitalInformation?.hospitalId;
            
            if (hospitalId) {
                const response = await doctorAppointmentService.GetActiveDoctorAppointmentsAsync(hospitalId);
                this.doctorAppointments = response || [];
            }
            hideLoader();
        },
        selectAppointment(appointment) {
            this.$emit('submit', appointment); // Emit the selected appointment
            this.closePatientsList(); // Close the modal
        },
        closePatientsList() {
            this.$emit('close'); // Emit close event to parent
            this.searchQuery = ''; // Reset search query
        },
        formatDate(dateString) {
            if (!dateString) return 'N/A';
            const date = new Date(dateString);
            return date.toLocaleDateString();
        }
    },
    mounted() {
        this.fetchDoctorAppointments();
    }
}
</script>

<style scoped>
/* Modal Overlay */
.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 1060;
}

.modal-container {
    background: white;
    border-radius: 8px;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.15);
    max-width: 90%;
    width: 800px;
    max-height: 80vh;
    overflow: hidden;
    z-index: 1061;
}

.modal-content {
    display: flex;
    flex-direction: column;
    height: 100%;
}

.modal-header {
    padding: 1rem 1.5rem;
    border-bottom: 1px solid #dee2e6;
    background: #f8f9fa;
}

.modal-body {
    padding: 1.5rem;
    overflow-y: auto;
    flex: 1;
}

.modal-footer {
    padding: 1rem 1.5rem;
    border-top: 1px solid #dee2e6;
    background: #f8f9fa;
}

/* Table container */
.table-container {
    max-height: 300px;
    overflow-y: auto;
    border: 1px solid #dee2e6;
    border-radius: 4px;
}

.table {
    margin-bottom: 0;
}

.table th {
    position: sticky;
    top: 0;
    background: #f8f9fa;
    z-index: 1;
}

/* Backdrop */
.modal-backdrop {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    z-index: 1050;
}

/* Input group */
.input-group {
    margin-bottom: 1rem;
}

/* Responsive design */
@media (max-width: 768px) {
    .modal-container {
        max-width: 95%;
        width: 95%;
        margin: 1rem;
    }
    
    .table-container {
        max-height: 250px;
    }
}

.btn-sm {
    padding: 0.25rem 0.5rem;
    font-size: 0.875rem;
}

.text-center {
    text-align: center;
}

.py-4 {
    padding-top: 1.5rem;
    padding-bottom: 1.5rem;
}
</style>