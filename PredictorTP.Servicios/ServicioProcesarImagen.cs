using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PredictorTP.Entidades.EF;
using PredictorTP.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace PredictorTP.Servicios
{
    public interface IServicioProcesarImagen
    {
        List<ResultadoImagen> GetImagenesDelUsuario(int userID);
        void GuardarResultado(string fileName, List<string> personas, int userId);
    }


    public class ServicioProcesarImagen : IServicioProcesarImagen
    {

        private IRepositorioProcesarImagen _repositorioProcesarImagen;

        public ServicioProcesarImagen(IRepositorioProcesarImagen repositorioProcesarImagen)
        {
            this._repositorioProcesarImagen = repositorioProcesarImagen;
        }

        public void GuardarResultado(string fileName, List<string> personas, int userId)
        {
            ResultadoImagen nuevoResultadoImagen = new ResultadoImagen(fileName, userId);
            int idResultadoImagenGuardado = this._repositorioProcesarImagen.GuardarResultadoImagen(nuevoResultadoImagen);

            List<PersonaDetectadum> personasDetectadas = new List<PersonaDetectadum>();

            if (personas != null && personas.Count > 0)
            {
                foreach (string p in personas)
                {
                    PersonaDetectadum nuevaPersonasDetectadas = new PersonaDetectadum(idResultadoImagenGuardado, p);
                    personasDetectadas.Add(nuevaPersonasDetectadas);
                }
            }
            else {
                PersonaDetectadum nuevaPersonasDetectadas = new PersonaDetectadum(idResultadoImagenGuardado, "No se detectaron personas.");
                personasDetectadas.Add(nuevaPersonasDetectadas);
            }

            this._repositorioProcesarImagen.GuardarPersonasDetectadas(personasDetectadas);
        }

        public List<ResultadoImagen> GetImagenesDelUsuario(int userID)
        {
            return this._repositorioProcesarImagen.GetImagenesDelUsuario(userID);
        }


    }
}
