using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace E_Library.DAL
{
    public partial class ELibraryEntities : DbContext
    {
        public ELibraryEntities(string connectionstring) : base(connectionstring)
        {
        }
    }
}