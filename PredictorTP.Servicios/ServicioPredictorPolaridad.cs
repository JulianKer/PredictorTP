using Microsoft.ML;
using PredictorTP.Entidades;

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
        private static readonly string modeloPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "modelo_polaridad.zip");
        private static readonly string datosPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Entrenamiento", "polaridad.tsv");

        private static List<ResultadoPolaridad> _misResultados = new();

        public ServicioPredictorPolaridad()
        {
            _mlContext = new MLContext();

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
            _misResultados.Add(resultadoAGuardar);
        }

        public List<ResultadoPolaridad> obtenerTodosLosResultados()
        {
            return _misResultados;
        }































        /*
        public static List<Resultado> misResultados { get; set; } = new List<Resultado>();

        public Resultado PredecirSentimiento(string texto)
        {
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Entrenamiento", "sentimientos.tsv");

            var mlContext = new MLContext();

            var data = mlContext.Data.LoadFromTextFile<DatoSentimiento>(
                rutaArchivo, hasHeader: true);

            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(DatoSentimiento.Text))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression("Label", "Features"));

            var model = pipeline.Fit(data); // con los datos extraidos del archvo .tsv, entreno al pipeline y obtengo el modelo entrenado

            var predictor = mlContext.Model.CreatePredictionEngine<DatoSentimiento, PrediccionSentimiento>(model); // creo un motor para predecir usando dicho modelo

            var resultado = predictor.Predict(new DatoSentimiento { Text = texto }); // realizo una predicción pasándole el string obtenido del usuario

            string prediccion = (resultado.Prediction ? "Positiva" : "Negativa");
            double porcentajePositivo = resultado.Probability * 100;   // con una sencilla operación puedo obtener el porcentaje de ambas polaridades (Positivo y Negativo)
            double porcentajeNegativo = 100 - porcentajePositivo;

            return new Resultado(texto, prediccion, porcentajeNegativo, porcentajePositivo); // devuelvo la respuesta obtenida
        }


        public void guardarResultado(Resultado resultadoAGuardar)
        {
            ServicioPredictorSentimiento.misResultados.Add(resultadoAGuardar);
        }

        public List<Resultado> obtenerTodosLosResultados()
        {
            return ServicioPredictorSentimiento.misResultados;
        }
        */
    }
}
