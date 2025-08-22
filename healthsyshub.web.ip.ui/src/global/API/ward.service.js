import { send } from '@/global/API/api.service'
import { endpoints } from '@/environment'

export default {
    // Get all wards
    async GetAllWardsAsync() {
        const url = endpoints.UrlConstants.GetAllWardsAsync;
        const response = await send("GET", url);
        return response;
    },

    // Get active wards
    async GetActiveWardsAsync() {
        const url = endpoints.UrlConstants.GetActiveWardsAsync;
        const response = await send("GET", url);
        return response;
    },

    // Get ward by ID
    async GetWardByIdAsync(wardId) {
        const url = `${endpoints.UrlConstants.GetWardByIdAsync}/${wardId}`;
        const response = await send("GET", url);
        return response;
    },

    // Get wards by hospital ID
    async GetWardsByHospitalIdAsync(hospitalId) {
        const url = `${endpoints.UrlConstants.GetWardsByHospitalIdAsync}/${hospitalId}`;
        const response = await send("GET", url);
        return response;
    },

    // Create or update ward
    async InsertOrUpdateWardAsync(ward) {
        const url = endpoints.UrlConstants.InsertOrUpdateWardAsync;
        const response = await send("POST", url, ward);
        return response;
    },

    // Delete ward
    async DeleteWardAsync(wardId) {
        const url = `${endpoints.UrlConstants.DeleteWardAsync}/${wardId}`;
        const response = await send("DELETE", url);
        return response;
    }
}