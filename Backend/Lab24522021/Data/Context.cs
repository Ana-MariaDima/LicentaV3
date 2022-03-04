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
        public DbSet<Retete> Reteta { get; set; }
        public DbSet<SubCategoriiIngrediente> SubCategorieIngredient { get; set; }
        public DbSet<CategoriiIngrediente> CategorieIngredient { get; set; }
        public DbSet<CategoriiRetete> CategorieReteta { get; set; }
        public DbSet<TipuriRetete> TipReteta { get; set; }
        public DbSet<Ingrediente> Ingredient { get; set; }
        public DbSet<Unitati> Unitate { get; set; }
        public DbSet<RetetaIngrediente> RetetaIngredient { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Aprecieri> Apreciere { get; set; }
        public DbSet<Pahare> Pahar { get; set; }
        public bool Ingrediente { get; internal set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
         
        protected override void OnModelCreating(ModelBuilder builder)
        {

            //UNIQ CONSTRAINS

            //User
            //-username
            builder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            //-email
            builder.Entity<User>()
               .HasIndex(u => u.Email)
               .IsUnique();

            //Aprecieri - reteta+user
            builder.Entity<Aprecieri>()
               .HasIndex(u => new { u.IdReteta, u.IdUser })
               .IsUnique();

            //Unitati -Nume unitate
            builder.Entity<Unitati>()
                .HasIndex(u => u.Nume_unitate)
                .IsUnique();

            //CategorieIngrediente - nume Categorie
            builder.Entity<CategoriiIngrediente>()
               .HasIndex(u => u.Nume_categoriie_ingredient)
               .IsUnique();

            //SubCategorieIngrediente - nume SubCategorie
            builder.Entity<SubCategoriiIngrediente>()
              .HasIndex(u => u.Nume_Subcategorie_ingredient)
              .IsUnique();

            //Ingrediente - nume ingredient
            builder.Entity<Ingrediente>()
              .HasIndex(u => u.Nume_ingredient)
              .IsUnique();

            //CategoriiRetete - nume categorie reteta
            builder.Entity<CategoriiRetete>()
                .HasIndex(u => u.Nume_Categorie_Retete)
                .IsUnique();

            //TipuriRetete - nume tip reteta 
            builder.Entity<TipuriRetete>()
                .HasIndex(u => u.Nume_Tip_Retete)
                .IsUnique();

            //Pahare - nume pahar
            //CategoriiRetete - nume categorie 
            builder.Entity<Pahare>()
                .HasIndex(u => u.Nume_Pahar)
                .IsUnique();

            //Reteta -nume reteta
            builder.Entity<Retete>()
                .HasIndex(u => u.Nume_reteta)
                .IsUnique();




            //RELATIONS BETWEEN MODELS 

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
            builder.Entity<Unitati>()
                .HasMany(m1 => m1.ReteteIngrediente)
                .WithOne(m2 => m2.Unitate);

            builder.Entity<RetetaIngrediente>()
                .HasOne(m2 => m2.Unitate)
                .WithMany(m1 => m1.ReteteIngrediente);

            // Many to Many - Retete-Ingrediente 

            builder.Entity<RetetaIngrediente>().HasKey(mr => new { mr.IdReteta, mr.IdIngredient });

            builder.Entity<RetetaIngrediente>()
                   .HasOne<Ingrediente>(mr => mr.Ingredient)
                   .WithMany(m3 => m3.RetetaIngredient)
                   .HasForeignKey(mr => mr.IdIngredient);

            builder.Entity<RetetaIngrediente>()
                   .HasOne<Retete>(mr => mr.Reteta)
                   .WithMany(m3 => m3.RetetaIngredient)
                   .HasForeignKey(mr => mr.IdReteta)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Cascade); 

            // Many to Many - Retete-Useri 

            builder.Entity<Aprecieri>().HasKey(mr => new { mr.IdReteta, mr.IdUser });

            builder.Entity<Aprecieri>()
                   .HasOne<User>(mr => mr.User)
                   .WithMany(m3 => m3.Apreciere)
                   .HasForeignKey(mr => mr.IdUser);

            builder.Entity<Aprecieri>()
                   .HasOne<Retete>(mr => mr.Reteta)
                   .WithMany(m3 => m3.Apreciere)
                   .HasForeignKey(mr => mr.IdReteta)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Cascade); 



            base.OnModelCreating(builder);
        }
    }
}
