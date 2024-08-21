<template>
    <div class="form-group">
        <label class="form-label">{{ props.label}}:<i class="fa fa-help text-warning"></i></label>
        <input v-if="$props.type == 'text'" v-model="model" class="form-control" :readonly="$props.readonly" :disabled="$props.disabled"/>
        <textarea v-else-if="$props.type == 'textarea'" v-model="model" class="form-control" :readonly="$props.readonly" :disabled="$props.disabled">
        </textarea>
        <select v-else-if="$props.type == 'select'" v-model="model" class="form-select" :readonly="$props.readonly" :disabled="$props.disabled">
            <option v-for="o in $props.options">{{ o }}</option>
        </select>
        <span v-show-preserve-space="showPlaceholder" class="mt-1 help">{{ $props.placeholder }}</span>
    </div>
</template>

<script setup lang="ts">
import { defineProps, defineModel, withDefaults, Directive, DirectiveBinding } from 'vue'

const vShowPreserveSpace: Directive = {
    updated: (el: HTMLElement, binding: DirectiveBinding) => {
        el.style.visibility = !!binding.value ? 'visible' : 'hidden' 
    }
}
interface IProps {
    label: string,
    placeholder: string,
    readonly: boolean|undefined,
    disabled: boolean|undefined,
    type: 'text'|'textarea'|'select',
    options: string[]|undefined,
    showPlaceholder: boolean
}

const props = withDefaults(defineProps<IProps>(), {
    type: 'text',
    showPlaceholder: true
})
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
        &:disabled {
            filter: brightness(75%);
        }
    }
}

.help {
    color: gray;
    cursor: default;
    font-style: italic;
}
</style>