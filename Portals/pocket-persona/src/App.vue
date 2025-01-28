<script setup lang="ts">
import { computed } from 'vue'
import { RouterView } from 'vue-router';
import { ToastPanel, useSiteSwitcherStore, SiteSwitcherPanel } from 'tzz-shared'

const siteSwitcherStore = useSiteSwitcherStore();

const mainStyle = computed(() => {
  return siteSwitcherStore.isSwitcherOpen ? 'grid-column: 1;' : 'grid-column: span 2;'
})
</script>

<template>
  <div id="grid" class="w-100 h-100">
    <header>
      <!-- <NavBar/> -->
    </header>
    <main class="w-100 h-100" :style="mainStyle">
      <RouterView/>
    </main>
    <SiteSwitcherPanel id="site-switcher"/>
    <footer>
      <!-- <Footer/> -->
    </footer>
  </div>
  <ToastPanel/>
</template>

<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
