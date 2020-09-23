<template>
  <div class="home">
    <search-bar @search="addMovies"></search-bar>
    <div v-if="noResults">
      No results found! Try a different search
    </div>
    <div v-if="$auth.isAuthenticated">
      
    </div>
  </div>
</template>

<script>
import SearchBar from '@/components/SearchBar';
import { mapGetters, mapActions } from 'vuex';

export default {
  name: "Home",
  components: {
    SearchBar
  },
  data() {
    return {
      noResults: false
    }
  },
  methods: {
    ...mapGetters(['userCollections']),
    ...mapActions(['setSearchResults']),

    addMovies(results) {
      if (results.data.length > 0) {
        this.noResults = false;
        this.setSearchResults(results.data);
        this.$router.push(`/search?q=${results.searchTerm}`);
      } else {
        this.noResults = true;
      }
    }
  },
  mounted() {
    console.log('asdf', this.userCollections());
  }
};
</script>
