<template>
  <div>
    <v-dialog v-model="dialog" max-width="600px" @click:outside="resetSort">
      <template v-slot:activator="{ on, attrs }">
        <v-btn v-bind="attrs" v-on="on">Set Ranking</v-btn>
      </template>
      <ranking-modal
        @close="dialog = false"
        @ranking-saved="$emit('ranking-saved')"
      />
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
  data() {
    return {
      dialog: false
    };
  },
  methods: {
    resetSort() {
      this.$store.dispatch("resetTempRanking");
    }
  }
};
</script>

<style scoped></style>
