$(document).ready(function(){
    var tablaMascotas = $("#tablaMascotas").DataTable({
      "columnDefs":[{
        "targets": -1,
        "data":null,
        "defaultContent": "<div class='text-center'><div class='btn-group'><button class='btn btn-info btnEditar'>Editar</button><button class='btn btn-danger btnBorrar'>Borrar</button></div></div>"  
       }],
        //Para cambiar el lenguaje a español
        "language": {
            // ...
        }
    });
    
    $("#btnNuevo").click(function(){
        $("#formMascotas").trigger("reset");
        $(".modal-header").css("background-color", "#1cc88a");
        $(".modal-header").css("color", "white");
        $(".modal-title").text("Nueva Mascota");            
        $("#modalCRUD").modal("show");        
        idmascotas = null;
        opcion = 1; //alta
    });    
    
    var fila; //capturar la fila para editar o borrar el registro
        
    //botón EDITAR    
    $(document).on("click", ".btnEditar", function(){
        fila = $(this).closest("tr");
        idmascotas = parseInt(fila.find('td:eq(0)').text());
        nombre_mascota = fila.find('td:eq(1)').text();
        tipo_animal = fila.find('td:eq(2)').text();
        raza_animal = fila.find('td:eq(3)').text();
        edad_mascota = parseInt(fila.find('td:eq(4)').text());
        fecha_nacimiento = fila.find('td:eq(5)').text();
        dueno = fila.find('td:eq(6)').text();

        // Obtener el ID del dueño basado en el nombre
        var usuarios_dueno_idusuarios = $("#usuarios_dueno_idusuarios option").filter(function() {
            return $(this).text() == dueno;
        }).val();

        $("#nombre_mascota").val(nombre_mascota);
        $("#tipo_animal").val(tipo_animal);
        $("#raza_animal").val(raza_animal);
        $("#edad_mascota").val(edad_mascota);
        $("#fecha_nacimiento").val(fecha_nacimiento);
        $("#usuarios_dueno_idusuarios").val(usuarios_dueno_idusuarios);
        opcion = 2; //editar
        
        $(".modal-header").css("background-color", "#4e73df");
        $(".modal-header").css("color", "white");
        $(".modal-title").text("Editar Mascota");            
        $("#modalCRUD").modal("show");  
        
    });
    
    //botón BORRAR
    $(document).on("click", ".btnBorrar", function(){    
        fila = $(this);
        idmascotas = parseInt($(this).closest("tr").find('td:eq(0)').text());
        opcion = 3; //borrar
        var respuesta = confirm("¿Está seguro de eliminar el registro: "+idmascotas+"?");
        if(respuesta){
            $.ajax({
                url: "bd/crud_mascotas.php",
                type: "POST",
                dataType: "json",
                data: {opcion:opcion, idmascotas:idmascotas},
                success: function(){
                    tablaMascotas.row(fila.parents('tr')).remove().draw();
                }
            });
        }   
    });
        
    $("#formMascotas").submit(function(e){
        e.preventDefault();    
        nombre_mascota = $.trim($("#nombre_mascota").val());
        tipo_animal = $.trim($("#tipo_animal").val());
        raza_animal = $.trim($("#raza_animal").val());
        edad_mascota = $.trim($("#edad_mascota").val());
        fecha_nacimiento = $.trim($("#fecha_nacimiento").val());
        usuarios_dueno_idusuarios = $.trim($("#usuarios_dueno_idusuarios").val());    
        $.ajax({
            url: "bd/crud_mascotas.php",
            type: "POST",
            dataType: "json",
            data: {
                nombre_mascota: nombre_mascota, 
                tipo_animal: tipo_animal,
                raza_animal: raza_animal,
                edad_mascota: edad_mascota,
                fecha_nacimiento: fecha_nacimiento,
                usuarios_dueno_idusuarios: usuarios_dueno_idusuarios,
                idmascotas: idmascotas,
                opcion: opcion
            },
            success: function(data){  
                console.log(data);
                idmascotas = data[0].idmascotas;            
                nombre_mascota = data[0].nombre_mascota;
                tipo_animal = data[0].tipo_animal;
                raza_animal = data[0].raza_animal;
                edad_mascota = data[0].edad_mascota;
                fecha_nacimiento = data[0].fecha_nacimiento;
                dueno = data[0].dueno;
                if(opcion == 1){
                    tablaMascotas.row.add([
                        idmascotas,
                        nombre_mascota,
                        tipo_animal,
                        raza_animal,
                        edad_mascota,
                        fecha_nacimiento,
                        dueno,
                        "<div class='text-center'><div class='btn-group'><button class='btn btn-info btnEditar'>Editar</button><button class='btn btn-danger btnBorrar'>Borrar</button></div></div>"
                    ]).draw();
                } else {
                    tablaMascotas.row(fila).data([
                        idmascotas,
                        nombre_mascota,
                        tipo_animal,
                        raza_animal,
                        edad_mascota,
                        fecha_nacimiento,
                        dueno,
                        "<div class='text-center'><div class='btn-group'><button class='btn btn-info btnEditar'>Editar</button><button class='btn btn-danger btnBorrar'>Borrar</button></div></div>"
                    ]).draw();
                }       
            },
            error: function(jqXHR, textStatus, errorThrown) {
                console.log('Error en la petición AJAX:', textStatus, errorThrown);
            }        
        });
        $("#modalCRUD").modal("hide");    
        
    });    
});
