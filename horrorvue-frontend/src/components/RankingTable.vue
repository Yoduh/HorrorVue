<template>
  <v-simple-table dark>
    <template v-slot:default>
      <thead>
        <tr>
          <th class="text-left">
            Name
          </th>
          <th class="text-left">
            Rank/Rating
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="rank in rankings()" :key="rank.id">
          <td>{{ userName(rank.userId) }}</td>
          <td>
            #{{ rankForMovie(rank) }}
            <v-icon color="yellow lighten-1" small>mdi-star</v-icon>)
          </td>
        </tr>
      </tbody>
    </template>
  </v-simple-table>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "RankingTable",
  props: ["movieId"],
  methods: {
    ...mapGetters(["selectedCollection", "rankings"]),
    rankForMovie(ranking) {
      const rank = ranking.order.indexOf(this.movieId);
      const rating = ranking.ratings[rank];
      this.stars = rating;
      return `${rank + 1} (${rating.toFixed(1)}`;
    },
    userName(id) {
      const user = this.selectedCollection().appUsers.find(u => u.id === id);
      return user.firstName + " " + user.lastName.charAt(0) + ".";
    }
  },
  computed: {
    users() {
      let users = [];
      users = this.selectedCollection().rankings.map(r => {
        let appUser = this.selectedCollection().appUsers.find(
          u => u.id == r.userId
        );
        return {
          id: appUser.id,
          rankings: r.order,
          ratings: r.ratings,
          collection: this.selectedCollection()
        };
      });
      return users;
    }
  },
  watch: {
    rankings(val) {
      console.log("val", val);
    }
  }
};
</script>

<style scoped>
tr {
  text-align: center;
}
td,
th {
  text-align: center !important;
  padding-left: 0 !important;
  padding-right: 0 !important;
}
</style>
