import { send } from '@/global/API/api.service'
import { endpoints } from '@/environment'
import { useAuthStore } from '@/stores/auth.store'


export default {
    GetInPatientsAsync() {
        console.log("GetInPatientsAsync")
    },
    GetActiveInPatientsAsync() {
        console.log("GetActiveInPatientsAsync")
    }
}