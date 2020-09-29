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
        <tr v-for="(rank, i) in mockData" :key="i">
          <td>{{ rank.name }}</td>
          <td>{{ rank.rank }}</td>
        </tr>
      </tbody>
    </template>
  </v-simple-table>
</template>

<script>
export default {
  name: "RankingTable",
  props: ["rankings", "collections"],
  data() {
    return {
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
  }
};
</script>

<style scoped></style>
