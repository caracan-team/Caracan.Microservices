using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Configuration
{
    public class DbOptions
    {
        public string Connection { get; set; }
        public DbType Type { get; set; }
    }

    public enum DbType
    {
        Mysql,
        Postgresql,
        Mssql
    }
}
