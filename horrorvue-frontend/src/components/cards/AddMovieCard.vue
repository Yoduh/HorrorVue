<template>
    <card-base :result="result">
        <v-card-actions :class="`rounded-b-xl`">
            <v-btn v-if="!result.added" text @click="addMovie(result)">Add</v-btn>
            <v-btn v-else text @click="removeMovie(result)">Remove</v-btn>
            <v-icon class="checkmark" :class="{ selected : result.added }">mdi-checkbox-marked-circle</v-icon>
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
    name: "AddMovieCard",
    components: {
        CardBase,
        InfoModal
    },
    props: ['result'],
    methods: {
        addMovie(result) {
            this.$emit('addMovie', result);
        },
        removeMovie(result) {
            this.$emit('removeMovie', result);
        }
    }
}
</script>

<style scoped>
.checkmark {
    opacity: 0.2;
}
.selected {
    opacity: 1;
    color: green;
}
.v-card__actions {
    background-color: white;
}
.v-card__actions > .v-btn.v-btn {
    padding: 0 2px;
}
</style>