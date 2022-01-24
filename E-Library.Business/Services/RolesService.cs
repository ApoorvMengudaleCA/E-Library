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
    public class RolesService: BusinessBaseClass, IRoles
    {
        private readonly ELibraryEntities _context;
        public RolesService(ApplicationAPIKeys keys) : base(keys)
        {
            _context = new ELibraryEntities(SecurityKeys.DatabaseConnectionString);
        }

        public List<Entities.Roles> GetAll()
        {
            try
            {
                List<Entities.Roles> RoleList = new List<Entities.Roles>();

                using (_context)
                {
                    RoleList = _context.Roles.Where(x => x.IsDeleted == false).Select(x => new Entities.Roles
                    {
                        RoleId = x.RoleId,
                        RoleName = x.RoleName,
                        RoleLevel = x.RoleLevel,
                        RoleType = x.RoleType,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate
                    }).ToList();

                    return RoleList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Entities.Roles GetData(int ID)
        {
            try
            {
                Entities.Roles edRoles = new Entities.Roles();
                using (_context)
                {
                    var daRoles = _context.Roles
                        .Where(x => x.RoleId == ID && x.IsDeleted == false)
                        .SingleOrDefault();

                    edRoles.RoleId = daRoles.RoleId;
                    edRoles.RoleName = daRoles.RoleName;
                    edRoles.RoleLevel = daRoles.RoleLevel;
                    edRoles.RoleType = daRoles.RoleType;
                    edRoles.CreatedBy = daRoles.CreatedBy;
                    edRoles.CreatedDate = daRoles.CreatedDate;
                    edRoles.UpdatedBy = daRoles.UpdatedBy;
                    edRoles.UpdatedDate = daRoles.UpdatedDate;

                    return edRoles;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Save(Entities.Roles Roles)
        {
            int result = 0;
            try
            {
                using (_context)
                {
                    int userID;
                    try
                    {
                        if (Roles.RoleId > 0)
                        {
                            var col = _context.Roles.Where(x => x.RoleId == Roles.RoleId).FirstOrDefault();
                            if (col != null)
                            {
                                DAL.Role model = new DAL.Role();
                                col.RoleName = Roles.RoleName;
                                col.RoleLevel = Roles.RoleLevel;
                                col.RoleType = Roles.RoleType;
                                col.UpdatedDate = DateTime.UtcNow;
                                col.UpdatedBy = Roles.UpdatedBy;
                                userID = Convert.ToInt32(col.UpdatedBy);
                                _context.SaveChanges();
                                result = col.RoleId;
                            }
                            else
                                throw new Exception("Record Not Found");
                        }
                        else
                        {
                            bool test = _context.Roles.Where(x => x.IsDeleted == false).Any(x => x.RoleName.ToUpper().Equals(Roles.RoleName.ToUpper()));
                            if (!test)
                            {
                                DAL.Role model = new DAL.Role();
                                model.RoleName = Roles.RoleName;
                                model.RoleLevel = Roles.RoleLevel;
                                model.RoleType = Roles.RoleType;
                                model.CreatedDate = DateTime.UtcNow;
                                model.CreatedBy = Roles.CreatedBy;
                                _context.Roles.Add(model);
                                userID = Convert.ToInt32(model.CreatedBy);
                                _context.SaveChanges();
                                result = model.RoleId;
                            }
                            else
                            {
                                throw new Exception("Record already exists for " + Roles.RoleName);
                            }
                        }
                        _context.SaveChanges();
                        //txn.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        //txn.Rollback();
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
                    var col = _context.Roles.Where(x => x.RoleId == ID).FirstOrDefault();
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