using System;
using Prova30.Models;

namespace Prova30.Data;

public interface IUsuarioRepository
{
    List<Usuario> ListarTodos();
    Usuario? BuscarPorId(int id);
    void Cadastrar(Usuario usuario);
    Usuario? BuscarUsuarioPorEmailSenha(string email, string senha);
    void Salvar();
}
