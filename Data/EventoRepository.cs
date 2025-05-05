using System;
using Prova30.Models;

namespace Prova30.Data;

public class EventoRepository : IEventoRepository
{
    private readonly AppDbContext _context;
    public EventoRepository(AppDbContext context)
    {
        _context = context;
    }
    public void Cadastrar(Evento evento)
    {
         _context.Eventos.Add(evento);
    }

    public Evento? EventoUsuario(int usuarioId)
    {
        return _context.Eventos.FirstOrDefault(p => p.Id == usuarioId); 
    }

    public List<Evento> ListarTodos()
    {
        return _context.Eventos.ToList();
    }

    public void Salvar()
    {
        _context.SaveChanges();
    }
}
