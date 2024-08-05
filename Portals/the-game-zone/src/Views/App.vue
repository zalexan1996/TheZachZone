<script setup lang="ts">
import {RouterView} from 'vue-router';
import NavBar from '@components/NavBar.vue';
import Footer from '@components/Footer.vue';
import ToastPanel from '@components/Common/ToastPanel.vue'
</script>
<template>
  <header>
    <NavBar/>
  </header>
  <main>
    <RouterView/>
  </main>
  <footer>
    <Footer/>
  </footer>
  <ToastPanel/>
</template>