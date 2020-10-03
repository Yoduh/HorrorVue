<template>
  <div
    class="franchise-panel"
    :id="'franchise' + franchise.id"
    @wheel="scrollHorizontal($event, franchise.id)"
  >
    <v-container fluid class="franchise-container pt-0">
      <v-row class="movieRow">
        <transition-group name="flip-movies" tag="div">
          <v-col
            v-for="movie in franchise.movies"
            :key="movie.id"
            class="movieColumn justify-sm-start pt-1"
          >
            <collapse-transition>
              <ranking-table
                v-show="expandRankings"
                :franchise="franchise"
                :movieId="movie.id"
              ></ranking-table>
            </collapse-transition>
            <collection-movie-card :result="movie" @info="info(movie)" />
          </v-col>
        </transition-group>
      </v-row>
    </v-container>
  </div>
</template>

<script>
import CollectionMovieCard from "@/components/cards/CollectionMovieCard";
import RankingTable from "@/components/RankingTable";
import { CollapseTransition } from "@ivanv/vue-collapse-transition";

export default {
  name: "FranchisePanel",
  props: ["franchise", "expandRankings"],
  components: {
    CollectionMovieCard,
    RankingTable,
    CollapseTransition
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
      }
    };
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
      // if cant horizontal scroll anymore, return to vertical scrolling
      if (
        (el.scrollWidth - el.clientWidth === el.scrollLeft && delta < 0) ||
        (el.scrollLeft === 0 && delta >= 0)
      )
        return;

      el.scrollLeft -= delta * 40; // Multiplication factor changes speed of scroll
      e.preventDefault();
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
.movieRow > div {
  display: flex;
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
.flip-movies-move {
  transition: transform 0.8s ease;
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
