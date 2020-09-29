<template>
  <div>
    <v-tooltip top>
      <template v-slot:activator="{ on, attrs }">
        <v-btn
          @click="dialog = !dialog"
          v-bind="attrs"
          v-on="on"
          class="info-btn"
          light
          icon
        >
          <v-icon>mdi-information-outline</v-icon>
        </v-btn>
      </template>
      <span>Movie Info</span>
    </v-tooltip>
    <v-dialog v-model="dialog" max-width="600px">
      <v-card>
        <v-card-title>
          <span class="headline">{{ title }}</span>
        </v-card-title>
        <v-card-subtitle class="pb-0">
          {{ subtitle }}
        </v-card-subtitle>
        <v-card-text class="pb-0">
          <v-container>
            <div class="text--primary">
              {{ description }}
            </div>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="dialog = false"
            >Close</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
export default {
  name: "InfoModal",
  props: ["title", "subtitle", "description"],
  data: () => ({
    dialog: false
  })
};
</script>

<style scoped></style>
