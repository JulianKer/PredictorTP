
using PredictorTP.Entidades.EF;
using PredictorTP.Repositorios;
using BCrypt.Net;
using System;

namespace PredictorTP.Servicios
{

    public interface IServicioUsuario
    {
        Usuario buscarUsuarioPorId(int v);
        void eliminarUsuarioPorId(int id);
        string ActualizarUsuario(Usuario userBdd);
        Task Registrar(Usuario usuario);
        Task<string> ConfirmarCuenta(string token);
    }

    public class ServicioUsuario : IServicioUsuario
    {
        private IUsuarioRepositorio _usuarioRepositorio;
        private IServicioEmail _servicioEmail;

        public ServicioUsuario(IUsuarioRepositorio usuarioRepositorio, IServicioEmail servicioEmail)
        {
            this._usuarioRepositorio = usuarioRepositorio;
            this._servicioEmail = servicioEmail;
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

        public async Task Registrar(Usuario usuario)
        {
            usuario.Contrasenia = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasenia);

            usuario.Tokenconfirmacion = Guid.NewGuid().ToString();
            usuario.Verificado = false;

            await this._usuarioRepositorio.CargarNuevoUsuario(usuario);

            var urlConfirmacion = $"http://localhost:5032/Acceso/ConfirmarCuenta?token={usuario.Tokenconfirmacion}";

            await _servicioEmail.EnviarConfirmacion(
                usuario.Email,
                "Confirma tu cuenta",
                $"<h1>Bienvenido</h1><p>Hacé click en este enlace para confirmar tu cuenta:</p><a href=\"{urlConfirmacion}\">{urlConfirmacion}</a>"
            );
        }

        public async Task<string> ConfirmarCuenta(string token)
        {
            var usuario = await _usuarioRepositorio.ObtenerUsuarioPorToken(token);
            if (usuario == null) return "La cuenta no pudo ser confirmada correctamente.";

            usuario.Verificado = true;
            usuario.Tokenconfirmacion = null;
            await _usuarioRepositorio.ActualizarUsuario(usuario);

            return "Su cuenta se ha confirmado correctamente! Inicie sesion.";
        }

        public string ActualizarUsuario(Usuario userBdd)
        {
            this._usuarioRepositorio.ActualizarUsuario(userBdd);
            return "¡Datos actualizados correctamente!";
        }
    }
}
