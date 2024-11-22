$(document).ready(function(){
    var tablaTipoAtencion = $("#tablaTipoAtencion").DataTable({
      "columnDefs":[{
        "targets": -1,
        "data":null,
        "defaultContent": "<div class='text-center'><div class='btn-group'><button class='btn btn-info btnEditar'>Editar</button><button class='btn btn-danger btnBorrar'>Borrar</button></div></div>"  
       }],
        //Para cambiar el lenguaje a español
        "language": {
            // Puedes copiar el objeto de idioma del ejemplo anterior
        }
    });
    
    $("#btnNuevo").click(function(){
        $("#formTipoAtencion").trigger("reset");
        $(".modal-header").css("background-color", "#1cc88a");
        $(".modal-header").css("color", "white");
        $(".modal-title").text("Nuevo Tipo de Atención o Servicio");            
        $("#modalCRUD").modal("show");        
        idatencion_o_servicio = null;
        opcion = 1; //alta
    });    
    
    var fila; //capturar la fila para editar o borrar el registro
        
    //botón EDITAR    
    $(document).on("click", ".btnEditar", function(){
        fila = $(this).closest("tr");
        idatencion_o_servicio = parseInt(fila.find('td:eq(0)').text());
        nombre_tipo = fila.find('td:eq(1)').text();
        precio_tipo = parseFloat(fila.find('td:eq(2)').text());
        
        $("#nombre_tipo").val(nombre_tipo);
        $("#precio_tipo").val(precio_tipo);
        opcion = 2; //editar
        
        $(".modal-header").css("background-color", "#4e73df");
        $(".modal-header").css("color", "white");
        $(".modal-title").text("Editar Tipo de Atención o Servicio");            
        $("#modalCRUD").modal("show");  
        
    });
    
    //botón BORRAR
    $(document).on("click", ".btnBorrar", function(){    
        fila = $(this);
        idatencion_o_servicio = parseInt($(this).closest("tr").find('td:eq(0)').text());
        opcion = 3; //borrar
        var respuesta = confirm("¿Está seguro de eliminar el registro: "+idatencion_o_servicio+"?");
        if(respuesta){
            $.ajax({
                url: "bd/crud_tipo_atencion.php",
                type: "POST",
                dataType: "json",
                data: {opcion:opcion, idatencion_o_servicio:idatencion_o_servicio},
                success: function(){
                    tablaTipoAtencion.row(fila.parents('tr')).remove().draw();
                }
            });
        }   
    });
        
    $("#formTipoAtencion").submit(function(e){
        e.preventDefault();    
        nombre_tipo = $.trim($("#nombre_tipo").val());
        precio_tipo = parseFloat($.trim($("#precio_tipo").val()));
        $.ajax({
            url: "bd/crud_tipo_atencion.php",
            type: "POST",
            dataType: "json",
            data: {
                nombre_tipo: nombre_tipo, 
                precio_tipo: precio_tipo,
                idatencion_o_servicio: idatencion_o_servicio,
                opcion: opcion
            },
            success: function(data){  
                console.log(data);
                idatencion_o_servicio = data[0].idatencion_o_servicio;            
                nombre_tipo = data[0].nombre_tipo;
                precio_tipo = data[0].precio_tipo;
                if(opcion == 1){
                    tablaTipoAtencion.row.add([
                        idatencion_o_servicio,
                        nombre_tipo,
                        precio_tipo,
                        "<div class='text-center'><div class='btn-group'><button class='btn btn-info btnEditar'>Editar</button><button class='btn btn-danger btnBorrar'>Borrar</button></div></div>"
                    ]).draw();
                } else {
                    tablaTipoAtencion.row(fila).data([
                        idatencion_o_servicio,
                        nombre_tipo,
                        precio_tipo,
                        "<div class='text-center'><div class='btn-group'><button class='btn btn-info btnEditar'>Editar</button><button class='btn btn-danger btnBorrar'>Borrar</button></div></div>"
                    ]).draw();
                }       
            },
            error: function(jqXHR, textStatus, errorThrown){
                console.log('Error:', textStatus, errorThrown);
            }        
        });
        $("#modalCRUD").modal("hide");    
        
    });    
});
