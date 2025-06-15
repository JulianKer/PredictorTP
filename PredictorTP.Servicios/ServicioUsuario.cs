
using PredictorTP.Entidades.EF;
using PredictorTP.Repositorios;

namespace PredictorTP.Servicios
{

    public interface IServicioUsuario
    {
        Usuario buscarUsuarioPorId(int v);
        void eliminarUsuarioPorId(int id);
        void Registrar(Usuario usuario);
    }

    public class ServicioUsuario : IServicioUsuario
    {
        private IUsuarioRepositorio _usuarioRepositorio;

        public ServicioUsuario(IUsuarioRepositorio usuarioRepositorio)
        {
            this._usuarioRepositorio = usuarioRepositorio;
        }

        public Usuario buscarUsuarioPorId(int id)
        {
            return this._usuarioRepositorio.buscarUsuarioPorId(id);
        }

        public void eliminarUsuarioPorId(int id)
        {
            Usuario userAEliminar = buscarUsuarioPorId(id);
            if (userAEliminar != null)
            {
                this._usuarioRepositorio.eliminarUsuario(userAEliminar);
            }
        }

        public void Registrar(Usuario usuario)
        {
            this._usuarioRepositorio.CargarNuevoUsuario(usuario);
        }
    }
}
