<template>
    <card-base :result="result">
        <v-card-actions>
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
    name: "CollectionMovieCard",
    components: {
        CardBase
    },
    props: ['result'],
    methods: {
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
</style>