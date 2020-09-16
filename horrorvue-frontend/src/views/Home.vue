<template>
  <div class="home">
    <search-bar @search="addMovies"></search-bar>
  </div>
</template>

<script>
import SearchBar from '@/components/SearchBar';
import { mapActions } from 'vuex';

export default {
  name: "Home",
  components: {
    SearchBar
  },
  methods: {
    ...mapActions(['setSearchResults']),

    addMovies(results) {
      this.setSearchResults(results.data);
      this.$router.push(`/search?q=${results.searchTerm}`);
    }
  }
};
</script>
