window.generarPDF = function () {
    const element = document.getElementById('reporteParaImprimir');

    if (!element) {
        console.error('Elemento no encontrado');
        return;
    }

    const clonedElement = element.cloneNode(true);
    clonedElement.style.width = '1100px';
    document.body.appendChild(clonedElement);
    clonedElement.style.position = 'absolute';
    clonedElement.style.left = '-9999px';

    html2canvas(clonedElement, {
        scale: 1,
        useCORS: true,
        logging: true
    }).then(canvas => {
        // Limpia el elemento clonado
        document.body.removeChild(clonedElement);

        const imgData = canvas.toDataURL('image/png');
        const pdf = new window.jspdf.jsPDF('l', 'mm', 'a4');

        // Calculamos las dimensiones
        const imgWidth = 277; // Ancho A4 landscape (297-20)
        const imgHeight = canvas.height * imgWidth / canvas.width;

        pdf.addImage(imgData, 'PNG', 10, 10, imgWidth, imgHeight);

        const fecha = new Date().toISOString().slice(0, 10);
        const titulo = document.querySelector('.modal-title').textContent || 'Reporte';

        // Descargamos el PDF
        pdf.save(`${titulo.replace(/\s+/g, '_')}_${fecha}.pdf`);
    }).catch(error => {
        console.error('Error al generar el PDF', error);
        alert('Hubo un error al generar el PDF. Por favor, intente nuevamente.');
    });
}

// Función para imprimir reporte
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

// Función para imprimir comanda
window.imprimirComanda = function () {
    const contenidoOriginal = document.body.innerHTML;
    const contenidoImprimir = document.getElementById('comanda').innerHTML;

    document.body.innerHTML = `
        <div style="padding: 20px;">
            ${contenidoImprimir}
        </div>
    `;

    window.print();
    document.body.innerHTML = contenidoOriginal;
}