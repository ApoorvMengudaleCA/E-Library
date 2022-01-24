using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Business.Contracts
{
    public interface IRoles
    {
        List<Entities.Roles> GetAll();
        Entities.Roles GetData(int ID);
        int Save(Entities.Roles roles);
        bool Delete(int ID, int UserId);
    }
}
