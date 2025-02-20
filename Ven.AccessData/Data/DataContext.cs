using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Ven.Shared.Entities;

namespace Ven.AccessData.Data;

//datacontext hereda de dbcontext(viene del nuget) para tener sus funciones en la base de datos 
//DataContext hereda de DbContext → Esto hace que DataContext tenga las funciones para conectarse y trabajar con la base de datos
public class DataContext : DbContext
{
    //El constructor base(options) conecta DataContext con la base de datos → Permite que DataContext sepa qué base de datos usar.
    public DataContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Country>Countries=>Set<Country>();

    //base.OnModelCreating(modelBuilder) 
    //mantiene las configuraciones de DbContext → Así no se pierden reglas importantes de la base de datos.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //validacion de los modelos
        modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();    


        //para evitar el borrado de cascada
        DisableCascadingDelete(modelBuilder);
    }

    private void DisableCascadingDelete(ModelBuilder modelBuilder)
    {
        var relationShips = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var item in relationShips)
        {
            item.DeleteBehavior=DeleteBehavior.Restrict;
        }
    }
}
