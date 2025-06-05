<template>
    <div class="container">
        <div class="row">
            <Panel class="w-auto">
                <AssignSocialLinkForm></AssignSocialLinkForm>
            </Panel>
        </div>
    </div>
</template>
<script setup lang="ts">
import { Panel, InputText } from 'tzz-shared';
import AssignSocialLinkForm from '@/components/SocialLinks/AssignSocialLinkForm.vue'
</script>