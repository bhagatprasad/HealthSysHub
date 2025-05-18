export interface HospitalDepartmentInformation {
  hospitalDepartmentId?: string | null;
  hospitalId?: string | null;
  departmentId?: string | null;
  departmentName?: string | null;
  headOfDepartment?: string | null;
  createdBy?: string | null;
  createdOn: Date | string;
  modifiedBy?: string | null;
  modifiedOn?: Date | string | null;
  isActive: boolean;
}
