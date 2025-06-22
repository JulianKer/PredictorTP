using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using PredictorTP.Entidades;
using PredictorTP.Servicios;


public interface IServicioPredictorIdioma
{
    ResultadoIdioma predecirIdioma(string fraseEnIdioma);
    void guardarResultdoIdioma(ResultadoIdioma nuevoResultadoLenguaje);
    List<ResultadoIdioma> ObtenerResultadosIdioma();
}


public class ServicioPredictorIdioma : IServicioPredictorIdioma
{
    private readonly MLContext _mlContext;
    private readonly PredictionEngine<DatoIdioma, PrediccionIdioma> _predEngine;
    private static readonly string modeloPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "modelo_idiomas.zip");
    private static readonly string datosPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Entrenamiento", "idiomas.tsv");

    private static List<ResultadoIdioma> _misResultadosLenguaje = new();

    public ServicioPredictorIdioma()
    {
        _mlContext = new MLContext();

        // si NO tengo guardado el modelo en un zip, lo creo, lo entreno y lo guardo en un .zip
        if (!File.Exists(modeloPath))
        {
            var data = _mlContext.Data.LoadFromTextFile<DatoIdioma>(datosPath, hasHeader: true);

            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label")
                .Append(_mlContext.Transforms.Text.FeaturizeText("Features", nameof(DatoIdioma.Text)))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var modelo = pipeline.Fit(data);

            _mlContext.Model.Save(modelo, data.Schema, modeloPath);
        }

        // si ya existe o bien ya se creo en el if anterior, lo busco para usarlo
        using var stream = new FileStream(modeloPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var loadedModel = _mlContext.Model.Load(stream, out _);

        _predEngine = _mlContext.Model.CreatePredictionEngine<DatoIdioma, PrediccionIdioma>(loadedModel);
    }

    public ResultadoIdioma predecirIdioma(string fraseEnIdioma)
    {
        var resultado = _predEngine.Predict(new DatoIdioma { Text = fraseEnIdioma });
        double confianza = Math.Round(resultado.Score.Max() * 100, 4);

        return new ResultadoIdioma(fraseEnIdioma, resultado.PredictedLabel, confianza);
    }

    public void guardarResultdoIdioma(ResultadoIdioma nuevoResultadoLenguaje)
    {
        _misResultadosLenguaje.Add(nuevoResultadoLenguaje);
    }

    public List<ResultadoIdioma> ObtenerResultadosIdioma()
    {
        return ServicioPredictorIdioma._misResultadosLenguaje;
    }
}

