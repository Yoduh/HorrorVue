<template>
    <card-base :result="result">
        <v-card-actions :class="`rounded-b`">
            <v-spacer></v-spacer>
            <v-btn
                icon
                @click="$emit('expand')"
            >
                <v-icon>{{ result.show ? 'mdi-chevron-up' : 'mdi-chevron-down' }}</v-icon>
            </v-btn>
        </v-card-actions>
    </card-base>
</template>

<script>
import CardBase from '@/components/cards/CardBase';

export default {
    name: "CollectionMovieCard",
    components: {
        CardBase
    },
    props: ['result'],
    methods: {
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
.v-icon {
    color: black !important;
}
</style>