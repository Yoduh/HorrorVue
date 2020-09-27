<template>
    <div class="add-movies-wrapper">
        <search-bar @search="searchMovies" :placeholder="query"></search-bar>
        <v-container>
            <v-row dense v-if="results.length > 0">
                <div
                v-for="result in results"
                :key="result.tmdId"
                >
                    <add-movie-card 
                    v-if="!containsMovieId(collection, result)" 
                    :result="result"
                    class="mx-2"
                    @addMovie="addMovie"
                    @removeMovie="removeMovie"
                    />
                </div>
            </v-row>
            <save-new-modal 
                :show="moviesToAdd.length"
                @save="save">
            </save-new-modal>
        </v-container>
    </div>
</template>

<script>
import SearchBar from '@/components/SearchBar';
import SaveNewModal from '@/components/modals/SaveNewModal';
import AddMovieCard from '@/components/cards/AddMovieCard';
import { mapGetters, mapActions } from 'vuex';
import api from '@/api/tmdb.js';
import db from '@/api/db.js';

export default {
    name: "AddMovies",
    components: {
        SearchBar,
        SaveNewModal,
        AddMovieCard
    },
    props: {
        query: {
            type: String
        },
        collection: {
            type: Array,
            default: function () { return [] }
        }
    },
    data() {
        return {
            results: [],
            moviesToAdd: []
        }
    },
    methods: {
        ...mapGetters(['searchResults']),
        ...mapActions(['setSearchResults']),
        async searchMovies(results) {
            this.setSearchResults(results.data);
            this.results = results.data.map(result => {
                return { ...result, added: false, show: false };
            });
            this.$router.push(`/search?q=${results.searchTerm}`);
        },
        containsMovieId(movieArray, movie) {
            let ret = false;
            movieArray.forEach(m => {
                if (m.id === movie.id) {
                    ret = true;
                }
            });
            return ret;
        },
        addMovie(movie) {
            this.moviesToAdd.push(movie);
            movie.added = true;
        },
        removeMovie(movie) {
            this.moviesToAdd = this.moviesToAdd.filter(m => {
                m.id !== movie.id;
            });
            movie.added = false;
        },
        save(name) {
            if (this.collection.length === 0)
                db.newCollection(this.moviesToAdd, name);
            // else
            //     db.updateCollection()

            // go back home after save
            // this.$router.push("/");
        }
    },
    async created() {
        // user has refreshed the page and we need to reset search results
        if (this.results.length === 0) {
            const movies = await api.fetchMovies(this.query);
            this.setSearchResults(this.results);
            this.results = movies.map(result => {
                return { ...result, added: false,  show: false };
            });;
        }
        else {
            this.results = this.searchResults().map(result => {
                return { ...result, added: false,  show: false };
            });
        }
    }
}
</script>

<style scoped>
.save-btn {
    bottom: 1rem !important;
}
</style>