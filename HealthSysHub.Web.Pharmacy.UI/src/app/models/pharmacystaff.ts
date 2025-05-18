export interface PharmacyStaff {
  staffId: string;
  firstName?: string | null;
  lastName?: string | null;
  designation?: string | null;
  phoneNumber?: string | null;
  email?: string | null;
  hospitalId?: string | null;
  pharmacyId?: string | null;
  createdOn?: Date | string | null;
  createdBy?: string | null;
  modifiedOn?: Date | string | null;
  modifiedBy?: string | null;
  isActive: boolean;
}
