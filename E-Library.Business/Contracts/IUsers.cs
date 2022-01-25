using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Business.Contracts
{
    public interface IUsers
    {
        List<Entities.Users> GetAll();
        Entities.Users GetData(int ID);
        int Save(Entities.Users users);
        bool Delete(int ID, int UserId);
    }
}
