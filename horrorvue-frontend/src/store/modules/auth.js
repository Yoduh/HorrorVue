//example imgur login response
//http://localhost:8080/oauth2/callback#access_token=e355fe58e2acf5e86758e17e08a29e0a076e43ec&expires_in=315360000&token_type=bearer&refresh_token=c1d68b5256447d5f32f90ca17272a4f562fbced6&account_username=Yoduh88&account_id=33329788

const state = {
    token: window.localStorage.getItem('google_token') ,
    user: null
};

const getters = {
    isLoggedIn: state => !!state.user,
    user: state => state.user
};

const actions = {
    login: ({ commit }, user) => {
        commit('setUser', user);
    },

    finalizeLogin: ({ commit }, hash) => {
        console.log('hash', hash);
        // const query = qs.parse(hash.replace('#', ''));
        commit('setToken', null);//query.access_token);
        // window.localStorage.setItem('google_token', query.access_token);
    },

    logout: ({ commit }) => { 
        commit('setToken', null);
        window.localStorage.removeItem('google_token');
    }
};

const mutations = {
    setToken: (state, token) => {
        state.token = token;
    },
    setUser: (state, user) => {
        state.user = user;
    }
};

export default {
    state,
    getters,
    actions,
    mutations
};