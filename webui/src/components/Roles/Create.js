import axios from "axios";
import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { toggleContext } from "./index.js";
import { commonContext } from "../App.js";
import { toast } from "react-toastify";
import useLoader from "../Hooks/useLoader.js";

const Create = () => {
  const [formValues, setformValues] = useState({
    RoleName: "",
    RoleLevel: "",
    RoleType: "",
  });
  const [errors, setErrors] = useState({});
  const [isSubmit, setIsSubmit] = useState(false);
  const [loader, showLoader, hideLoader] = useLoader();

  const { setToggle } = useContext(toggleContext);
  const { headers, localUser, getRolesData } = useContext(commonContext);

  const navigate = useNavigate();

  const inputEvent = (event) => {
    const name = event.target.name;
    const value = event.target.value;
    setformValues((lastValue) => {
      return {
        ...lastValue,
        [name]: value,
      };
    });
  };

  useEffect(() => {
    if (Object.keys(errors).length === 0 && isSubmit) {
      formValues.isDeleted = 0;
      formValues.CreatedBy = localUser.UserId;
      formValues.RoleType = formValues.RoleType.toUpperCase();
      showLoader();
      axios
        .post(
          "http://e-library.somee.com/Roles/Save",
          JSON.stringify(formValues),
          { headers: headers }
        )
        .then((res) => {
          if (res.data > 0) {
            getRolesData();
            setformValues({ RoleName: "", RoleLevel: "", RoleType: "" });
            toast.success("Role saved successfully!");
            setToggle(false);
            hideLoader();
            navigate("/ManageRoles");
          } else {
            hideLoader();
            navigate("/ManageRoles/Create");
            toast.error("Failed to save role!");
          }
        })
        .catch((res) => {
          navigate("/ManageRoles/Create");
          toast.error("Failed to save roles!");
        });
    }
  }, [errors]);

  const handleBack = () => {
    setToggle(false);
    navigate("/ManageRoles");
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setErrors(validate(formValues));
    setIsSubmit(true);
  };

  const validate = (values) => {
    const errors = {};
    if (!values.RoleName && values.RoleName === "") {
      errors.RoleName = "Role name cannot be empty!";
    }
    if (!values.RoleLevel && values.RoleLevel === "") {
      errors.RoleLevel = "Role level cannot be empty!";
    }
    if (!values.RoleType && values.RoleType === "") {
      errors.RoleType = "Role type cannot be empty!";
    } else if (values.RoleType.length > 1) {
      errors.RoleType = "Role type cannot be greater than one character!";
    }
    return errors;
  };

  return (
    <div className="container">
      {loader}
      <div className="py-5 px-5">
        <div>
          <button className="btn" onClick={handleBack}>
            <i className="fas fa-arrow-left"></i>&nbsp;Back
          </button>
        </div>
        <div className="container col-md-5 shadow p-3 mb-5 bg-body rounded">
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <input
                type="text"
                className="form-control"
                placeholder="Enter role name"
                onChange={inputEvent}
                value={formValues.RoleName}
                name="RoleName"
              />
            </div>
            <p className="text-danger">{errors.RoleName}</p>
            <div className="mb-3">
              <input
                type="text"
                className="form-control"
                placeholder="Enter role level"
                onChange={inputEvent}
                value={formValues.RoleLevel}
                name="RoleLevel"
              />
            </div>
            <p className="text-danger">{errors.RoleLevel}</p>
            <div className="mb-3">
              <input
                type="text"
                className="form-control"
                placeholder="Enter role Type"
                onChange={inputEvent}
                value={formValues.RoleType.toUpperCase()}
                name="RoleType"
              />
            </div>
            <p className="text-danger">{errors.RoleType}</p>
            <div className="form-group mb-3 text-center">
              <button className="btn btn-outline-dark">Save Role</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default Create;
