<template>
  <v-select
    :items="users"
    item-text="name"
    item-value="userId"
    v-model="selected"
    label="Sort By"
    class="pa-0 ml-5 mt-0"
    placeholder="Release Year"
    v-on:input="sort"
  ></v-select>
</template>

<script>
import { mapActions, mapGetters } from "vuex";

export default {
  name: "SortSelect",
  data() {
    return {
      selected: 0,
      collectionMovies: this.$store.getters.selectedCollection
    };
  },
  computed: {
    users() {
      const users = this.selectedCollection().rankings.map(r => {
        let appUser = this.selectedCollection().appUsers.find(
          u => u.id == r.userId
        );
        return {
          userId: appUser.id,
          name: appUser.firstName + " " + appUser.lastName
        };
      });
      users.unshift({ userId: 0, name: "Release Year" });
      return users;
    }
  },
  methods: {
    ...mapActions(["sortSelected"]),
    ...mapGetters(["selectedCollection"]),
    sort(userId) {
      let sortedMovies = [];
      if (userId === 0) {
        sortedMovies = this.collectionMovies.movies.sort(
          (a, b) => new Date(a.release_date) - new Date(b.release_date)
        );
      } else {
        const order = this.selectedCollection().rankings.find(
          r => r.userId === userId
        ).order;
        if (order !== undefined) {
          sortedMovies = this.collectionMovies.movies.sort(function(a, b) {
            return order.indexOf(a.id) - order.indexOf(b.id);
          });
        }
      }
      this.sortSelected(sortedMovies.map(m => m.id));
    }
  }
};
</script>

<style scoped></style>
