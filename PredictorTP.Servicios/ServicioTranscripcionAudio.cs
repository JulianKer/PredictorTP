﻿using Whisper.net;
using Whisper.net.Ggml;
using System.Diagnostics;

namespace PredictorTP.Servicios
{
    public interface ITranscripcionAudio
    {
        Task<string> TranscribirAsync(string filePath);
    }

    public class ServicioTranscripcionAudio : ITranscripcionAudio
    {
        private const string Modelo = "ggml-medium.bin";

        public async Task<string> TranscribirAsync(string filePath)
        {
            if (!File.Exists(Modelo))
            {
                using var modelStream = await WhisperGgmlDownloader.Default.GetGgmlModelAsync(GgmlType.Medium);
                using var fileWriter = File.OpenWrite(Modelo);
                await modelStream.CopyToAsync(fileWriter);
            }

            var convertedPath = Path.Combine(Path.GetDirectoryName(filePath), "converted.wav");

            var ffmpeg = new ProcessStartInfo
            {
                FileName = @"C:\ffmpeg\ffmpeg\bin\ffmpeg.exe",
                Arguments = $"-y -i \"{filePath}\" -ar 16000 -ac 1 -c:a pcm_s16le \"{convertedPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };


            using (var process = Process.Start(ffmpeg))
            {
                string errorOutput = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    throw new Exception("Error al convertir el archivo WAV a 16kHz con ffmpeg:\n" + errorOutput);

                }
                Console.WriteLine("FFmpeg error output:\n" + errorOutput);

            }

            using var factory = WhisperFactory.FromPath(Modelo);
            using var processor = factory.CreateBuilder()
                                         //.WithLanguage("es")
                                         .WithLanguageDetection()
                                         .Build();

            using var audioStream = File.OpenRead(convertedPath);
            string texto = "";

            await foreach (var segment in processor.ProcessAsync(audioStream))
            {
                texto += segment.Text;
            }

            try { File.Delete(convertedPath); } catch { }


            var txtFolder = Path.Combine(Directory.GetCurrentDirectory(), "TempTxt");
            Directory.CreateDirectory(txtFolder);

            var txtFilePath = Path.Combine(txtFolder, $"transcripcion_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
            await File.WriteAllTextAsync(txtFilePath, texto);

            return texto;
        }
    }
}
