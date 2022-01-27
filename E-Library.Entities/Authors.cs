using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Library.Entities
{
    public class Authors
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}