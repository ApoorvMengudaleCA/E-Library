import React, { useContext, useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { toggleContext, roleDataContext } from "./index.js";
import axios from "axios";
import useLoader from "../Hooks/useLoader.js";

const View = () => {
  const navigate = useNavigate();
  const { setToggle } = useContext(toggleContext);
  const [roleData, setRoleData] = useState({});
  const handleBack = () => {
    setToggle(false);
    setRoleData({});
    navigate("/ManageRoles");
  };

  const { id } = useParams();
  useEffect(() => {
    showLoader();
    async function fetchData() {
      await axios
        .get(`https://e-library.somee.com/Roles/GetDataById?ID=${id}`, {})
        .then((res) => {
          setRoleData(res.data);
          hideLoader();
        });
    }
    fetchData();
  }, []);
  const [loader, showLoader, hideLoader] = useLoader();
  return (
    <div className="container">
      {loader}

      <div className="py-5 px-5">
        <div>
          <button className="btn" onClick={handleBack}>
            <i className="fas fa-arrow-left"></i>&nbsp;Back
          </button>
          <div className="container col-md-5 shadow p-3 mb-5 bg-body rounded">
            <div className="mb-3">Role Id: {roleData.RoleId}</div>
            <div className="mb-3">Role Name: {roleData.RoleName}</div>
            <div className="mb-3">Role Level: {roleData.RoleLevel}</div>
            <div className="mb-3">Role Type: {roleData.RoleType}</div>
            <div className="mb-3">Role Created By: {roleData.CreatedBy}</div>
            {roleData.CreatedDate !== null && (
              <div className="mb-3">
                Role Created Date:
                {new Date(roleData.CreatedDate).toLocaleDateString("en-US")}
              </div>
            )}
            {roleData.UpdatedBy !== null && (
              <div className="mb-3">Role Updated By: {roleData.UpdatedBy}</div>
            )}
            {roleData.UpdatedDate !== null && (
              <div className="mb-3">
                Role Updated Date:
                {new Date(roleData.UpdatedDate).toLocaleDateString("en-US")}
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default View;
