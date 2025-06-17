using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PredictorTP.Entidades.ProcesarImagen;
using Azure;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace PredictorTP.Servicios
{
    public interface IFaceEmotionService
    {
        Task<AnalisisFacialResultado> AnalizarDesdeStreamAsync(Stream stream);
    }

    public class FaceEmotionService : IFaceEmotionService
    {
        private readonly IFaceClient _faceClient;

        public FaceEmotionService(IConfiguration config)
        {
            _faceClient = new FaceClient(new ApiKeyServiceClientCredentials(
                "C8Y656yPBjwcqYZxQbExjbz4tigS3YUy3R7ybppATlcgk1Vk12LhJQQJ99BFACZoyfiXJ3w3AAAKACOGBt6I"))
            {
                Endpoint = "https://face-api-julian.cognitiveservices.azure.com"
            };
        }

        public async Task<AnalisisFacialResultado> AnalizarDesdeStreamAsync(Stream stream)
        {
            FaceAttributeType[] atributos = new FaceAttributeType[] { FaceAttributeType.Emotion };
            var rostros = await _faceClient.Face.DetectWithStreamAsync(
                stream,
                returnFaceAttributes: atributos);

            var resultado = new AnalisisFacialResultado();

            foreach (var rostro in rostros)
            {
                var e = rostro.FaceAttributes.Emotion;
                var lista = new[]
                {
                ("Alegría", e.Happiness),
                ("Tristeza", e.Sadness),
                ("Enojo", e.Anger),
                ("Desprecio", e.Contempt),
                ("Miedo", e.Fear),
                ("Sorpresa", e.Surprise),
                ("Neutral", e.Neutral),
                ("Desagrado", e.Disgust)
            };

                var principal = lista.OrderByDescending(x => x.Item2).First();
                resultado.Emociones.Add($"{principal.Item1} ({principal.Item2:P1})");

                resultado.Rostros.Add(new RectanguloRostro
                {
                    Left = rostro.FaceRectangle.Left,
                    Top = rostro.FaceRectangle.Top,
                    Width = rostro.FaceRectangle.Width,
                    Height = rostro.FaceRectangle.Height
                });
            }

            return resultado;
        }
    }

}
