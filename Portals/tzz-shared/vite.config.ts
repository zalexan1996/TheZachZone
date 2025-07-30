import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { resolve } from 'node:path'

export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': resolve(__dirname, 'src/'),
      "@components": resolve(__dirname, 'src/components/'),
      '@stores': resolve(__dirname, 'src/stores/')
    }
  },
  css: {
    preprocessorOptions: {
      scss: {
        additionalData: `@import "./public/theme.scss";`,
        quietDeps: true
      }
    }
  },
  build: {
    minify: true,
    lib: {
      entry: resolve(__dirname, 'src/index.ts'),
      name: 'tzz-shared',
      fileName: 'tzz-shared'
    },
    rollupOptions: {
      external: [
        'vue',
        'vue-router',
        'vee-validate',
        'bootstrap',
        '@fortawesome/fontawesome-free',
        'pinia',
        '@popperjs'
      ],
      output: {
        globals: {
          vue: 'Vue',
          'vue-router': 'vue-router',
          'bootstrap': 'bootstrap',
          '@fortawesome/fontawesome-free': '@fortawesome/fontawesome-free',
          'pinia': 'pinia'
        }
      }
    }
  }
})
