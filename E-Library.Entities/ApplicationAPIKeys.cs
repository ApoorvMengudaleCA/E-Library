﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Library.Entities
{
    public class ApplicationAPIKeys
    {
        public static ApplicationAPIKeys AppKeys = new ApplicationAPIKeys();

        public string DatabaseConnectionString { get; set; }
        public string LogDatabaseConnectionString { get; set; }
        public string webapibaseurl { get; set; }
    }
}