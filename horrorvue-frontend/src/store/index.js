  
import Vuex from 'vuex';
import Vue from 'vue';
import user from './modules/user';
import search from './modules/search';
import collections from './modules/collections';
import rankings from './modules/rankings';

Vue.use(Vuex);

export default new Vuex.Store({
    modules: {
        user,
        search,
        collections,
        rankings
    }
});