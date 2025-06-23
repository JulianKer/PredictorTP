using Microsoft.ML;
using PredictorTP.Entidades;
using PredictorTP.Repositorios;

namespace PredictorTP.Servicios
{

    public interface IServicioPredictorPolaridad
    {
        List<ResultadoPolaridad> obtenerTodosLosResultados();
        ResultadoPolaridad PredecirPolaridad(string texto);
        void guardarResultado(ResultadoPolaridad resultadoAGuardar);
    }

    public class ServicioPredictorPolaridad : IServicioPredictorPolaridad
    {

        private readonly MLContext _mlContext;
        private readonly PredictionEngine<DatoPolaridad, PrediccionPolaridad> _predEngine;

        private static readonly string modeloPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "modelo_polaridad.zip");
        private static readonly string datosPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "polaridad.tsv");
        private readonly IRepositorioPredictorPolaridad _repositorio;

        public ServicioPredictorPolaridad(IRepositorioPredictorPolaridad repositorio)
        {
            _mlContext = new MLContext();
            _repositorio = repositorio;

            // si no existe el modelo entrenado, lo genero y lo guardo
            if (!File.Exists(modeloPath))
            {
                var data = _mlContext.Data.LoadFromTextFile<DatoPolaridad>(datosPath, hasHeader: true);

                var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(DatoPolaridad.Text))
                    .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression("Label", "Features"));

                var modelo = pipeline.Fit(data);

                _mlContext.Model.Save(modelo, data.Schema, modeloPath);
            }

            // acá ya uso el modelo que cree recien o si ya existía, uso el que habia guardado en el .zip
            using var stream = new FileStream(modeloPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var loadedModel = _mlContext.Model.Load(stream, out _);

            _predEngine = _mlContext.Model.CreatePredictionEngine<DatoPolaridad, PrediccionPolaridad>(loadedModel);
        }

        public ResultadoPolaridad PredecirPolaridad(string texto)
        {
            var resultado = _predEngine.Predict(new DatoPolaridad { Text = texto });

            double porcentajePositivo = Math.Round(resultado.Probability * 100, 2);
            double porcentajeNegativo = 100 - porcentajePositivo;

            string prediccion;
            if (porcentajePositivo >= 30 && porcentajePositivo <= 70)
            {
                prediccion = "Dudoso";
            }
            else if (resultado.Prediction)
            {
                prediccion = "Positiva";
            }
            else
            {
                prediccion = "Negativa";
            }

            return new ResultadoPolaridad(texto, prediccion, porcentajeNegativo, porcentajePositivo);
        }

        public void guardarResultado(ResultadoPolaridad resultadoAGuardar)
        {
            _repositorio.GuardarResultadoPolaridad(resultadoAGuardar);
        }

        public List<ResultadoPolaridad> obtenerTodosLosResultados()
        {
            return _repositorio.GetResultadosPolaridad();
        }
    }
}
