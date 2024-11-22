$(document).ready(function () {
    var tablaCitas = $("#tablaCitas").DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ registros",
            "zeroRecords": "No se encontraron resultados",
            "info": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "infoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "infoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sSearch": "Buscar:",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior",
            },
            "sProcessing": "Procesando...",
        },
    });

    var idcitas = null;
    var opcion = null;
    var fila = null;

    // Botón para abrir el modal "Nueva Cita"
    $("#btnNuevo").click(function () {
        $("#formCitas").trigger("reset");
        $(".modal-header").css("background-color", "#1cc88a");
        $(".modal-header").css("color", "white");
        $(".modal-title").text("Nueva Cita");
        $("#modalCRUD").modal("show");
        idcitas = null;
        opcion = 1; // Crear nueva cita
    });

    // Botón para editar
    $(document).on("click", ".btnEditar", function () {
        fila = $(this).closest("tr");
        idcitas = parseInt(fila.find("td:eq(0)").text());
        $("#fecha_apartada").val(fila.find("td:eq(1)").text());
        $("#hora_inicio").val(fila.find("td:eq(2)").text());
        $("#hora_final").val(fila.find("td:eq(3)").text());
        $("#estado_cita").val(fila.find("td:eq(4)").data("id"));
        $("#tipo_atencion").val(fila.find("td:eq(5)").data("id"));
        $("#usuario_veterinario").val(fila.find("td:eq(6)").data("id"));
        $("#id_mascota").val(fila.find("td:eq(7)").data("id"));

        opcion = 2; // Editar cita existente
        $(".modal-header").css("background-color", "#4e73df");
        $(".modal-header").css("color", "white");
        $(".modal-title").text("Editar Cita");
        $("#modalCRUD").modal("show");
    });

    // Botón para eliminar
    $(document).on("click", ".btnBorrar", function () {
        fila = $(this);
        idcitas = parseInt($(this).closest("tr").find("td:eq(0)").text());
        opcion = 3; // Eliminar cita
        var respuesta = confirm("¿Está seguro de eliminar esta cita?");
        if (respuesta) {
            $.ajax({
                url: "bd/crud_citas.php",
                type: "POST",
                dataType: "json",
                data: { opcion: opcion, idcitas: idcitas },
                success: function () {
                    tablaCitas.row(fila.parents("tr")).remove().draw();
                },
                error: function (xhr) {
                    alert("Error al eliminar la cita: " + xhr.responseText);
                },
            });
        }
    });

    // Guardar cita
    $("#formCitas").submit(function (e) {
        e.preventDefault();
        var datos = {
            idcitas: idcitas,
            fecha_apartada: $("#fecha_apartada").val(),
            hora_inicio: $("#hora_inicio").val(),
            hora_final: $("#hora_final").val(),
            estado_cita: $("#estado_cita").val(),
            tipo_atencion: $("#tipo_atencion").val(),
            usuario_veterinario: $("#usuario_veterinario").val(),
            id_mascota: $("#id_mascota").val(),
            opcion: opcion,
        };

        $.ajax({
            url: "bd/crud_citas.php",
            type: "POST",
            dataType: "json",
            data: datos,
            success: function (data) {
                if (opcion == 1) {
                    tablaCitas.row.add([
                        data[0].idcitas,
                        data[0].fecha_apartada,
                        data[0].hora_inicio,
                        data[0].hora_final,
                        data[0].nombre_estado,
                        data[0].nombre_tipo,
                        data[0].nombre_usuario,
                        data[0].nombre_mascota,
                        "<div class='text-center'><div class='btn-group'><button class='btn btn-info btnEditar'>Editar</button><button class='btn btn-danger btnBorrar'>Borrar</button></div></div>",
                    ]).draw();
                } else {
                    tablaCitas.row(fila).data([
                        data[0].idcitas,
                        data[0].fecha_apartada,
                        data[0].hora_inicio,
                        data[0].hora_final,
                        data[0].nombre_estado,
                        data[0].nombre_tipo,
                        data[0].nombre_usuario,
                        data[0].nombre_mascota,
                        "<div class='text-center'><div class='btn-group'><button class='btn btn-info btnEditar'>Editar</button><button class='btn btn-danger btnBorrar'>Borrar</button></div></div>",
                    ]).draw();
                }
                $("#modalCRUD").modal("hide");
            },
            error: function (xhr) {
                alert("Error al guardar la cita: " + xhr.responseText);
            },
        });
    });
});
