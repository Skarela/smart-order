  var archivo;

    $('#file').on('change', function (e) {
        var files = e.target.files;
        if (files.length > 0) {
            if (window.FormData !== undefined) {
                archivo = new FormData();
                for (var i = 0; i < files.length; i++) {
                    archivo.append("file" + i, files[i]);
                }
                //return archivo;
            }
        }
        else {
            alert("No se aceptan subidas de archivos");
        }
    });

    $('#subirArchivo').on('click', function () {
        var controller = $(this).data('request-url');
        $.ajax({
            url: controller,
            data: archivo,
            type: 'POST',
            dataType: 'json',
            processData: false,
            contentType: false,
            enctype: 'multipart/form-data',
            success: function (data) {

                if ($.fn.DataTable.isDataTable('#tablaResultados')) {
                    $('#tablaResultados').DataTable().destroy();
                    $('#tablaResultados').empty();
                }

                $('#resultados').insertAfter($('.tab-content'));

                if (data != null && data.Success) {

                    if ($('#alertMsg').hasClass('alert alert-danger')) {
                        $('#alertMsg').removeClass('alert alert-danger');
                    }
                    $('#alertMsg').addClass('alert alert-success');
                    $('#alertMsg').html(data.Mensaje);
                    cargarTabla(data.DatosTabla);
                    $('body').removeClass('loading');
                }

                else {
                    $('#alertMsg').addClass('alert alert-danger');
                    $('#alertMsg').html(data.Mensaje);
                    if (data.DatosTabla != null) {
                        cargarTablaErrores(data.DatosTabla);
                    }
                }
            },
            error: function (request, status, error) {
                $('#alertMsg').addClass('alert alert-danger');
                $('#alertMsg').html(request.responseText);
            }
        });
    });

    function cargarTablaErrores(jsonObject) {
        table = $('#tablaResultados').DataTable({
            aaData: jsonObject,
            dom: 'Birtlp',
            buttons: [{ extend: 'excel', text: 'Exportar', className: 'btn btn-success' }],
            columns: [
                { title: "Fila", data: "Row" },
                { title: "Observación", data: "Details" }
            ]
        });

        //table.buttons().container().insertBefore('#tablaResultados_filter');
    }

    function cargarTabla(jsonObject) {
        table = $('#tablaResultados').DataTable({
            aaData: jsonObject,
            scrollX: true,
            dom: 'Birtlp',
            buttons: [{ extend: 'excel', text: 'Exportar', className: 'btn btn-success' }],
            columns: [
                { title: "Punto de Venta", data: "BranchId" },
                { title: "Ruta", data: "RouteId" },
                { title: "Número de Viajes", data: "Travels" },
                { title: "Lunes", data: "Monday" },
                { title: "Martes", data: "Tuesday" },
                { title: "Miércoles", data: "Wednesday" },
                { title: "Jueves", data: "Thursday" },
                { title: "Viernes", data: "Friday" },
                { title: "Sábado", data: "Saturday" },
                { title: "Domingo", data: "Sunday" },
            ]
        });
    }
    

