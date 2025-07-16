<template>
  <div>
    <!-- Table card-1 -->
    <div class="col-md-12 col-xl-4">
      <div class="card flat-card">
        <div class="row-table">
          <!-- Card content -->
        </div>
      </div>
      <!-- Widget primary card -->
    </div>
    <!-- Other cards -->
  </div>
</template>

<script>
export default {
  name: 'DashboardCards',
  data() {
    return {
      stats: [
        { icon: 'eye', value: '10k', label: 'Visitors', color: 'text-c-green' },
        // Other stats data
      ]
    }
  }
}
</script>