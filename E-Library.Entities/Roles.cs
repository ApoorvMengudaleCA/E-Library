using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Library.Entities
{
    public class Roles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public Nullable<int> RoleLevel { get; set; }
        public string RoleType { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime>  UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}