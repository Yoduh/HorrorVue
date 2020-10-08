<template>
  <v-container v-if="user()">
    <v-row>
      <v-form>
        <v-row>Your Information</v-row>
        <v-row>
          <v-col cols="12" sm="6">
            <v-text-field
              :value="user().firstName"
              label="First Name"
              dark
              disabled
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6">
            <v-text-field
              :value="user().lastName"
              label="Last Name"
              dark
              disabled
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6">
            <v-text-field
              :value="user().email"
              dark
              label="Email"
              disabled
            ></v-text-field>
          </v-col>
        </v-row>
      </v-form>
    </v-row>
    <v-row align="end">
      <h3>Select collections and invite your friends to rank them!</h3>
      <v-btn class="ml-auto" @click="selectToggle">{{ selectAll }}</v-btn>
    </v-row>
    <v-row>
      <v-col cols="12" class="mt-3 pa-0">
        <v-list dark>
          <template v-for="(collection, index) in user().collections">
            <v-divider v-if="index != 0" :key="`divider-${index}`"></v-divider>
            <v-list-item two-line :key="collection.id">
              <v-list-item-content>
                <v-list-item-title>{{ collection.name }}</v-list-item-title>
                <v-list-item-subtitle>
                  Ranked:
                  <span :class="isRanked(index, collection.id)">{{
                    isRanked(index, collection.id)
                  }}</span>
                </v-list-item-subtitle>
              </v-list-item-content>
              <v-list-item-action>
                <v-checkbox
                  v-model="selected"
                  :value="{ id: collection.id, name: collection.name }"
                ></v-checkbox>
              </v-list-item-action>
            </v-list-item>
          </template>
        </v-list>
      </v-col>
    </v-row>
    <v-row class="mt-3">
      <v-btn class="ml-auto" @click="dialog = true">
        <v-icon class="mr-2">mdi-email-outline</v-icon> E-mail Invites
      </v-btn>
      <email-modal
        :dialog="dialog"
        @close="dialog = false"
        :collections="selected.slice()"
      ></email-modal>
    </v-row>
  </v-container>
</template>

<script>
import EmailModal from "@/components/modals/EmailModal";
import { mapGetters } from "vuex";

export default {
  name: "Profile",
  components: {
    EmailModal
  },
  data() {
    return {
      selected: [],
      dialog: false
    };
  },
  methods: {
    ...mapGetters(["user"]),
    isRanked(index, id) {
      const ranking = this.user().collections[index].rankings.find(
        r => r.collectionId === id
      );
      if (
        ranking &&
        ranking.order.length === this.user().collections[index].movies.length
      ) {
        return "Yes";
      } else if (ranking) {
        return "Incomplete";
      } else {
        return "No";
      }
    },
    selectToggle() {
      if (this.selected.length < this.user().collections.length) {
        this.selected = this.user().collections.map(c => {
          return { id: c.id, name: c.name };
        });
      } else {
        this.selected = [];
      }
    }
  },
  computed: {
    selectAll() {
      if (this.selected.length < this.user().collections.length) {
        return "Select All";
      } else {
        return "Deselect All";
      }
    }
  }
};
</script>
<style scoped>
.Yes {
  color: green !important;
}
.No {
  color: red !important;
}
.Incomplete {
  color: yellow !important;
}
</style>
