export interface PharmacyOrder {
  pharmacyOrderId: string;
  pharmacyOrderRequestId?: string | null;
  orderReferance?: string | null;
  pharmacyId?: string | null;
  itemQty?: number | null;
  totalAmount?: number | null;
  discountAmount?: number | null;
  finalAmount?: number | null;
  balanceAmount?: number | null;
  notes?: string | null;
  status?: string | null;
  createdBy?: string | null;
  createdOn?: Date | string | null;
  modifiedBy?: string | null;
  modifiedOn?: Date | string | null;
  isActive: boolean;
}
