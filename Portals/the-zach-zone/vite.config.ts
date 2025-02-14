import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { resolve } from 'node:path/win32'

export default defineConfig({
  plugins: [vue()],
  server: {
    port: 8000
  },
  resolve: {
    alias: {
      '@': resolve(__dirname, 'src/'),
      "@components": resolve(__dirname, 'src/Components/'),
      "@views": resolve(__dirname, 'src/Views/'),
      "@services": resolve(__dirname, 'src/Services/'),
      "@stores": resolve(__dirname, 'src/Stores/'),
    }
  },
  css: {
    preprocessorOptions: {
      scss: {
        additionalData: `@import "./src/assets/styles/_theme.scss";`,
        quietDeps: true
      }
    }
  },
})