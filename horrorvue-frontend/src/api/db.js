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
  async deleteCollection(id) {
    return axios.delete(`${ROOT_URL}/api/collection/${id}`);
  },
  async updateCollection(movies, id) {
    return axios.patch(`${ROOT_URL}/api/collection/${id}`, movies).then(res => {
      if (res.data.isSuccess) {
        store.dispatch("updateCollectionMovies", res.data.data.movies);
        return res.data.data;
      } else {
        console.log("save error");
        console.log(res.data.message);
        return null;
      }
    });
  },
  // userId: google id
  getUser(userId) {
    return axios
      .get(`${ROOT_URL}/api/user/${userId}`)
      .then(res => {
        return res.data;
      })
      .catch(err => {
        if (err.response === undefined) {
          return null;
        } else if (err.response.status === 404) {
          return undefined;
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
  createRanking(collection, movies) {
    const ranking = {
      userId: store.getters.user.id,
      collectionId: collection.id,
      order: movies.map(m => m.id)
    };
    return axios.post(`${ROOT_URL}/api/ranking`, ranking);
  },
  updateRanking(ranking, movies) {
    ranking = { ...ranking, order: movies.map(m => m.id) };
    return axios.patch(`${ROOT_URL}/api/ranking/${ranking.id}`, ranking);
  },
  sendInvite(userId, emails, collectionIds) {
    const reqs = [];
    emails.forEach(email => {
      collectionIds.forEach(collectionId => {
        reqs.push({
          FromUserId: userId,
          ToUserEmail: email,
          CollectionId: collectionId
        });
      });
    });
    return axios.post(`${ROOT_URL}/api/invite/`, reqs).then(res => {
      if (res.data.isSuccess) {
        return res.data;
      } else {
        console.log("error", res.data.message);
        return res.data;
      }
    });
  },
  rejectInvite(inviteId) {
    return axios.delete(`${ROOT_URL}/api/invite/${inviteId}`).then(res => {
      if (res.data.isSuccess) {
        return true;
      } else {
        console.log("error", res.data.message);
        return false;
      }
    });
  },
  acceptInvite(invite) {
    return axios
      .patch(
        `${ROOT_URL}/api/collection/${invite.collectionId}/user/${invite.toUserId}`,
        invite
      )
      .then(res => {
        if (res.data.isSuccess) {
          return res.data.data;
        } else {
          console.log("error", res.data.message);
          return false;
        }
      });
  }
};
