using System;
using Microsoft.AspNetCore.Mvc;
using Prova30.Data;
using Prova30.Models;

namespace Prova30.Controllers;

[ApiController]
[Route("api/eventos")]
public class EventoController : ControllerBase
{
    private readonly AppDbContext _context;

    public EventoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("listar")]
    public IActionResult Listar()
    {
        var eventos = _context.Eventos.ToList();
        return Ok(eventos);
    }

    [HttpGet("usuario/{UsuarioId}")]
    public IActionResult Usuario([FromServices] Evento evento, int UsuarioId)
    {
        var usuario = _context.Usuarios.Find(UsuarioId);
        if (usuario == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        var eventos = _context.Eventos.Where(e => e.UsuarioId == UsuarioId).ToList();
        return Ok(eventos);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar([FromBody] Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
        return Created("", evento);
    }
}
