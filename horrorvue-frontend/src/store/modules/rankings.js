const state = {
  collectionRankings: null,
  userRankings: null
};

const getters = {
  collectionRankings: state => state.collectionRankings,
  userRankings: state => state.userRankings
};

const actions = {
  setCollectionRankings: ({ commit, dispatch }, rankings) => {
    commit("setCollectionRankings", rankings);
    dispatch("setUserRankings", rankings);
  },
  setUserRankings: ({ commit, rootState }, rankings) => {
    const userRankings = rankings.filter(ranking => {
      return ranking.userId === rootState.user.user.id;
    });
    commit("setUserRankings", userRankings);
  }
};

const mutations = {
  setCollectionRankings: (state, rankings) => {
    state.collectionRankings = rankings;
  },
  setUserRankings: (state, rankings) => {
    state.userRankings = rankings;
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
