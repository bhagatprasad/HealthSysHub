export interface PharmacyOrderRequestItem {
  pharmacyOrderRequestItemId: string;
  pharmacyOrderRequestId?: string | null;
  pharmacyId?: string | null;
  hospitalId?: string | null;
  medicineId?: string | null;
  itemQty?: number | null;
  usage?: string | null;
  createdBy?: string | null;
  createdOn?: Date | string | null;
  modifiedBy?: string | null;
  modifiedOn?: Date | string | null;
  isActive: boolean;
}
