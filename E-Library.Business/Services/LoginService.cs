using E_Library.Business.Common;
using E_Library.Business.Contracts;
using E_Library.DAL;
using E_Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Library.Business.Services
{
    public class LoginService : BusinessBaseClass, ILogin
    {
        private readonly ELibraryEntities _context;
        public LoginService(ApplicationAPIKeys keys) : base(keys)
        {
            _context = new ELibraryEntities(SecurityKeys.DatabaseConnectionString);
        }
        public Entities.Users Authenticate_User(Entities.Users users)
        {
            try
            {
                Entities.Users edUsers = new Entities.Users();
                using (_context)
                {
                    var col = _context.Users.Where(x => x.UserName == users.UserName && x.UserPassword == users.UserPassword && x.IsDeleted == false).FirstOrDefault();
                    if (col != null)
                    {
                        edUsers.UserId = col.UserId;
                    }
                    else
                    {
                        throw new Exception("Record Not Found");
                    }                  
                    
                    return edUsers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}