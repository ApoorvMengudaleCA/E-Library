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
    public class BooksService : BusinessBaseClass, IBooks
    {
        private readonly ELibraryEntities _context;
        public BooksService(ApplicationAPIKeys keys) : base(keys)
        {
            _context = new ELibraryEntities(SecurityKeys.DatabaseConnectionString);
        }

        public List<Entities.Books> GetAll()
        {
            try
            {
                List<Entities.Books> BookList = new List<Entities.Books>();

                using (_context)
                {
                    BookList = _context.Books.Where(x => x.IsDeleted == false).Select(x => new Entities.Books
                    {
                        BookId = x.BookId,
                        BookImage = x.BookImage,
                        BookName = x.BookName,
                        ISBN = x.ISBN,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate
                    }).ToList();

                    return BookList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Entities.Books GetData(int ID)
        {
            try
            {
                Entities.Books edBooks = new Entities.Books();
                using (_context)
                {
                    var daBooks = _context.Books
                        .Where(x => x.BookId == ID && x.IsDeleted == false)
                        .SingleOrDefault();

                    if (daBooks != null)
                    {
                        edBooks.BookId = daBooks.BookId;
                        edBooks.BookName = daBooks.BookName;
                        edBooks.ISBN = daBooks.ISBN;
                        edBooks.BookImage = daBooks.BookImage;
                        edBooks.Description = daBooks.Description;
                        edBooks.CreatedBy = daBooks.CreatedBy;
                        edBooks.CreatedDate = daBooks.CreatedDate;
                        edBooks.UpdatedBy = daBooks.UpdatedBy;
                        edBooks.UpdatedDate = daBooks.UpdatedDate;
                    }
                    else
                    {
                        throw new Exception("Record Not Found");
                    }

                    return edBooks;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Save(Entities.Books Books)
        {
            int result = 0;
            try
            {
                using (_context)
                {
                    int userID;
                    try
                    {
                        if (Books.BookId > 0)
                        {
                            var col = _context.Books.Where(x => x.BookId == Books.BookId && x.IsDeleted == false).FirstOrDefault();
                            if (col != null)
                            {
                                DAL.Book model = new DAL.Book();
                                col.BookName = Books.BookName;
                                col.BookImage = Books.BookImage;
                                col.ISBN = Books.ISBN;
                                col.Description = Books.Description;
                                col.UpdatedDate = DateTime.UtcNow;
                                col.UpdatedBy = Books.UpdatedBy;
                                userID = Convert.ToInt32(col.UpdatedBy);
                                _context.SaveChanges();
                                result = col.BookId;
                            }
                            else
                                throw new Exception("Record Not Found");
                        }
                        else
                        {
                            bool test = _context.Books.Where(x => x.IsDeleted == false).Any(x => x.BookName.ToUpper().Equals(Books.BookName.ToUpper()));
                            if (!test)
                            {
                                DAL.Book model = new DAL.Book();
                                model.BookName = Books.BookName;
                                model.BookImage = Books.BookImage;
                                model.ISBN = Books.ISBN;
                                model.Description = Books.Description;
                                model.CreatedDate = DateTime.UtcNow;
                                model.CreatedBy = Books.CreatedBy;
                                _context.Books.Add(model);
                                userID = Convert.ToInt32(model.CreatedBy);
                                _context.SaveChanges();
                                result = model.BookId;
                            }
                            else
                            {
                                throw new Exception("Record already exists for " + Books.BookName);
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
                    var col = _context.Books.Where(x => x.BookId == ID).FirstOrDefault();
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