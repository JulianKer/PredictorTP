using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PredictorTP.Servicios;
using System.IO;
using System.Threading.Tasks;

namespace PredictorTP.Web.Controllers
{
    public class TranscripcionController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ITranscripcionAudio _transcripcionAudio;

        public TranscripcionController(IWebHostEnvironment env, ITranscripcionAudio transcripcionAudio)
        {
            _env = env;
            _transcripcionAudio = transcripcionAudio;
        }

        public IActionResult Grabar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadAudio(IFormFile audioFile)
        {
            if (audioFile == null || audioFile.Length == 0)
                return BadRequest("Archivo vacío");
            // Crear directorio temporal para almacenar el archivo de audio
            var rutaTemp = Path.Combine(_env.ContentRootPath, "TempAudio");
            Directory.CreateDirectory(rutaTemp);

            var filePath = Path.Combine(rutaTemp, "grabacion.wav");

            // Guardar el archivo de audio en el directorio temporal
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await audioFile.CopyToAsync(stream);
            }

            // Llamada al papi whisper
            var texto = await _transcripcionAudio.TranscribirAsync(filePath);
            return Ok(texto);
        }
    }
}
