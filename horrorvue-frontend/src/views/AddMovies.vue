<template>
    <div class="add-movies-wrapper">
        <search-bar @search="searchMovies" :placeholder="query"></search-bar>
        <v-container fluid>
            <v-row dense v-if="results.length > 0">
                <v-col
                v-for="result in results"
                :key="result.tmdId"
                :sm="6" :md="3"
                >
                    <v-card 
                    v-if="!containsMovieId(collection, result)" 
                    >
                        <v-img
                        :src="getImagePath(result.poster_path)"
                        contain
                        class="white--text align-end"
                        height="300px"
                        >
                        </v-img>
                        <v-card-title v-text="result.title"></v-card-title>
                        <v-card-subtitle v-text="result.release_date.substring(0,4)"></v-card-subtitle>
                        <v-card-actions>
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
                        <v-expand-transition>
                            <div v-show="result.show">
                                <v-divider></v-divider>
                                <v-card-text v-text="result.overview"></v-card-text>
                            </div>
                        </v-expand-transition>
                    </v-card>
                </v-col>
            </v-row>
            <save-new-modal 
                :show="moviesToAdd.length"
                @save="save">
            </save-new-modal>
        </v-container>
        <v-btn @click="save">WTF</v-btn>
    </div>
</template>

<script>
import SearchBar from '@/components/SearchBar';
import SaveNewModal from '@/components/modals/SaveNewModal';
import { mapGetters, mapActions } from 'vuex';
import api from '@/api/tmdb.js';
import db from '@/api/db.js';

export default {
    name: "AddMovies",
    components: {
        SearchBar,
        SaveNewModal
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
        getImagePath(apiPath) {
            return `https://image.tmdb.org/t/p/w500${apiPath}`;
        },
        changeResultShow(selected) {
            this.results.forEach(result => {
                if (result.id === selected.id)
                {
                    result.show = !result.show;
                    return;
                }
            })
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
.checkmark {
    opacity: 0.2;
}
.selected {
    opacity: 1;
    color: green;
}
.v-card__title {
    word-break: normal !important;
}
.save-btn {
    bottom: 1rem !important;
}
</style>