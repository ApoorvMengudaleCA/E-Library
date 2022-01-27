using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Business.Contracts
{
    public interface IAuthors
    {
        List<Entities.Authors> GetAll();
        Entities.Authors GetData(int ID);
        int Save(Entities.Authors authors);
        bool Delete(int ID, int UserId);
    }
}
