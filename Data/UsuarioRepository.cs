using System;
using Prova30.Models;

namespace Prova30.Data;
public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;
    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public List<Usuario> ListarTodos()
    {
        return _context.Usuarios.ToList();
    }
    
    public Usuario? BuscarPorId(int id)
    {
        return _context.Usuarios.FirstOrDefault(p => p.Id == id);   
    }

    public void Cadastrar(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
    }

    public void Salvar()
    {
        _context.SaveChanges();
    }
    public Usuario? BuscarUsuarioPorEmailSenha(string email, string senha)
    {
        Usuario? usuarioExistente = 
            _context.Usuarios.FirstOrDefault
            (x => x.Email == email && x.Senha == senha);
        return usuarioExistente;
    }
}
