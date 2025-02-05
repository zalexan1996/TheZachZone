<script setup lang="ts">
import { computed, ref } from 'vue'
import { InputText } from 'tzz-shared';
import { useArcanaStore } from '@/Stores/arcanaStore';
import { useForm, useIsFormDirty, useIsFormValid } from 'vee-validate'
import * as yup from 'yup'

const arcanaStore = useArcanaStore();

const existingArcana = computed((): string[] => arcanaStore.arcana.map(x => x.name))
const form = useForm({
    validationSchema: yup.object().shape({
        name: yup.string()
            .required()
            .test('not-existing', 'This arcana already exists', 
                v => !existingArcana.value.includes(v))
    })
})

const isFormValid = useIsFormValid()
const isFormDirty = useIsFormDirty()

const [name] = form.defineField('name')
const addArcana = async () => {
    if (!isFormValid) {
        return;
    }
    
    await arcanaStore.addArcana(name.value)
    name.value = ''
    await arcanaStore.getArcana();
}
</script>

<template>
    <div class="d-flex flex-flex-row align-items-end justify-content-start p-3">
        <InputText label="Name" type="text" v-model="name"
            placeholder="Provide a name for the arcana.." class="mx-3 mb-0" for="name" v-onEnter="() => addArcana()"/>
        <button class="btn btn-danger" @click="addArcana" :disabled="!(isFormValid && isFormDirty)">Add</button>
    </div>
</template>