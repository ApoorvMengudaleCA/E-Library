using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Library.Entities
{
    public class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string DisplayName { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? RoleId { get; set; }        
    }

}