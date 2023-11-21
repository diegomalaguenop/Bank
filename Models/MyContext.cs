using Microsoft.EntityFrameworkCore;

namespace Bank.Models
{
    public class MyContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
    }
}
