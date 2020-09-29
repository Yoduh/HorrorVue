<template>
  <div>
    <v-dialog v-model="dialog" max-width="600px">
      <template v-slot:activator="{ on, attrs }">
        <v-btn v-bind="attrs" v-on="on">Set Ranking</v-btn>
      </template>
      <ranking-modal :collection="collection" />
    </v-dialog>
  </div>
</template>
<script>
import RankingModal from "@/components/modals/RankingModal";

export default {
  name: "RankingBtn",
  components: {
    RankingModal
  },
  props: ["collection"],
  data() {
    return {
      dialog: false
    };
  }
};
</script>

<style scoped></style>
