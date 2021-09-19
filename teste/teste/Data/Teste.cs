using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using teste.Models;

namespace teste.Data
{
    /// <summary>
    /// classe para recolher os dados particulares dos Utilizadores
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }

        public string Fotografia { get; set; }

        public DateTime RegisterTime { get; set; }


    }

    /// <summary>
    /// criação da BD do projeto
    /// </summary>


    public class Teste : IdentityDbContext<ApplicationUser>
    {

        public Teste(DbContextOptions<Teste> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // insert DB seed

           

            

            modelBuilder.Entity<Bebidas>().HasData(
              new Bebidas { Id = 1, Nome = "Vinho Rose Mateus", Descricao = " MATEUS ROSÉ é um vinho leve, fresco, jovem e ligeiramente «pétillant»", Preco = 20F, Stock = "Sim" },
              new Bebidas { Id = 2, Nome = "Vinho do Porto Ferreira", Descricao = "É vinificado pelo método tradicional do vinho do Porto.", Preco = 30F, Stock = "Sim" },
              new Bebidas { Id = 3, Nome = "Veritas Moscatel", Descricao = "Vinho Moscatel de Setúbal", Preco = 10F, Stock = "Sim" },
              new Bebidas { Id = 4, Nome = "Grants Whisky", Descricao = "Grant’s é um whisky extraordinário e um dos mais complexos produzidos na Escócia.", Preco = 15F, Stock = "Sim" },
              new Bebidas { Id = 5, Nome = "Ciroc Vodka", Descricao = "Cîroc Vodka é uma marca de vodca eau-de-vie de luxo, fabricada com uvas da região Carântono-Marítimo, da França", Preco = 30F, Stock = "Sim" },
              new Bebidas { Id = 6, Nome = "Malibu Rum", Descricao = "Nada bate um original, e Malibu não é apenas o original, é o mais vendido rum do Caribe com sabor natural de coco ", Preco = 15F, Stock = "Sim" },
              new Bebidas { Id = 7, Nome = "Cachaça 51", Descricao = "Cachaça, o sabor e aroma perfeito da original caipirinha brasileira.", Preco = 11F, Stock = "Sim" },
              new Bebidas { Id = 8, Nome = "Moet&Chandon", Descricao = "Moet & Chandon, um champanhe de estilo único e elegante.", Preco = 50F, Stock = "Sim" },
              new Bebidas { Id = 9, Nome = "Super Bock Pack15", Descricao = "O sabor autêntico. Super Bock Original é a única cerveja portuguesa com 37 medalhas de ouro consecutivas", Preco = 7F, Stock = "Sim" }

           );




        }



        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Bebidas> Bebidas { get; set; }

        public virtual DbSet<Reservas> Reservas { get; set; }


    }
}

