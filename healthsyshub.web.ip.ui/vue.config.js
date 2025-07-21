const { defineConfig } = require('@vue/cli-service')
const path = require('path')

module.exports = defineConfig({
  transpileDependencies: true,
  configureWebpack: {
    devtool: 'source-map', // Add this line for proper source mapping
    resolve: {
      alias: {
        '@': path.resolve(__dirname, 'src'),
        '@assets': path.resolve(__dirname, 'src/assets')
      }
    }
  },
  // Extra protection for source maps
  productionSourceMap: true // Ensures source maps are generated in production builds too
})