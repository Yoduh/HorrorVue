<template>
  <v-list dense class="blue-grey darken-4">
    <v-list-item-group v-model="movies">
      <p class="font-italic text-caption grey--text text--lighten-1 mb-1 ml-3">
        Drag and drop to set rankings
      </p>
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
  props: ["collection"],
  data() {
    return {
      isDragging: false,
      delayedDragging: false,
      movies: this.collection.movies,
      userRanking: this.userRankings().find(
        ranking => ranking.collectionId === this.collection.id
      )
    };
  },
  methods: {
    ...mapActions(["setUserRankings", "addOrUpdateRankings"]),
    ...mapGetters(["userRankings"]),
    orderList() {
      this.list = this.list.sort((one, two) => {
        return one.order - two.order;
      });
    },
    onMove({ relatedContext, draggedContext }) {
      const relatedElement = relatedContext.element;
      const draggedElement = draggedContext.element;
      return (
        (!relatedElement || !relatedElement.fixed) && !draggedElement.fixed
      );
    },
    async saveRankings() {
      // update existing ranking
      console.log("updating ranking");
      if (this.userRanking !== undefined) {
        const res = await db.updateRanking(this.userRanking, this.movies);
        if (res.data.isSuccess) this.addOrUpdateRankings(res.data.data);
        else console.log("error updating ranking", res.data.message);
      }
      // create new ranking
      else {
        const res = await db.createRanking(this.collection, this.movies);
        if (res.data.isSuccess) this.addOrUpdateRankings(res.data);
        else console.log("error creating ranking", res.data.message);
      }
      this.$emit("close");
    }
  },
  computed: {
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
  },
  created() {
    const userRanking = this.userRankings().find(
      ranking => ranking.collectionId === this.collection.id
    );
    if (userRanking !== undefined) {
      this.movies.sort(function(a, b) {
        return (
          userRanking.order.indexOf(a.id) - userRanking.order.indexOf(b.id)
        );
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
