export interface ProcessPharmacyOrderRequest {
  pharmacyOrderRequestId?: string; // Guid represented as string
  status?: string | null;
  notes?: string | null;
  modifiedOn?: string | null; // DateTimeOffset as ISO string
  modifiedBy?: string | null; // Guid represented as string
}