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
    public class AuthorsService : BusinessBaseClass, IAuthors
    {
        private readonly ELibraryEntities _context;
        public AuthorsService(ApplicationAPIKeys keys) : base(keys)
        {
            _context = new ELibraryEntities(SecurityKeys.DatabaseConnectionString);
        }

        public List<Entities.Authors> GetAll()
        {
            try
            {
                List<Entities.Authors> AuthorList = new List<Entities.Authors>();

                using (_context)
                {
                    AuthorList = _context.Authors.Where(x => x.IsDeleted == false).Select(x => new Entities.Authors
                    {
                        AuthorId = x.AuthorId,
                        AuthorName = x.AuthorName,
                        Email = x.Email,
                        ContactNumber = x.ContactNumber,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate
                    }).ToList();

                    return AuthorList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Entities.Authors GetData(int ID)
        {
            try
            {
                Entities.Authors edAuthors = new Entities.Authors();
                using (_context)
                {
                    var daAuthors = _context.Authors
                        .Where(x => x.AuthorId == ID && x.IsDeleted == false)
                        .SingleOrDefault();

                    if (daAuthors != null)
                    {
                        edAuthors.AuthorId = daAuthors.AuthorId;
                        edAuthors.AuthorName = daAuthors.AuthorName;
                        edAuthors.Email = daAuthors.Email;
                        edAuthors.ContactNumber = daAuthors.ContactNumber;
                        edAuthors.CreatedBy = daAuthors.CreatedBy;
                        edAuthors.CreatedDate = daAuthors.CreatedDate;
                        edAuthors.UpdatedBy = daAuthors.UpdatedBy;
                        edAuthors.UpdatedDate = daAuthors.UpdatedDate;
                    }
                    else
                    {
                        throw new Exception("Record Not Found");
                    }

                    return edAuthors;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Save(Entities.Authors Authors)
        {
            int result = 0;
            try
            {
                using (_context)
                {
                    int userID;
                    try
                    {
                        if (Authors.AuthorId > 0)
                        {
                            var col = _context.Authors.Where(x => x.AuthorId == Authors.AuthorId && x.IsDeleted == false).FirstOrDefault();
                            if (col != null)
                            {
                                DAL.Author model = new DAL.Author();
                                col.AuthorName = Authors.AuthorName;
                                col.Email = Authors.Email;
                                col.ContactNumber = Authors.ContactNumber;
                                col.UpdatedDate = DateTime.UtcNow;
                                col.UpdatedBy = Authors.UpdatedBy;
                                userID = Convert.ToInt32(col.UpdatedBy);
                                _context.SaveChanges();
                                result = col.AuthorId;
                            }
                            else
                                throw new Exception("Record Not Found");
                        }
                        else
                        {
                            bool test = _context.Authors.Where(x => x.IsDeleted == false).Any(x => x.AuthorName.ToUpper().Equals(Authors.AuthorName.ToUpper()));
                            if (!test)
                            {
                                DAL.Author model = new DAL.Author();
                                model.AuthorName = Authors.AuthorName;
                                model.Email = Authors.Email;
                                model.ContactNumber = Authors.ContactNumber;
                                model.CreatedDate = DateTime.UtcNow;
                                model.CreatedBy = Authors.CreatedBy;
                                _context.Authors.Add(model);
                                userID = Convert.ToInt32(model.CreatedBy);
                                _context.SaveChanges();
                                result = model.AuthorId;
                            }
                            else
                            {
                                throw new Exception("Record already exists for " + Authors.AuthorId);
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
                    var col = _context.Authors.Where(x => x.AuthorId == ID).FirstOrDefault();
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