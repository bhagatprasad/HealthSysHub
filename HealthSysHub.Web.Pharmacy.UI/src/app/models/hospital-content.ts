export interface HospitalContent {
  hospitalContentId: string;
  hospitalId?: string | null;
  description?: string | null;
  createdBy?: string | null;
  createdOn: Date | string;
  modifiedBy?: string | null;
  modifiedOn?: Date | string | null;
  isActive: boolean;
}
