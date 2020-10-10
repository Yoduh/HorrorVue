<template>
  <v-container v-if="user()">
    <!-- user info -->
    <v-row>
      <v-form>
        <v-row><div class="text-h3">My Profile</div></v-row>
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
    <v-row v-if="user().invites.length > 0">
      <div class="text-h4">You've been invited to rank another collection!</div>
      <v-list dense class="mb-5" dark>
        <template v-for="(invite, index) in user().invites">
          <v-divider v-if="index != 0" :key="`divider-${index}`"></v-divider>
          <v-list-item two-line class="pr-0" :key="invite.id">
            <v-list-item-content>
              <v-list-item-title>
                {{ invite.collection.name }}
              </v-list-item-title>
              <v-list-item-subtitle>
                Invited by: {{ invite.fromUser.firstName }}
                {{ invite.fromUser.lastName }}
              </v-list-item-subtitle>
            </v-list-item-content>
            <v-list-item-action class="flex-row">
              <v-btn class="ml-3" color="success" @click="subscribe(invite)"
                >Accept</v-btn
              >
              <v-btn class="mx-3" color="error" @click="reject(invite)"
                >Reject</v-btn
              >
            </v-list-item-action>
          </v-list-item>
        </template>
      </v-list>
    </v-row>
    <!-- user collections -->
    <v-row align="end">
      <div class="text-h4">Your collections</div>
      <v-btn class="ml-auto" @click="selectToggle">{{ selectAll }}</v-btn>
    </v-row>
    <v-row>
      <v-icon color="primary">
        mdi-account-multiple
      </v-icon>
      = collection you're subscribed to
    </v-row>
    <v-row>
      <v-col cols="12" class="mt-3 pa-0">
        <v-list dark>
          <template v-for="(collection, index) in collections">
            <v-divider v-if="index != 0" :key="`divider-${index}`"></v-divider>
            <v-list-item three-line :key="collection.id">
              <v-list-item-content>
                <v-list-item-title>
                  <v-icon v-if="collection.useGroupIcon" color="primary">
                    mdi-account-multiple
                  </v-icon>
                  {{ collection.name }}
                </v-list-item-title>
                <v-list-item-subtitle>
                  Ranked:
                  <span :class="isRanked(index, collection.id)">{{
                    isRanked(index, collection.id)
                  }}</span>
                </v-list-item-subtitle>
                <v-list-item-subtitle>
                  Created by {{ createdBy(collection) }}
                </v-list-item-subtitle>
              </v-list-item-content>
              <v-list-item-action>
                <v-checkbox v-model="selected" :value="collection"></v-checkbox>
              </v-list-item-action>
            </v-list-item>
          </template>
        </v-list>
      </v-col>
    </v-row>
    <v-row class="mt-3">
      <v-btn class="ml-auto" @click.stop="dialog = true">
        <v-icon class="mr-2">mdi-email-outline</v-icon> Send Invites
      </v-btn>
      <email-modal
        :dialog="dialog"
        @close="dialog = false"
        :collections="selected"
      ></email-modal>
    </v-row>
  </v-container>
</template>

<script>
import EmailModal from "@/components/modals/EmailModal";
import db from "@/api/db";
import { mapGetters, mapActions } from "vuex";
import eventBus from "@/eventBus";

export default {
  name: "Profile",
  components: {
    EmailModal
  },
  data() {
    return {
      selected: [],
      dialog: false,
      collections: [],
      isLoading: true,
      isNotLoading: false
    };
  },
  methods: {
    ...mapGetters(["user"]),
    ...mapActions([
      "removeInvite",
      "addCollection",
      "removeInvite",
      "toggleSnackbar"
    ]),
    isRanked(index, id) {
      const ranking = this.user().collections[index].rankings.find(
        r => r.collectionId === id && r.userId === this.user().id
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
        this.selected = this.collectionIdNames;
      } else {
        this.selected = [];
      }
    },
    createdBy(collection) {
      if (collection.createdBy === this.user().id) {
        return "you!";
      } else {
        this.$set(collection, "useGroupIcon", true);
        const user = collection.appUsers.find(
          u => u.id === collection.createdBy
        );
        return `${user.firstName} ${user.lastName}`;
      }
    },
    async subscribe(invite) {
      // returns collection
      const res = await db.acceptInvite(invite);
      if (res) {
        this.addCollection(res);
        this.removeInvite(invite.id);
      }
    },
    async reject(invite) {
      const res = await db.rejectInvite(invite.id);
      if (res) {
        this.removeInvite(invite.id);
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
  },
  updated() {
    if (this.user() && this.isLoading) {
      this.collections = this.user().collections;
      this.isLoading = false;
    }
  },
  created() {
    if (this.user() && this.isLoading) {
      this.collections = this.user().collections;
      this.isLoading = false;
    }
    eventBus.$emit("open-snackbar", "from Profile!");
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
.v-list {
  width: 100%;
  background-color: rgb(51, 57, 65);
}
.v-list-item__title {
  color: white;
}
.v-list-item__subtitle {
  color: grey !important;
}
</style>
