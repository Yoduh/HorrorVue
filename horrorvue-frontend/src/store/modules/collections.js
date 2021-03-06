const state = {
  collections: null,
  selectedCollection: null,
  tempRanking: null,
  tempMovies: [],
  rankOrderMovies: null
};

const getters = {
  collections: state => state.collections,
  collectionById: state => id => {
    return state.collections.find(c => c.id === id);
  },
  selectedCollection: state => state.selectedCollection,
  tempRanking: state => state.tempRanking,
  tempMovies: state => state.tempMovies
};

const actions = {
  setCollections: ({ commit }, collections) => {
    commit("setCollections", collections);
  },
  addCollection: ({ commit }, collection) => {
    commit("addCollection", collection);
  },
  removeCollection: ({ commit }, id) => {
    const index = state.collections.findIndex(c => c.id === id);
    commit("removeCollection", index);
    commit("setSelectedCollection", null);
  },
  selectCollectionById: ({ commit }, id) => {
    if (id === -1) {
      commit("setSelectedCollection", null);
      return;
    }
    const coll = state.collections.find(c => c.id === id);
    commit("setSelectedCollection", coll);
    commit("setRankings", coll.rankings, { root: true });
  },
  updateCollectionRanking: ({ commit, dispatch }, ranking) => {
    const collection = { ...state.selectedCollection };
    let idx = collection.rankings.findIndex(r => r.id === ranking.id);
    if (idx === -1) {
      collection.rankings.push(ranking);
    } else {
      collection.rankings[idx] = { ...ranking };
    }
    idx = state.collections.findIndex(c => c.id === state.selectedCollection);
    commit("updateCollections", collection, idx);
    commit("setRankings", collection.rankings, { root: true });
    dispatch("selectCollectionById", collection.id);
  },
  updateCollectionMovies: ({ commit }, args) => {
    const movies = args[0];
    const name = args[1];
    commit("setMovies", movies);
    if (state.selectedCollection.name !== name) {
      commit("setName", name);
    }
  },
  sortSelected: ({ commit }, order) => {
    // arrays sort in-place! sort using a copy to not mutate
    const sortedMovies = [...state.selectedCollection.movies].sort(function(
      a,
      b
    ) {
      return order.indexOf(a.id) - order.indexOf(b.id);
    });
    commit("setMovies", sortedMovies);
  },
  setTempRanking: ({ commit }, ranking) => {
    commit("setTempRanking", ranking);
  },
  resetTempRanking: ({ commit }) => {
    const newRanks = JSON.parse(JSON.stringify(state.rankOrderMovies));
    commit("setTempRanking", newRanks);
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
    sorted.forEach((m, idx) => (m.rating = userRanking.ratings[idx]));
    commit("setRankOrderMovies", sorted);
  },
  setTempMovies: ({ commit }, movies) => {
    commit("setTempMovies", movies);
  },
  addToTempMovies: ({ commit }, movie) => {
    if (state.tempMovies && state.tempMovies.length > 0)
      commit("setTempMovies", [...state.tempMovies, movie]);
    else commit("setTempMovies", [movie]);
  },
  removeFromTempMovies: ({ commit }, movie) => {
    commit(
      "setTempMovies",
      state.tempMovies.filter(m => m.tmdId !== movie.tmdId)
    );
  },
  resetTempMovies: ({ commit }) => {
    commit("setTempMovies", null);
  }
};

const mutations = {
  setCollections: (state, collections) => {
    state.collections = collections;
  },
  setSelectedCollection: (state, collection) => {
    state.selectedCollection = collection;
  },
  setMovies: (state, movies) => {
    state.selectedCollection.movies = movies;
  },
  setName: (state, name) => {
    state.selectedCollection.name = name;
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
  },
  removeCollection: (state, index) => {
    state.collections.splice(index, 1);
  },
  setTempMovies: (state, movies) => {
    state.tempMovies = movies;
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
