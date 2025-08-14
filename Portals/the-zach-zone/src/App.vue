<template>
  <RouterLink :to="{name: 'Admin'}" v-if="showAdminButton">
    <button class="btn btn-primary admin-button">
      <span class="fa fa-square-up-right me-1"></span>
      Admin
    </button>
  </RouterLink>
  <RouterLink :to="{name: 'Home'}" v-else-if="showBackButton">
    <button class="btn btn-primary admin-button">
      <span class="fa fa-arrow-left me-1"></span>
      Back
    </button>
  </RouterLink>
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

.admin-button {
  position: fixed;
  margin: 1rem;
  bottom: 0px;
  right: 0px;

  border: solid 1px #eab1f8;
}
</style>


<script setup lang="ts">
import { computed, onMounted } from 'vue'
import { RouterView, useRoute } from 'vue-router'
import { useAccountStore } from '@stores/accountStore.ts'
import { ToastPanel } from 'tzz-shared'

const accountStore = useAccountStore();
const route = useRoute()

const showAdminButton = computed(() => route.name != 'Admin' && accountStore.isAdmin());
const showBackButton = computed(() => route.name == 'Admin' && accountStore.isAdmin());

onMounted(async () => {
  await accountStore.loadUserInfo();
})
</script>