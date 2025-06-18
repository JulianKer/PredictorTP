

const video = document.getElementById("video");
const canvas = document.getElementById("canvas");
const resultado = document.getElementById("resultado");

const MODEL_URL = "https://cdn.jsdelivr.net/gh/vladmandic/face-api/model/";

async function cargarModelos() {
    await faceapi.nets.tinyFaceDetector.loadFromUri(MODEL_URL);
    await faceapi.nets.ageGenderNet.loadFromUri(MODEL_URL);
    await faceapi.nets.faceExpressionNet.loadFromUri(MODEL_URL);
    resultado.innerText = "Modelos cargados. Iniciando cámara...";
}

async function iniciarCamara() {
    try {
        const stream = await navigator.mediaDevices.getUserMedia({ video: true });
        video.srcObject = stream;

        return new Promise(resolve => {
            video.onloadedmetadata = () => {
                video.play();
                resolve();
            };
        });
    } catch (error) {
        resultado.innerText = "Error al acceder a la cámara";
        console.error(error);
    }
}

async function detectar() {
    const opciones = new faceapi.TinyFaceDetectorOptions({ inputSize: 320 });

    const displaySize = { width: video.videoWidth, height: video.videoHeight };
    canvas.width = displaySize.width;
    canvas.height = displaySize.height;
    faceapi.matchDimensions(canvas, displaySize);

    setInterval(async () => {
        const detecciones = await faceapi
            .detectAllFaces(video, opciones)
            .withAgeAndGender()
            .withFaceExpressions();

        window.ultimasDetecciones = [];

        const redimensionadas = faceapi.resizeResults(detecciones, displaySize);
        const ctx = canvas.getContext("2d");
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        redimensionadas.forEach(det => {
            const box = det.detection.box;
            const age = Math.round(det.age);
            const gender = det.gender;
            const genderProb = (det.genderProbability * 100).toFixed(0);

            const expr = det.expressions;
            const emocion = Object.entries(expr).sort((a, b) => b[1] - a[1])[0][0];

            ctx.strokeStyle = "#FF0000";
            ctx.lineWidth = 2;
            ctx.strokeRect(box.x, box.y, box.width, box.height);

            ctx.fillStyle = "#FF0000"; // si queiro modificar estilos del recuadro o fuente, cambiar esto
            ctx.font = "20px Arial";
            const texto = `${gender} (${genderProb}%), Edad: ${age}, Emoción: ${emocion}`;
            ctx.fillText(texto, box.x, box.y > 20 ? box.y - 8 : box.y + 20);
        });

        resultado.innerText = `Caras detectadas: ${redimensionadas.length}`;
    }, 350);
}

async function main() {
    await cargarModelos();
    await iniciarCamara();
    detectar();
}

window.addEventListener("load", main);

