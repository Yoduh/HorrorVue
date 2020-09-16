const state = {
    lastSearched: "",
    searchResults: null
};

const getters = {
    searchResults: state => state.searchResults,
    lastSearched: state=> state.lastSearched
};

const actions = {
    setSearchResults: ({ commit }, results) => {
        console.log('setresults', results);
        commit('setSearchResults', results);
    },
    setLastSearched: ({ commit }, term) => {
        commit('setLastSearched', term);
    }
};

const mutations = {
    setSearchResults: (state, results) => {
        console.log('mutator', results);
        state.searchResults = results;
    },
    setLastSearched: (state, term) => {
        state.lastSearched = term;
    }
};

export default {
    state,
    getters,
    actions,
    mutations
};
