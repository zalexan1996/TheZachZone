<template>
    <div class="form-group">
        <label class="form-label">{{ props.label}}:</label>
        <input v-model="model" class="form-control" :placeholder="props.placeholder"/>
    </div>
</template>

<script setup lang="ts">
import { defineProps, defineModel } from 'vue'

interface IProps {
    label: string,
    placeholder: string
}

const props = defineProps<IProps>()
const model = defineModel<string>()
</script>

<style scoped lang="scss">
.form-group {
    display: flex;
    flex-direction: column;
    align-items: start;
    margin-bottom: 1rem;

    input {
        min-width: 20rem;
    }
}
</style>