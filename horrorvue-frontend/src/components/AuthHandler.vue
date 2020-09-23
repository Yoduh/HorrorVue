<template>
<div>
    <div v-if="$auth.loading">
        Please wait...
    </div>
    <div v-else>
        Loading collections...
    </div>
</div>
</template>

<script>
import db from '@/api/db'
import store from '@/store';
import { mapActions } from 'vuex';

export default {
    name: 'AuthHandler',
    methods: {
        ...mapActions(['finalizeLogin', 'setUserCollections'])
    },
    data() {
        return {
            isLoading: this.$auth.loading
        }
    },
    created() {
        
        // this.$router.push('/');
    },
    async updated() {
        if (this.$auth.isAuthenticated) {
            this.finalizeLogin(this.$auth.user);
            const user = await db.getUser(store.getters.user.id);
            this.setUserCollections(user.data.collections);
            this.$router.push('/');
        }
    }
}
</script>

<style scoped>
</style>