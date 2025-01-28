import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import {resolve} from 'node:path'

export default defineConfig({
  plugins: [vue()],
  server: {
    port: 8020,
  },
  
  resolve: {
    alias: {
      '@': resolve(__dirname, 'src/'),
      "@components": resolve(__dirname, 'src/Components/'),
      "@views": resolve(__dirname, 'src/Views/'),
      "@services": resolve(__dirname, 'src/Services/'),
      "@stores": resolve(__dirname, 'src/Stores/'),
      '@games': resolve(__dirname, 'public/games/')
    }
  },
  css: {
    preprocessorOptions: {
        scss: {
            quietDeps: true
        },
    }
  },
})
