<template>
    <div class="add-movies-wrapper">
        <search-bar @search="searchMovies" :placeholder="query"></search-bar>
        <v-container fluid>
            <v-row dense>
                <v-col
                v-for="result in results"
                :key="result.id"
                :cols="3"
                >
                    <v-card>
                        <v-img
                        :src="getImagePath(result.poster_path)"
                        contain
                        class="white--text align-end"
                        height="300px"
                        >
                        </v-img>
                        <v-card-title v-text="result.title"></v-card-title>
                        <v-card-subtitle v-text="result.release_date"></v-card-subtitle>
                        <v-card-actions>
                            <v-btn text>Add</v-btn>
                            <v-btn text>Remove</v-btn>
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
                                <v-card-text v-text="result.overview">TESTING TEST TEST</v-card-text>
                            </div>
                        </v-expand-transition>
                    </v-card>
                </v-col>
            </v-row>
        </v-container>
    </div>
</template>

<script>
import SearchBar from '@/components/SearchBar';
import { mapGetters, mapActions } from 'vuex';
//import api from '@/api/tmdb.js';

export default {
    name: "AddMovies",
    components: {
        SearchBar
    },
    props: {
        query: {
            type: String
        }
    },
    data() {
        return {
            results: []
        }
    },
    methods: {
        ...mapGetters(['searchResults']),
        ...mapActions(['setSearchResults']),
        async searchMovies(results) {
            this.setSearchResults(results.data);
            this.results = results.data.map(result => {
                return { ...result, show: false };
            });
            this.$router.push(`/search?q=${results.searchTerm}`);
        },
        getImagePath(apiPath) {
            return `https://image.tmdb.org/t/p/w500${apiPath}`;
        },
        changeResultShow(selected) {
            console.log('selected', selected);
            this.results.forEach(result => {
                if (result.id === selected.id)
                {
                    console.log('changing ' + result.show + ' to ' + !result.show);
                    result.show = !result.show;
                    return;
                }
            })
        }
    },
    created() {
        this.results = this.searchResults().map(result => {
            return { ...result, show: false };
        });
    }
}
</script>

<style scoped>

</style>