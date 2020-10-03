<template>
  <v-simple-table dark>
    <template v-slot:default>
      <thead>
        <tr>
          <th class="text-left">
            Name
          </th>
          <th class="text-left">
            Rank
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(user, i) in users" :key="i">
          <td>{{ user.name }}</td>
          <td>{{ rankForMovie(movieId, user.rankings) }}</td>
        </tr>
      </tbody>
    </template>
  </v-simple-table>
</template>

<script>
export default {
  name: "RankingTable",
  props: ["franchise", "movieId"],
  data() {
    return {
      users: [],
      mockData: [
        {
          name: "Alex",
          rank: "1"
        },
        {
          name: "Noelle",
          rank: "3"
        }
      ]
    };
  },
  methods: {
    rankForMovie(movieId, rankings) {
      return rankings.indexOf(movieId) + 1;
    }
  },
  created() {
    if (this.franchise.rankings.length > 0) {
      this.users = this.franchise.rankings.map(r => {
        let appUser = this.franchise.appUsers.find(u => u.id == r.userId);
        return {
          name: appUser.firstName + " " + appUser.lastName.charAt(0) + ".",
          rankings: r.order
        };
      });
    }
  }
};
</script>

<style scoped></style>
