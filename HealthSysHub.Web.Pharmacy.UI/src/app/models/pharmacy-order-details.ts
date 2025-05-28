import { PharmacyOrderItemDetails } from "./pharmacy-order-item-details";

export interface PharmacyOrderDetails {
  pharmacyOrderId: string; // Guid as string
  pharmacyOrderRequestId?: string | null; // Guid as string
  pharmacyId?: string | null; // Guid as string
  orderReference?: string | null;
  name?: string | null;
  phone?: string | null;
  doctorName?: string | null;
  itemQty?: number | null; // long as number
  totalAmount?: number | null; // decimal as number
  discountAmount?: number | null; // decimal as number
  finalAmount?: number | null; // decimal as number
  balanceAmount?: number | null; // decimal as number
  notes?: string | null;
  status?: string | null;
  createdBy?: string | null; // Guid as string
  createdOn?: string | null; // DateTimeOffset as ISO string
  modifiedBy?: string | null; // Guid as string
  modifiedOn?: string | null; // DateTimeOffset as ISO string
  isActive: boolean;
  pharmacyOrderItemDetails: PharmacyOrderItemDetails[];
}