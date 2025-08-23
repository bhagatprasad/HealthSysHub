import { send } from '@/global/API/api.service'
import { endpoints } from '@/environment'

export default {
    // Get all consultations
    async GetAllConsultationsAsync() {
        const url = endpoints.UrlConstants.GetAllConsultationsAsync;
        const response = await send("GET", url);
        return response;
    },

    // Get consultation by appointment ID
    async GetConsultationByAppointmentIdAsync(appointmentId) {
        const url = `${endpoints.UrlConstants.GetConsultationByAppointmentIdAsync}/${appointmentId}`;
        const response = await send("GET", url);
        return response;
    },

    // Get consultation by consultation ID
    async GetConsultationByConsultationIdAsync(consultationId) {
        const url = `${endpoints.UrlConstants.GetConsultationByConsultationIdAsync}/${consultationId}`;
        const response = await send("GET", url);
        return response;
    },

    // Get consultations by doctor ID
    async GetConsultationsByDoctorAsync(doctorId) {
        const url = `${endpoints.UrlConstants.GetConsultationsByDoctorAsync}/${doctorId}`;
        const response = await send("GET", url);
        return response;
    },

    // Get consultations by hospital ID
    async GetConsultationsByHospitalAsync(hospitalId) {
        const url = `${endpoints.UrlConstants.GetConsultationsByHospitalAsync}/${hospitalId}`;
        const response = await send("GET", url);
        return response;
    },

    // Create or update consultation details
    async InsertOrUpdateConsultationDetailsAsync(consultationDetails) {
        const url = endpoints.UrlConstants.InsertOrUpdateConsultationDetailsAsync;
        const response = await send("POST", url, consultationDetails);
        return response;
    }
}