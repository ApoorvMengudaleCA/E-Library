import axios from "axios";
import React, { useState, useEffect, createContext } from "react";
import Roles from "./Roles/Roles";

const headers = {
  "Content-type": "application/json",
  "Access-Control-Allow-Origin": "*",
};
export const RolesContext = createContext();
export default function App() {
  const [roles, setRoles] = useState([]);
  const [iserror, setIserror] = useState(false);
  const [errorMessages, setErrorMessages] = useState([]);

  function getRolesData() {
    fetch("https://e-library.somee.com/Roles/GetAll", {})
      .then((res) => res.json())
      .then((result) => {
        setRoles(result);
        console.log(result);
      })
      .catch((error) => {
        setErrorMessages(["Cannot load user data"]);
        setIserror(true);
      });
    // axios
    //   .get("https://e-library.somee.com/Roles/GetAll")
    //   .then((res) => res.json())
    //   .then((result) => {
    //     setRoles(result);
    //     console.log(result);
    //   })
    //   .catch((error) => {
    //     setErrorMessages(["Cannot load user data"]);
    //     setIserror(true);
    //   });
  }
  useEffect(() => {
    getRolesData();
  }, []);
  const roleContextValues = {
    roles,
    setRoles,
    getRolesData,
  };

  return (
    <RolesContext.Provider value={roleContextValues}>
      {/* <GetAllRoles /> */}
      <Roles />
    </RolesContext.Provider>
  );
}
