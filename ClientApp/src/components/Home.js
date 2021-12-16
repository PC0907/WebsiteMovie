import React from "react";
import ReactDOM from "react-dom";
import { useState, useEffect } from "react";

function Home() {
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
    setMovies(info["movies"]);
    setOrigMovieCount(info["movies"]);
  };
  const Search = (key) => {
    const newResults = origMovieCount.filter((movie) =>
      movie.title.includes(key)
    );
    console.log("newResults", newResults);
    setMovies(newResults);
  };

  const movieJsx = movies.map((movie) => (
    <div className="card" >
    <img class="bd-placeholder-img card-img-top" width="100%" height="180" src={movie.posterUrl} />
      <div className="card-body">
        <h5 className="card-title">{movie.title}</h5>
        <p className="card-text">{movie.plot}</p>
        <p className="card-text">
          <small class="text-muted">{movie.year}</small>
        </p>
      </div>
    </div>
  ));

  return (
    <div>

      <form class="form-inline my-2 my-lg-0">
      Search <input class="form-control mr-sm-2" type="text" onChange={(event) => Search(event.target.value)} placeholder="Search" aria-label="Search" />
    </form>
      <div className="card mb-3 my-3">{movieJsx}</div>
    </div>
  );
}

export default Home;
