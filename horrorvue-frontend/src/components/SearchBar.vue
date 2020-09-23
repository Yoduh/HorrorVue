<template>
    <div class="search-bar">
        <v-autocomplete
            v-model="model"
            :items="items"
            :loading="isLoading"
            :search-input.sync="search"
            color="white"
            hide-no-data
            hide-selected
            item-text="Description"
            item-value="API"
            label="Franchise Name"
            :placeholder="placeholder"
            prepend-icon="mdi-database-search"
            return-object
            @keydown.enter="searchMovies"
        ></v-autocomplete>
      </div>
</template>

<script>
import api from '@/api/tmdb.js';
import { mapActions } from 'vuex';

export default {
    name: "SearchBar",
    props: {
        placeholder: {
            type: String,
            default: "Start typing to search existing franchises or to add new franchise"
        } 
    },
    data() {
        return {
            descriptionLimit: 60,
            entries: [],
            isLoading: false,
            model: null,
            search: null,
            items: [], // needs to eventually be computed.  see docs

            results: null
        }
    },
    methods: {
        ...mapActions(['setLastSearched']),

        async searchMovies() {
            this.setLastSearched(this.search);
            const movies = await api.fetchMovies(this.search);
            const results = {
                data: movies,
                searchTerm: this.search
            }
            this.$emit('search', results);
        }
    }
}
</script>

<style scoped>

</style>