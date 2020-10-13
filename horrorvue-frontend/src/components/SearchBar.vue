<template>
  <div class="search-bar">
    <v-text-field
      v-model="search"
      :value="searchBarText"
      dark
      :placeholder="placeholder"
      filled
      rounded
      dense
      @keydown.enter="searchMovies"
    ></v-text-field>
  </div>
</template>

<script>
import api from "@/api/tmdb.js";
import { mapGetters } from "vuex";

export default {
  name: "SearchBar",
  props: {
    placeholder: {
      type: String,
      default: "Search by movie name to start creating a new collection"
    }
  },
  data() {
    return {
      isLoading: false,
      search: null,
      results: null,
      searchBarText: ""
    };
  },
  computed: {
    names() {
      if (this.collections()) {
        return this.collections().map(c => c.name);
      } else {
        return [];
      }
    }
  },
  methods: {
    ...mapGetters(["collections"]),
    async searchMovies() {
      if (!this.search) return;
      const movies = await api.fetchMovies(this.search);
      const results = {
        data: movies,
        searchTerm: this.search
      };
      if (movies.length === 0) {
        this.$eventBus.$emit(
          "open-snackbar",
          "No movies found! try a different search term",
          "error"
        );
      } else {
        this.$emit("search", results);
      }
    }
  }
};
</script>

<style scoped>
input {
  color: white;
}
</style>
