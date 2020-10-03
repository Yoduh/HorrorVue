<template>
  <div class="home">
    <div v-if="!$auth.isAuthenticated">
      Log in to use this site
    </div>
    <div v-else>
      <search-bar @search="searchFranchise"></search-bar>
      <!-- <icon-medal
        width="50"
        height="50"
        icon-name="medal"
        icon-color1="#ffe27a"
        icon-color2="#f9cf58"
      ></icon-medal> -->

      <div v-if="noResults">
        No movies for that franchise were found, try another one?
      </div>
      <div v-if="collectionsView === null || collectionsView.length === 0">
        You have no collections yet, search for a franchise above to add one!
      </div>
      <div v-else>
        <v-expansion-panels dark>
          <v-expansion-panel
            v-for="collection in collectionsView"
            :key="collection.id"
            @click="select(collection.id)"
          >
            <v-expansion-panel-header>{{
              collection.name
            }}</v-expansion-panel-header>
            <v-expansion-panel-content>
              <button-bar>
                <v-col cols="12" md="auto" class="pb-3 pb-md-0">
                  <ranking-btn />
                </v-col>
                <v-col cols="12" md="4" class="pb-0 pl-0">
                  <sort-select />
                </v-col>
              </button-bar>
              <v-btn
                elevation="2"
                class="mr-2"
                fab
                small
                @click="expand = !expand"
                ><v-icon>{{ showRankings }}</v-icon>
              </v-btn>
              Toggle Rankings
              <franchise-panel
                :franchise="collection"
                :expandRankings="expand"
              />
              <!-- columns of rankings.  sort by person puts  -->
            </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
      </div>
    </div>
  </div>
</template>

<script>
import SearchBar from "@/components/SearchBar";
import FranchisePanel from "@/components/FranchisePanel";
import ButtonBar from "@/components/ButtonBar";
import RankingBtn from "@/components/buttons/RankingBtn";
import SortSelect from "@/components/buttons/SortSelect";
import { mapGetters, mapActions } from "vuex";

export default {
  name: "Home",
  components: {
    SearchBar,
    FranchisePanel,
    ButtonBar,
    RankingBtn,
    SortSelect
  },
  data() {
    return {
      noResults: false,
      expand: false,
      date: null
    };
  },
  computed: {
    collectionsView() {
      try {
        return this.$store.getters.collections.slice();
      } catch (e) {
        return [];
      }
    },
    showRankings() {
      if (this.expand) return "mdi-chevron-up";
      else return "mdi-chevron-down";
    }
  },
  methods: {
    ...mapGetters(["collections", "selectedCollection", "user"]),
    ...mapActions([
      "setSearchResults",
      "setCollections",
      "selectCollectionById",
      "setTempRanking"
    ]),

    searchFranchise(results) {
      if (results.data.length > 0) {
        this.noResults = false;
        this.setSearchResults(results.data);
        this.$router.push(`/search?q=${results.searchTerm}`);
      } else {
        this.noResults = true;
      }
    },
    select(id) {
      this.selectCollectionById(id);
    }
  },
  updated() {
    console.log(
      "colls",
      this.$store.getters.collections.map(c => c.name)
    );
  }
};
</script>
