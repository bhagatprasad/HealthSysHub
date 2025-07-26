import { useToast } from 'vue-toastification'

const toast = useToast()

// Default icons for each type (can be SVG components)
const DEFAULT_ICONS = {
  success: '✅',
  error: '❌',
  info: 'ℹ️',
  warning: '⚠️'
}

// Default timeouts (ms)
const DEFAULT_TIMEOUTS = {
  success: 2000,
  error: 4000,
  info: 3000,
  warning: 3500
}

export default {
  success(msg, options = {}) {
    return toast.success(msg, {
      timeout: DEFAULT_TIMEOUTS.success,
      icon: DEFAULT_ICONS.success,
      ...options
    })
  },
  error(msg, options = {}) {
    return toast.error(msg, {
      timeout: DEFAULT_TIMEOUTS.error,
      icon: DEFAULT_ICONS.error,
      ...options
    })
  },
  info(msg, options = {}) {
    return toast.info(msg, {
      timeout: DEFAULT_TIMEOUTS.info,
      icon: DEFAULT_ICONS.info,
      ...options
    })
  },
  warning(msg, options = {}) {
    return toast.warning(msg, {
      timeout: DEFAULT_TIMEOUTS.warning,
      icon: DEFAULT_ICONS.warning,
      ...options
    })
  },
  // Custom toast method
  custom(payload, options = {}) {
    return toast(payload, {
      timeout: 2500,
      ...options
    })
  }
}