using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using alimentosAPI.Models;
using alimentosAPI.Models.Enuns;
using AlimentosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;


namespace alimentosAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){
            
        }

        public DbSet <Alimentos> TB_ALIMENTOS {get; set;}
        public DbSet <Usuario> TB_USUARIOS {get;set;}

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("varchar").HaveMaxLength(200);
        }
        

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alimentos>().ToTable("TB_Alimentos");
            modelBuilder.Entity<Usuario>().ToTable("TB_USUARIOS");
                

            //Relacionamento One To Many
            modelBuilder.Entity<Usuario>()
            .HasMany(e => e.Alimentos)
            .WithOne(e => e.Usuario)
            .HasForeignKey(e => e.UsuarioId)
            .IsRequired(false);

            modelBuilder.Entity<Alimentos>().HasData
            (
                new Alimentos() { Id = 1, Nome = "Maçã", Calorias=56, Carboidratos=15, Proteinas=0, Gorduras=0, Fibras=1, Sodio = 0, Tipo=ClasseEnum.Frutas, UsuarioId =1},
                new Alimentos() { Id = 2, Nome = "Banana", Calorias=98,Carboidratos=26, Proteinas=1, Gorduras=0, Fibras=2, Sodio= 21, Tipo=ClasseEnum.Frutas, UsuarioId =1 },
                new Alimentos() { Id = 3, Nome = "Picanha", Calorias=207, Carboidratos=1, Proteinas=20, Gorduras=11, Fibras=0, Sodio= 450, Tipo=ClasseEnum.Carnes, UsuarioId =1 }
            );
            

            //Usando hash and salt.
            string defaultPassword = "senhaPadrao";
            byte[] salt, hash;
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(defaultPassword));
            }

            //Inicio da criacao do usuario padrao

            Usuario user = new Usuario();
            user.Id = 1;
            user.Username = "UsuarioAdmin";
            user.PasswordString = string.Empty;
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.Perfil = "Admin";
            user.Email = "torresssgio123@gmail.com";
            
            modelBuilder.Entity<Usuario>().Property(u=> u.Perfil).HasDefaultValue("Jogador");
        }

    }
}