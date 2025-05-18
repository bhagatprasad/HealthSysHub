import { HospitalContactInformation } from "./hospital-contact-information";
import { HospitalContentInformation } from "./hospital-content-information";
import { HospitalDepartmentInformation } from "./hospital-department-information";
import { HospitalSpecialtyInformation } from "./hospital-specialty-information";

export interface HospitalInformation {
  hospitalId?: string | null;
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
  hospitalTypeName?: string | null;
  createdBy?: string | null;
  createdOn?: Date | string | null;
  modifiedBy?: string | null;
  modifiedOn?: Date | string | null;
  isActive: boolean;
  hospitalContactInformation: HospitalContactInformation[];
  hospitalContentInformation: HospitalContentInformation;
  hospitalDepartmentInformation: HospitalDepartmentInformation[];
  hospitalSpecialtyInformation: HospitalSpecialtyInformation[];
}
