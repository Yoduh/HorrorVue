/* eslint-disable no-undef */
import axios from "axios";

const http = axios.create({
  baseURL: "https://api.themoviedb.org"
});
export default {
  fetchMovies(search) {
    let results = null;
    return http
      .get(
        `/3/search/movie?api_key=${process.env.API_KEY}&language=en-US&query=${search}&page=1&include_adult=false`
      )
      .then(res => {
        if (res.status == 200) {
          results = res.data.results.map(result => {
            // "id" is already an auto generated key in our database so we have to rename the id on the returned object
            const tempId = result.id;
            delete result.id;
            return { ...result, tmdId: tempId };
          });
          return results;
        }
      });
  }
};
