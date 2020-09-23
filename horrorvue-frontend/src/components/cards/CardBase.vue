<template>
    <v-card>
        <v-img
        :src="getImagePath(result.poster_path)"
        contain
        class="white--text align-end"
        height="300px"
        >
        </v-img>
        <v-card-title v-text="result.title"></v-card-title>
        <v-card-subtitle v-text="result.release_date.substring(0,4)"></v-card-subtitle>

        <slot></slot>
        
        <v-expand-transition>
            <div v-show="result.show">
                <v-divider></v-divider>
                <v-card-text v-text="result.overview"></v-card-text>
            </div>
        </v-expand-transition>
    </v-card>
</template>

<script>
export default {
    name: "CardBase",
    props: ['result'],
    methods: {
        getImagePath(apiPath) {
            return `https://image.tmdb.org/t/p/w500${apiPath}`;
        }
    }
}
</script>

<style scoped>
.v-card__title {
    word-break: normal !important;
}
</style>