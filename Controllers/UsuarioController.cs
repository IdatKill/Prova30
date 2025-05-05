using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Prova30.Data;
using Prova30.Models;

namespace Prova30.Controllers;

[ApiController]
[Route("api/usuario")]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("listar")]
    public IActionResult Listar()
    {
        var usuarios = _context.Usuarios.ToList();
        return Ok(usuarios);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar([FromBody] Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return Created("", usuario);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] Usuario usuario)
    {
        Usuario? usuarioExistente = _context.Usuarios
        .FirstOrDefault(u => u.Email == usuario.Email && u.Senha == usuario.Senha);

        if (usuarioExistente == null)
        {
            return Unauthorized(new { mensagem = "Usuário ou senha inválidos!" });
        }

        return Ok("Logado com sucesso!");
    }
}
