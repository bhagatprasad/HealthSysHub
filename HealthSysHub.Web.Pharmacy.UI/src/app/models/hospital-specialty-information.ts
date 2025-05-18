export interface HospitalSpecialtyInformation {
  hospitalSpecialtyId?: string | null;
  hospitalId?: string | null;
  specializationId?: string | null;
  specializationName?: string | null;
  createdBy?: string | null;
  createdOn: Date | string;
  modifiedBy?: string | null;
  modifiedOn?: Date | string | null;
  isActive: boolean;
}
