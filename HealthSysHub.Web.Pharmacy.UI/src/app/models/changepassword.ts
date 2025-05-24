export interface ChangePassword {
    id?: string;                   // Guid? in C#
    hospitalId?: string;           // Guid? in C#
    staffId?: string;              // Guid? in C#
    labId?: string;                // Guid? in C# (new property)
    pharmacyId?: string;           // Guid? in C# (new property)
    password?: string;
    createdBy?: string | null;
    createdOn?: Date | string | null;
    modifiedBy?: string | null;
    modifiedOn?: Date | string | null;
}