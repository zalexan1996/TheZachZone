<template>
    <div class="grid justify-content-center align-items-center text-center">
      <div>
        <h1 class="big-header">
          <RouterLink :to="{name: 'Home'}">
            The Zach Zone
          </RouterLink></h1>
        <hr/>
      </div>
      <RouterView/>
    </div>
</template>


<style scoped lang="scss">
.grid {
  display: grid;
  grid-template-rows: 250px auto;
}
</style>


<script setup lang="ts">
import { RouterView } from 'vue-router'
</script>