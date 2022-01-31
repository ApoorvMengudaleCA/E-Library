import React, { useState, useContext, useEffect, createContext } from "react";
import { toast } from "react-toastify";
import { commonContext } from "../App";
import axios from "axios";
import { Outlet, useNavigate } from "react-router-dom";
import Navbar from "../NavBar";
import { confirmAlert } from "react-confirm-alert";
import "react-confirm-alert/src/react-confirm-alert.css";
import Loader from "../Loader.js";

export const toggleContext = createContext();
export const roleDataContext = createContext();

const ManageRoles = () => {
  const { getRolesData, roles, localUser } = useContext(commonContext);
  const [toggle, setToggle] = useState(false);
  const [roleData, setRoleData] = useState({});
  const navigate = useNavigate();

  useEffect(() => {
    getRolesData();
  }, [toggle]);

  // useEffect(() => {
  //   getRolesData();
  // }, [roles]);

  const handleAdd = () => {
    setToggle(true);
    navigate("/ManageRoles/Create");
  };
  const handleView = (id) => {
    loadRole(id);
    setToggle(true);
    navigate(`/ManageRoles/View/${id}`);
  };

  const handleEdit = (id) => {
    loadRole(id);
    setToggle(true);
    navigate(`/ManageRoles/Edit/${id}`);
  };

  const handleDelete = (id) => {
    confirmAlert({
      title: "Confirm to delete",
      message: "Are you sure you want to delete?",
      buttons: [
        {
          label: "Yes",
          onClick: () => {
            axios
              .get(
                `http://e-library.somee.com/Roles/Delete?Id=${id}&UserId=${localUser.UserId}`
              )
              .then((res) => {
                if (res.data === true) {
                  getRolesData();
                  toast.success("Role deleted successfully!");
                  navigate("/ManageRoles");
                } else {
                  toast.error("Failed to delete role!");
                }
              });
          },
        },
        {
          label: "No",
        },
      ],
      closeOnEscape: true,
      closeOnClickOutside: true,
    });
  };

  const loadRole = async (id) => {
    const res = await axios.get(
      `https://e-library.somee.com/Roles/GetDataById?ID=${id}`
    );
    setRoleData(res.data);
  };

  const toggleContextValues = {
    toggle,
    setToggle,
  };

  const roleDataContextValues = {
    roleData,
    setRoleData,
  };

  return (
    <toggleContext.Provider value={toggleContextValues}>
      <roleDataContext.Provider value={roleDataContextValues}>
        <Navbar />
        {toggle && <Outlet />}
        {!toggle && (
          <div className="container">
            <div className="py-4">
              <div>
                <h1 style={{ float: "left" }}>Roles</h1>
                <button
                  style={{ float: "right" }}
                  onClick={handleAdd}
                  className="btn btn-dark  mt-2 mr-2"
                >
                  Add Role
                </button>
              </div>
              <table className="table border shadow">
                <thead className="thead-dark">
                  <tr>
                    <th scope="col">#</th>
                    <th scope="col">Name</th>
                    <th scope="col">Level</th>
                    <th scope="col">Type</th>
                    <th>Actions</th>
                  </tr>
                </thead>
                <tbody>
                  {roles.map((role, index) => (
                    <tr key={role.RoleId}>
                      <th scope="row">{index + 1}</th>
                      <td>{role.RoleName}</td>
                      <td>{role.RoleLevel}</td>
                      <td>{role.RoleType}</td>
                      <td className="">
                        <button
                          onClick={() => handleView(role.RoleId)}
                          className="btn btn-outline-dark"
                        >
                          View
                        </button>
                      </td>
                      <td>
                        <button
                          onClick={() => handleEdit(role.RoleId)}
                          className="btn btn-outline-dark"
                        >
                          Edit
                        </button>
                      </td>
                      <td>
                        <button
                          onClick={() => handleDelete(role.RoleId)}
                          className="btn btn-outline-dark"
                        >
                          Delete
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        )}
      </roleDataContext.Provider>
    </toggleContext.Provider>
  );
};

export default ManageRoles;
