const video = document.getElementById('camara');
const canvas = document.getElementById('foto');
const context = canvas.getContext('2d');
const capturarBtn = document.getElementById('capturar');
const emocionesUl = document.getElementById('emociones');

// Acceso a cámara
navigator.mediaDevices.getUserMedia({ video: true })
    .then(stream => video.srcObject = stream)
    .catch(err => console.error("No se pudo acceder a la cámara:", err));

capturarBtn.addEventListener('click', () => {
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;
    context.drawImage(video, 0, 0, canvas.width, canvas.height);

    const base64 = canvas.toDataURL('image/png').split(',')[1];

    fetch('/Predictor/Analizar', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ imagenBase64: base64 })
    })
        .then(async resp => {
            const data = await resp.json();

            if (!resp.ok) {
                throw new Error(data.mensaje || "Error desconocido del backend");
            }

            // Si todo está bien:
            data.rostros.forEach(r => {
                context.strokeStyle = 'red';
                context.lineWidth = 2;
                context.strokeRect(r.left, r.top, r.width, r.height);
            });

            emocionesUl.innerHTML = '';
            data.emociones.forEach(e => {
                const li = document.createElement('li');
                li.textContent = e;
                emocionesUl.appendChild(li);
            });

            console.log("Imagen guardada en:", data.imagenGuardada);
        })
        .catch(err => {
            console.error("Error al enviar la imagen:", err.message);
            alert("Hubo un error: " + err.message);
        });

});
