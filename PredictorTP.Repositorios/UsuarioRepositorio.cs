
using Microsoft.EntityFrameworkCore;
using PredictorTP.Entidades.EF;

namespace PredictorTP.Repositorios
{

    public interface IUsuarioRepositorio
    {
        Task CargarNuevoUsuario(Usuario usuario);
        Task ActualizarUsuario(Usuario usuario);
        Task<Usuario> ObtenerUsuarioPorToken(string token);
    }
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private PredictorBddContext _context;

        public UsuarioRepositorio(PredictorBddContext context)
        {
            _context = context;
        }

        public async Task CargarNuevoUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task ActualizarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> ObtenerUsuarioPorToken(string token)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Tokenconfirmacion == token);
        }
    }
}
