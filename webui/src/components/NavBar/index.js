import React, { useContext, useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import "../Styles/NavBar.css";
import Dropdown from "./DropDown";
import { commonContext } from "../App.js";
import { toast } from "react-toastify";

function Navbar() {
  const { setAuth, setLocalUser, localUser } = useContext(commonContext);

  const [click, setClick] = useState(false);
  const [dropdown, setDropdown] = useState(false);

  const navigate = useNavigate();

  const handleClick = () => setClick(!click);
  const closeMobileMenu = () => setClick(false);
  const handleLogout = () => {
    setAuth(false);
    window.sessionStorage.clear();
    navigate("/Login");
    setLocalUser({});
    toast.info("Logged out!");
  };

  const onMouseEnter = () => {
    if (window.innerWidth < 960) {
      setDropdown(false);
    } else {
      setDropdown(true);
    }
  };

  const onMouseLeave = () => {
    if (window.innerWidth < 960) {
      setDropdown(false);
    } else {
      setDropdown(false);
    }
  };

  return (
    <>
      <nav className="navbar-custom">
        <Link to="/" className="navbar-logo-custom" onClick={closeMobileMenu}>
          E-Library
        </Link>
        <div className="menu-icon" onClick={handleClick}>
          <i className={click ? "fas fa-times" : "fas fa-bars"} />
        </div>
        <ul className={click ? "nav-menu-custom active" : "nav-menu-custom"}>
          <li
            className="nav-item-custom"
            onMouseEnter={onMouseEnter}
            onMouseLeave={onMouseLeave}
          >
            <ul className="nav-item-mid">
              <li className="nav-links-custom" onClick={closeMobileMenu}>
                Management <i className="fas fa-caret-down" />
              </li>
              {dropdown && <Dropdown />}
            </ul>
          </li>
          <li className="nav-item-custom">
            <Link
              to="/Books"
              className="nav-links-custom"
              onClick={closeMobileMenu}
            >
              Books
            </Link>
          </li>
          <li className="nav-item-custom">
            <Link
              to="/Authors"
              className="nav-links-custom"
              onClick={closeMobileMenu}
            >
              Authors
            </Link>
          </li>
        </ul>
        <ul className="nav-menu-user">
          {/* {localUser.DisplayName === undefined ? (
            handleLogout()
          ) : (
            <div className="nav-item-user" style={{ color: "white" }}>
              Hi&nbsp;{localUser.DisplayName}!
            </div>
          )} */}
          <div className="nav-item-user" style={{ color: "white" }}>
            Hi&nbsp;{localUser.DisplayName}!
          </div>
          <li className="nav-item-user">
            <button className="nav-link-button" onClick={handleLogout}>
              Logout &nbsp;<i className="fas fa-sign-out-alt"></i>
            </button>
          </li>
        </ul>
      </nav>
    </>
  );
}

export default Navbar;
