<template>
    <div class="grid justify-content-center align-items-center text-center">
      <div>
        <h1 class="big-header">
          <RouterLink :to="{name: 'Home'}">
            The Zach Zone
          </RouterLink></h1>
          <hr/>
          <p style="font-style: italic;">I am Zach. These are my apps.</p>
      </div>
      <RouterView/>
    </div>
    <ToastPanel/>
</template>


<style scoped lang="scss">
.grid {
  display: grid;
  grid-template-rows: 250px auto;
}
</style>


<script setup lang="ts">
import { onMounted } from 'vue'
import { RouterView } from 'vue-router'
import { useAccountStore } from '@stores/accountStore.ts'
import { ToastPanel } from 'tzz-shared'

const accountStore = useAccountStore();

onMounted(async () => {
  await accountStore.loadUserInfo();
})
</script>