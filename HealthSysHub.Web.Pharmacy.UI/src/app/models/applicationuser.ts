export interface IApplicationUser {
    id?: string;
    fullName?: string;
    firstName?: string;
    lastName?: string;
    email?: string;
    phone?: string;
    roleId?: string; // Guid in C# is typically represented as a string in TypeScript
    roleName?: string;
    hospitalId?: string; // Guid in C# is typically represented as a string in TypeScript
    staffId?: string; // Guid in C# is typically represented as a string in TypeScript
    hospitalName?: string;
    designation?: string;
}
