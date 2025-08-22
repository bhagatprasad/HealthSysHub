import { send } from '@/global/API/api.service'
import { endpoints } from '@/environment'

export default {
    // Get appointments with optional filters
    async GetDoctorAppointmentsAsync(hospitalId, { dateTime = null, doctorId = null, patientName = null, phone = null } = {}) {
        const url = `${endpoints.UrlConstants.GetDoctorAppointmentsAsync}/${hospitalId}`;
        const params = {};
        if (dateTime) params.dateTime = dateTime;
        if (doctorId) params.doctorId = doctorId;
        if (patientName) params.patientName = patientName;
        if (phone) params.phone = phone;
        const response = await send("GET", url, null, params);
        return response;
    },

    // Get single appointment by ID
    async GetDoctorAppointmentByIdAsync(appointmentId) {
        const url = `${endpoints.UrlConstants.GetDoctorAppointmentByIdAsync}/${appointmentId}`;
        const response = await send("GET", url);
        return response;
    },

    // Create or update appointment
    async InsertOrUpdateDoctorAppointment(doctorAppointment) {
        const url = endpoints.UrlConstants.InsertOrUpdateDoctorAppointment;
        const response = await send("POST", url, doctorAppointment);
        return response;
    },

    // Delete appointment
    async DeleteDoctorAppointmentAsync(appointmentId) {
        const url = `${endpoints.UrlConstants.DeleteDoctorAppointmentAsync}/${appointmentId}`;
        const response = await send("DELETE", url);
        return response;
    },

    // Get active appointments
    async GetActiveDoctorAppointmentsAsync(hospitalId) {
        const url = `${endpoints.UrlConstants.GetActiveDoctorAppointmentsAsync}/${hospitalId}`;
        const response = await send("GET", url);
        return response;
    },

    // Get appointments by date range
    async GetDoctorAppointmentsByDateRangeAsync(hospitalId, startDate, endDate) {
        const url = `${endpoints.UrlConstants.GetDoctorAppointmentsByDateRangeAsync}/${hospitalId}`;
        const params = {
            startDate: startDate.toISOString(),
            endDate: endDate.toISOString()
        };
        const response = await send("GET", url, null, params);
        return response;
    },

    // Generate token number
    async GenerateTokenNumberAsync(hospitalId, doctorId = null, appointmentDate) {
        const url = `${endpoints.UrlConstants.GenerateTokenNumberAsync}/${hospitalId}`;
        const params = {
            doctorId: doctorId || '',
            appointmentDate: appointmentDate.toISOString()
        };
        const response = await send("GET", url, null, params);
        return response;
    },

    // Get appointment details with optional filters
    async GetDoctorAppointmentDetailsAsync(hospitalId, { dateTime = null, doctorId = null, patientName = null, phone = null } = {}) {
        const url = `${endpoints.UrlConstants.GetDoctorAppointmentDetailsAsync}/${hospitalId}`;
        const params = {};
        if (dateTime) params.dateTime = dateTime;
        if (doctorId) params.doctorId = doctorId;
        if (patientName) params.patientName = patientName;
        if (phone) params.phone = phone;
        const response = await send("GET", url, null, params);
        return response;
    },

    // Get appointments report
    async GetAppointmentsReportAsync(request) {
        const url = endpoints.UrlConstants.GetAppointmentsReportAsync;
        const response = await send("POST", url, request);
        return response;
    }
}
