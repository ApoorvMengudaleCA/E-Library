using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Library.Business.Common;
using E_Library.Business.Contracts;
using E_Library.DAL;
using E_Library.Entities;

namespace E_Library.Business.Services
{
    public class UsersService: BusinessBaseClass, IUsers
    {
        private readonly ELibraryEntities _context;
        public UsersService(ApplicationAPIKeys keys) : base(keys)
        {
            _context = new ELibraryEntities(SecurityKeys.DatabaseConnectionString);
        }

        public List<Entities.Users> GetAll()
        {
            try
            {
                List<Entities.Users> UserList = new List<Entities.Users>();

                using (_context)
                {
                    UserList = _context.Users.Where(x => x.IsDeleted == false).Select(x => new Entities.Users
                    {
                        UserId = x.UserId,
                        UserName = x.UserName,
                        DisplayName = x.DisplayName,
                        First_Name = x.First_Name,
                        Last_Name = x.Last_Name,
                        Email = x.Email,
                        ContactNo = x.ContactNo,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                        RoleId = x.RoleId        
                    }).ToList();

                    return UserList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Entities.Users GetData(int ID)
        {
            try
            {
                Entities.Users edUsers = new Entities.Users();
                using (_context)
                {
                    var daUsers = _context.Users
                        .Where(x => x.UserId == ID && x.IsDeleted == false)
                        .SingleOrDefault();

                    if (daUsers != null)
                    {
                        edUsers.UserId = daUsers.UserId;
                        edUsers.UserName = daUsers.UserName;
                        edUsers.DisplayName = daUsers.DisplayName;
                        edUsers.First_Name = daUsers.First_Name;
                        edUsers.Last_Name = daUsers.Last_Name;
                        edUsers.Email = daUsers.Email;
                        edUsers.ContactNo = daUsers.ContactNo;
                        edUsers.CreatedBy = daUsers.CreatedBy;
                        edUsers.CreatedDate = daUsers.CreatedDate;
                        edUsers.UpdatedBy = daUsers.UpdatedBy;
                        edUsers.UpdatedDate = daUsers.UpdatedDate;
                        edUsers.RoleId = daUsers.RoleId;
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

        public int Save(Entities.Users Users)
        {
            int result = 0;
            try
            {
                using (_context)
                {
                    int userID;
                    try
                    {
                        if (Users.UserId > 0)
                        {
                            var col = _context.Users.Where(x => x.UserId == Users.UserId && x.IsDeleted == false).FirstOrDefault();
                            if (col != null)
                            {
                                DAL.User model = new DAL.User();
                                col.UserName = Users.UserName;
                                col.DisplayName = Users.DisplayName;
                                col.First_Name = Users.First_Name;
                                col.Last_Name = Users.Last_Name;
                                col.Email = Users.Email;
                                col.ContactNo = Users.ContactNo;
                                col.UpdatedDate = DateTime.UtcNow;
                                col.UpdatedBy = Users.UpdatedBy;
                                userID = Convert.ToInt32(col.UpdatedBy);
                                col.RoleId = Users.RoleId;
                                _context.SaveChanges();
                                result = col.UserId;
                            }
                            else
                                throw new Exception("Record Not Found");
                        }
                        else
                        {
                            bool test = _context.Users.Where(x => x.IsDeleted == false).Any(x => x.UserName.ToUpper().Equals(Users.UserName.ToUpper()));
                            if (!test)
                            {
                                DAL.User model = new DAL.User();
                                model.UserName = Users.UserName;
                                model.DisplayName = Users.DisplayName;
                                model.First_Name = Users.First_Name;
                                model.Last_Name = Users.Last_Name;
                                model.Email = Users.Email;
                                model.ContactNo = Users.ContactNo;
                                model.CreatedDate = DateTime.UtcNow;
                                model.CreatedBy = Users.CreatedBy;
                                model.RoleId = Users.RoleId;
                                _context.Users.Add(model);
                                userID = Convert.ToInt32(model.CreatedBy);
                                _context.SaveChanges();
                                result = model.UserId;
                            }
                            else
                            {
                                throw new Exception("Record already exists for " + Users.UserName);
                            }
                        }
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int ID, int UserId)
        {
            bool success = false;
            try
            {
                using (_context)
                {
                    int userID;
                    var col = _context.Users.Where(x => x.UserId == ID).FirstOrDefault();
                    if (col != null)
                    {
                        col.IsDeleted = true;
                        col.UpdatedBy = UserId;
                        col.UpdatedDate = DateTime.UtcNow;
                        userID = Convert.ToInt32(col.UpdatedBy);
                        _context.SaveChanges();
                    }
                    success = true;
                    return success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}