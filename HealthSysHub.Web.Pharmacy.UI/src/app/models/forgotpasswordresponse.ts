export interface ForgotPasswordResponse {
  id?: string; // Use string for GUID representation in TypeScript
  staffId?: string; // Use string for GUID representation in TypeScript
  success: boolean;
  message: string;
}
