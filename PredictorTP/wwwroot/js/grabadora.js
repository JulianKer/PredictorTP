// grabadora.js (versión reutilizable a futuro xd)

// Este script busca elementos en base a atributos "data-grabadora-input" y "data-grabadora-form"
// colocados directamente en los formularios.

document.addEventListener("DOMContentLoaded", () => {
    const startButton = document.getElementById("startButton");
    const stopButton = document.getElementById("stopButton");
    const status = document.getElementById("status");

    const form = document.querySelector("form[data-grabadora-input][data-grabadora-form]");

    if (!startButton || !stopButton || !status || !form) {
        console.error("Faltan elementos esenciales en el DOM o en el formulario");
        return;
    }

    const inputId = form.getAttribute("data-grabadora-input");
    const formId = form.getAttribute("data-grabadora-form");

    const inputTexto = document.getElementById(inputId);
    const formElement = document.getElementById(formId);

    if (!inputTexto || !formElement) {
        console.error("No se encontraron el input o el formulario con los IDs indicados");
        return;
    }

    let mediaRecorder;
    let audioChunks = [];

    startButton.onclick = async () => {
        try {
            const stream = await navigator.mediaDevices.getUserMedia({ audio: true });

            mediaRecorder = new MediaRecorder(stream, { mimeType: 'audio/webm' });
            audioChunks = [];
            mediaRecorder.start();

            mediaRecorder.ondataavailable = e => audioChunks.push(e.data);

            mediaRecorder.onstart = () => {
                status.textContent = "🎙️ Grabando...";
                startButton.classList.add("ocultar-icono");
                stopButton.classList.remove("ocultar-icono");
            };
      
            mediaRecorder.onstop = async () => {
                status.textContent = "⏳ Transcribiendo...";
                const audioBlob = new Blob(audioChunks, { type: 'audio/webm' });

                const formData = new FormData();
                formData.append('audioFile', audioBlob, 'grabacion.webm');

                try {
                    const response = await fetch('/Predictor/GenerarTextoDesdeAudio', {
                        method: 'POST',
                        body: formData
                    });

                    if (!response.ok) {
                        status.textContent = "❌ Error en la transcripción";
                        return;
                    }

                    const texto = await response.text();
                    inputTexto.value = texto;
                    formElement.submit();

                } catch (err) {
                    console.error("Error al transcribir:", err);
                    status.textContent = "❌ Falló la conexión al servidor";
                }

                stopButton.classList.add("ocultar-icono");
                startButton.classList.remove("ocultar-icono");
            };
        } catch (err) {
            console.error("Error al acceder al micrófono:", err);
            status.textContent = "❌ No se pudo acceder al micrófono";
        }
    };

    stopButton.onclick = () => {
        if (mediaRecorder && mediaRecorder.state !== "inactive") {
            mediaRecorder.stop();
        }
    };
});
