using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alimentosAPI.Models.Enuns;
using AlimentosAPI.Models;
using System.Text.Json.Serialization;

namespace alimentosAPI.Models
{
    public class Alimentos
    {
        public int Id { get; set;}
        public string Nome { get; set;}
        public int Calorias {get; set;}
        public int Carboidratos {get; set;}
        public int Proteinas {get; set;}
        public int Gorduras {get; set;}
        public int Fibras { get; set;}
        public int Sodio { get; set;}
        public ClasseEnum Tipo { get; set;}

        public ClasseEnum Classe {get; set;}

        public byte[]? FotoAlimento {get;set;}

        public int? UsuarioId {get;set;}

        public Usuario? Usuario {get;set;}

    }

}