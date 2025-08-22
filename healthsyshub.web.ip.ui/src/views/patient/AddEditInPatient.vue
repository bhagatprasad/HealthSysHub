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
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="Name">Patient Name</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-user"></i></span>
                                </div>
                                <input type="text" class="form-control" id="Name" v-model="formData.name" placeholder="Enter Patient Name" required>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="Phone">Patient Phone</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-phone"></i></span>
                                </div>
                                <input type="text" class="form-control" id="Phone" v-model="formData.phone" placeholder="Enter Patient Phone" required>
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
</template>

<script>
export default {
    name: "addEditInPatient",
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
            formData: {
                name: this.patientData ? this.patientData.name : '',
                phone: this.patientData ? this.patientData.phone : '',
                // Initialize other fields similarly
            }
        }
    },
    methods: {
        submitForm() {
            this.$emit('submit', this.formData);
        }
    },
    watch: {
        patientData: {
            immediate: true,
            handler(newVal) {
                if (newVal) {
                    this.formData = {
                        name: newVal.name,
                        phone: newVal.phone,
                        // Update other fields similarly
                    };
                }
            }
        }
    }
}
</script>

<style scoped>
/* Add any additional styles here */
</style>