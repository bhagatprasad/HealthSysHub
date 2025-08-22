import { send } from '@/global/API/api.service'
import { endpoints } from '@/environment'


export default {
    async GetInPatientsAsync(currentHospitalId) {
        const url = `${endpoints.UrlConstants.GetInPatientsAsync}/${currentHospitalId}`;
        const response = await send("GET", url);
        return response;
    },
    GetActiveInPatientsAsync() {
        console.log("GetActiveInPatientsAsync")
    }
}