import React, { useContext } from "react";
import { commonContext } from "../App";
import NavBar from "../NavBar/index.js";

const Home = () => {
  const { localUser } = useContext(commonContext);
  return (
    <>
      <NavBar />
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "90vh",
        }}
      >
        <h1>Home</h1>
      </div>
    </>
  );
};

export default Home;
