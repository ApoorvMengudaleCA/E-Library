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
        public Entities.Users Authenticate_User(string UserName, string UserPassword)
        {
            try
            {
                Entities.Users edLogin = new Entities.Users();
                using (_context)
                {
                    var daLogin = _context.Users
                        .Where(x => x.UserName == UserName && x.UserPassword == UserPassword && x.IsActive == true && x.IsDeleted == false)
                        .SingleOrDefault();
                    if (daLogin != null)
                    {
                        edLogin.UserId = daLogin.UserId;
                    }
                    else
                    {
                        throw new Exception("Record Not Found");
                    }                  
                    
                    return edLogin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}