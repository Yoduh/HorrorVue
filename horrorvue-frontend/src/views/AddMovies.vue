<template>
  <div class="add-movies-wrapper">
    <search-bar @search="searchMovies" :placeholder="query"></search-bar>
    <v-container>
      <v-row dense v-if="results.length > 0">
        <div v-for="result in results" :key="result.tmdId">
          <add-movie-card
            v-if="!containsMovieId(collection, result)"
            :result="result"
            class="mx-2"
            @addMovie="addMovie"
            @removeMovie="removeMovie"
          />
        </div>
      </v-row>
      <save-new-modal
        :show="tempMovies() && tempMovies().length > 0"
        :name="query"
        @save="save"
      >
      </save-new-modal>
    </v-container>
  </div>
</template>

<script>
import SearchBar from "@/components/SearchBar";
import SaveNewModal from "@/components/modals/SaveNewModal";
import AddMovieCard from "@/components/cards/AddMovieCard";
import { mapGetters, mapActions } from "vuex";
import api from "@/api/tmdb.js";
import db from "@/api/db.js";

export default {
  name: "AddMovies",
  components: {
    SearchBar,
    SaveNewModal,
    AddMovieCard
  },
  props: {
    query: {
      type: String
    },
    collection: {
      type: Array,
      default: function() {
        return [];
      }
    }
  },
  data() {
    return {
      results: []
    };
  },
  methods: {
    ...mapGetters([
      "searchResults",
      "collections",
      "selectedCollection",
      "user",
      "tempMovies"
    ]),
    ...mapActions([
      "setSearchResults",
      "addCollection",
      "selectCollectionById",
      "setTempMovies",
      "addToTempMovies",
      "removeFromTempMovies",
      "resetTempMovies",
      "updateCollectionMovies"
    ]),
    async searchMovies(searchResults) {
      this.setSearchResults(searchResults.data);
      if (this.tempMovies()) {
        this.results = this.tempMovies().slice();
      }
      searchResults.data.forEach(result => {
        if (this.results.find(r => r.tmdId === result.tmdId) !== undefined) {
          return;
        } else this.results.push({ ...result, added: false, show: false });
      });
      // this.$router.push(`/search?q=${results.searchTerm}`);
    },
    containsMovieId(movieArray, movie) {
      let ret = false;
      movieArray.forEach(m => {
        if (m.id === movie.id) {
          ret = true;
        }
      });
      return ret;
    },
    addMovie(movie) {
      movie.added = true;
      this.addToTempMovies(movie);
    },
    removeMovie(movie) {
      movie.added = false;
      this.removeFromTempMovies(movie);
    },
    async save(name) {
      let res = null;
      if (name === "") {
        this.$eventBus.$emit(
          "open-snackbar",
          "Error: Collection name can not be blank",
          "error"
        );
        return;
      }
      if (!this.selectedCollection()) {
        if (this.collections().find(c => c.name === name) !== undefined) {
          this.$eventBus.$emit(
            "open-snackbar",
            "Error: Collection with that name already exists",
            "error"
          );
          return;
        }
        res = await db.newCollection(this.tempMovies(), name);
        if (res.isSuccess) {
          res = this.addCollection(res.data);
          this.$eventBus.$emit(
            "open-snackbar",
            "Collection successfully created",
            "success"
          );
        } else {
          this.$eventBus.$emit("open-snackbar", res.message, "error");
        }
      } else {
        res = await db.updateCollection(
          this.tempMovies(),
          this.selectedCollection().id,
          name
        );
        if (res.isSuccess) {
          this.updateCollectionMovies([res.data.movies, name]);
          this.$eventBus.$emit(
            "open-snackbar",
            "Collection successfully updated",
            "success"
          );
        } else {
          this.$eventBus.$emit("open-snackbar", res.message, "error");
        }
      }
      // go back home after save
      this.$router.push("/");
    }
  },
  async created() {
    this.resetTempMovies();
    const id = window.localStorage.getItem("selectedCollection");
    if (id !== null) {
      // ideally figure out how to reset selectedCollection on refresh but for now if refresh
      // detected send user back home
      window.localStorage.removeItem("selectedCollection");
      this.$router.push("/");
      return;
    }
    if (this.results.length === 0) {
      const movies = await api.fetchMovies(this.query);
      // user is editing pre-existing collection
      if (this.selectedCollection()) {
        this.setTempMovies(
          this.selectedCollection().movies.map(m => {
            return { ...m, added: true };
          })
        );
        window.localStorage.setItem(
          "selectedCollection",
          this.selectedCollection().id
        );
        this.tempMovies().forEach(m => {
          this.results.push(m);
        });
      }
      this.setSearchResults(this.results);
      movies.forEach(result => {
        if (
          this.results.find(r => {
            return r.tmdId === result.tmdId;
          }) !== undefined
        ) {
          return;
        } else this.results.push({ ...result, added: false, show: false });
      });
    } else {
      this.results = this.searchResults().map(result => {
        return { ...result, added: false, show: false };
      });
    }
  }
};
</script>

<style scoped>
.save-btn {
  bottom: 1rem !important;
}
</style>
