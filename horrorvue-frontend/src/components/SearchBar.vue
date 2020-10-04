<template>
  <div class="search-bar">
    <v-autocomplete
      v-model="model"
      :items="names"
      :loading="isLoading"
      :search-input.sync="search"
      :value="value"
      background-color="gray"
      dark
      hide-no-data
      hide-selected
      item-text="Description"
      item-value="API"
      label="Franchise Name"
      :placeholder="placeholder"
      no-data-text="No saved franchises"
      :clearable="true"
      prepend-icon="mdi-database-search"
      return-object
      @keydown.enter="searchMovies"
      @change="searchMovies"
    ></v-autocomplete>
  </div>
</template>

<script>
import api from "@/api/tmdb.js";
import { mapActions, mapGetters } from "vuex";

export default {
  name: "SearchBar",
  props: {
    placeholder: {
      type: String,
      default:
        "Start typing to search existing franchises or to add new franchise"
    }
  },
  data() {
    return {
      descriptionLimit: 60,
      entries: [],
      isLoading: false,
      model: null,
      search: null,
      results: null,
      value: null
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
    ...mapActions(["setLastSearched"]),
    ...mapGetters(["collections"]),
    async searchMovies() {
      if (!this.model) return;
      this.setLastSearched(this.search);
      const existing = this.collections().find(c => c.name === this.model);
      if (existing !== undefined) {
        this.$emit("scroll-to", existing.id);
        return;
      }
      const movies = await api.fetchMovies(this.search);
      const results = {
        data: movies,
        searchTerm: this.search
      };
      this.$emit("search", results);
    }
  }
};
</script>

<style scoped>
input {
  color: white;
}
</style>
