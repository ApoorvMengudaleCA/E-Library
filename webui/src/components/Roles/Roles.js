import React, { useContext, useState } from "react";
import { RolesContext } from "../App";
import MaterialTable from "material-table";
import axios from "axios";

const headers = {
  "Content-type": "application/json",
};
var columns = [
  { title: "Id", field: "RoleId", hidden: true },
  {
    title: "Name",
    field: "RoleName",
    validate: (rowData) =>
      rowData.RoleName === undefined || rowData.RoleName === ""
        ? "Required"
        : true,
  },
  { title: "Level", field: "RoleLevel" },
  { title: "Type", field: "RoleType", hidden: true },
  { title: "Created By", field: "CreatedBy" },
  {
    title: "Created Date",
    field: "CreatedDate",
    type: "date",
    dateSetting: { locale: "en-US" },
  },
  { title: "Updated By", field: "UpdatedBy" },
  {
    title: "Updated Date",
    field: "UpdatedDate",
    type: "date",
    dateSetting: { locale: "en-US" },
  },
];

var today = new Date(),
  time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds(),
  date =
    today.getFullYear() + "/" + (today.getMonth() + 1) + "/" + today.getDate();

export default function Roles() {
  const { roles, setRoles, getRolesData } = useContext(RolesContext);
  const [iserror, setIserror] = useState(false);
  const [errorMessages, setErrorMessages] = useState([]);

  const handleRowAdd = (newData, resolve) => {
    let errorList = [];
    if (newData.RoleName === undefined) {
      errorList.push("Please enter role name.");
    }
    if (newData.RoleLevel === undefined) {
      errorList.push("Please enter role level.");
    }
    if (errorList.length < 1) {
      newData.CreatedDate = date + " " + time;
      newData.IsDeleted = false;
      axios
        .post(
          "https://e-library.somee.com/Roles/Save",
          JSON.stringify(newData),
          { headers: headers }
        )
        .then((res) => {
          if (res.request.response > 0) {
            alert("Role Saved Successfully!");
            getRolesData();
            resolve();
          } else {
            alert("Failed to save!");
          }
        });
    } else {
      setErrorMessages(errorList);
      setIserror(true);
      resolve();
    }
  };

  const handleRowUpdate = (newData, oldData, resolve) => {
    let errorList = [];
    if (newData.RoleName === undefined) {
      errorList.push("Please enter role name.");
    }
    if (newData.Level === undefined) {
      errorList.push("Please enter role level.");
    }
    if (newData.IsDeleted === undefined) {
      errorList.push("Please set IsDeleted.");
    }
    if (errorList.length < 1) {
      fetch
        .patch("/users/" + newData.id, newData)
        .then((res) => {
          const dataUpdate = [...roles];
          const index = oldData.tableData.id;
          dataUpdate[index] = newData;
          //setData([...dataUpdate]);
          resolve();
          setIserror(false);
          setErrorMessages([]);
        })
        .catch((error) => {
          setErrorMessages(["Update failed! Server error"]);
          setIserror(true);
          resolve();
        });
    } else {
      setErrorMessages(errorList);
      setIserror(true);
      resolve();
    }
  };

  const handleRowDelete = (oldData, resolve) => {
    oldData.UserId = 1;
    axios
      .get(
        `https://e-library.somee.com/Roles/Delete?Id=${oldData.RoleId}&UserId=${oldData.UserId}`,
        JSON.stringify(oldData.RoleId, 1),
        { headers: headers }
      )
      .then((res) => {
        if (res.request.response === "true") {
          alert("Role Deleted Successfully!");
          getRolesData();
          resolve();
        } else {
          alert("Failed to delete!");
        }
      })
      .catch((error) => {
        setErrorMessages(["Delete failed! Server error"]);
        setIserror(true);
        resolve();
      });
  };

  return (
    <div>
      {
        <MaterialTable
          title="Role Details"
          columns={columns}
          data={roles}
          options={{
            actionsColumnIndex: -1,
          }}
          editable={{
            onRowUpdate: (newData, oldData) =>
              new Promise((resolve) => {
                handleRowUpdate(newData, oldData, resolve);
              }),
            onRowAdd: (newData) =>
              new Promise((resolve) => {
                handleRowAdd(newData, resolve);
              }),
            onRowDelete: (oldData) =>
              new Promise((resolve) => {
                handleRowDelete(oldData, resolve);
              }),
          }}
        />
      }
    </div>
  );
}
