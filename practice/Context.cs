using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace practice
{
    public class Context : DbContext
    {
        public Context()
            : base("practiceEntities")
        { }

        public DbSet<users> users { get; set; }
    }
}
