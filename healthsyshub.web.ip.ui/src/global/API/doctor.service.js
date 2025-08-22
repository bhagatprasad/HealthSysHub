import { send } from '@/global/API/api.service'
import { endpoints } from '@/environment'

export default {
    // Create or update doctor
    async InsertOrUpdateDoctorAsync(doctor) {
        const url = endpoints.UrlConstants.InsertOrUpdateDoctorAsync;
        const response = await send("POST", url, doctor);
        return response;
    },

    // Get doctor by ID
    async GetDoctorByIdAsync(doctorId) {
        const url = `${endpoints.UrlConstants.GetDoctorByIdAsync}/${doctorId}`;
        const response = await send("GET", url);
        return response;
    },

    // Get doctors by hospital ID
    async GetDoctorsByHospitalAsync(hospitalId) {
        const url = `${endpoints.UrlConstants.GetDoctorsByHospitalAsync}/${hospitalId}`;
        const response = await send("GET", url);
        return response;
    }
}