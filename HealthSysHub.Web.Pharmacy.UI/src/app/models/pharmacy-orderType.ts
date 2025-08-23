export interface PharmacyOrderType {
  pharmacyOrderTypeId?: string;
  name?: string;
  description?: string;
  createdBy?: string;
  createdOn?: string; 
  modifiedBy?: string;
  modifiedOn?: string;
  isActive: boolean;
}