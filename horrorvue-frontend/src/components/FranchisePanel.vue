<template>
    <div class="franchise-panel" :id="'franchise' + franchise.id" @wheel.prevent="scrollHorizontal($event, franchise.id)">
        <!-- <draggable v-model="franchise.movies" group="movies" @start="drag=true" @end="drag=false" class="flex"> -->
            <v-container fluid class="franchise-container">
                <v-row class="movieRow">
                    <v-col v-for="movie in franchise.movies" :key="movie.id" class="movieColumn">
                        <v-row>
                            <collection-movie-card :result="movie" @info="info(movie)" />
                        </v-row>
                        <!-- rankings -->
                        <v-row class="rankingRow">
                            <v-col>
                                <div>Alex: #1</div>
                                <div>Noelle: #2</div>
                            </v-col>
                        </v-row>
                    </v-col>
                </v-row>
            </v-container>
        <!-- </draggable> -->
    </div>
</template>

<script>
import CollectionMovieCard from '@/components/cards/CollectionMovieCard';
// import draggable from 'vuedraggable'

export default {
    name: "FranchisePanel",
    props:
        ['franchise']
    ,
    components: {
        CollectionMovieCard,
        // draggable
    },
    data() {
        return {
            ops: {
                vuescroll: {
                    sizeStrategy:"number"
                },
                bar: {
                    keepShow: true,
                    size: "10px"
                }
            }
        }
    },
    methods: {
        info(id) {
            this.franchise.movies.forEach(movie => {
                if (movie.id === id)
                {
                    movie.show = !movie.show;
                    return;
                }
            })
        },
        scrollHorizontal(e, id) {
            var el = document.getElementById('franchise' + id);
            e = window.event || e;
            var delta = Math.max(-1, Math.min(1, (e.wheelDelta || -e.detail)));
            el.scrollLeft -= (delta * 80); // Multiplied by 80
            e.preventDefault();
        }
    }
}
</script>

<style scoped>
.row {
    margin: 0;
}
.movieColumn {
    flex-grow: 0;
}
.movieRow {
    flex-wrap: nowrap;
}
.franchise-panel{
    height: 100%;
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

