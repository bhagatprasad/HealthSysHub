import { send } from '@/global/API/api.service'
import { endpoints } from '@/environment'
import { useAuthStore } from '@/stores/auth.store'


export default {
    async getBedAssignmentsAsync() {
        var response = await send("GET", endpoints.UrlConstants.GetBedAssignmentsAsync);
        return response;
    },
    async getActiveBedAssignmentsAsync() {
        var response = await send("GET", endpoints.UrlConstants.GetActiveBedAssignmentsAsync);
        return response;
    }
}