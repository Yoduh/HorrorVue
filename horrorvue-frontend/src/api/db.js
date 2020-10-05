import axios from "axios";
import store from "@/store";

const ROOT_URL = "https://localhost:5001";
export default {
  newCollection(collection, name) {
    const request = {
      id: 0,
      CreatedBy: store.getters.user.id,
      Name: name,
      Movies: [...collection]
    };
    return axios.post(`${ROOT_URL}/api/collection`, request).then(res => {
      if (res.data.isSuccess) {
        return res.data.data;
      } else {
        console.log("save error");
        console.log(res.data.message);
        return null;
      }
    });
  },
  deleteCollection(id) {
    return axios.delete(`${ROOT_URL}/api/collection/${id}`);
  },
  // userId: google id
  async getUser(userId) {
    return axios
      .get(`${ROOT_URL}/api/user/${userId}`)
      .then(res => {
        if (res.data.collections && res.data.collections.length > 0) {
          this.getRankingsForCollections(res.data.collections.map(c => c.id));
        }
        return res.data;
      })
      .catch(err => {
        if (err.response === undefined) {
          return null;
        } else if (err.response.status === 404) {
          return this.createUser(store.getters.user);
        } else {
          console.log("err", err.response);
        }
      });
  },
  createUser(user) {
    const div = user.sub.indexOf("|");
    const googleId = user.sub.slice(div + 1);
    const newUser = {
      FirstName: user.given_name,
      LastName: user.family_name,
      GoogleId: googleId,
      Email: user.email
    };
    return axios
      .post(`${ROOT_URL}/api/user`, newUser)
      .then(res => {
        return res.data.data;
      })
      .catch(err => {
        console.log("create err", err);
        return null;
      });
  },
  async getRankingsForCollections(collIds) {
    axios.get(`${ROOT_URL}/api/ranking/collections`, {
      params: { collections: collIds }
    });
  },
  createRanking(collection, movies) {
    const ranking = {
      userId: store.getters.user.user.id,
      collectionId: collection.id,
      order: movies.map(m => m.id)
    };
    return axios.post(`${ROOT_URL}/api/ranking`, ranking);
  },
  updateRanking(ranking, movies) {
    ranking = { ...ranking, order: movies.map(m => m.id) };
    return axios.patch(`${ROOT_URL}/api/ranking/${ranking.id}`, ranking);
  }
};
