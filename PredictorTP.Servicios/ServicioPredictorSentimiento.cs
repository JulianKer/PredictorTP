using Microsoft.ML;
using PredictorTP.Entidades;

namespace PredictorTP.Servicios
{

    public interface IServicioPredictorSentimiento
    {
        List<Resultado> obtenerTodosLosResultados();
        Resultado PredecirSentimiento(string texto);
        void guardarResultado(Resultado resultadoAGuardar);
    }
    public class ServicioPredictorSentimiento : IServicioPredictorSentimiento
    {
        public static List<Resultado> misResultados { get; set; } = new List<Resultado>();

        public Resultado PredecirSentimiento(string texto)
        {
            var mlContext = new MLContext();

            var data = mlContext.Data.LoadFromTextFile<DatoSentimiento>(
                "C:\\Users\\germa\\source\\repos\\PredictorTP\\PredictorTP.Servicios\\Entrenamiento\\sentimientos.tsv", hasHeader: true);

            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(DatoSentimiento.Text))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression("Label", "Features"));

            var model = pipeline.Fit(data); //con esto lo entreno, osea antes obtuve el tsv, le extraje los datos y ahora lo entreno 

            var predictor = mlContext.Model.CreatePredictionEngine<DatoSentimiento, PrediccionSentimiento>(model); // este es el motor para usarlo para predecir

            var resultado = predictor.Predict(new DatoSentimiento { Text = texto });

            string prediccion = (resultado.Prediction ? "Positiva" : "Negativa");
            double porcentajePositivo = resultado.Probability * 100;
            double porcentajeNegativo = 100 - porcentajePositivo;

            return new Resultado(texto, prediccion, porcentajeNegativo, porcentajePositivo);
        }


        public void guardarResultado(Resultado resultadoAGuardar)
        {
            ServicioPredictorSentimiento.misResultados.Add(resultadoAGuardar);
        }

        public List<Resultado> obtenerTodosLosResultados()
        {
            return ServicioPredictorSentimiento.misResultados;
        }

    }
}
