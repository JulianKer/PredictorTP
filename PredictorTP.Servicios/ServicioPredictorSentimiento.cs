using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using PredictorTP.Entidades;
using PredictorTP.Servicios;


public interface IServicioPredictorSentimiento
{
    ResultadoSentimiento predecirSentimiento(string fraseConSentimiento);
    void guardarResultdoSentimiento(ResultadoSentimiento nuevoResultadoSentimiento);
    List<ResultadoSentimiento> ObtenerResultadosSentimiento();
}


public class ServicioPredictorSentimiento : IServicioPredictorSentimiento
{
    private readonly MLContext _mlContext;
    private readonly PredictionEngine<DatoSentimiento, PrediccionIdioma> _predEngine;
    private static readonly string modeloPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "modelo_sentimiento.zip");
    private static readonly string datosPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Entrenamiento", "sentimiento.tsv");

    private static List<ResultadoSentimiento> _misResultadosLenguaje = new();

    public ServicioPredictorSentimiento()
    {
        _mlContext = new MLContext();

        // si NO tengo guardado el modelo en un zip, lo creo, lo entreno y lo guardo en un .zip
        if (!File.Exists(modeloPath))
        {
            var data = _mlContext.Data.LoadFromTextFile<DatoSentimiento>(datosPath, hasHeader: true);

            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label")
                .Append(_mlContext.Transforms.Text.FeaturizeText("Features", nameof(DatoSentimiento.Text)))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var modelo = pipeline.Fit(data);

            _mlContext.Model.Save(modelo, data.Schema, modeloPath);
        }

        // si ya existe o bien ya se creo en el if anterior, lo busco para usarlo
        using var stream = new FileStream(modeloPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var loadedModel = _mlContext.Model.Load(stream, out _);

        _predEngine = _mlContext.Model.CreatePredictionEngine<DatoSentimiento, PrediccionIdioma>(loadedModel);
    }

    public ResultadoSentimiento predecirSentimiento(string fraseConSentimiento)
    {
        var resultado = _predEngine.Predict(new DatoSentimiento { Text = fraseConSentimiento });
        double confianza = Math.Round(resultado.Score.Max() * 100, 4);
        foreach (var valor in resultado.Score)
        {
            Console.WriteLine(valor);
        }
        return new ResultadoSentimiento(fraseConSentimiento, resultado.PredictedLabel, confianza);
    }

    public void guardarResultdoSentimiento(ResultadoSentimiento nuevoResultadoLenguaje)
    {
        _misResultadosLenguaje.Add(nuevoResultadoLenguaje);
    }

    public List<ResultadoSentimiento> ObtenerResultadosSentimiento()
    {
        return ServicioPredictorSentimiento._misResultadosLenguaje;
    }
}

