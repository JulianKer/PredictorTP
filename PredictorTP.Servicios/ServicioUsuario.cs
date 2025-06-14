
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
        private IUsuarioRepositorio usuarioRepositorio;

        public ServicioUsuario(IUsuarioRepositorio usuarioRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public void Registrar(Usuario usuario)
        {
            usuarioRepositorio.CargarNuevoUsuario(usuario);
        }
    }
}
