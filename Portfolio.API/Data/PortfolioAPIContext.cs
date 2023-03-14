using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.API.Data
{
    public class PortfolioAPIContext : DbContext
    {
        public PortfolioAPIContext (DbContextOptions<PortfolioAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
