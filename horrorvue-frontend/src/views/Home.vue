<template>
  <div class="home">
    <div v-if="!$auth.isAuthenticated">
      Log in to use this site
    </div>
    <div v-else> 
      <search-bar @search="addMovies"></search-bar>
      <div v-if="noResults">
        No movies for that franchise were found, try another one?
      </div>
      <div v-if="collections.length == 0">
        You have no collections yet, search for a franchise above to add one!
      </div>
      <div v-else>
        <v-expansion-panels dark>
          <v-expansion-panel
            v-for="collection in collections" :key="collection.id">
            <v-expansion-panel-header>{{ collection.name }}</v-expansion-panel-header>
            <v-expansion-panel-content>
              <franchise-panel :franchise="collection" />
            </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
      </div>
    </div>
  </div>
</template>

<script>
import SearchBar from '@/components/SearchBar';
import FranchisePanel from '@/components/FranchisePanel';
import { mapGetters, mapActions } from 'vuex';

export default {
  name: "Home",
  components: {
    SearchBar,
    FranchisePanel
  },
  data() {
    return {
      noResults: false
    }
  },
  computed: {
    collections() {
      return this.userCollections();
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
  }
}
</script>
