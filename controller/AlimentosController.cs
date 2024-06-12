using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alimentosAPI.Data;
using alimentosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alimentosAPI.controller
{
    [ApiController]
    [Route("[controller]")]
    public class AlimentosController : ControllerBase
    {
        private readonly DataContext _context;

        public AlimentosController(DataContext context) {
            _context = context;
        }

        [HttpGet("{id}")] //buscar pelo id

        public async Task<IActionResult> GetSingle(int id)
        {
            try 
            {
                Alimentos a = _context.TB_ALIMENTOS
                    .FirstOrDefault(aBusca => aBusca.Id == id);
                
                return Ok(a);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> Get()
        {
            try 
            {
                List <Alimentos> lista = await _context.TB_ALIMENTOS.ToListAsync();
                return Ok(lista);
            } 
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]

        public async Task<IActionResult> Update(Alimentos novoAlimento) 
        {
            try{
                if (novoAlimento.Calorias < 1)
                {
                    throw new System.Exception ("Calorias não podem ser menores do que 1");
                }
                _context.TB_ALIMENTOS.Update(novoAlimento);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        
        [HttpDelete("{id}")]

        public async Task <IActionResult> Delete (int id)
        {

            try 
            {
                Alimentos aRemover = await _context.TB_ALIMENTOS.FirstOrDefaultAsync(a => a.Id == id);

                if (aRemover == null)
                return NotFound();

                _context.TB_ALIMENTOS.Remove(aRemover);
                int linhasAfetadas = await _context.SaveChangesAsync();
                return Ok(linhasAfetadas);

            }

            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 

    }
}