### DevNotes para usar servicio de WHISPER 

### Requisitos previos

## Instalar Dependencias 
```
https://www.gyan.dev/ffmpeg/builds/
```
En la seccion de "Release builds" descargar el archivo `ffmpeg-realese-full.7z` y descomprimirlo en una carpeta de tu eleccion.

Copiar el path del archivo `ffmpeg.exe` y pegarlo en el archivo `ServicioTranscripcionAudio` en la variable `FILENAME`.

```
 FileName = @"C:\ffmpeg\ffmpeg\bin\ffmpeg.exe",
```

### Como funciona?
## No lo se tu dime deau.

1. El servicio se ejecuta en segundo plano y escucha las peticiones de transcripcion de audio.
2. Cuando recibe una peticion, se encarga de transcribir el audio a texto utilizando el servicio de WHISPER.
3. Una vez que la transcripcion esta completa, se guarda el resultado en un archivo de texto.

### Documentacion 

## Recorder (esto despues borro a este archivo quedo deprecado)
https://github.com/mattdiamond/Recorderjs/blob/master/dist/recorder.js

## WHISPER 
https://github.com/sandrohanea/whisper.net
https://www.nuget.org/packages/Whisper.net/1.7.0

## FFmpeg 
https://ffmpeg.org/documentation.html