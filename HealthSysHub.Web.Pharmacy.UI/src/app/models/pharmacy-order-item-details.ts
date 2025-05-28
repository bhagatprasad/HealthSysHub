export interface PharmacyOrderItemDetails {
  pharmacyOrderItemId: string; // Guid as string
  pharmacyId?: string | null; // Guid as string
  medicineId?: string | null; // Guid as string
  medicineName?: string | null;
  itemQty?: number | null; // long as number
  unitPrice?: number | null; // decimal as number
  totalAmount?: number | null; // decimal as number
  createdBy?: string | null; // Guid as string
  createdOn?: string | null; // DateTimeOffset as ISO string
  modifiedBy?: string | null; // Guid as string
  modifiedOn?: string | null; // DateTimeOffset as ISO string
  isActive: boolean;
}
