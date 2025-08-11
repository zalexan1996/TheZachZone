<script setup lang="ts">
import { AddSocialLinkGiftCommand } from '@/Services/pp.api';
import { InputText, Modal } from 'tzz-shared';
import { useForm, useIsFormValid, useIsFormDirty } from 'vee-validate';
import { computed, ref, watch } from 'vue';
import * as yup from 'yup'

interface IProps {
    isVisible: boolean,
    socialLinkId: number
}
const props = defineProps<IProps>();
const emits = defineEmits(['cancel', 'submit'])
const isVisible = ref<boolean>(false)

const form = useForm({
    validationSchema: {
        name: yup.string().required(),
        acquiredAt: yup.string().required(),
    }
});

const [name] = form.defineField('name')
const [acquiredAt] = form.defineField('acquiredAt')
const isFormValid = useIsFormValid()
const isFormDirty = useIsFormDirty()

watch(() => props.isVisible, () => isVisible.value = props.isVisible)

const formData = computed(() => {
    return {
        socialLinkId: props.socialLinkId,
        giftName: name.value,
        acquiredAd: acquiredAt.value
    } as AddSocialLinkGiftCommand
})
</script>

<template>
    <Modal title="Add Gift" :is-visible="isVisible" 
        :is-submit-disabled="!(isFormValid && isFormDirty)"
        @submit="() => emits('submit', formData)" 
        @cancel="() => emits('cancel')">
        <div class="d-flex flex-column justify-content-start align-items-stretch">
            <div class="container=fluid">
                <div class="row">
                    <div class="col">
                        <InputText label="Gift Name" v-model="name"/>
                    </div>
                    <div class="col">
                        <InputText label="Acquired At" v-model="acquiredAt"/>
                    </div>
                </div>
            </div>
        </div>
    </Modal>
</template>