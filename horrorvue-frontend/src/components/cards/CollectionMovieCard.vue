<template>
    <card-base :result="result">
        <v-card-actions :class="`rounded-b`">
            <v-spacer></v-spacer>
            <info-modal 
            :title="result.title" 
            :subtitle="result.release_date.substring(0,4)"
            :description="result.overview" 
            />
        </v-card-actions>
    </card-base>
</template>

<script>
import CardBase from '@/components/cards/CardBase';
import InfoModal from '@/components/modals/InfoModal';

export default {
    name: "CollectionMovieCard",
    components: {
        CardBase,
        InfoModal
    },
    props: ['result'],
    methods: {
    }
}
</script>

<style scoped>
.v-card__actions {
    background-color: white;
}
</style>