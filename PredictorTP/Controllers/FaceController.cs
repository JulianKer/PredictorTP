using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PredictorTP.Entidades.ProcesarImagen;
using static System.Net.WebRequestMethods;

namespace PredictorTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaceController : ControllerBase
    {
        private readonly string endpoint = "https://face-api-julian.cognitiveservices.azure.com/";
        private readonly string subscriptionKey = "C8Y656yPBjwcqYZxQbExjbz4tigS3YUy3R7ybppATlcgk1Vk12LhJQQJ99BFACZoyfiXJ3w3AAAKACOGBt6I";

        [HttpPost("detectbase64")]
        public async Task<IActionResult> DetectFacesBase64([FromBody] ImageBase64Request request)
        {
            if (string.IsNullOrEmpty(request?.ImagenBase64))
                return BadRequest("La imagen es requerida.");

            byte[] imageBytes;
            try
            {
                imageBytes = Convert.FromBase64String(request.ImagenBase64);
            }
            catch
            {
                return BadRequest("Imagen en base64 inválida.");
            }

            string requestUrl = $"{endpoint}/face/v1.0/detect" +
                "?returnFaceId=true" +
                "&returnFaceLandmarks=false" +
                "&returnFaceAttributes=age,gender,smile" +
                "&recognitionModel=recognition_04" +
                "&detectionModel=detection_03";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                using (var content = new ByteArrayContent(imageBytes))
                {
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                    var response = await client.PostAsync(requestUrl, content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return Content(responseString, "application/json");
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, responseString);
                    }
                }
            }
        }
    }
    public class ImageBase64Request
    {
        public string ImagenBase64 { get; set; }
    }
}
