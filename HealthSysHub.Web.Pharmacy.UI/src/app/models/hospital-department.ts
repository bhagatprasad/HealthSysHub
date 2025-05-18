export interface HospitalDepartment {
  hospitalDepartmentId: string;
  hospitalId?: string | null;
  departmentId?: string | null;
  headOfDepartment: string;
  createdBy?: string | null;
  createdOn: Date | string;
  modifiedBy?: string | null;
  modifiedOn?: Date | string | null;
  isActive: boolean;
}
