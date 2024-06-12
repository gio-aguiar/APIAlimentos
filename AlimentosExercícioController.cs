using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alimentosAPI.Models;
using alimentosAPI.Models.Enuns;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace alimentosAPI
{
    [ApiController]
    [Route("[controller]")]
    public class AlimentosExercícioController : ControllerBase
    {
        private static List<Alimentos> alimentos = new List<Alimentos>()
        {
    new Alimentos() { Id = 0, Nome = "Maçã", Calorias=56, Carboidratos=15, Proteinas=0, Gorduras=0, Fibras=1, Sodio = 0, Tipo=ClasseEnum.Frutas },
    new Alimentos() { Id = 1, Nome = "Banana", Calorias=98,Carboidratos=26, Proteinas=1, Gorduras=0, Fibras=2, Sodio= 21, Tipo=ClasseEnum.Frutas },
    new Alimentos() { Id = 2, Nome = "Picanha", Calorias=207, Carboidratos=1, Proteinas=20, Gorduras=11, Fibras=0, Sodio= 450, Tipo=ClasseEnum.Carnes }

        };

        [HttpGet ("GetByNome/{Nome}")]
        public IActionResult GetSingle(string Nome)
        {
            List<Alimentos> buscaNome = alimentos.FindAll(alimentos => alimentos.Nome.Contains(Nome));
            return Ok(buscaNome);
        }

        [HttpPost("PostValidacaoCalorias")]
        public IActionResult PostValidacao(Alimentos novoAlimento)
        {
            if (novoAlimento.Calorias <= 0)
                return BadRequest("O ali");
        
            alimentos.Add(novoAlimento);
            return Ok(alimentos);
        }

        [HttpPost("PostValidacaoNome")]
        public IActionResult PostValidacaoNome(Alimentos novoAlimento)
        {
            if (novoAlimento.Nome == null)
                return BadRequest("O alimento deverá obrigatóriamente ter um nome.");
        
            alimentos.Add(novoAlimento);
            return Ok(alimentos);
        }

        [HttpGet("GetByClasse/{classeId}")]
        public IActionResult GetByClasse(int classeId)
        {
            var alimentosByClasse = alimentos.Where(p => (int)p.Id == classeId).ToList();
        
            if (alimentosByClasse.Count() == 0)
            {
                return BadRequest("Nenhum personagem encontrado");
            
            }
            return Ok(alimentosByClasse);
        }



    }
}