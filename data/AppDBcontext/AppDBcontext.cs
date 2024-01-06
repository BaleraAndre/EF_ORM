using Microsoft.EntityFrameworkCore;
using EF.Models.entities;
namespace EF.AppDBcontext;


    public class AppDBcontext : DbContext
    {

        public AppDBcontext(DbContextOptions<AppDBcontext> options) :base(options){}
        
        public DbSet<Pessoa> pessoas { get; set; }
    }



