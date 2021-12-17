import React from "react";
import ReactDOM from "react-dom";
import { useState, useEffect } from "react";
import 'bootstrap/dist/css/bootstrap.css';

function Profile() {
  const [item, SearchData] = useState([]);
  const [movies, setMovies] = useState([]);
  const [origMovieCount, setOrigMovieCount] = useState([]);

  useEffect(() => {
    fetchItems();
  }, []);

  const fetchItems = async () => {
    const url = "Movie.json";
    const response = await fetch(url);
    const info = await response.json();
    console.log(info["movies"]);
    var movies = info["movies"];

    var list = new Set([10, 20, 30, 50, 40, 50]);
    const newResults = movies.filter((movie) =>
      list.has(movie.id)
    );
    console.log("newResults", newResults);
    setMovies(newResults);
    setOrigMovieCount(newResults);
  };

  const Search = (key) => {
    const newResults = origMovieCount.filter((movie) =>
      movie.title.includes(key)
    );
    console.log("newResults", newResults);
    setMovies(newResults);
  };

  const movieJsx = movies.map((movie) => (

    <div class="column">
    <div class="card">
    <img class="bd-placeholder-img card-img-top" width="100%" height="180" src={movie.posterUrl} />
      <h3>{movie.title}</h3>
      <p>{movie.year}</p>
      <p>{movie.plot}</p>
    </div>
  </div>
  ));

  return (
    <div>
      <h1> My Playlist </h1>
      <form class="form-inline my-2 my-lg-0">
      Search the movie by name <input class="form-control mr-sm-2" type="text" onChange={(event) => Search(event.target.value)} placeholder="Search" aria-label="Search" />
    </form>
      <div class="row my-3">{movieJsx}</div>
    </div>
  );
}

export default Profile;
