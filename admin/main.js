$(document).ready(function(){
    var tablaUsuarios = $("#tablaUsuarios").DataTable({
      "columnDefs":[{
        "targets": -1,
        "data":null,
        "defaultContent": "<div class='text-center'><div class='btn-group'><button class='btn btn-primary btnEditar'>Editar</button><button class='btn btn-danger btnBorrar'>Borrar</button></div></div>"  
       }],
        //Para cambiar el lenguaje a español
        "language": {
            "lengthMenu": "Mostrar _MENU_ registros",
            "zeroRecords": "No se encontraron resultados",
            "info": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "infoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "infoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sSearch": "Buscar:",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast":"Último",
                "sNext":"Siguiente",
                "sPrevious": "Anterior"
             },
             "sProcessing":"Procesando...",
        }
    });
    
    $("#btnNuevo").click(function(){
        $("#formUsuarios").trigger("reset");
        $(".modal-header").css("background-color", "#1cc88a");
        $(".modal-header").css("color", "white");
        $(".modal-title").text("Nuevo Usuario");            
        $("#modalCRUD").modal("show");        
        idusuarios = null;
        opcion = 1; //alta
    });    
    
    var fila; //capturar la fila para editar o borrar el registro
        
    //botón EDITAR    
    $(document).on("click", ".btnEditar", function(){
        fila = $(this).closest("tr");
        idusuarios = parseInt(fila.find('td:eq(0)').text());
        nombre_usuario = fila.find('td:eq(1)').text();
        correo_usuario = fila.find('td:eq(2)').text();
        telefono_usuario = fila.find('td:eq(3)').text();
        direccion_usuario = fila.find('td:eq(4)').text();
        contraseña_usuario = fila.find('td:eq(5)').text();
        nombre_rol = fila.find('td:eq(6)').text();

        // Obtener el ID del rol basado en el nombre
        var Roles_idroles = $("#Roles_idroles option").filter(function() {
            return $(this).text() == nombre_rol;
        }).val();

        $("#nombre_usuario").val(nombre_usuario);
        $("#correo_usuario").val(correo_usuario);
        $("#telefono_usuario").val(telefono_usuario);
        $("#direccion_usuario").val(direccion_usuario);
        $("#contraseña_usuario").val(contraseña_usuario);
        $("#Roles_idroles").val(Roles_idroles);
        opcion = 2; //editar
        
        $(".modal-header").css("background-color", "#4e73df");
        $(".modal-header").css("color", "white");
        $(".modal-title").text("Editar Usuario");            
        $("#modalCRUD").modal("show");  
        
    });
    
    //botón BORRAR
    $(document).on("click", ".btnBorrar", function(){    
        fila = $(this);
        idusuarios = parseInt($(this).closest("tr").find('td:eq(0)').text());
        opcion = 3; //borrar
        var respuesta = confirm("¿Está seguro de eliminar el registro: "+idusuarios+"?");
        if(respuesta){
            $.ajax({
                url: "bd/crud.php",
                type: "POST",
                dataType: "json",
                data: {opcion:opcion, idusuarios:idusuarios},
                success: function(){
                    tablaUsuarios.row(fila.parents('tr')).remove().draw();
                }
            });
        }   
    });
        
    $("#formUsuarios").submit(function(e){
        e.preventDefault();    
        nombre_usuario = $.trim($("#nombre_usuario").val());
        correo_usuario = $.trim($("#correo_usuario").val());
        telefono_usuario = $.trim($("#telefono_usuario").val());
        direccion_usuario = $.trim($("#direccion_usuario").val());
        contraseña_usuario = $.trim($("#contraseña_usuario").val());
        Roles_idroles = $.trim($("#Roles_idroles").val());    
        $.ajax({
            url: "bd/crud.php",
            type: "POST",
            dataType: "json",
            data: {
                nombre_usuario:nombre_usuario, 
                correo_usuario:correo_usuario,
                telefono_usuario:telefono_usuario,
                direccion_usuario:direccion_usuario,
                contraseña_usuario:contraseña_usuario,
                Roles_idroles:Roles_idroles,
                idusuarios:idusuarios,
                opcion:opcion
            },
            success: function(data){  
                console.log(data);
                idusuarios = data[0].idusuarios;            
                nombre_usuario = data[0].nombre_usuario;
                correo_usuario = data[0].correo_ususario;
                telefono_usuario = data[0].telefono_usuario;
                direccion_usuario = data[0].direccion_usuario;
                contraseña_usuario = data[0].contraseña_usuario;
                nombre_rol = data[0].nombre_rol;
                if(opcion == 1){
                    tablaUsuarios.row.add([
                        idusuarios,
                        nombre_usuario,
                        correo_usuario,
                        telefono_usuario,
                        direccion_usuario,
                        contraseña_usuario,
                        nombre_rol,
                        "<div class='text-center'><div class='btn-group'><button class='btn btn-info btnEditar'>Editar</button><button class='btn btn-danger btnBorrar'>Borrar</button></div></div>"
                    ]).draw();
                } else {
                    tablaUsuarios.row(fila).data([
                        idusuarios,
                        nombre_usuario,
                        correo_usuario,
                        telefono_usuario,
                        direccion_usuario,
                        contraseña_usuario,
                        nombre_rol,
                        "<div class='text-center'><div class='btn-group'><button class='btn btn-info btnEditar'>Editar</button><button class='btn btn-danger btnBorrar'>Borrar</button></div></div>"
                    ]).draw();
                }       
            }        
        });
        $("#modalCRUD").modal("hide");    
        
    });    
});
