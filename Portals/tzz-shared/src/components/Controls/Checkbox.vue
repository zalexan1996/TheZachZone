<template>
    <div class="d-flex flex-column justify-content-start">
        <label class="form-label">{{ $props.label }}:<i v-if="!!$props.helpText" class="fa fa-circle-question text-info ms-1" :title="$props.helpText"></i></label>
        <div class="form-check">
            <input v-model="model" class="form-check-input" :readonly="$props.readonly" :disabled="$props.disabled" type="checkbox"/>
            <span class="form-check-label help">{{ props.placeholder}}</span>
        </div>
        <ErrorMessage class="text-danger" :name="$props.for"></ErrorMessage>
    </div>
</template>

<script setup lang="ts">
import { defineProps, defineModel, withDefaults } from 'vue'

interface IProps {
    label: string,
    placeholder: string,
    readonly: boolean|undefined,
    disabled: boolean|undefined,
    checked: boolean,
    helpText: string|undefined,
    for: string
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