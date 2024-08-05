<template>
    <div>
        <label class="form-label">{{ props.label}}:</label>
        <input v-model="model" class="form-control"/>
    </div>
</template>

<script setup lang="ts">
import { defineProps, defineModel } from 'vue'

interface IProps {
    label: string,
}

const props = defineProps<IProps>()
const model = defineModel<string>()
</script>