using Microsoft.ML;
using PredictorTP.Entidades;

namespace PredictorTP.Servicios
{

    public interface IServicioPredictor
    {
        List<Resultado> obtenerTodosLosResultados();
        Resultado predecir(string texto);
        void guardarResultado(Resultado resultadoAGuardar);
    }
    public class ServicioPredictor : IServicioPredictor
    {
        public static List<Resultado> misResultados { get; set; } = new List<Resultado>();

        public ServicioPredictor()
        {
            /*if (ServicioPredictor.misResultados == null)
            {
                ServicioPredictor.misResultados = new List<Resultado>();
            }*/
        }
        public Resultado predecir(string texto)
        {
            var mlContext = new MLContext();

            var data = mlContext.Data.LoadFromTextFile<DatoSentimiento>(
                "C:\\Users\\germa\\source\\repos\\PredictorTP\\PredictorTP.Servicios\\Entrenamiento\\sentimientos.tsv", hasHeader: true);

            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(DatoSentimiento.Text))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression("Label", "Features"));

            var model = pipeline.Fit(data); //con esto lo entreno, osea antes obtuve el tsv, le extraje los datos y ahora lo entreno 

            var predictor = mlContext.Model.CreatePredictionEngine<DatoSentimiento, PrediccionSentimiento>(model); // este es el motor para usarlo para predecir


            /*
                Console.Write("\nEscribe una frase para analizar (o 'salir'): ");
                var texto = Console.ReadLine();
                if (texto?.ToLower() == "salir") break;
                var resultado = predictor.Predict(new SentimentData { Text = texto });
                Console.WriteLine($"Predicción: {(resultado.Prediction ? "Positivo" : "Negativo")}, Probabilidad: {resultado.Probability:P2}");
                */

            var resultado = predictor.Predict(new DatoSentimiento { Text = texto });

            string prediccion = (resultado.Prediction ? "Positiva" : "Negativa");
            double porcentajePositivo = resultado.Probability * 100;
            double porcentajeNegativo = 100 - porcentajePositivo;

            return new Resultado(texto, prediccion, porcentajeNegativo, porcentajePositivo);
        }


        public void guardarResultado(Resultado resultadoAGuardar)
        {
            ServicioPredictor.misResultados.Add(resultadoAGuardar);
        }

        public List<Resultado> obtenerTodosLosResultados()
        {
            return ServicioPredictor.misResultados;
        }

    }
}
