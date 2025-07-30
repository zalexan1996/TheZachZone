<template>
    <Modal title="Add an Arcana" 
        :is-visible="isVisible"
        :is-submit-disabled="!(isFormValid && isFormDirty)"
        @cancel="e => emits('cancel', e)"
        @submit="onSubmit">
        <div class="d-flex flex-column align-items-start justify-content-start">
            <InputText for="arcanaName" :disabled="false" label="Arcana name" placeholder="Provide a name for the arcana..." type="text" v-model="arcanaName"/>
        </div>
    </Modal>
</template>
<script setup lang="ts">
import { defineProps, ref, watch } from 'vue'
import { InputText, Modal } from 'tzz-shared';
import { useArcanaStore } from '@/Stores/arcanaStore';
import { useForm, useIsFormValid, useIsFormDirty } from 'vee-validate';
import * as yup from 'yup'
interface IProps {
    isVisible: boolean
}

const arcanaStore = useArcanaStore()
const emits = defineEmits(['cancel', 'submit'])
const props = withDefaults(defineProps<IProps>(), {
    isVisible: false,
})
const addArcanaForm = useForm({
    validationSchema: {
        arcanaName: yup.string().required(),
    }
})
const [arcanaName] = addArcanaForm.defineField('arcanaName')

const isFormValid = useIsFormValid()
const isFormDirty = useIsFormDirty()

const isVisible = ref<boolean>(false)

watch(() => props.isVisible, () => isVisible.value = props.isVisible)

const onSubmit = async (e: any) => {
    // Add the Arcana
    await arcanaStore.addArcana(arcanaName.value)
    
    // Notify parent
    emits('submit', e)
}
</script>
