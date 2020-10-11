<template>
  <div class="home">
    <div v-if="!$auth.isAuthenticated">
      Log in to use this site
    </div>
    <div v-else>
      <search-bar @search="searchFranchise" @scroll-to="scrollTo"></search-bar>
      <div v-if="noResults">
        No movies for that franchise were found, try another one?
      </div>
      <div v-if="isLoading()">Loading...</div>
      <div
        v-else-if="
          !isLoading() && (collections() === null || collections().length === 0)
        "
      >
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
              <span>
                <v-tooltip top v-if="createdByOther(collection.createdBy)">
                  <template v-slot:activator="{ on, attrs }">
                    <v-icon
                      color="primary"
                      class="mr-2 ml-n5"
                      v-bind="attrs"
                      v-on="on"
                    >
                      mdi-account-multiple
                    </v-icon>
                  </template>
                  <span>Subscribed Collection</span>
                </v-tooltip>
                <span v-else class="ml-3" />
                <strong>{{ collection.name }}</strong>
              </span>
              <span
                v-if="panel === index"
                class="ml-auto"
                :class="'btns' + collection.id"
                @click.stop=""
              >
                <edit-btn
                  :disabled="!canDeleteOrEdit(collection.createdBy)"
                ></edit-btn>
                <delete-btn
                  :canDelete="canDeleteOrEdit(collection.createdBy)"
                  :collection="collection"
                  :userId="user().id"
                  @delete="deleteCollection(collection.id)"
                  @unsubscribe="unsubCollection(collection.id)"
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
    ...mapGetters(["collections", "selectedCollection", "user", "isLoading"]),
    ...mapActions([
      "setSearchResults",
      "setCollections",
      "selectCollectionById",
      "removeCollection",
      "setRankOrderMovies",
      "resetTempRanking"
    ]),
    createdByOther(createdById) {
      return this.user().id !== createdById;
    },
    searchFranchise(results) {
      window.localStorage.removeItem("selectedCollection");
      this.selectCollectionById(null);
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
      this.setRankOrderMovies();
      this.resetTempRanking();
    },
    rankingSaved() {
      // force a resort in case franchise is sorted by user ranking and user updates their ranking
      const input = this.$refs.sorting.find(
        s => s.collectionMovies.name === this.selectedCollection().name
      ).$children[0];
      input.setValue(input.value);
    },
    async deleteCollection(id) {
      const res = await db.deleteCollection(id);
      if (res.data.isSuccess) {
        this.$eventBus.$emit("open-snackbar", "Collection deleted", "success");
        this.removeCollection(id);
      } else {
        this.$eventBus.$emit("open-snackbar", res.data.message, "error");
      }
    },
    async unsubCollection(id) {
      const res = await db.unsubCollection(id, this.user().id);
      if (res.data.isSuccess) {
        this.$eventBus.$emit(
          "open-snackbar",
          "Unsubscribed from collection",
          "success"
        );
        this.removeCollection(id);
      } else {
        this.$eventBus.$emit("open-snackbar", res.data.message, "error");
      }
    },
    canDeleteOrEdit(createdBy) {
      return this.user().id === createdBy;
    }
  },
  created() {
    this.selectCollectionById(-1);
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
