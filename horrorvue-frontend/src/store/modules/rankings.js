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
    console.log("setting collection rankings");
    commit("setCollectionRankings", rankings);
    dispatch("setUserRankings", rankings);
  },
  setUserRankings: ({ commit, rootState }, rankings) => {
    console.log("setting user rankings", rankings);
    const userRankings = rankings.filter(ranking => {
      return ranking.userId === rootState.user.user.id;
    });
    console.log("userRankings", userRankings);
    commit("setUserRankings", userRankings);
  },
  addOrUpdateRankings: ({ dispatch }, ranking) => {
    console.log("add or updating", ranking);
    let updated = false;
    const newRankings = state.collectionRankings.map(r => {
      if (r.id === ranking.id) {
        updated = true;
        return ranking;
      } else {
        return r;
      }
    });
    console.log("newRankings", newRankings);
    if (!updated) {
      newRankings.push(ranking);
    }
    dispatch("setCollectionRankings", newRankings);
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
