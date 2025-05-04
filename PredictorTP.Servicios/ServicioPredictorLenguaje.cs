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
            
            var mlContext = new MLContext();

            var data = mlContext.Data.LoadFromTextFile<DatoLenguaje>(
                "C:\\Users\\germa\\source\\repos\\PredictorTP\\PredictorTP.Servicios\\Entrenamiento\\idiomas.tsv", hasHeader: true);

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
                .Append(mlContext.Transforms.Text.FeaturizeText("Features", nameof(DatoLenguaje.Text)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var model = pipeline.Fit(data);

            var predictor = mlContext.Model.CreatePredictionEngine<DatoLenguaje, PrediccionLenguaje>(model);
            
            var resultado = predictor.Predict(new DatoLenguaje { Text = fraseEnIdioma });
            double porcentajeDeConfianza = Math.Round( (resultado.Score.Max() * 100), 4);

            return new ResultadoLenguaje(fraseEnIdioma, resultado.PredictedLabel, porcentajeDeConfianza);
        }
    }
}
