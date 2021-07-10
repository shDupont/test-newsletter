using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newsletter.Models
{
    public class ClientesContext:DbContext
    {
        public ClientesContext(DbContextOptions<ClientesContext> options):base(options)
        {

        }

        public DbSet<Clientes> Clientes { get; set; }
    }
}
