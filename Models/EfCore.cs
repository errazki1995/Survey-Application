using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace EsisaSurvey.Models
{
    public class EfCore : DbContext
    {
        public EfCore()
        {
        }

        public EfCore(DbContextOptions<EfCore> options)
          : base(options)
        { }
        public DbSet<Client> Client { get; set; }
        public DbSet<Commercant> Commercant { get; set; }
        public DbSet<PostSurveyResultModel> SurveyResult { get; set; }
        public DbSet<ChangeSurveyModel> Survey { get; set; }
        public DbSet<SurveyResultLog> SurveyResulLog { get; set; }
        public DbSet<Survey> SurveyStore { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=survay2");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // modelBuilder.Entity<PostSurveyResultModel>().Property(x=>x.id).valueGenera

            modelBuilder.Entity<PostSurveyResultModel>()
       .Property(p => p.id)
       .ValueGeneratedOnAdd();
            modelBuilder.Entity<SurveyResultLog>()
       .Property(p => p.id)
       .ValueGeneratedOnAdd();
            

        }
    }
}
