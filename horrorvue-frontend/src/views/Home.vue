<template>
  <div class="home">
    <div v-if="!$auth.isAuthenticated">
      Log in to use this site
    </div>
    <div v-else>
      <search-bar @search="searchFranchise" @scroll-to="scrollTo"></search-bar>
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
      <div v-if="collections() === null || collections().length === 0">
        You have no collections yet, search for a franchise above to add one!
      </div>
      <div v-else>
        <v-expansion-panels dark v-model="panel">
          <v-expansion-panel
            v-for="(collection, index) in collections()"
            :key="collection.id"
            @click="select(collection.id)"
          >
            <v-expansion-panel-header :id="collection.id">
              <strong>{{ collection.name }}</strong>
              <span
                v-if="panel === index"
                class="ml-auto"
                :class="'btns' + collection.id"
                @click.stop=""
              >
                <edit-btn></edit-btn>
                <delete-btn
                  @delete="deleteCollection(collection.id)"
                ></delete-btn>
              </span>
            </v-expansion-panel-header>
            <v-expansion-panel-content v-if="selectedCollection()">
              <button-bar>
                <v-col cols="12" md="auto" class="pb-3 pb-md-0">
                  <ranking-btn @ranking-saved="rankingSaved" />
                </v-col>
                <v-col cols="12" md="4" class="pb-0 pl-0">
                  <sort-select ref="sorting" />
                </v-col>
              </button-bar>
              <div v-if="selectedCollection().rankings.length > 0">
                <v-btn
                  elevation="2"
                  class="mr-2"
                  fab
                  small
                  @click="expand = !expand"
                  ><v-icon>{{ showRankings }}</v-icon>
                </v-btn>
                Toggle Rankings
              </div>
              <franchise-panel :expandRankings="expand" />
            </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
      </div>
    </div>
  </div>
</template>

<script>
import db from "@/api/db";
import SearchBar from "@/components/SearchBar";
import FranchisePanel from "@/components/FranchisePanel";
import ButtonBar from "@/components/ButtonBar";
import RankingBtn from "@/components/buttons/RankingBtn";
import SortSelect from "@/components/buttons/SortSelect";
import EditBtn from "@/components/buttons/EditBtn";
import DeleteBtn from "@/components/buttons/DeleteBtn";
import { mapGetters, mapActions } from "vuex";

export default {
  name: "Home",
  components: {
    SearchBar,
    FranchisePanel,
    ButtonBar,
    RankingBtn,
    SortSelect,
    EditBtn,
    DeleteBtn
  },
  data() {
    return {
      noResults: false,
      expand: false,
      date: null,
      panel: []
    };
  },
  computed: {
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
      "setTempRanking",
      "removeCollection",
      "removeUserCollection"
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
    scrollTo(e) {
      const el = document.getElementById(e);
      el.scrollIntoView();
      el.click();
    },
    select(id) {
      this.selectCollectionById(id);
    },
    rankingSaved() {
      // force a resort in case franchise is sorted by user ranking and user updates their ranking
      console.log("sorting", this.$refs.sorting);
      const input = this.$refs.sorting.find(
        s => s.collectionMovies.name === this.selectedCollection().name
      ).$children[0];
      input.setValue(input.value);
    },
    async deleteCollection(id) {
      const res = await db.deleteCollection(id);
      if (res.data.isSuccess) {
        this.removeCollection(id);
        this.removeUserCollection(id);
      }
    }
  },
  watch: {
    panel(value) {
      console.log("panel", value);
    }
  }
};
</script>

<style scoped>
.v-expansion-panel-header {
  justify-content: space-between;
}
.v-expansion-panel-header > *:not(.v-expansion-panel-header__icon) {
  flex: none;
}
</style>
