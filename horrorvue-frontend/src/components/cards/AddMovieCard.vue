<template>
    <card-base :result="result">
        <v-card-actions :class="`rounded-b-xl`">
            <v-btn v-if="!result.added" text @click="addMovie(result)">Add</v-btn>
            <v-btn v-else text @click="removeMovie(result)">Remove</v-btn>
            <v-icon class="checkmark" :class="{ selected : result.added }">mdi-checkbox-marked-circle</v-icon>
            <v-spacer></v-spacer>
            <v-btn
                icon
                @click="changeResultShow(result)"
            >
                <v-icon>{{ result.show ? 'mdi-chevron-up' : 'mdi-chevron-down' }}</v-icon>
            </v-btn>
        </v-card-actions>
    </card-base>
</template>

<script>
import CardBase from '@/components/cards/CardBase';

export default {
    name: "AddMovieCard",
    components: {
        CardBase
    },
    props: ['result'],
    methods: {
        addMovie(result) {
            this.$emit('addMovie', result);
        },
        removeMovie(result) {
            this.$emit('removeMovie', result);
        },
        changeResultShow(result) {
            this.$emit('changeResultShow', result);
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
</style>