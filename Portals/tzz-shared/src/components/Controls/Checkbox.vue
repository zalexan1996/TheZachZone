<template>
    <div class="d-flex flex-column justify-content-start">
        <label class="form-label">{{ $props.label }}:</label>
        <div class="form-check">
            <input v-model="model" class="form-check-input" :readonly="$props.readonly" :disabled="$props.disabled" type="checkbox"/>
            <span class="form-check-label help">{{ props.placeholder}}</span>
        </div>
    </div>
</template>

<script setup lang="ts">
import { defineProps, defineModel, withDefaults } from 'vue'

interface IProps {
    label: string,
    placeholder: string,
    readonly: boolean|undefined,
    disabled: boolean|undefined,
    checked: boolean
}

const props = withDefaults(defineProps<IProps>(), {
    checked: false
})
const model = defineModel<string>()
</script>

<style scoped lang="scss">

.help {
    color: gray;
    cursor: default;
    font-style: italic;
}
</style>