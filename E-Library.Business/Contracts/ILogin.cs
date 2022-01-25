using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Business.Contracts
{
    public interface ILogin
    {
        Entities.Users Authenticate_User(string UserName, string UserPassword);
    }
}
