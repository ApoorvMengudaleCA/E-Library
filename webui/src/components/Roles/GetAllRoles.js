import React, { useContext} from "react";
import {RolesContext} from '../App'

export default function GetAllRoles() {
  const rolesData = useContext(RolesContext)
  console.log(rolesData)
  return (
    <div>
      <h2>Roles Data</h2>
      <table>
        <thead>
          <tr>
            <th>Role Id</th>
            <th>Role Name</th>
            <th>Role Level</th>
          </tr> 
        </thead>
        <tbody>
        {rolesData.roles.map((item)=> (
            <tr key={item.RoleId}>
              <td>{item.RoleId}</td>
              <td>{item.RoleName}</td>
              <td>{item.RoleLevel}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
