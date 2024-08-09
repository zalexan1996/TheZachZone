<template>
<div>
    <button v-if="model.length > maxPerPage" class="btn btn-outline-secondary"><span class="fa fa-angle-left"></span></button>
    <img height="92" class="img-responsive" v-for="data in model" :src="'data:image/png;base64,' + data"/>
    <button v-if="model.length > maxPerPage" class="btn btn-outline-secondary"><span class="fa fa-angle-right"></span></button>
</div>
</template>

<script setup lang="ts">

interface IProps {
    maxPerPage: number
}

const model = defineModel<string[]>()
const props = withDefaults(defineProps<IProps>(), {
    maxPerPage: 5
})

</script>