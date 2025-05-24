export interface ActivateOrInActivateUser  {
  id?: string; // Use string for GUID representation in TypeScript
  hospitalId?: string; // Use string for GUID representation in TypeScript
  staffId?: string; // Use string for GUID representation in TypeScript
  labId?: string; // Use string for GUID representation in TypeScript
  pharmacyId?: string; // Use string for GUID representation in TypeScript
  isActive: boolean;
  modifiedBy?: string; // Use string for GUID representation in TypeScript
  modifiedOn?: Date; // Use Date for DateTimeOffset representation in TypeScript
}

