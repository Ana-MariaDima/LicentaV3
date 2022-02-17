using Laborator54522021.Models;
using Licenta.Models;
using Licenta.Models.Relations.Many_to_Many;
using Licenta.Models.Relations.One_to_Many;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Data
{
    public class Context : DbContext
    {
        public DbSet<DataBaseModel> DataBaseModels { get; set; }


       // public DbSet<Bucatari> Bucatar{ get; set; }
        public DbSet<Retete> Reteta { get; set; }

        public DbSet<SubCategoriiIngrediente>  SubCategorieIngredient { get; set; }
        public DbSet<CategoriiIngrediente> CategorieIngredient { get; set; }

        public DbSet<CategoriiRetete> CategorieReteta { get; set; }

        public DbSet<Ingrediente> Ingredient { get; set; }
        public DbSet<Unitati> Unitate { get; set; }

        public DbSet<RetetaIngrediente> RetetaIngredient { get; set; }
        public DbSet<User> Users { get; set; }
        public bool Ingrediente { get; internal set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // One to Many
            //-Bucatari-Retete
            /* builder.Entity<Bucatari>()
                     .HasMany(m1 => m1.Retete)
                     .WithOne(m2 => m2.Bucatar);

             builder.Entity<Retete>()
                 .HasOne(m2 => m2.Bucatar)
                 .WithMany(m1 => m1.Retete);
            */
           //Uniq Constaints 
            builder.Entity<CategoriiRetete>()
                .HasIndex(u => u.Nume_Categorie_Retete)
                .IsUnique();

            builder.Entity<SubCategoriiIngrediente>()
                .HasIndex(u => u.Nume_Subcategoriie_ingredient)
                .IsUnique();

            builder.Entity<Ingrediente>()
             .HasIndex(u => u.Nume_ingredient)
             .IsUnique();

            builder.Entity<Retete>()
                .HasIndex(u => u.Nume_reteta)
                .IsUnique();

            builder.Entity<Unitati>()
                .HasIndex(u => u.Nume_unitate)
                .IsUnique();

            //-CategorieRetete- Retete
            builder.Entity<CategoriiRetete>()
             .HasMany(m1 => m1.Retete)
             .WithOne(m2 => m2.CategorieReteta);

            builder.Entity<Retete>()
                .HasOne(m2 => m2.CategorieReteta)
                .WithMany(m1 => m1.Retete);

            //-TipuriRetete- Retete
            builder.Entity<TipuriRetete>()
             .HasMany(m1 => m1.Retete)
             .WithOne(m2 => m2.TipReteta);

            builder.Entity<Retete>()
                .HasOne(m2 => m2.TipReteta)
                .WithMany(m1 => m1.Retete);

            //-Pahare- Retete
            builder.Entity<Pahare>()
             .HasMany(m1 => m1.Retete)
             .WithOne(m2 => m2.Pahar);

            builder.Entity<Retete>()
                .HasOne(m2 => m2.Pahar)
                .WithMany(m1 => m1.Retete);

            //SubCategorieIngrediente-Ingrediente
            builder.Entity<SubCategoriiIngrediente>()
            .HasMany(m1 => m1.Ingrediente)
            .WithOne(m2 => m2.SubCategorieIngredient);

            builder.Entity<Ingrediente>()
                .HasOne(m2 => m2.SubCategorieIngredient)
                .WithMany(m1 => m1.Ingrediente);
            //CategorieIngrediente-SubCategorieIngrediente


            builder.Entity<CategoriiIngrediente>()
            .HasMany(m1 => m1.SubCategoriiIngrediente)
            .WithOne(m2 => m2.CategorieIngredient);

            builder.Entity<SubCategoriiIngrediente>()
                .HasOne(m2 => m2.CategorieIngredient)
                .WithMany(m1 => m1.SubCategoriiIngrediente);


            //Unitati-Ingrediente


            // Many to Many

            builder.Entity<RetetaIngrediente>().HasKey(mr => new { mr.IdReteta, mr.IdIngredient });

            builder.Entity<RetetaIngrediente>()
                   .HasOne<Ingrediente>(mr => mr.Ingredient)
                   .WithMany(m3 => m3.RetetaIngredient)
                   .HasForeignKey(mr => mr.IdIngredient);



            builder.Entity<RetetaIngrediente>()
                   .HasOne<Retete>(mr => mr.Reteta)
                   .WithMany(m3 => m3.RetetaIngredient)
                   .HasForeignKey(mr => mr.IdReteta);


            base.OnModelCreating(builder);
        }
    }
}
