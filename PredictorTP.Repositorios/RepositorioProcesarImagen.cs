using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using PredictorTP.Entidades.EF;

namespace PredictorTP.Repositorios
{
    public interface IRepositorioProcesarImagen
    {
        List<ResultadoImagen> GetImagenesDelUsuario(int userID);
        void GuardarPersonasDetectadas(List<PersonaDetectadum> personasDetectadas);
        int GuardarResultadoImagen(ResultadoImagen nuevoResultadoImagen);
        List<int> ObtenerCantidadesEmociones();
        List<int> ObtenerCantidadesIdioma();
        List<int> ObtenerCantidadesPolaridad();
        List<int> ObtenerCantidadEstadisticas();
        List<int> ObtenerCantidadEstadisticasPersonasEmociones();
        List<int> ObtenerCantidadEstadisticasUsuariosAdmin();
        List<string> ObtenerLabelEstadisticas();
        List<string> ObtenerLabelEstadisticasPersonasEmociones();
        List<string> ObtenerLabelEstadisticasUsuariosAdmin();
        List<string> ObtenerLabelsEmociones();
        List<string> ObtenerLabelsIdioma();
        List<string> ObtenerLabelsPolaridad();
    }



    public class RepositorioProcesarImagen : IRepositorioProcesarImagen
    {
        private PredictorBddContext _contexto;
        public RepositorioProcesarImagen(PredictorBddContext contexto)
        {
            this._contexto = contexto;
        }

        public void GuardarPersonasDetectadas(List<PersonaDetectadum> personasDetectadas)
        {
            this._contexto.PersonaDetectada.AddRange(personasDetectadas);
            this._contexto.SaveChanges();
        }

        public int GuardarResultadoImagen(ResultadoImagen nuevoResultadoImagen)
        {
            this._contexto.ResultadoImagens.Add(nuevoResultadoImagen);
            this._contexto.SaveChanges();

            return nuevoResultadoImagen.ResultadoImagenId;
        }

        public List<ResultadoImagen> GetImagenesDelUsuario(int userID)
        {
            List<ResultadoImagen> lista = this._contexto.ResultadoImagens
                .Where(r => r.UserId == userID)
                .Include(r => r.PersonaDetectada)
                .OrderByDescending(r => r.ResultadoImagenId)
                .ToList();

            foreach (ResultadoImagen r in lista)
            {
                foreach (PersonaDetectadum persona in r.PersonaDetectada)
                {
                    persona.ResultadoImagen = null;
                    //Console.WriteLine(persona.DescripcionPersona);
                }
            }
            return lista;
        }
        
        
        //-------GRAFICOSS--------------------------------------------------------------------

        public List<int> ObtenerCantidadEstadisticas()
        {
            var totalHombres = this._contexto.PersonaDetectada
                .Count(p => p.DescripcionPersona.ToLower().Contains("hombre"));

            var totalMujeres = this._contexto.PersonaDetectada
                .Count(p => p.DescripcionPersona.ToLower().Contains("mujer"));

            List<int> datos = new List<int>();
            datos.Add(totalHombres);
            datos.Add(totalMujeres);

            return datos;
        }

        public List<string> ObtenerLabelEstadisticas()
        {
            List<string> datos = new List<string>();
            datos.Add("Hombre");
            datos.Add("Mujer");

            return datos;
        }
        public List<int> ObtenerCantidadEstadisticasPersonasEmociones()
        {
            var totalPersonasNeutral = this._contexto.PersonaDetectada
                .Count(p => p.DescripcionPersona.ToLower().Contains("neutral"));

            var totalPersonasFeliz = this._contexto.PersonaDetectada
                .Count(p => p.DescripcionPersona.ToLower().Contains("feliz"));

            var totalPersonasTriste = this._contexto.PersonaDetectada
                .Count(p => p.DescripcionPersona.ToLower().Contains("triste"));

            var totalPersonasEnojado = this._contexto.PersonaDetectada
                .Count(p => p.DescripcionPersona.ToLower().Contains("enojado"));

            var totalPersonasAsustado = this._contexto.PersonaDetectada
                .Count(p => p.DescripcionPersona.ToLower().Contains("asustado"));

            var totalPersonasDisgustado = this._contexto.PersonaDetectada
                .Count(p => p.DescripcionPersona.ToLower().Contains("disgustado"));            
            
            var totalPersonasSorprendido = this._contexto.PersonaDetectada
                .Count(p => p.DescripcionPersona.ToLower().Contains("sorprendido"));

            List<int> datos = new List<int>();
            datos.Add(totalPersonasNeutral);
            datos.Add(totalPersonasFeliz);            
            datos.Add(totalPersonasTriste);
            datos.Add(totalPersonasEnojado);
            datos.Add(totalPersonasAsustado);
            datos.Add(totalPersonasDisgustado);
            datos.Add(totalPersonasSorprendido);

            return datos;
        }
        public List<string> ObtenerLabelEstadisticasPersonasEmociones()
        {
            List<string> datos = new List<string>();
            datos.Add("Neutral");
            datos.Add("Feliz");
            datos.Add("Triste");
            datos.Add("Enojado");
            datos.Add("Asustado");
            datos.Add("Disgustado");
            datos.Add("Sorprendido");

            return datos;
        }

        public List<int> ObtenerCantidadEstadisticasUsuariosAdmin()
        {
            var totalPersonasAdmins = this._contexto.Usuarios
                .Count(u => u.Administrador);

            var totalPersonasNoAdmins = this._contexto.Usuarios
                .Count(u => !u.Administrador);

            List<int> datos = new List<int>();
            datos.Add(totalPersonasAdmins);
            datos.Add (totalPersonasNoAdmins);

            return datos;
        }
        public List<string> ObtenerLabelEstadisticasUsuariosAdmin()
        {
            List<string> datos = new List<string>();
            datos.Add("Administradores");
            datos.Add("Usuarios");

            return datos;
        }

        public List<int> ObtenerCantidadesPolaridad()
        {
            var totalPositivas = this._contexto.DatoPolaridads
                .Count(d => d.Resutlado.ToLower().Equals("positiva"));

            var totalNegativas = this._contexto.DatoPolaridads
                .Count(d => !d.Resutlado.ToLower().Equals("positiva"));

            List<int> datos = new List<int>();
            datos.Add(totalPositivas);
            datos.Add(totalNegativas);

            return datos;
        }

        public List<string> ObtenerLabelsPolaridad()
        {
            List<string> datos = new List<string>();
            datos.Add("Positivas");
            datos.Add("Negativas");

            return datos;
        }

        public List<string> ObtenerLabelsEmociones()
        {
            List<string> datos = new List<string>();
            datos.Add("Felicidad");
            datos.Add("Tristeza");
            datos.Add("Enojo");
            datos.Add("Frustración");
            datos.Add("Miedo");
            datos.Add("Vergüenza");
            datos.Add("Amor");
            datos.Add("Sarcasmo");

            return datos;
        }

        public List<string> ObtenerLabelsIdioma()
        {
            List<string> datos = new List<string>();
            datos.Add("Inglés");
            datos.Add("Español");

            return datos;
        }

        public List<int> ObtenerCantidadesEmociones()
        {
            var totalFelicidad = this._contexto.DatoSentimientos
                 .Count(d => d.Sentimiento.ToLower().Equals("felicidad"));

            var totalTristeza = this._contexto.DatoSentimientos
                .Count(d => !d.Sentimiento.ToLower().Equals("tristeza"));

            var totalEnojo = this._contexto.DatoSentimientos
                 .Count(d => d.Sentimiento.ToLower().Equals("enojo"));

            var totalFrustracion = this._contexto.DatoSentimientos
                .Count(d => !d.Sentimiento.ToLower().Equals("frustración"));

            var totalMiedo = this._contexto.DatoSentimientos
                 .Count(d => d.Sentimiento.ToLower().Equals("miedo"));

            var totalVerguenza = this._contexto.DatoSentimientos
                .Count(d => !d.Sentimiento.ToLower().Equals("vergüenza"));

            var totalAmor = this._contexto.DatoSentimientos
                .Count(d => !d.Sentimiento.ToLower().Equals("amor"));

            var totalSarcasmo = this._contexto.DatoSentimientos
                .Count(d => !d.Sentimiento.ToLower().Equals("sarcasmo"));

            List<int> datos = new List<int>();
            datos.Add(totalFelicidad);
            datos.Add(totalTristeza);
            datos.Add(totalEnojo);
            datos.Add(totalFrustracion);
            datos.Add(totalMiedo);
            datos.Add(totalVerguenza);
            datos.Add(totalAmor);
            datos.Add(totalSarcasmo);

            return datos;
        }

        public List<int> ObtenerCantidadesIdioma()
        {
            var totalIngles = this._contexto.DatoIdiomas
                 .Count(d => d.Idioma.ToLower().Equals("inglés"));

            var totalEspaniol = this._contexto.DatoIdiomas
                .Count(d => !d.Idioma.ToLower().Equals("español"));

            List<int> datos = new List<int>();
            datos.Add(totalIngles);
            datos.Add(totalEspaniol);

            return datos;
        }

        //----------------------------------------------------------------------------
    }
}
