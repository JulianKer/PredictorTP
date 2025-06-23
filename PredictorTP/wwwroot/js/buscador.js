let mic = document.getElementById("mic");
let stop_mic = document.getElementById("stop");
let enviar = document.getElementById("enviar");

mic.addEventListener("click", () => {
    mic.classList.add("ocultar-icono");
    mic.classList.remove("mostrar-icono");

    enviar.classList.add("ocultar-icono");
    enviar.classList.remove("mostrar-icono");


    stop_mic.classList.remove("ocultar-icono");
    stop_mic.classList.add("mostrar-icono");
});

stop_mic.addEventListener("click", () => {
    stop_mic.classList.add("ocultar-icono");
    stop_mic.classList.remove("mostrar-icono");

    enviar.classList.add("mostrar-icono");
    enviar.classList.remove("ocultar-icono");

    mic.classList.remove("ocultar-icono");
    mic.classList.add("mostrar-icono");
});
