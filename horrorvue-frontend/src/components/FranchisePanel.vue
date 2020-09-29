<template>
  <div
    class="franchise-panel"
    :id="'franchise' + franchise.id"
    @wheel.prevent="scrollHorizontal($event, franchise.id)"
  >
    <!-- <draggable v-model="franchise.movies" group="movies" @start="drag=true" @end="drag=false" class="flex"> -->
    <v-container fluid class="franchise-container">
      <v-row>
        <v-menu transition="fab-transition">
          <template v-slot:activator="{ on, attrs }">
            <v-btn
              elevation="2"
              fab
              @click="toggleRankings"
              v-bind="attrs"
              v-on="on"
              ><v-icon>{{ showRankings }}</v-icon>
            </v-btn>
          </template>
        </v-menu>
        Toggle Rankings
      </v-row>
      <v-row class="movieRow">
        <v-col
          v-for="movie in franchise.movies"
          :key="movie.id"
          class="movieColumn justify-sm-start"
        >
          <ranking-table></ranking-table>
          <collection-movie-card :result="movie" @info="info(movie)" />
          <!-- rankings -->
          <!-- <v-spacer /> -->
        </v-col>
      </v-row>
    </v-container>
    <!-- </draggable> -->
  </div>
</template>

<script>
import CollectionMovieCard from "@/components/cards/CollectionMovieCard";
import RankingTable from "@/components/RankingTable";
// import draggable from 'vuedraggable'

export default {
  name: "FranchisePanel",
  props: ["franchise"],
  components: {
    CollectionMovieCard,
    RankingTable
    // draggable
  },
  data() {
    return {
      ops: {
        vuescroll: {
          sizeStrategy: "number"
        },
        bar: {
          keepShow: true,
          size: "10px"
        }
      },
      expanded: false
    };
  },
  computed: {
    showRankings() {
      if (this.expanded) return "mdi-chevron-up";
      else return "mdi-chevron-down";
    }
  },
  methods: {
    info(id) {
      this.franchise.movies.forEach(movie => {
        if (movie.id === id) {
          movie.show = !movie.show;
          return;
        }
      });
    },
    scrollHorizontal(e, id) {
      var el = document.getElementById("franchise" + id);
      e = window.event || e;
      var delta = Math.max(-1, Math.min(1, e.wheelDelta || -e.detail));
      el.scrollLeft -= delta * 80; // Multiplied by 80
      e.preventDefault();
    },
    toggleRankings() {
      this.expanded = !this.expanded;
    }
  }
};
</script>

<style scoped>
.row {
  margin: 0;
}
.movieRow {
  flex-wrap: nowrap;
  width: fit-content;
}
.franchise-panel {
  width: 100%;
  overflow: auto;
}
.rankingRow {
  position: relative;
  top: -15px;
}
/* height (horizontal) */
::-webkit-scrollbar {
  height: 10px;
  cursor: pointer;
}

/* Track */
::-webkit-scrollbar-track {
  background: transparent;
  border-radius: 10px;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #888;
  border-radius: 10px;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #555;
}
</style>
