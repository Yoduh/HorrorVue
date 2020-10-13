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
      return res.data;
    });
  },
  async deleteCollection(id) {
    return axios.delete(`${ROOT_URL}/api/collection/${id}`);
  },
  async unsubCollection(collectionId, userId) {
    return axios.delete(
      `${ROOT_URL}/api/collection/${collectionId}/user/${userId}`
    );
  },
  async updateCollection(movies, id, name) {
    const req = {
      id: id,
      Name: name,
      Movies: [...movies]
    };
    return axios.patch(`${ROOT_URL}/api/collection/${id}`, req).then(res => {
      return res.data;
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
        console.log("error on create", err);
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
      return res.data;
    });
  },
  rejectInvite(inviteId) {
    return axios.delete(`${ROOT_URL}/api/invite/${inviteId}`).then(res => {
      return res.data;
    });
  },
  acceptInvite(invite) {
    return axios
      .patch(
        `${ROOT_URL}/api/collection/${invite.collectionId}/user/${invite.toUserId}`,
        invite
      )
      .then(res => {
        res.data;
      });
  }
};
