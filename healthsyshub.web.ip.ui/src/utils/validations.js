// Base validators
const required = (value) => !!value || 'This field is required'
const email = (value) => {
  const pattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
  return pattern.test(value) || 'Invalid email format'
}
const minLength = (min) => (value) => 
  value?.length >= min || `Minimum ${min} characters`
const maxLength = (max) => (value) => 
  (value && value.length <= max) || `Maximum ${max} characters`
const regex = (pattern, message) => (value) => 
  !value || pattern.test(value) || message

// Custom validators
const passwordStrength = (value) => {
  const strongRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/
  return strongRegex.test(value) || 
    'Password must contain uppercase, lowercase, number, and special character'
}

// Validation composer
export const useValidation = () => ({
  // Core validators
  required,
  email,
  minLength,
  maxLength,
  regex,
  
  // Custom validators
  passwordStrength,
  
  // Validator builder
  createValidator: (rules) => (value) => {
    for (const rule of rules) {
      const result = rule(value)
      if (typeof result === 'string') return result
    }
    return true
  }
})