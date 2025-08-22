import { send } from '@/global/API/api.service'
import { endpoints } from '@/environment'

export default {
    // Get all beds
    async GetAllBeds() {
        const url = endpoints.UrlConstants.GetAllBeds;
        const response = await send("GET", url);
        return response;
    },

    // Get active beds
    async GetActiveBeds() {
        const url = endpoints.UrlConstants.GetActiveBeds;
        const response = await send("GET", url);
        return response;
    },

    // Get bed by ID
    async GetBedById(bedId) {
        const url = `${endpoints.UrlConstants.GetBedById}/${bedId}`;
        const response = await send("GET", url);
        return response;
    },

    // Get beds by ward ID
    async GetBedsByWard(wardId) {
        const url = `${endpoints.UrlConstants.GetBedsByWard}/${wardId}`;
        const response = await send("GET", url);
        return response;
    },

    // Get beds by status
    async GetBedsByStatus(status) {
        const url = `${endpoints.UrlConstants.GetBedsByStatus}/${encodeURIComponent(status)}`;
        const response = await send("GET", url);
        return response;
    },

    // Get beds by type
    async GetBedsByType(bedType) {
        const url = `${endpoints.UrlConstants.GetBedsByType}/${encodeURIComponent(bedType)}`;
        const response = await send("GET", url);
        return response;
    },

    // Create or update bed
    async CreateOrUpdateBed(wardBed) {
        const url = endpoints.UrlConstants.CreateOrUpdateBed;
        const response = await send("POST", url, wardBed);
        return response;
    },

    // Delete bed
    async DeleteBed(bedId) {
        const url = `${endpoints.UrlConstants.DeleteBed}/${bedId}`;
        const response = await send("DELETE", url);
        return response;
    },

    // Get bed count by ward
    async GetBedCountByWard(wardId) {
        const url = `${endpoints.UrlConstants.GetBedCountByWard}/${wardId}`;
        const response = await send("GET", url);
        return response;
    },

    // Get available beds count
    async GetAvailableBedsCount(wardId) {
        const url = `${endpoints.UrlConstants.GetAvailableBedsCount}/${wardId}`;
        const response = await send("GET", url);
        return response;
    }
}