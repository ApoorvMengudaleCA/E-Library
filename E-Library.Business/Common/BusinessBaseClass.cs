using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Library.Entities;

namespace E_Library.Business.Common
{
    public class BusinessBaseClass
    {
        internal ApplicationAPIKeys SecurityKeys;

        public BusinessBaseClass(ApplicationAPIKeys securityKeys)
        {
            SecurityKeys = securityKeys;
        }
    }
}