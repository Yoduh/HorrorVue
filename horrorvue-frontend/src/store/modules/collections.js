const state = {
  collections: null,
  selectedCollection: null,
  tempRanking: null,
  rankOrderMovies: null
};

const getters = {
  collections: state => state.collections,
  collectionById: state => id => {
    return state.collections.find(c => c.id === id);
  },
  selectedCollection: state => state.selectedCollection,
  tempRanking: state => state.tempRanking
};

const actions = {
  setCollections: ({ commit }, collections) => {
    commit("setCollections", collections);
  },
  addCollection: ({ commit }, collection) => {
    commit("addCollection", collection);
  },
  selectCollectionById: ({ commit, dispatch }, id) => {
    commit(
      "setSelectedCollection",
      state.collections.find(c => c.id === id)
    );
    dispatch("setRankOrderMovies");
    dispatch("setTempRanking", state.rankOrderMovies);
  },
  updateCollectionRanking: ({ commit, dispatch }, ranking) => {
    const collection = { ...state.selectedCollection };
    let idx = collection.rankings.findIndex(r => r.id === ranking.id);
    collection.rankings[idx] = ranking;
    idx = state.collections.findIndex(c => c.id === state.selectedCollection);
    commit("updateCollections", collection, idx);
    dispatch("selectCollectionById", collection.id);
  },
  sortSelected: ({ commit }, order) => {
    // arrays sort in-place! sort using a copy to not mutate
    const sortedMovies = [...state.selectedCollection.movies].sort(function(
      a,
      b
    ) {
      return order.indexOf(a.id) - order.indexOf(b.id);
    });
    commit("setSort", sortedMovies);
  },
  setTempRanking: ({ commit }, ranking) => {
    commit("setTempRanking", ranking);
  },
  resetTempRanking: ({ commit }) => {
    commit("setTempRanking", state.rankOrderMovies);
  },
  setRankOrderMovies: ({ commit, rootState }) => {
    const userRanking = state.selectedCollection.rankings.find(
      r => r.userId === rootState.user.user.id
    );
    if (userRanking === undefined) {
      // no ranking, sort by default
      commit("setRankOrderMovies", state.selectedCollection.movies);
      return;
    }
    const sorted = [...state.selectedCollection.movies].sort(function(a, b) {
      return userRanking.order.indexOf(a.id) - userRanking.order.indexOf(b.id);
    });
    commit("setRankOrderMovies", sorted);
  }
};

const mutations = {
  setCollections: (state, collections) => {
    state.collections = collections;
  },
  setSelectedCollection: (state, collection) => {
    state.selectedCollection = collection;
  },
  setSort: (state, movies) => {
    state.selectedCollection.movies = movies;
  },
  setTempRanking: (state, ranking) => {
    state.tempRanking = ranking;
  },
  setRankOrderMovies: (state, movies) => {
    state.rankOrderMovies = movies;
  },
  updateCollections: (state, collection, idx) => {
    state.collections[idx] = collection;
  },
  addCollection: (state, collection) => {
    state.collections.push(collection);
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
