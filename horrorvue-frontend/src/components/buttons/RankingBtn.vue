<template>
  <div>
    <v-dialog
      v-model="dialog"
      max-width="800px"
      @click:outside="resetSort"
      class="ranking-dialog"
    >
      <template v-slot:activator="{ on, attrs }">
        <v-btn v-bind="attrs" v-on="on">Set Ranking</v-btn>
      </template>
      <ranking-modal
        :dialog="dialog"
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
      console.log("resetting");
      this.$store.dispatch("resetTempRanking");
    }
  }
};
</script>

<style scoped>
.ranking-dialog {
  overflow-x: hidden;
}
.v-dialog {
  overflow-x: hidden;
}
</style>
