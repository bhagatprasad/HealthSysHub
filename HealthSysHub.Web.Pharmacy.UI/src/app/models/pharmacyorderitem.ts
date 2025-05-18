export interface PharmacyOrderItem {
  pharmacyOrderItemId: string;
  pharmacyOrderId?: string | null;
  pharmacyId?: string | null;
  medicineId?: string | null;
  itemQty?: number | null;
  unitPrice?: number | null;
  totalAmount?: number | null;
  createdBy?: string | null;
  createdOn?: Date | string | null;
  modifiedBy?: string | null;
  modifiedOn?: Date | string | null;
  isActive: boolean;
}
