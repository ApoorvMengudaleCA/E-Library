import React from "react";
import loaderGif from "../Resources/Loader/Loader.svg";

const Loader = () => {
  return (
    <div className="fp-container">
      <img src={loaderGif} className="fp-loader" alt="loading" />
    </div>
  );
};

export default Loader;
