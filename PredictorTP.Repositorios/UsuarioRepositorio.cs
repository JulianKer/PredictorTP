
using Microsoft.EntityFrameworkCore;
using PredictorTP.Entidades.EF;

namespace PredictorTP.Repositorios
{

    public interface IUsuarioRepositorio
    {
        void ActualizarUsuario(Usuario userBdd);
        Usuario buscarUsuarioPorId(int id);
        void CargarNuevoUsuario(Usuario usuario);
        void eliminarUsuario(Usuario userAEliminar);
    }
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private PredictorBddContext _context;

        public UsuarioRepositorio(PredictorBddContext context)
        {
            _context = context;
        }

        public void ActualizarUsuario(Usuario userBdd)
        {
            this._context.Usuarios.Update(userBdd);
            this._context.SaveChanges();
        }

        public Usuario buscarUsuarioPorId(int id)
        {
            return this._context.Usuarios.Find(id);
        }

        public void CargarNuevoUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void eliminarUsuario(Usuario userAEliminar)
        {
            this._context.Usuarios.Remove(userAEliminar);
            this._context.SaveChanges();
        }
    }
}
