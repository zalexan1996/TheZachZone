<script setup lang="ts">
import { computed } from 'vue'
import { RouterView } from 'vue-router';
import { ToastPanel, useSiteSwitcherStore, SiteSwitcherPanel, Panel } from 'tzz-shared'
import Footer from '@/components/Footer.vue';
import NavBar from '@/components/NavBar.vue';
const siteSwitcherStore = useSiteSwitcherStore();

const mainStyle = computed(() => {
  return siteSwitcherStore.isSwitcherOpen ? 'grid-column: 1;' : 'grid-column: span 2;'
})
</script>

<template>
  <div id="grid" class="w-100 h-100">
    <main class="w-100 h-100" :style="mainStyle">
      <div class="d-flex w-100 h-100 justify-content-center align-items-center py-4 px-4">
          <Panel style="background-color: #0d0d0dd0" id="master-panel">
            <div class="d-flex flex-column justify-content-start align-items-stretch h-100">
              <div class="d-flex flex-row justify-content-between align-items-start">
                <h3 class="text-white">Pocket Persona</h3>
              </div>
              <NavBar/>
              <RouterView class="h-100"/>
            </div>
          </Panel>
      </div>
    </main>
    <SiteSwitcherPanel id="site-switcher"/>
    <footer>
      <Footer/>
    </footer>
  </div>
  <ToastPanel/>
</template>

<style scoped lang="scss">

#master-panel {
    width: 100%;
    height: 100%;
    box-sizing: border-box;
}
#grid {
  display: grid;
  grid-template-rows: auto min-content;
  grid-template-columns: auto 20vw;
}

#site-switcher {
  background-color: #0d0d0d7d !important;
}

main {
  grid-row: 1;
  height: auto;
}

#right-pane {
  grid-row: 1;
  grid-column: 2;
  display: relative;
}

footer {
  grid-row: 2;
  grid-column: span 2;
}
</style>