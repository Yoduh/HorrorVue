<template>
  <div class="home">
    <div v-if="!$auth.isAuthenticated">
      Log in to use this site
    </div>
    <div v-else> 
      <search-bar @search="searchFranchise"></search-bar>
      <icon-medal
        width="50"
        height="50"
        icon-name="medal"
        icon-color1="#ffe27a"
        icon-color2="#f9cf58"
      ></icon-medal>

      <div v-if="noResults">
        No movies for that franchise were found, try another one?
      </div>
      <div v-if="collectionsView === null || collectionsView.length === 0">
        You have no collections yet, search for a franchise above to add one!
      </div>
      <div v-else>
        <v-expansion-panels dark>
          <v-expansion-panel
            v-for="collection in collectionsView" :key="collection.id">
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
import IconMedal from '@/components/icons/IconMedal';
import { mapGetters, mapActions } from 'vuex';

export default {
  name: "Home",
  components: {
    SearchBar,
    FranchisePanel,
    IconMedal
  },
  data() {
    return {
      noResults: false
    }
  },
  computed: {
    collectionsView() {
      return this.collections();
    }
  },
  methods: {
    ...mapGetters(['collections']),
    ...mapActions(['setSearchResults', 'setCollections']),

    searchFranchise(results) {
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
