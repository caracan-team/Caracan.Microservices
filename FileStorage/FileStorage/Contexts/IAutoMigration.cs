using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Contexts
{
    public interface IAutoMigration
    {
        void Migrate();
    }
}
