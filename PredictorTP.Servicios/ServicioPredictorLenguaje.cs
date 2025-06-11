using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using PredictorTP.Entidades;

namespace PredictorTP.Servicios
{
    public interface IServicioPredictorLenguaje
    {
        List<ResultadoLenguaje> ObtenerResultadosLenguaje();
        ResultadoLenguaje predecirLenguaje(string fraseEnIdioma);
        void guardarResultdoLenguaje(ResultadoLenguaje nuevoResultadoLenguaje);
    }
    public class ServicioPredictorLenguaje : IServicioPredictorLenguaje
    {
        public static List<ResultadoLenguaje> _misResultadosLenguaje = new List<ResultadoLenguaje>();
        

        /* tengo que buscar la forma de hacer este "entrenamiento" en un archivo de configuracion apenas arranca la app para no estar entrenado cada
         * vez q se le hace request (por la inyeccion scopped). 
         * 
         * Nota para el julian del futuro: Revisar de guardar el modelo entrenado en un .zip y despues
         * abrirlo en una variable dataviewschema o algo asi, entonces ya me da el modelo entrenado (capaz así funca mas fluido)
         
        private static MLContext mlContext = new MLContext();

        private static IDataView data = mlContext.Data.LoadFromTextFile<DatoLenguaje>(
            "C:\\Users\\germa\\source\\repos\\PredictorTP\\PredictorTP.Servicios\\Entrenamiento\\idiomas.tsv", hasHeader: true);

        private static IEstimator<ITransformer> pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
            .Append(mlContext.Transforms.Text.FeaturizeText("Features", nameof(DatoLenguaje.Text)))
            .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
        */




        public void guardarResultdoLenguaje(ResultadoLenguaje nuevoResultadoLenguaje)
        {
            ServicioPredictorLenguaje._misResultadosLenguaje.Add(nuevoResultadoLenguaje);
        }

        public List<ResultadoLenguaje> ObtenerResultadosLenguaje()
        {
            return ServicioPredictorLenguaje._misResultadosLenguaje;
        }

        public ResultadoLenguaje predecirLenguaje(string fraseEnIdioma)
        {
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Entrenamiento", "idiomas.tsv");

            var mlContext = new MLContext();

            var data = mlContext.Data.LoadFromTextFile<DatoLenguaje>(
                rutaArchivo, hasHeader: true); // obtengo el archivo para entrenar

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
                .Append(mlContext.Transforms.Text.FeaturizeText("Features", nameof(DatoLenguaje.Text)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))   // creo el pipeline usando clasificación
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var model = pipeline.Fit(data);  // entreno el pipeline con el archivo obtenido anteriormente y genero el modelo

            var predictor = mlContext.Model.CreatePredictionEngine<DatoLenguaje, PrediccionLenguaje>(model);   // creo el motor de predicción usando el modelo entrenado

            PrediccionLenguaje resultado = predictor.Predict(new DatoLenguaje { Text = fraseEnIdioma });  // genreo una predicción pasándole el string del usuario

            double porcentajeDeConfianza = Math.Round( (resultado.Score.Max() * 100), 4); // en este array de "score" obteno el valor máximo que es la clasificación 
                                                                                          // con mayor puntaje de acierto.

            return new ResultadoLenguaje(fraseEnIdioma, resultado.PredictedLabel, porcentajeDeConfianza); // retorno el resultado obtenido.
        }
    }
}
