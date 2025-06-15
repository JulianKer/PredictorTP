
using PredictorTP.Entidades.EF;
using PredictorTP.Repositorios;

namespace PredictorTP.Servicios
{

    public interface IServicioUsuario
    {
        Usuario buscarUsuarioPorId(int v);
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

        public void Registrar(Usuario usuario)
        {
            this._usuarioRepositorio.CargarNuevoUsuario(usuario);
        }
    }
}
