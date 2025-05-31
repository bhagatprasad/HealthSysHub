export interface Pharmacy {
  pharmacyId: string;
  pharmacyName: string;
  pharmacyCode: string;
  registrationNumber: string;
  address: string;
  city: string;
  state: string;
  country: string;
  postalCode: string;
  phoneNumber: string;
  email: string;
  website: string;
  logoUrl: string;
  hospitalId?: string | null;
  createdBy?: string | null;
  createdOn: Date | string;
  modifiedBy?: string | null;
  modifiedOn?: Date | string | null;
  isActive: boolean;
}

