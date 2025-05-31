export interface PharmacyOrderRequest {
  pharmacyOrderRequestId: string;
  patientPrescriptionId?: string;
  hospitalId?: string;
  pharmacyId?: string;
  patientId?: string;
  hospitalName?: string;
  doctorName?: string;
  name?: string;
  phone?: string;
  notes?: string;
  status?: string;
  createdBy?: string;
  createdOn?: Date;
  modifiedBy?: string;
  modifiedOn?: Date;
  isActive: boolean;
}