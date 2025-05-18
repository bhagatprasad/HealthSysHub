export interface Hospital {
  hospitalId: string;
  hospitalName?: string | null;
  hospitalCode?: string | null;
  registrationNumber?: string | null;
  address?: string | null;
  city?: string | null;
  state?: string | null;
  country?: string | null;
  postalCode?: string | null;
  phoneNumber?: string | null;
  email?: string | null;
  website?: string | null;
  logoUrl?: string | null;
  hospitalTypeId?: string | null;
  createdBy?: string | null;
  createdOn?: Date | string | null;
  modifiedBy?: string | null;
  modifiedOn?: Date | string | null;
  isActive: boolean;
}
