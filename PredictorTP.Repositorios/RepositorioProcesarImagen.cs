using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PredictorTP.Entidades.EF;

namespace PredictorTP.Repositorios
{
    public interface IRepositorioProcesarImagen
    {
        List<ResultadoImagen> GetImagenesDelUsuario(int userID);
        void GuardarPersonasDetectadas(List<PersonaDetectadum> personasDetectadas);
        int GuardarResultadoImagen(ResultadoImagen nuevoResultadoImagen);
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
    }
}
