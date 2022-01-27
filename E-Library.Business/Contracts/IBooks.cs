using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Business.Contracts
{
    public interface IBooks
    {
        List<Entities.Books> GetAll();
        Entities.Books GetData(int ID);
        int Save(Entities.Books books);
        bool Delete(int ID, int UserId);
    }
}
