import { send } from '@/global/API/api.service'
import { endpoints } from '@/environment'

export default {
    // Get inpatients by hospital ID
    async GetInPatientsAsync(hospitalId) {
        const url = `${endpoints.UrlConstants.GetInPatientsAsync}/${hospitalId}`;
        const response = await send("GET", url);
        return response;
    },

    // Get active inpatients
    async GetActiveInPatientsAsync() {
        const url = endpoints.UrlConstants.GetActiveInPatientsAsync;
        const response = await send("GET", url);
        return response;
    },

    // Get inpatient by ID
    async GetInPatientByIdAsync(inpatientId) {
        const url = `${endpoints.UrlConstants.GetInPatientByIdAsync}/${inpatientId}`;
        const response = await send("GET", url);
        return response;
    },

    // Create or update inpatient
    async InsertOrUpdateInpatientAsync(inpatient) {
        const url = endpoints.UrlConstants.InsertOrUpdateInpatientAsync;
        const response = await send("POST", url, inpatient);
        return response;
    },

    // Delete inpatient
    async DeleteInpatientAsync(inpatientId) {
        const url = `${endpoints.UrlConstants.DeleteInpatientAsync}/${inpatientId}`;
        const response = await send("DELETE", url);
        return response;
    },

    // Get inpatients by status
    async GetInpatientsByStatusAsync(status) {
        const url = `${endpoints.UrlConstants.GetInpatientsByStatusAsync}/${encodeURIComponent(status)}`;
        const response = await send("GET", url);
        return response;
    }
}