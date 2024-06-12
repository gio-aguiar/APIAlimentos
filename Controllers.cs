using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using alimentosAPI.Models;
using alimentosAPI.Models.Enuns;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace alimentosAPI.Controllers {
    [ApiController]
    [Route("[controller]")]

    public class AlimentosExemploController : ControllerBase 
    {
        private static List<Alimentos> alimentos = new List<Alimentos>()
        {
    new Alimentos() { Id = 1, Nome = "Maçã", Calorias=56, Carboidratos=15, Proteinas=0, Gorduras=0, Fibras=1, Sodio = 0, Tipo=ClasseEnum.Frutas },
    new Alimentos() { Id = 2, Nome = "Banana", Calorias=98,Carboidratos=26, Proteinas=1, Gorduras=0, Fibras=2, Sodio= 21, Tipo=ClasseEnum.Frutas },
    new Alimentos() { Id = 3, Nome = "Picanha", Calorias=207, Carboidratos=1, Proteinas=20, Gorduras=11, Fibras=0, Sodio= 450, Tipo=ClasseEnum.Carnes }

        };


        [HttpGet("Get")]
        public IActionResult GetFirst(){
            Alimentos a = alimentos[0];
            return Ok(a);
        }

        [HttpGet("GetAll")]
        public IActionResult Get(){
            return Ok(alimentos);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id){
            return Ok(alimentos.FirstOrDefault(al => al.Id ==id));
        }

        [HttpPost]

        public IActionResult AddAlimentos(Alimentos novoAlimento) 
        {
            alimentos.Add(novoAlimento);
            return Ok(alimentos);
        }
    }

}
