<script setup lang="ts">
import { InputText, Modal } from 'tzz-shared';
import { useForm, useIsFormValid, useIsFormDirty } from 'vee-validate';
import { ref, watch } from 'vue';
import * as yup from 'yup'

interface IProps {
    isVisible: boolean
}
const props = defineProps<IProps>();
const emits = defineEmits(['cancel', 'submit'])
const isVisible = ref<boolean>(false)

const form = useForm({
    validationSchema: {
        rank: yup.number().required(),
        order: yup.number().required(),
        text: yup.string().required(),
        requirement: yup.string().notRequired()
    }
});

const [rank] = form.defineField('rank')
const [order] = form.defineField('order')
const [text] = form.defineField('text')
const [requirement] = form.defineField('requirement')
const isFormValid = useIsFormValid()
const isFormDirty = useIsFormDirty()

watch(() => props.isVisible, () => isVisible.value = props.isVisible)

</script>

<template>
    <Modal title="Add Dialog" :is-visible="isVisible" 
        :is-submit-disabled="!(isFormValid && isFormDirty)"
        @submit="() => emits('submit')" 
        @cancel="() => emits('cancel')">
        <div class="d-flex flex-column justify-content-start align-items-stretch">
            <div class="container=fluid">
                <div class="row">
                    <div class="col-2">
                        <InputText label="Rank" v-model="rank"/>
                    </div>
                    <div class="col-2">
                        <InputText label="Order" v-model="order"/>
                    </div>
                    <div class="col">
                        <InputText label="Optional Rank Requirement" v-model="requirement"/>
                    </div>
                </div>
            </div>
            <InputText label="Text" v-model="text"/>
        </div>
    </Modal>
</template>