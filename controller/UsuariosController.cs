using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using alimentosAPI.Data;
using AlimentosAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RpgApi.Utils;


namespace AlimentosAPI.controller
{
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context)
        {
            _context = context;
        }

     private async Task<bool> UsuarioExistente(string username)
       {
        if(await _context.TB_USUARIOS.AnyAsync(x=> x.Username.ToLower() == username.ToLower()))
        {
            return true;
        }
        return false;
    }

    
        [HttpPost("Registrar")]

    public async Task<IActionResult> RegistrarUsuario(Usuario user)
    {
        try
        {
            if (await UsuarioExistente(user.Username))
            throw new System.Exception("Nome de Usuario já existente");

            Criptografia.CriarPasswordHash(user.PasswordString, out byte[] hash, out byte[] salt);
            user.PasswordString = string.Empty;
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            await _context.TB_USUARIOS.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user.Id);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("Autenticar")]
    public async Task<IActionResult> AutenticarUsuario (Usuario credenciais)
    {
        try
        {
            Usuario? usuario = await _context.TB_USUARIOS
                    .FirstOrDefaultAsync(x => x.Username.ToLower().Equals(credenciais.Username.ToLower()));
                    if (usuario == null)
                    {
                        throw new System.Exception("Usuario não encontrado");
                    }
                    else if(!Criptografia
                            .VerificarPasswordHash(credenciais.PasswordString, usuario.PasswordHash, usuario.PasswordSalt))
                            {
                                throw new System.Exception("Senha incorreta");
                            }
                            else
                            {
                                return Ok(usuario);
                            }
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                
    }

        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaDto alterarSenhaDto)
        {
            try
            {
                var usuario = await _context.TB_USUARIOS.FirstOrDefaultAsync(x => x.Id == alterarSenhaDto.UserId);

                if (usuario == null)
                    throw new Exception("Usuário não encontrado");

                // verificar se a senha esta certa
                if (!Criptografia.VerificarPasswordHash(alterarSenhaDto.SenhaAtual, usuario.PasswordHash, usuario.PasswordSalt))
                    throw new Exception("Senha atual incorreta");

                Criptografia.CriarPasswordHash(alterarSenhaDto.NovaSenha, out byte[] hash, out byte[] salt);
                usuario.PasswordHash = hash;
                usuario.PasswordSalt = salt;

                _context.TB_USUARIOS.Update(usuario);
                await _context.SaveChangesAsync();

                usuario.DataAcesso = DateTime.Now;

                _context.Entry(usuario).State = EntityState.Modified;
                await _context.SaveChangesAsync();


                return Ok("Senha alterada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                List<Usuario> lista = await _context.TB_USUARIOS.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> GetUsuario(int usuarioId)
        {
        try
        {
        //List exigirá o using System.Collections.Generic
        Usuario usuario = await _context.TB_USUARIOS //Busca o usuário no banco através do Id
        .FirstOrDefaultAsync(x => x.Id == usuarioId);
        return Ok(usuario);
        }
        catch (System.Exception ex)
        {
        return BadRequest(ex.Message);
        }
        }

                [HttpGet("GetByLogin/{login}")]
        public async Task<IActionResult> GetUsuario(string login)
        {
        try
        {
        //List exigirá o using System.Collections.Generic
        Usuario usuario = await _context.TB_USUARIOS //Busca o usuário no banco através do login
        .FirstOrDefaultAsync(x => x.Username.ToLower() == login.ToLower());
        return Ok(usuario);
        }
        catch (System.Exception ex)
        {
        return BadRequest(ex.Message);
        }
        }

                [HttpPut("AtualizarEmail")]
        public async Task<IActionResult> AtualizarEmail(Usuario u)
        {
            try
            {
            Usuario usuario = await _context.TB_USUARIOS //Busca o usuário no banco através do Id
            .FirstOrDefaultAsync(x => x.Id == u.Id);
            usuario.Email = u.Email;
            var attach = _context.Attach(usuario);
            attach.Property(x => x.Id).IsModified = false;
            attach.Property(x => x.Email).IsModified = true;
            int linhasAfetadas = await _context.SaveChangesAsync(); //Confirma a alteração no banco
            return Ok(linhasAfetadas); //Retorna as linhas afetadas (Geralmente sempre 1 linha msm)
            }
            catch (System.Exception ex)
            {
            return BadRequest(ex.Message);
            }
        }

        //Método para alteração da foto
    [HttpPut("AtualizarFoto")]
    public async Task<IActionResult> AtualizarFoto(Usuario u)
        {
            try
            {
            Usuario usuario = await _context.TB_USUARIOS
            .FirstOrDefaultAsync(x => x.Id == u.Id);
            usuario.Foto = u.Foto;
            var attach = _context.Attach(usuario);
            attach.Property(x => x.Id).IsModified = false;
            attach.Property(x => x.Foto).IsModified = true;
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