
using PredictorTP.Entidades.EF;
using PredictorTP.Repositorios;

namespace PredictorTP.Servicios
{

    public interface IServicioUsuario
    {
        void Registrar(Usuario usuario);
    }

    public class ServicioUsuario : IServicioUsuario
    {
        private IUsuarioRepositorio _usuarioRepositorio;

        public ServicioUsuario(IUsuarioRepositorio usuarioRepositorio)
        {
            this._usuarioRepositorio = usuarioRepositorio;
        }

        public void Registrar(Usuario usuario)
        {
            this._usuarioRepositorio.CargarNuevoUsuario(usuario);
        }
    }
}
