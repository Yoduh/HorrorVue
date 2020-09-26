<template>
    <v-container class="franchise-panel">
        <v-row>
            <draggable v-model="franchise.movies" group="movies" @start="drag=true" @end="drag=false" class="flex" forceFallback: true>
                <div v-for="movie in franchise.movies" :key="movie.id">
                    <collection-movie-card :result="movie" class="mx-2" @expand="expand(movie.id)" />
                </div>
            </draggable>
        </v-row>
    </v-container>
</template>

<script>
import CollectionMovieCard from '@/components/cards/CollectionMovieCard';
import draggable from 'vuedraggable'

export default {
    name: "FranchisePanel",
    props:
        ['franchise']
    ,
    components: {
        CollectionMovieCard,
        draggable
    },
    methods: {
        expand(id) {
            console.log(id);
            this.franchise.movies.forEach(movie => {
                if (movie.id === id)
                {
                    movie.show = !movie.show;
                    return;
                }
            })
        }
    }
}
</script>

<style scoped>
.flex {
    display: flex;
    overflow: auto;
}
</style>