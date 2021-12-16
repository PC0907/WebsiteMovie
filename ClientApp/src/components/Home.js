import React from 'react';
import ReactDOM from 'react-dom';
import { useState, useEffect } from "react";

function Home() {
  const [item, SearchData] = useState([]);
  const [cryptos, setCryptos] = useState([]);
  const [origCryptosCount, setOrigCryptosCount] = useState([]);

  useEffect(() => {
    fetchItems();
  }, []);

  const fetchItems = async () => {
    const url = "weatherforecast";
    const response = await fetch(url);
    const info = await response.json();
    console.log(info)
    setCryptos(info); 
    setOrigCryptosCount(info);
  };
  const Search = key => {
    const newResults = origCryptosCount.filter(crypto => crypto.summary.includes(key));
    console.log('newResults', newResults);
    setCryptos(newResults);
  };

  const cryptoJsx = cryptos.map(forecast => (
    <div className="card">  
              <img className="card-img-top img-thumbnail" src="" alt="Card image cap" />

              <div className="card-body">
                <h5 className="card-title">{forecast.date}</h5>
                <p className="card-text">
                 {forecast.summary}
                </p>
                <p className="card-text">
                  <small class="text-muted">Last updated 3 mins ago</small>
                </p>
              </div>
            </div>
  ));

  return (
    <div>
      Search:
      <input type="text" onChange={event => Search(event.target.value)} />
      <div className="card my-3">
      {cryptoJsx}
      </div>
    </div>
  );
}

export default Home;
