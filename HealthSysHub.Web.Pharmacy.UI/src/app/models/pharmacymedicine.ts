export interface PharmacyMedicine {
  medicineId: string;
  medicineName?: string | null;
  genericName?: string | null;
  dosageForm?: string | null;
  strength?: string | null;
  manufacturer?: string | null;
  batchNumber?: string | null;
  expiryDate?: Date | string | null;
  pricePerUnit?: number | null;
  stockQuantity: number;
  pharmacyId?: string | null;
  createdBy?: string | null;
  createdOn?: Date | string | null;
  modifiedBy?: string | null;
  modifiedOn?: Date | string | null;
  isActive: boolean;
}
