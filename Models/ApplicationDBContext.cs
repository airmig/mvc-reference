using Microsoft.EntityFrameworkCore;
namespace MVCReferenceProject.Models;
public class ApplicationDBContext:DbContext{

    public DbSet<Customer> Customers {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        optionsBuilder.UseSqlServer("Server=localhost;Database=CodeFirstDB;User Id=sa;Password=;TrustServerCertificate=true;");
    }
}