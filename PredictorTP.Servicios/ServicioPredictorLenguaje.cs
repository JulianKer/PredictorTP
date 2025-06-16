using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using PredictorTP.Entidades;
using PredictorTP.Servicios;


public interface IServicioPredictorLenguaje
{
    ResultadoLenguaje predecirLenguaje(string fraseEnIdioma);
    void guardarResultdoLenguaje(ResultadoLenguaje nuevoResultadoLenguaje);
    List<ResultadoLenguaje> ObtenerResultadosLenguaje();
}


public class ServicioPredictorLenguaje : IServicioPredictorLenguaje
{
    private readonly MLContext _mlContext;
    private readonly PredictionEngine<DatoLenguaje, PrediccionLenguaje> _predEngine;
    private static readonly string modeloPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "modelo_idiomas.zip");
    private static readonly string datosPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Entrenamiento", "idiomas.tsv");

    private static List<ResultadoLenguaje> _misResultadosLenguaje = new();

    public ServicioPredictorLenguaje()
    {
        _mlContext = new MLContext();

        // si NO tengo guardado el modelo en un zip, lo creo, lo entreno y lo guardo en un .zip
        if (!File.Exists(modeloPath))
        {
            var data = _mlContext.Data.LoadFromTextFile<DatoLenguaje>(datosPath, hasHeader: true);

            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label")
                .Append(_mlContext.Transforms.Text.FeaturizeText("Features", nameof(DatoLenguaje.Text)))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var modelo = pipeline.Fit(data);

            _mlContext.Model.Save(modelo, data.Schema, modeloPath);
        }

        // si ya existe o bien ya se creo en el if anterior, lo busco para usarlo
        using var stream = new FileStream(modeloPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var loadedModel = _mlContext.Model.Load(stream, out _);

        _predEngine = _mlContext.Model.CreatePredictionEngine<DatoLenguaje, PrediccionLenguaje>(loadedModel);
    }

    public ResultadoLenguaje predecirLenguaje(string fraseEnIdioma)
    {
        var resultado = _predEngine.Predict(new DatoLenguaje { Text = fraseEnIdioma });
        double confianza = Math.Round(resultado.Score.Max() * 100, 4);

        return new ResultadoLenguaje(fraseEnIdioma, resultado.PredictedLabel, confianza);
    }

    public void guardarResultdoLenguaje(ResultadoLenguaje nuevoResultadoLenguaje)
    {
        _misResultadosLenguaje.Add(nuevoResultadoLenguaje);
    }

    public List<ResultadoLenguaje> ObtenerResultadosLenguaje()
    {
        return ServicioPredictorLenguaje._misResultadosLenguaje;
    }
}

