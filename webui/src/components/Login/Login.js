import axios from "axios";
import React, { useContext, useState, useEffect } from "react";
import "../Styles/Login.css";
import { commonContext } from "../App";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.min.css";
import useLoader from "../Hooks/useLoader";

export default function Login() {
  const { getCurrentUserData, headers, setAuth } = useContext(commonContext);
  const [inputtext, setinputtext] = useState({
    UserName: "",
    UserPassword: "",
  });
  const navigate = useNavigate();

  const [loader, showLoader, hideLoader] = useLoader();

  const [warnusername, setwarnusername] = useState(false);
  const [warnpassword, setwarnpassword] = useState(false);

  const [eye, seteye] = useState(true);
  const [password, setpassword] = useState("password");
  const [type, settype] = useState(false);

  const inputEvent = (event) => {
    const name = event.target.name;
    const value = event.target.value;
    setinputtext((lastValue) => {
      return {
        ...lastValue,
        [name]: value,
      };
    });
  };

  const submitForm = async (e) => {
    showLoader();
    e.preventDefault();
    setwarnusername(false);
    setwarnpassword(false);
    if (inputtext.UserName === "" || inputtext.UserPassword === "") {
      if (inputtext.UserName === "") {
        setwarnusername(true);
      }
      if (inputtext.UserPassword === "") {
        setwarnpassword(true);
      }
    } else {
      await axios
        .post(
          "http://e-library.somee.com/Login/Authenticate_User",
          JSON.stringify(inputtext),
          { headers: headers }
        )
        .then((res) => {
          if (res.data === "Record Not Found") {
            hideLoader();
            setAuth(false);
            navigate("/Login");
            toast.error("Login failed, Please try again!");
          } else if (res.data.UserId > 0) {
            getCurrentUserData(res.data.UserId);
            setAuth(true);
            hideLoader();
            toast.success("Logged in successfully!");
            navigate("/");
          } else {
            hideLoader();
            setAuth(false);
            navigate("/Login");
            toast.error("Login failed, Please try again!");
          }
        })
        .catch((res) => {
          setAuth(false);
          hideLoader();
          navigate("/Login");
          toast.danger("Login Failed!");
        });
    }
  };

  const Eye = () => {
    if (password === "password") {
      setpassword("text");
      seteye(false);
      settype(true);
    } else {
      setpassword("password");
      seteye(true);
      settype(false);
    }
  };

  return (
    <>
      {loader}
      <div className="container-custom">
        <div className="card">
          <div className="title">
            <h3>E - Library</h3>
          </div>
          <form onSubmit={submitForm}>
            <div className="input-text">
              <input
                type="text"
                className={`${warnusername ? "warning" : ""}`}
                placeholder="Enter your username"
                value={inputtext.UserName}
                onChange={inputEvent}
                name="UserName"
              />
              <i className="fa fa-envelope"></i>
            </div>
            <div className="input-text">
              <input
                type={password}
                className={`${warnpassword ? "warning" : ""} ${
                  type ? "type_password" : ""
                }`}
                placeholder="Enter your password"
                value={inputtext.UserPassword}
                onChange={inputEvent}
                name="UserPassword"
              />
              <i className="fa fa-lock"></i>
              <i
                onClick={Eye}
                className={`fa ${eye ? "fa-eye-slash" : "fa-eye"}`}
              ></i>
            </div>
            <div className="buttons">
              <button className="submitBtn" type="submit">
                Sign in
              </button>
            </div>
            <div className="forgot">
              <p>
                Forgot your password? <a href="#">Reset Password</a>
              </p>
            </div>
          </form>
        </div>
      </div>
    </>
  );
}
