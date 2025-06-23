let popup = document.querySelector(".pop-up-fondo");
let btn = document.getElementById("btnMostrarPopup");
let btnCancelar = document.getElementById("btn-cancelar-eliminacion");


if (btn) {
    btn.addEventListener("click", () => {
        popup.style.display = "flex";
        popup.classList.toggle("mostrar");
    })
}

if (btnCancelar) {
    btnCancelar.addEventListener("click", () => {
        popup.style.display = "none";
        popup.classList.toggle("mostrar");
    })
}