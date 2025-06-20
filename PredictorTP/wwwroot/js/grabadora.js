let recorder;
let audioContext;

document.getElementById("startButton").onclick = async () => {
    const stream = await navigator.mediaDevices.getUserMedia({ audio: true });

    audioContext = new (window.AudioContext || window.webkitAudioContext)();
    const input = audioContext.createMediaStreamSource(stream);

    recorder = new Recorder(input, { numChannels: 1 });
    recorder.record();

    document.getElementById("status").textContent = "Grabando...";
    document.getElementById("stopButton").disabled = false;
    document.getElementById("startButton").disabled = true;
};

document.getElementById("stopButton").onclick = () => {
    recorder.stop();
    recorder.exportWAV(async (blob) => {
        document.getElementById("status").textContent = "Procesando...";

        const formData = new FormData();
        formData.append('audioFile', blob, 'grabacion.wav');

        const response = await fetch('/Transcripcion/UploadAudio', {
            method: 'POST',
            body: formData
        });

        const texto = await response.text();
        document.getElementById("transcript").textContent = texto;
        document.getElementById("status").textContent = "Transcripción completa";

        recorder.clear();
        document.getElementById("stopButton").disabled = true;
        document.getElementById("startButton").disabled = false;
    });
};
