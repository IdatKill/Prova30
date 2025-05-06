using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prova30.Data;
using Prova30.Models;

namespace Prova30.Controllers;

[ApiController]
[Route("api/eventos")]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _repository;

    public EventoController(IEventoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("listar")]
    [Authorize(Roles = "admin")]
    public IActionResult Listar()
    {
        var eventos = _repository.ListarTodos();
        return Ok(eventos);
    }

    [HttpGet("eventoUsuario/{UsuarioId}")]
    public IActionResult Usuario(int UsuarioId)
    {
        var evento = _repository.EventoUsuario(UsuarioId);
        if (evento == null)
            return NotFound(new { mensagem = "Nenhum evento encontrado" });

        return Ok(evento);
    }

    
    [HttpPost("cadastrar")]
    [Authorize(Roles = "admin")]
    public IActionResult Cadastrar([FromBody] Evento evento)
    {
        _repository.Cadastrar(evento);
        _repository.Salvar();
        return Created("", evento);
    }
}
