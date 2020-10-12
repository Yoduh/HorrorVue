<template>
  <div>
    <v-row align="end">
      <div class="text-h4">My collections</div>
      <v-btn class="ml-auto" @click="selectToggle">{{ selectAll }}</v-btn>
    </v-row>
    <v-row>
      <v-icon color="primary">
        mdi-account-multiple
      </v-icon>
      = subscribed to collection
    </v-row>
    <v-row>
      <v-col cols="12" class="mt-3 pa-0">
        <v-list dark>
          <template v-for="(collection, index) in collections">
            <v-divider v-if="index != 0" :key="`divider-${index}`"></v-divider>
            <v-list-item two-line :key="collection.id">
              <v-list-item-content>
                <v-list-item-title class="text-h6">
                  {{ collection.name }}
                  <v-icon v-if="collection.useGroupIcon" color="primary">
                    mdi-account-multiple
                  </v-icon>
                </v-list-item-title>
                <v-list-item-subtitle>
                  <v-row>
                    <v-col cols="12" md="2" class="py-1">
                      Ranked:
                      <span :class="isRanked(index, collection.id)">
                        {{ isRanked(index, collection.id) }}
                      </span>
                    </v-col>
                    <v-col cols="12" md="3" class="py-1">
                      Created by:
                      <span class="white--text">
                        {{ createdBy(collection) }}
                      </span>
                    </v-col>
                    <v-col cols="12" md="2" class="d-flex align-start py-1">
                      Subscribers:
                      <span class="ml-1 white--text">
                        {{ collection.appUsers.length - 1 }}
                      </span>
                      <subscribers-modal
                        v-if="collection.appUsers.length > 1"
                        :appUsers="collection.appUsers"
                        :user="user()"
                      />
                    </v-col>
                  </v-row>
                </v-list-item-subtitle>
              </v-list-item-content>
              <v-list-item-action>
                <v-checkbox
                  v-model="selected"
                  :value="collection"
                  :disabled="collection.createdBy !== user().id"
                ></v-checkbox>
              </v-list-item-action>
            </v-list-item>
          </template>
        </v-list>
      </v-col>
    </v-row>
    <v-row class="mt-3">
      <v-btn class="ml-auto" @click.stop="openModal">
        <v-icon class="mr-2">mdi-email-outline</v-icon> Send Invites
      </v-btn>
      <email-modal
        :dialog="dialog"
        @close="dialog = false"
        :collections="selected"
      ></email-modal>
    </v-row>
  </div>
</template>

<script>
import EmailModal from "@/components/modals/EmailModal";
import SubscribersModal from "@/components/modals/SubscribersModal";

export default {
  name: "ProfileCollections",
  components: {
    EmailModal,
    SubscribersModal
  },
  props: ["collections", "user"],
  data() {
    return {
      dialog: false,
      selected: [],
      createdBySelf: []
    };
  },
  methods: {
    isRanked(index, id) {
      const ranking = this.user().collections[index].rankings.find(
        r => r.collectionId === id && r.userId === this.user().id
      );
      if (!ranking) return "No";

      this.user().collections[index].movies.forEach(m => {
        if (!ranking.order.includes(m.id)) return "Incomplete";
      });
      return "Yes";
    },
    createdBy(collection) {
      if (collection.createdBy === this.user().id) {
        return "You";
      } else {
        this.$set(collection, "useGroupIcon", true);
        const user = collection.appUsers.find(
          u => u.id === collection.createdBy
        );
        return `${user.firstName} ${user.lastName}`;
      }
    },
    selectToggle() {
      if (this.selected.length < this.createdBySelf.length) {
        this.selected = this.createdBySelf;
      } else {
        this.selected = [];
      }
    },
    openModal() {
      if (this.selected.length > 0) {
        this.dialog = true;
      } else {
        this.$eventBus.$emit(
          "open-snackbar",
          "Select at least one collection first!",
          "error"
        );
      }
    }
  },
  computed: {
    selectAll() {
      if (this.selected.length < this.createdBySelf.length) {
        return "Select All";
      } else {
        return "Deselect All";
      }
    }
  },
  created() {
    this.createdBySelf = this.user().collections.filter(
      c => c.createdBy === this.user().id
    );
  }
};
</script>

<style scoped>
.Yes {
  color: #28a745 !important;
}
.No {
  color: #dc3545 !important;
}
.Incomplete {
  color: #ffc107 !important;
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
