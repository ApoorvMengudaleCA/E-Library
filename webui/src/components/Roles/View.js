import React, { useContext, useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { toggleContext, roleDataContext } from "./index.js";
import { toast } from "react-toastify";
import axios from "axios";

const View = () => {
  const navigate = useNavigate();
  const { setToggle } = useContext(toggleContext);
  const { roleData, setRoleData } = useContext(roleDataContext);
  const handleBack = () => {
    setToggle(false);
    setRoleData({});
    navigate("/ManageRoles");
  };

  return (
    <div className="container">
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
