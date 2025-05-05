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
    private readonly IUsuarioRepository _repository;

    public UsuarioController(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("listar")]
    public IActionResult Listar()
    {
        var usuarios = _repository.ListarTodos();
        return Ok(usuarios);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar([FromBody] Usuario usuario)
    {
        _repository.Cadastrar(usuario);
        _repository.Salvar();
        return Created("", usuario);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] Usuario usuario)
    {
        Usuario? usuarioExistente = _repository
            .BuscarUsuarioPorEmailSenha(usuario.Email, usuario.Senha);

        if (usuarioExistente == null)
        {
            return Unauthorized(new { mensagem = "Usuário ou senha inválidos!" });
        }

        // string token = GerarToken(usuarioExistente);
        return Ok("Usuário Logado!");
    }
}
