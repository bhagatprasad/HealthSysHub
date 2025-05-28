export interface PharmacyOrdersProcessRequest {
  pharmacyOrderId?: string; // Guid as string
  status?: string | null;
  notes?: string | null;
  modifiedOn?: string | null; // DateTimeOffset as ISO string
  modifiedBy?: string | null; // Guid as string
}
