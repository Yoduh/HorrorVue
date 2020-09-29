const state = {
  lastSearched: "",
  searchResults: null,
  userCollections: null
};

const getters = {
  searchResults: state => state.searchResults,
  lastSearched: state => state.lastSearched,
  userCollections: state => state.userCollections
};

const actions = {
  setSearchResults: ({ commit }, results) => {
    commit("setSearchResults", results);
  },
  setLastSearched: ({ commit }, term) => {
    commit("setLastSearched", term);
  },
  setUserCollections: ({ commit }, collections) => {
    commit("setUserCollections", collections);
  }
};

const mutations = {
  setSearchResults: (state, results) => {
    state.searchResults = results;
  },
  setLastSearched: (state, term) => {
    state.lastSearched = term;
  },
  setUserCollections: (state, collections) => {
    state.userCollections = collections;
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
