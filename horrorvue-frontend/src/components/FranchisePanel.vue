<template>
    <div class="franchise-panel">
        <div v-for="movie in franchise.movies" :key="movie.id">
            <collection-movie-card :result="movie" />
        </div>
    </div>
</template>

<script>
import CollectionMovieCard from '@/components/cards/CollectionMovieCard';
export default {
    name: "FranchisePanel",
    props:
        ['franchise']
    ,
    components: {
        CollectionMovieCard
    }
}
</script>

<style scoped>

</style>