



/* --------------------------------- IDIOMAS   -------------------------------------*/
document.getElementById("idioma-form").addEventListener("submit", async function (e) {
    e.preventDefault();

    const form = e.target;
    const formData = new FormData(form);

    try {
        const response = await fetch("/Entrenar/ModeloIdioma", {
            method: "POST",
            body: formData
        });

        if (!response.ok) {
            document.getElementById("resultado-idioma").innerText = "Ocurrió un error al procesar el archivo.";
            return;
        }

        const data = await response.json();

        document.getElementById("resultado-idioma").innerHTML =
            "Frases totales: " + data.total +
            " | Frases aceptadas: " + data.coincidencias +
            " | Frases rechazadas: " + data.noCoincidencias;

        const tablaBody = document.querySelector("#tabla-datos-idioma tbody");
        tablaBody.innerHTML = "";

        const totales = data.frasesTotales || [];
        const coincidencias = data.frasesCoincidencias || [];
        const noCoincidencias = data.frasesNoCoincidencias || [];

        const esIgual = (a, b) => a._fraseEnIdioma === b._fraseEnIdioma && a._idioma === b._idioma;

        totales.forEach(item => {
            const fila = document.createElement("tr");
            const tdTexto = document.createElement("td");
            const tdLabel = document.createElement("td");

            tdTexto.textContent = item._fraseEnIdioma;
            console.log(item._fraseEnIdioma);
            tdLabel.textContent = item._idioma;
            console.log(item._idioma);

            if (coincidencias.some(c => esIgual(c, item))) {
                fila.classList.add("coincidencia");
            } else if (noCoincidencias.some(nc => esIgual(nc, item))) {
                fila.classList.add("no-coincidencia");
            }

            fila.appendChild(tdTexto);
            fila.appendChild(tdLabel);
            tablaBody.appendChild(fila);
        });

    } catch (error) {
        console.error("Error:", error);
        document.getElementById("resultado-idioma").innerText = "Error de red o del servidor.";
    }
});
/* -----------------------------------------------------------------------------------------*/









/* --------------------------------- IDIOMAS   -------------------------------------*/
document.getElementById("sentimiento-form").addEventListener("submit", async function (e) {
    e.preventDefault();

    const form = e.target;
    const formData = new FormData(form);

    try {
        const response = await fetch("/Entrenar/ModeloSentimiento", {
            method: "POST",
            body: formData
        });

        if (!response.ok) {
            document.getElementById("resultado-sentimiento").innerText = "Ocurrió un error al procesar el archivo.";
            return;
        }

        const data = await response.json();

        document.getElementById("resultado-sentimiento").innerHTML =
            "Frases totales: " + data.total +
            " | Frases aceptadas: " + data.coincidencias +
            " | Frases rechazadas: " + data.noCoincidencias;

        const tablaBody = document.querySelector("#tabla-datos-sentimientos tbody");
        tablaBody.innerHTML = "";

        const totales = data.frasesTotales || [];
        const coincidencias = data.frasesCoincidencias || [];
        const noCoincidencias = data.frasesNoCoincidencias || [];

        const esIgual = (a, b) => a._fraseConSentimiento === b._fraseConSentimiento && a._sentimiento === b._sentimiento;

        totales.forEach(item => {
            const fila = document.createElement("tr");
            const tdTexto = document.createElement("td");
            const tdLabel = document.createElement("td");

            tdTexto.textContent = item._fraseConSentimiento;
            tdLabel.textContent = item._sentimiento;

            if (coincidencias.some(c => esIgual(c, item))) {
                fila.classList.add("coincidencia");
            } else if (noCoincidencias.some(nc => esIgual(nc, item))) {
                fila.classList.add("no-coincidencia");
            }

            fila.appendChild(tdTexto);
            fila.appendChild(tdLabel);
            tablaBody.appendChild(fila);
        });

    } catch (error) {
        console.error("Error:", error);
        document.getElementById("resultado-sentimiento").innerText = "Error de red o del servidor.";
    }
});
/* -----------------------------------------------------------------------------------------*/










/* --------------------------------- IDIOMAS   -------------------------------------*/
document.getElementById("polaridad-form").addEventListener("submit", async function (e) {
    e.preventDefault();

    const form = e.target;
    const formData = new FormData(form);

    try {
        const response = await fetch("/Entrenar/ModeloPolaridad", {
            method: "POST",
            body: formData
        });

        if (!response.ok) {
            document.getElementById("resultado-polaridad").innerText = "Ocurrió un error al procesar el archivo.";
            return;
        }

        const data = await response.json();

        document.getElementById("resultado-polaridad").innerHTML =
            "Frases totales: " + data.total +
            " | Frases aceptadas: " + data.coincidencias +
            " | Frases rechazadas: " + data.noCoincidencias;

        const tablaBody = document.querySelector("#tabla-datos-polaridad tbody");
        tablaBody.innerHTML = "";

        const totales = data.frasesTotales || [];
        const coincidencias = data.frasesCoincidencias || [];
        const noCoincidencias = data.frasesNoCoincidencias || [];

        const esIgual = (a, b) => a._textoProcesado === b._textoProcesado && a._resutlado === b._resutlado;

        totales.forEach(item => {
            const fila = document.createElement("tr");
            const tdTexto = document.createElement("td");
            const tdLabel = document.createElement("td");

            tdTexto.textContent = item._textoProcesado;
            tdLabel.textContent = item._resutlado;

            if (coincidencias.some(c => esIgual(c, item))) {
                fila.classList.add("coincidencia");
            } else if (noCoincidencias.some(nc => esIgual(nc, item))) {
                fila.classList.add("no-coincidencia");
            }

            fila.appendChild(tdTexto);
            fila.appendChild(tdLabel);
            tablaBody.appendChild(fila);
        });

    } catch (error) {
        console.error("Error:", error);
        document.getElementById("resultado-polaridad").innerText = "Error de red o del servidor.";
    }
});
/* -----------------------------------------------------------------------------------------*/
