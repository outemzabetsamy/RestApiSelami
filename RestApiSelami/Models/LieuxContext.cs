
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiSelami.Models
{
    public class LieuxContext : DbContext
    {
        
        public LieuxContext(DbContextOptions<LieuxContext> options) : base(options)
        {

        }
        public DbSet<Lieux> Lieuxs { get; set; }

        internal Lieux SingleOrDefault(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
