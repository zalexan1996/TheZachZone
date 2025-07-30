<template>
    <form @submit="submitForm">
        <div class="d-flex flex-column m-0 b">
            <InputText class="mx-0 mb-0 text-secondary" v-model="gameId" label="Game" :options="gameOptions" type="select" v-if="character?.gameId"/>
            <hr/>
            <InputText class="mb-0 text-secondary" v-model="socialLinkName" label="Social Link Name"/>
            <InputText class="mb-0 text-secondary" v-model="socialLinkArcanaId" label="Social Link Arcana" :options="arcanaOptions" type="select"/>
            <span style="max-width: inherit" v-if="form.errors.value['']">{{ form.errors.value[''] }}</span>
            <button type="submit" :disabled="!(isFormValid && isFormDirty)" class="align-self-center mt-2 mx-0 btn btn-outline-success">Update</button>
        </div>
    </form>
</template>

<script setup lang="ts">
import { CharacterDto, SetSocialLinkCommand, UpdateCharacterCommand } from '@/Services/pp.api';
import { useGlobalStore } from '@/Stores/globalStore';
import { InputText } from 'tzz-shared';
import { useForm, useIsFormDirty, useIsFormValid, useIsFieldDirty, useSubmitForm } from 'vee-validate';
import { onMounted, ref } from 'vue';
import * as yup from 'yup'

interface IProps {
    characterId: number,
}
const props = defineProps<IProps>()
const form = useForm({
    validationSchema: yup.object({
        gameId: yup.number().required(),
        //socialLinkArcanaId: yup.number().notRequired(),
        socialLinkArcanaId: yup.number().nullable(),
        socialLinkName: yup.string().nullable(),
    }).test(
        'both-or-none',
        'Both Name and Arcana are required if either is specified.',    
    obj => {
        const a =obj.socialLinkArcanaId
        const b = obj.socialLinkName?.trim()
        const oneFilled = !!a || !!b;
        const bothFilled = !!a && !!b;
        return !oneFilled ? true : bothFilled
    })
})

const [gameId] = form.defineField('gameId')
const [socialLinkName] = form.defineField('socialLinkName')
const [socialLinkArcanaId] = form.defineField('socialLinkArcanaId')

const isFormValid = useIsFormValid()
const isFormDirty = useIsFormDirty()
const isGameIdDirty = useIsFieldDirty('gameId')

const character = ref<CharacterDto>()
const arcanaOptions = ref<[number, string][]>()
const gameOptions = ref<[number, string][]>()
const globalStore = useGlobalStore()


const submitForm = useSubmitForm(async x => {
    if (isGameIdDirty.value) {
        await globalStore.characterStore.updateCharacter({
            gameId: gameId.value,
            id: character.value?.characterId,
            name: character.value?.name
        } as UpdateCharacterCommand)
    }
    else {
        await globalStore.socialLinkStore.addSocialLink({
            name: socialLinkName.value,
            arcanaId: socialLinkArcanaId.value,
            characterId: character.value!.characterId
        } as SetSocialLinkCommand)
    }
})

onMounted(() => {
    gameOptions.value = globalStore.gameStore.getGameOptions()
    arcanaOptions.value = globalStore.arcanaStore.arcana.map(x => [x.id!, x.name!])
    character.value = globalStore.characterStore.getCharacterDetails(props.characterId)

    // Set default form values
    form.resetForm({
        values: {
            gameId: character.value.gameId,
            socialLinkArcanaId: globalStore.socialLinkStore.getForCharacter(props.characterId)?.arcanaId,
            socialLinkName: globalStore.socialLinkStore.getForCharacter(props.characterId)?.name
        }
    })
})
</script>

<style scoped lang="scss">
img {
    margin: {
        top: 1rem;
    }
}

.b {
    border-top: solid 1px #ccccccaa;
    border-bottom: solid 1px #ccccccaa;
    border-radius: 10px;
    padding: {
        top: 1rem;
        bottom: 1rem;
    };
}
</style>