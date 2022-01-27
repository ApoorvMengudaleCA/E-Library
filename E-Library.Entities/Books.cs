using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Library.Entities
{
    public class Books
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string ISBN { get; set; }
        public string BookImage { get; set; }
        public string Description { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}