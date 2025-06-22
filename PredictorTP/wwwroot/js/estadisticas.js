document.addEventListener('DOMContentLoaded', function () {
    let ctx = document.getElementById('grafico1').getContext('2d');

    let labels = window.chartLabels;
    let data = window.chartData;

    const grafico1 = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                label: 'Cantidad',
                data: data,
                backgroundColor: [
                    '#36A2EB', '#FF6384'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top'
                },
                title: {
                    display: true,
                    text: 'Géneros detectados en las imágenes.'
                }
            }
        }
    });


    labels = window.chartLabelsUsuariosAdmin;
    data = window.chartCantidadesUsuariosAdmin;
    ctx = document.getElementById('grafico3').getContext('2d');
    const grafico3 = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Cantidad',
                data: data,
                backgroundColor: [
                    'green', 'gray'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top'
                },
                title: {
                    display: true,
                    text: 'Cantidad de usuarios por rol.'
                }
            }
        }
    });


    labels = window.chartLabelsPersonasEmociones;
    data = window.chartCantidadesPersonasEmociones;
    ctx = document.getElementById('grafico4').getContext('2d');
    const grafico4 = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [{
                label: 'Cantidad',
                data: data,
                backgroundColor: [
                    '#9E9E9E', '#FFEB3B', '#3F51B5', '#F44336', '#BA68C8', '#8BC34A', '#4DD0E1'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top'
                },
                title: {
                    display: true,
                    text: 'Emociones detectadas en imágenes'
                }
            }
        }
    });



   


});
