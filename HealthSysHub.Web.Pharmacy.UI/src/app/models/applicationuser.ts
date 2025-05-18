export interface IApplicationUser {
    id?: string;                   // Guid? in C#
    fullName?: string;             // string? in C#
    firstName?: string;            // string? in C#
    lastName?: string;             // string? in C#
    email?: string;                // string? in C#
    phone?: string;                // string? in C#
    roleId?: string;               // Guid? in C#
    roleName?: string;             // string? in C#
    hospitalId?: string;           // Guid? in C#
    staffId?: string;              // Guid? in C#
    labId?: string;                // Guid? in C# (new property)
    pharmacyId?: string;           // Guid? in C# (new property)
    hospitalName?: string;         // string? in C#
    designation?: string;          // string? in C#
}