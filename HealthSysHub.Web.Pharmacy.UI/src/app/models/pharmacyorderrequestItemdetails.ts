export interface PharmacyOrderRequestItemDetails {
  pharmacyOrderRequestItemId?: string;
  hospitalId?: string;
  pharmacyId?: string;
  medicineId?: string;
  medicineName?: string;
  itemQty?: number;
  usage?: string;
  createdBy?: string;
  createdOn?: Date;
  modifiedBy?: string;
  modifiedOn?: Date;
  isActive: boolean;
}