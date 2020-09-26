<template>
    <div class="franchise-panel" :id="'franchise' + franchise.id" @wheel.prevent="scrollHorizontal($event, franchise.id)">
        <draggable v-model="franchise.movies" group="movies" @start="drag=true" @end="drag=false" class="flex">
            <div v-for="movie in franchise.movies" :key="movie.id">
                <collection-movie-card :result="movie" class="mx-2" @expand="expand(movie.id)" />
            </div>
        </draggable>
    </div>
</template>

<script>
import CollectionMovieCard from '@/components/cards/CollectionMovieCard';
import draggable from 'vuedraggable'

export default {
    name: "FranchisePanel",
    props:
        ['franchise']
    ,
    components: {
        CollectionMovieCard,
        draggable
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
        expand(id) {
            console.log(id);
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
.flex {
    display: flex;
}
.franchise-panel{
    height: 100%;
    width: 100%;
    overflow: auto;
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

