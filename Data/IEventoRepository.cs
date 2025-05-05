using System;
using Prova30.Models;

namespace Prova30.Data;

public interface IEventoRepository
{
    List<Evento> ListarTodos();
    Evento? EventoUsuario(int usuarioId);
    void Cadastrar(Evento evento);
    void Salvar();
}
