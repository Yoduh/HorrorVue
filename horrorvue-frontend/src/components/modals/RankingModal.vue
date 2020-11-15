<template>
  <v-list dense>
    <v-list-item-group :value="movies">
      <v-container class="modal-header white--text">
        <h1 v-if="hasRankings">Update Your Rankings!</h1>
        <h1 v-else>Create Your Rankings!</h1>
        <p class="font-italic grey--text text--lighten-1  text-caption mb-1">
          Drag and drop to set rankings. Click the stars to give a rating.
        </p>
      </v-container>
      <draggable
        class="list-group"
        tag="ul"
        v-model="movies"
        v-bind="dragOptions"
        :move="onMove"
        :scroll-sensitivity="100"
        :force-fallback="true"
        @start="isDragging = true"
        @end="isDragging = false"
      >
        <transition-group type="transition" :name="'flip-list'">
          <v-list-item
            v-for="(movie, index) in movies"
            :key="movie.id"
            class="pl-2 py-2"
          >
            <v-row justify="start" class="pl-2">
              <v-col cols="12" md="auto" class="py-0">
                <v-row>
                  <v-btn fab outlined small class="text-button white--text">
                    #{{ index + 1 }}
                  </v-btn>
                  <v-list-item-content>
                    <v-list-item-title
                      class="title text-body-1 font-weight-bold pl-4 grey--text text--lighten-5"
                    >
                      {{ movie.title }}
                      <span class="grey--text font-italic"
                        >({{ movie.release_date.substring(0, 4) }})</span
                      >
                    </v-list-item-title>
                  </v-list-item-content>
                </v-row>
              </v-col>
              <v-col
                cols="12"
                md="auto"
                class="ml-auto py-0 d-flex justify-space-around"
              >
                <v-rating
                  v-model="movie.rating"
                  background-color="#eee"
                  color="yellow lighten-1"
                  empty-icon="mdi-star-outline"
                  full-icon="mdi-star"
                  half-icon="mdi-star-half-full"
                  half-increments
                  clearable
                  hover
                  length="5"
                  size="25"
                ></v-rating>
              </v-col>
            </v-row>
          </v-list-item>
        </transition-group>
      </draggable>
    </v-list-item-group>
    <div class="d-flex justify-center">
      <v-btn elevation="2" color="success" large @click="saveRankings">
        Save
      </v-btn>
    </div>
  </v-list>
</template>

<script>
import db from "@/api/db";
import { mapActions, mapGetters } from "vuex";
import draggable from "vuedraggable";

export default {
  name: "RankingModal",
  props: ["dialog"],
  components: {
    draggable
  },
  data() {
    return {
      isDragging: false,
      delayedDragging: false,
      movies: []
    };
  },
  methods: {
    ...mapActions([
      "sortSelected",
      "setTempRanking",
      "updateCollectionRanking",
      "setRankOrderMovies",
      "resetTempRanking"
    ]),
    ...mapGetters(["selectedCollection", "user", "tempRanking"]),
    onMove({ relatedContext, draggedContext }) {
      const relatedElement = relatedContext.element;
      const draggedElement = draggedContext.element;
      return (
        (!relatedElement || !relatedElement.fixed) && !draggedElement.fixed
      );
    },
    async saveRankings() {
      this.$emit("close");
      let res = null;
      // update existing ranking
      const userRanking = this.selectedCollection().rankings.find(
        r => r.userId === this.user().id
      );
      if (userRanking !== undefined) {
        res = await db.updateRanking(userRanking, this.movies);
      }
      // create new ranking
      else {
        res = await db.createRanking(this.selectedCollection(), this.movies);
      }

      if (res.data.isSuccess) {
        if (userRanking !== undefined) {
          this.$eventBus.$emit("open-snackbar", "Ranking updated!", "success");
        } else {
          this.$eventBus.$emit("open-snackbar", "Ranking created!", "success");
        }
        this.updateCollectionRanking(res.data.data);
        this.setRankOrderMovies();
        this.resetTempRanking();
        this.$emit("ranking-saved");
      } else {
        this.$eventBus.$emit("open-snackbar", res.data.message, "error");
      }
    }
  },
  computed: {
    hasRankings() {
      const user = this.selectedCollection().rankings.find(
        r => r.userId === this.user().id
      );
      return !!user;
    },
    dragOptions() {
      return {
        animation: 0,
        group: "description",
        ghostClass: "ghost"
      };
    }
  },
  watch: {
    isDragging(newValue) {
      if (newValue) {
        this.delayedDragging = true;
        return;
      }
      this.$nextTick(() => {
        this.delayedDragging = false;
      });
    },
    dialog(val) {
      // if (val) this.movies = [...this.tempRanking()];
      if (val) this.movies = JSON.parse(JSON.stringify(this.tempRanking()));
    }
  },
  created() {
    // this.movies = [...this.tempRanking()];
    this.movies = JSON.parse(JSON.stringify(this.tempRanking()));
  }
};
</script>

<style scoped>
.flip-list-move {
  transition: transform 0.5s;
}
.no-move {
  transition: transform 0s;
}
.ghost {
  opacity: 0.5;
  background: #78909c;
}
.list-group {
  min-height: 20px;
  padding-left: 0;
}
.list-group-item {
  cursor: move;
}
.title {
  line-height: 1.4rem !important;
  white-space: inherit !important;
}
.v-list {
  background: rgb(40, 44, 52);
}
.v-list-item:hover {
  background-color: #232d33;
}
.v-icon {
  padding: 0.1rem !important;
}
</style>
