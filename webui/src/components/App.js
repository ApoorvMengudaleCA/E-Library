import React, { useState, useEffect, createContext } from "react";
import Login from "../components/Login/Login.js";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import "./Styles/App.css";
import "bootstrap/dist/css/bootstrap.css";
import { ToastContainer, Slide, toast } from "react-toastify";
import ManageRoles from "./Roles/index.js";
import Home from "./Home/index.js";
import Create from "./Roles/Create.js";
import View from "./Roles/View.js";
import Edit from "./Roles/Edit.js";

export const headers = {
  "Content-type": "application/json",
};

export const commonContext = createContext();
export default function App() {
  const [roles, setRoles] = useState([]);
  const [localUser, setLocalUser] = useState({});
  const [auth, setAuth] = useState(null);

  useEffect(() => {
    getRolesData();
    let localauth = sessionStorage.getItem("auth");
    localauth && JSON.parse(localauth) ? setAuth(true) : setAuth(false);
    let localuser = sessionStorage.getItem("localUser");
    localuser && setLocalUser(JSON.parse(localuser));
  }, []);

  useEffect(() => {
    sessionStorage.setItem("auth", auth);
    if (auth === false) {
      sessionStorage.clear();
    } else {
      sessionStorage.setItem("localUser", JSON.stringify(localUser));
    }
  }, [auth]);

  async function getRolesData() {
    await fetch("https://e-library.somee.com/Roles/GetAll", {})
      .then((res) => res.json())
      .then((result) => {
        setRoles(result);
      })
      .catch((error) => {
        toast.error("Unable to load roles data!");
      });
  }

  async function getCurrentUserData(id) {
    await fetch(`https://e-library.somee.com/Users/GetDataById?ID=${id}`, {})
      .then((res) => res.json())
      .then((result) => {
        sessionStorage.setItem("localUser", JSON.stringify(result));
        setLocalUser(result);
      })
      .catch((error) => {
        toast.error("Unable to load roles data!");
      });
  }

  const commonContextValues = {
    roles,
    setRoles,
    getRolesData,
    localUser,
    setLocalUser,
    getCurrentUserData,
    headers,
    auth,
    setAuth,
  };

  return (
    <>
      <ToastContainer
        autoClose={2000}
        closeOnClick={true}
        draggable={true}
        pauseOnFocusLoss={true}
        pauseOnHover={true}
        newestOnTop={true}
        rtl={false}
        transition={Slide}
        theme="light"
        position="top-right"
      />
      <Router>
        <commonContext.Provider value={commonContextValues}>
          <Routes>
            {!auth && <Route path="/Login" element={<Login />} />}

            {auth && (
              <>
                <Route path="/" exact element={<Home />} />
                <Route path="ManageRoles" exact element={<ManageRoles />}>
                  <Route path="Create" exact element={<Create />} />
                  <Route path="View/:id" exact element={<View />} />
                  <Route path="Edit/:id" exact element={<Edit />} />
                </Route>
              </>
            )}

            <Route path="*" element={<Navigate to={auth ? "/" : "/Login"} />} />
          </Routes>
        </commonContext.Provider>
      </Router>
    </>
  );
}
