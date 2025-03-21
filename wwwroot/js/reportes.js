// wwwroot/js/reportes.js
window.generarPDF = function () {
    // Obtener el contenido que queremos convertir
    const element = document.getElementById('reporteParaImprimir');

    if (!element) {
        console.error('Elemento no encontrado');
        return;
    }

    // Crea una copia del elemento para manipularlo sin afectar la visualización
    const clonedElement = element.cloneNode(true);
    clonedElement.style.width = '1100px'; // Ancho fijo para controlar el formato
    document.body.appendChild(clonedElement);
    clonedElement.style.position = 'absolute';
    clonedElement.style.left = '-9999px';

    // Usamos html2canvas para crear una imagen del contenido
    html2canvas(clonedElement, {
        scale: 1,
        useCORS: true,
        logging: true
    }).then(canvas => {
        // Limpia el elemento clonado
        document.body.removeChild(clonedElement);

        const imgData = canvas.toDataURL('image/png');
        const pdf = new window.jspdf.jsPDF('l', 'mm', 'a4'); // Orientación landscape

        // Calculamos las dimensiones
        const imgWidth = 277; // Ancho A4 landscape (297-20)
        const imgHeight = canvas.height * imgWidth / canvas.width;

        // Añadimos la imagen al PDF
        pdf.addImage(imgData, 'PNG', 10, 10, imgWidth, imgHeight);

        // Obtenemos fecha para el nombre del archivo
        const fecha = new Date().toISOString().slice(0, 10);
        const titulo = document.querySelector('.modal-title').textContent || 'Reporte';

        // Descargamos el PDF
        pdf.save(`${titulo.replace(/\s+/g, '_')}_${fecha}.pdf`);
    }).catch(error => {
        console.error('Error al generar el PDF', error);
        alert('Hubo un error al generar el PDF. Por favor, intente nuevamente.');
    });
}

// Función para imprimir directamente
window.imprimirReporte = function () {
    const contenidoOriginal = document.body.innerHTML;
    const contenidoImprimir = document.getElementById('reporteParaImprimir').innerHTML;

    document.body.innerHTML = `
        <div style="padding: 20px;">
            ${contenidoImprimir}
        </div>
    `;

    window.print();
    document.body.innerHTML = contenidoOriginal;
}