<template>
  <v-row justify="center">
    <v-tooltip bottom>
      <template v-slot:activator="{ on, attrs }">
        <v-fab-transition>
          <v-btn
            @click="dialog = !dialog"
            v-show="show"
            v-bind="attrs"
            v-on="on"
            class="save-btn"
            color="red"
            dark
            absolute
            fixed
            bottom
            right
            fab
          >
            <v-icon>mdi-content-save</v-icon>
          </v-btn>
        </v-fab-transition>
      </template>
      <span>Save selection</span>
    </v-tooltip>
    <v-dialog v-model="dialog" max-width="600px" dark>
      <v-card>
        <v-card-title>
          <span class="headline">Name this franchise before saving</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="12">
                <v-text-field
                  label="Name"
                  v-model="name"
                  autofocus
                  :rules="[rules.required]"
                ></v-text-field>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="dialog = false"
            >Cancel</v-btn
          >
          <v-btn color="blue darken-1" text @click="save">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
export default {
  name: "SaveNewModal",
  props: ["show"],
  data: () => ({
    dialog: false,
    name: "",
    rules: {
      required: value => !!value || "Required."
    }
  }),
  methods: {
    save() {
      if (this.name === "") return;
      this.dialog = false;
      this.$emit("save", this.name);
      this.name = "";
    }
  }
};
</script>

<style scoped>
.save-btn {
  bottom: 1rem !important;
}
</style>
