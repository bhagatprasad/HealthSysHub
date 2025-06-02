import { PharmacyOrderDetails } from "./pharmacy-order-details";

export interface PharmacyPaymentDetail extends PharmacyOrderDetails {
  paymentId?: string; // Guid as string
  hospitalId?: string; // Guid as string
  paymentNumber?: string | null;
  paymentDate?: string | null; // ISO string for DateTimeOffset
  paymentMethod?: string | null;
  paymentAmount?: number | null;
  referenceNumber?: string | null;
  paymentStatus?: string | null;
  paymentGateway?: string | null;
  gatewayResponse?: string | null;
  paymentNotes?: string | null;
}