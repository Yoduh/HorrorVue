<template>
    <draggable class="list-group" tag="ul" v-model="collection.movies" v-bind="dragOptions" :move="onMove" @start="isDragging=true" @end="isDragging=false">
        <transition-group type="transition" :name="'flip-list'">
          <li class="list-group-item" v-for="(movie, index) in collection.movies" :key="movie.id">
            {{ movie.title }}
            <span class="badge">{{ index }}</span>
          </li>
        </transition-group>
    </draggable>
</template>

<script>
import draggable from 'vuedraggable'

export default {
    name: "RankingModal",
    components: {
        draggable
    },
    props: [
        'collection'
    ],
    data() {
        return {
            isDragging: false,
            delayedDragging: false
        }
    },
    methods: {
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
    }
}
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
  background: #c8ebfb;
}
.list-group {
  background-color: white;
  min-height: 20px;
}
.list-group-item {
  cursor: move;
}
.list-group-item i {
  cursor: pointer;
}
</style>