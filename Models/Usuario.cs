using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using alimentosAPI.Models;
using Microsoft.Identity.Client;

namespace AlimentosAPI.Models
{
    public class Usuario
    {

        
        public int Id { get; set;}
        public string Username { get; set;}
       
        public byte[]? PasswordHash {get; set;}

        public byte[]? PasswordSalt {get; set;}

        public DateTime? DataAcesso {get; set;}

        [NotMapped]

        public string PasswordString {get; set;} = String.Empty;

        public List<Alimentos> Alimentos {get; set;} = new List<Alimentos>(); 
        
        public string Perfil {get; set;} = string.Empty;

        public string Email {get; set;} = string.Empty;
    }
}