<template>
    <div class="form-group">
        <label class="form-label">{{ props.label}}:<i v-if="!!$props.helpText" class="fa fa-circle-question text-info ms-1" :title="$props.helpText"></i></label>
        <input v-if="$props.type == 'text'" v-model="model" class="form-control" :readonly="$props.readonly" :disabled="$props.disabled" :placeholder="$props.placeholder" @change="e => emit('change', e)"/>
        <input v-else-if="$props.type == 'password'" type="password" v-model="model" class="form-control" :readonly="$props.readonly" :disabled="$props.disabled" :placeholder="$props.placeholder" @change="e => emit('change', e)"/>
        <textarea v-else-if="$props.type == 'textarea'" v-model="model" class="form-control" :readonly="$props.readonly" :disabled="$props.disabled" :placeholder="$props.placeholder" @change="e => emit('change', e)">
        </textarea>
        <select v-else-if="$props.type == 'select'" v-model="model" class="form-select" :readonly="$props.readonly" :disabled="$props.disabled" :placeholder="$props.placeholder" @change="e => emit('change', e)">
            <option v-for="o in $props.options" :value="o[0]">{{ o[1] }}</option>
        </select>
        <ErrorMessage class="text-danger" :name="$props.for"></ErrorMessage>
    </div>
</template>

<script setup lang="ts">
import { defineProps, defineModel, withDefaults, defineEmits } from 'vue'
import { ErrorMessage } from 'vee-validate'

interface IProps {
    label: string,
    placeholder: string,
    helpText: string|undefined,
    readonly: boolean|undefined,
    disabled: boolean|undefined,
    type: 'text'|'textarea'|'select'|'password',
    options: [number, string][]|undefined,
    for: string
}

const props = withDefaults(defineProps<IProps>(), {
    type: 'text'
})

const emit = defineEmits(['change'])

const model = defineModel<string>()
</script>

<style scoped lang="scss">
.form-group {
    display: flex;
    flex-direction: column;
    align-items: start;
    margin-bottom: 1rem;

    input, select, label {
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