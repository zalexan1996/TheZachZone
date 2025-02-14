<script setup lang="ts">
import { computed } from 'vue'
import {RouterView} from 'vue-router';
import NavBar from '@components/NavBar.vue';
import Footer from '@components/Footer.vue';
import { ToastPanel, useSiteSwitcherStore, SiteSwitcherPanel } from 'tzz-shared'

const siteSwitcherStore = useSiteSwitcherStore();

const mainStyle = computed(() => {
  return siteSwitcherStore.isSwitcherOpen ? 'grid-column: 1;' : 'grid-column: span 2;'
})
</script>
<template>
  <div id="grid" class="w-100 h-100">
    <header>
      <NavBar/>
    </header>
    <main class="w-100 h-100" :style="mainStyle">
      <RouterView/>
    </main>
    <SiteSwitcherPanel id="site-switcher"/>
    <footer>
      <Footer/>
    </footer>
  </div>
  <ToastPanel/>
</template>

<style scoped lang="scss">

#grid {
  display: grid;
  grid-template-rows: min-content auto min-content;
  grid-template-columns: auto 20vw;
}

header {
  grid-row: 1;
  grid-column: span 2;
}

main {
  grid-row: 2;
  height: auto;
}

#right-pane {
  grid-row: 2;
  grid-column: 2;
  display: relative;
}

footer {
  grid-row: 3;
  grid-column: span 2;
}
</style>