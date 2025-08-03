import { send } from '@/global/API/api.service'
import { endpoints } from '@/environment'

export default {

    async getHospitalInformationByIdAsync(currentHospitalId) {
        const url = `${endpoints.UrlConstants.GetHospitalInformationByIdAsync}/${currentHospitalId}`;

        const response = await send("GET", url);
        return response;

    }
}