using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Prova30.Data;
using Prova30.Models;

namespace Prova30.Controllers;

[ApiController]
[Route("api/usuario")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _repository;
    private readonly IConfiguration _configuration;

    public UsuarioController(IUsuarioRepository repository, IConfiguration configuration)
    {
        _configuration = configuration;
        _repository = repository;
    }

    [HttpGet("listar")]
    [Authorize(Roles = "admin")]
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

        string token = GerarToken(usuarioExistente);
        return Ok(token);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    private string GerarToken(Usuario usuario)
      {
          var claims = new[]
          {
              new Claim(ClaimTypes.Name, usuario.Email),
              new Claim(ClaimTypes.Role, usuario.Role.ToString())
          };

          var chave = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!);
          var assinatura = new SigningCredentials(
              new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256);

          var token = new JwtSecurityToken(
              claims: claims,
              expires: DateTime.UtcNow.AddHours(1),
              signingCredentials: assinatura);

          return new JwtSecurityTokenHandler().WriteToken(token);
      }
}
