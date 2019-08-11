using MicroServiceBase.Lib.AutoMigrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Contexts
{
    public class AutoMigration : IAutoMigration
    {
        private readonly FileContext _context;
        public AutoMigration(FileContext context)
        {
            _context = context;
        }
        public void Migrate()
        {
            _context.Database.EnsureCreated();
        }
    }
}
