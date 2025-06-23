function obtenerEmocionEnEspaniol(emocion) {
    switch (emocion) {
        case "neutral": return "Neutral";
        case "happy": return "Feliz";
        case "sad": return "Triste";
        case "angry": return "Enojado";
        case "fearful": return "Asustado";
        case "disgusted": return "Disgustado";
        case "surprised": return "Sorprendido";
        default: return emocion;
    }
}



let ultimasDetecciones = []

document.addEventListener('DOMContentLoaded', () => {
    document.querySelector('#botonCapturarFoto').addEventListener('click', function (e) {
        e.preventDefault();
        capturarFoto();
    });
});

function capturarFoto() {
    const video = document.getElementById('video');
    const canvas = document.createElement('canvas');
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;
    const ctx = canvas.getContext('2d');
    ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

    let personas = [];

    if (ultimasDetecciones.length >= 0) {

        ultimasDetecciones.forEach((det, i) => {
            i++
            const age = Math.round(det.age);
            const gender = det.gender == "male" ? "Hombre" : "Mujer";
            const genderProb = (det.genderProbability * 100).toFixed(0);
            const expr = det.expressions;
            let emocion = Object.entries(expr).sort((a, b) => b[1] - a[1])[0][0];

            emocion = obtenerEmocionEnEspaniol(emocion);
            
            const texto = `👤 Persona ${i} = ${gender}: ${genderProb}% | Edad: ${age} | Emoción: ${emocion}`;
            personas.push(texto);
        });

        //fetch 
        const base64 = canvas.toDataURL('image/png').split(',')[1];
        console.log("ANTES DEL FETCH")
        fetch('/Predictor/Analizar', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(
                {
                    imagenBase64: base64,
                    personas: personas
                }
            )
        })
            .then(async resp => {
                const data = await resp.json();

                if (!resp.ok) {
                    throw new Error(data.mensaje || "Error desconocido del backend."); //por si algo falló, te mando este msj.
                }
                console.log("ANTES DEL DATA")
                console.log(data)

                const historial = document.getElementById('historial-capturas');
                historial.innerHTML = ''; 

                data.forEach(r => {
                    const item = document.createElement('div');
                    item.className = 'item-historial';

                    const img = document.createElement('img');
                    img.src = "/img/imgs_users/" + r.rutaImg; 

                    const infoDiv = document.createElement('div');
                    infoDiv.className = 'info-textos';

                    r.personaDetectada.forEach(personaDetectada => { 
                        let p = document.createElement('p');
                        p.textContent = personaDetectada.descripcionPersona; 
                        infoDiv.appendChild(p);
                    });

                    item.appendChild(img);
                    item.appendChild(infoDiv);

                    historial.appendChild(item);
                });
            })
            .catch(err => {
                console.log("EN EL CATcHHHHHH")
                console.error("Error al enviar la imagen:", err.message);
                alert("Hubo un error: " + err.message);
            });
    }
}






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

        ultimasDetecciones = detecciones;

        const redimensionadas = faceapi.resizeResults(detecciones, displaySize);
        const ctx = canvas.getContext("2d");
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        redimensionadas.forEach((det, i) => {
            i++
            const box = det.detection.box;
            
            const age = Math.round(det.age);
            const gender = det.gender;  
            const genderProb = (det.genderProbability * 100).toFixed(0);    //----> acá le comento esto pero para que quede, al sacarlo, solo hago que el recuadro sea más fluido y para saber estos
                                                                            //----> datos, tenés que capturar y sacar una foto
            const expr = det.expressions;
            const emocion = Object.entries(expr).sort((a, b) => b[1] - a[1])[0][0];
            

            ctx.strokeStyle = "#FF0000";
            ctx.lineWidth = 4;
            ctx.strokeRect(box.x, box.y, box.width, box.height);

            ctx.fillStyle = "#FF0000"; // si queiro modificar estilos del recuadro o fuente, cambiar esto
            ctx.font = "30px Arial";
            //const texto = `${gender} (${genderProb}%), Edad: ${age}, Emoción: ${emocion}`;  ---> esta línea mostraba en vivo arriba del recuadrito, los datos de las personas, pero mejor lo movimos para que los muestre al tomar la foto
            const texto = `Persona ${i}`;
            ctx.fillText(texto, box.x, box.y > 20 ? box.y - 8 : box.y + 20);
        });

        resultado.innerText = `Caras detectadas: ${redimensionadas.length}`;
    }, 100);
}

async function main() {
    await cargarModelos();
    await iniciarCamara();
    detectar();
}

window.addEventListener("load", main);