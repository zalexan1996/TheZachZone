<template>
    <div class="d-flex flex-row h-100">
        <AdminSideNav class="me-4"></AdminSideNav>
        <RouterView/>
    </div>
</template>
<script lang="ts" setup>
import AdminSideNav from './AdminSideNav.vue'
import {RouterView} from 'vue-router';
</script>