export interface HospitalContact {
  hospitalContactId: string;
  hospitalId?: string | null;
  contactType?: string | null;
  phone?: string | null;
  email?: string | null;
  createdBy?: string | null;
  createdOn?: Date | string | null;
  modifiedBy?: string | null;
  modifiedOn?: Date | string | null;
  isActive: boolean;
}
