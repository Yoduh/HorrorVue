<template>
  <v-list dense class="blue-grey darken-4">
    <v-list-item-group :value="movies">
      <v-container class="modal-header white--text">
        <h1 v-if="hasRankings">Edit Your Rankings!</h1>
        <h1 v-else>Create Your Rankings!</h1>
        <p class="font-italic grey--text text--lighten-1  text-caption mb-1">
          Drag and drop to set rankings
        </p>
      </v-container>
      <draggable
        class="list-group"
        tag="ul"
        v-model="movies"
        v-bind="dragOptions"
        :move="onMove"
        @start="isDragging = true"
        @end="isDragging = false"
      >
        <transition-group type="transition" :name="'flip-list'">
          <v-list-item
            v-for="(movie, index) in movies"
            :key="movie.id"
            class="pl-2 py-2"
          >
            <v-btn fab outlined small class="text-button white--text">
              #{{ index + 1 }}
            </v-btn>

            <v-list-item-content class="pl-4">
              <v-list-item-title
                class="text-body-1 font-weight-bold grey--text text--lighten-5"
              >
                {{ movie.title }}
                <span class="grey--text font-italic"
                  >({{ movie.release_date.substring(0, 4) }})</span
                >
              </v-list-item-title>
            </v-list-item-content>
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
  components: {
    draggable
  },
  data() {
    return {
      isDragging: false,
      delayedDragging: false
    };
  },
  methods: {
    ...mapActions([
      "sortSelected",
      "setTempRanking",
      "updateCollectionRanking"
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
        res = await db.updateRanking(userRanking, this.tempRanking());
      }
      // create new ranking
      else {
        res = await db.createRanking(
          this.selectedCollection(),
          this.tempRanking()
        );
      }

      if (res.data.isSuccess) {
        this.updateCollectionRanking(res.data.data);
        this.$emit("ranking-saved");
      }
    }
  },
  computed: {
    movies: {
      get() {
        return this.tempRanking();
      },
      set(value) {
        this.setTempRanking(value);
      }
    },
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
    }
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
.v-list-item:hover {
  background-color: #232d33;
}
</style>
