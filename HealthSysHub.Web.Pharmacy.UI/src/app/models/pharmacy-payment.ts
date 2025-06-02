export interface PharmacyPayment {
    paymentId?: string; // Guid in C# is represented as a string in TypeScript
    pharmacyOrderId?: string; // Optional Guid
    hospitalId?: string; // Optional Guid
    pharmacyId?: string; // Optional Guid
    paymentNumber: string; // NOT NULL UNIQUE
    paymentDate?: string; // DateTimeOffset as string
    paymentMethod: string; // NOT NULL
    paymentAmount: number; // NOT NULL
    referenceNumber?: string; // Optional
    status: string; // NOT NULL
    paymentGateway?: string; // Optional
    gatewayResponse?: string; // Optional
    notes?: string; // Optional
    createdBy?: string; // Optional Guid
    createdOn: string; // DateTimeOffset as string
    modifiedBy?: string; // Optional Guid
    modifiedOn?: string; // Optional DateTimeOffset as string
    isActive: boolean; // Default is true
}
