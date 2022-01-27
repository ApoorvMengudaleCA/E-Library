using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Library.Entities
{
    public class BookAuthorMapping
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}