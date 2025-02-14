<template>
    <LoginForm id="login-form"/>
    <RouterLink class="subtitle" :to="{name: 'Home'}">Back to home...</RouterLink>
</template>
<script setup lang="ts">
import LoginForm from '@components/LoginForm.vue'
</script>

<style scoped lang="scss">

.subtitle {
    margin-top: 1rem;
    color: #eab1f8;
}

</style>