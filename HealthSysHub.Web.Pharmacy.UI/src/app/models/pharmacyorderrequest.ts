export interface PharmacyOrderRequest {
  pharmacyOrderRequestId: string;
  patientPrescriptionId?: string | null;
  hospitalId?: string | null;
  pharmacyId?: string | null;
  patientId?: string | null;
  hospitalName?: string | null;
  doctorName?: string | null;
  name?: string | null;
  phone?: string | null;
  notes?: string | null;
  status?: string | null;
  createdBy?: string | null;
  createdOn?: Date | string | null;
  modifiedBy?: string | null;
  modifiedOn?: Date | string | null;
  isActive: boolean;
}
