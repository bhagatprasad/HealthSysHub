export interface ResetPassword {
  id?: string;          // Use string for GUID representation in TypeScript
  staffId?: string;     // Use string for GUID representation in TypeScript
  password: string;
  modifiedBy?: string;  // Use string for GUID representation in TypeScript
  modifiedOn?: Date;    // Use Date for DateTimeOffset representation in TypeScript
}