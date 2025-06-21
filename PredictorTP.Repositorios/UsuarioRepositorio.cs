
using Microsoft.EntityFrameworkCore;
using PredictorTP.Entidades.EF;

namespace PredictorTP.Repositorios
{

    public interface IUsuarioRepositorio
    {
        Usuario buscarUsuarioPorId(int id);
        void eliminarUsuario(Usuario userAEliminar);
        Task CargarNuevoUsuario(Usuario usuario);
        Task ActualizarUsuario(Usuario usuario);
        Task<Usuario> ObtenerUsuarioPorToken(string token);

        Task<Usuario> BuscarUsuarioPorEmail(string email);
        List<Usuario> GetUsuarios(string? nombre);
        Usuario BuscarUsuarioPorEmailSync(string email);
    }
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private PredictorBddContext _context;

        public UsuarioRepositorio(PredictorBddContext context)
        {
            _context = context;
        }

        public Usuario buscarUsuarioPorId(int id)
        {
            return this._context.Usuarios.Find(id);
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

        public async Task<Usuario> BuscarUsuarioPorEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public void eliminarUsuario(Usuario userAEliminar)
        {
            this._context.Usuarios.Remove(userAEliminar);
            this._context.SaveChanges();
        }

        public List<Usuario> GetUsuarios(string? busquedaUsuario)
        {
            if (busquedaUsuario == null) {
                return this._context.Usuarios.ToList<Usuario>();
            }
            return this._context.Usuarios.Where(u => u.Nombre.ToLower().Contains(busquedaUsuario.ToLower())).ToList<Usuario>();
        }

        public Usuario BuscarUsuarioPorEmailSync(string email)
        {
            return this._context.Usuarios.FirstOrDefault(u => u.Email == email);
        }
    }
}
