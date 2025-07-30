<template>
    <div class="modal" :id="modalId">
        <div class="modal-dialog">
            <div class="modal-content">
                <div v-if="$props.title" class="modal-header">
                    <h5 class="modal-title">{{ $props.title }}</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <slot/>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" @click="onCancel">{{ $props.cancelVerb }}</button>
                    <button type="button" class="btn btn-primary" @click="onSubmit" :disabled="props.isSubmitDisabled">{{ $props.submitVerb }}</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { defineProps, watch, ref, onMounted } from 'vue';
import { Modal as BSModal }from 'bootstrap'
import { v4 as uuid } from 'uuid';
const modalId = 'tzz_modal_' + uuid()

interface IProps {
    title: string|undefined,
    submitVerb: 'Save' | 'Delete' | 'Add' | 'Update',
    cancelVerb: 'Cancel' | 'Close' | 'Back',
    isVisible:boolean,
    isSubmitDisabled:boolean
}
const props = withDefaults(defineProps<IProps>(), {
    submitVerb: 'Save',
    cancelVerb: 'Cancel',
    isVisible: false,
    isSubmitDisabled: true
})

const emits = defineEmits(['submit', 'cancel'])

const bootstrapModal = ref<BSModal|undefined>()
const isSubmitDisabled = ref<boolean>(true)

const onSubmit = (e: any) => emits('submit', e)
const onCancel = (e: any) => emits('cancel', e)
watch(() => props.isSubmitDisabled, () => {
    isSubmitDisabled.value = props.isSubmitDisabled
})
watch(() => props.isVisible, () => {
    if (!bootstrapModal.value) {
        let element = document.getElementById(modalId)!
        bootstrapModal.value = new BSModal(element)

        element.addEventListener('hidden.bs.modal', () => {
            onCancel(element)
        })
    }

    if (props.isVisible) {
        bootstrapModal.value.show();
    }
    else {
        bootstrapModal.value.hide();
    }
})

onMounted(() => {
    isSubmitDisabled.value = props.isSubmitDisabled
})
</script>


<style lang="scss" scoped>

    .btn-close {
        color: rgba(200, 200, 200) !important;
    }
    .modal-content {
        background-color: rgba(31, 31, 31);
        border: solid 1px rgb(72, 72, 72);
        border-radius: 2rem;
        color: rgba(200, 200, 200);
    }
</style>