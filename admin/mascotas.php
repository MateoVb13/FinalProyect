<?php require_once "vistas/parte_superior.php"?>

<!--INICIO del cont principal-->
<div class="container">
    <h1>Gestión de Mascotas</h1>

<?php
include_once 'bd/conexion.php';
$objeto = new Conexion();
$conexion = $objeto->Conectar();

// Consulta para obtener las mascotas y el nombre de su dueño
$consulta = "SELECT mascotas.*, usuarios.nombre_usuario AS dueno FROM mascotas INNER JOIN usuarios ON mascotas.usuarios_dueno_idusuarios = usuarios.idusuarios;";
$resultado = $conexion->prepare($consulta);
$resultado->execute();
$data = $resultado->fetchAll(PDO::FETCH_ASSOC);
?>

<div class="container">
    <div class="row">
        <div class="col-lg-12">            
            <button id="btnNuevo" type="button" class="btn btn-success" data-toggle="modal">Nuevo</button>    
        </div>    
    </div>    
</div>    
<br>  
<div class="table-responsive">        
    <table id="tablaMascotas" class="table table-striped table-bordered table-condensed" style="width:100%">
        <thead class="text-center">
            <tr>
                <th>ID</th>
                <th>Nombre</th>
                <th>Tipo de Animal</th>                                
                <th>Raza</th>  
                <th>Edad</th>
                <th>Fecha de Nacimiento</th>
                <th>Dueño</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
            <?php                            
            foreach($data as $dat) {                                                        
            ?>
            <tr>
                <td><?php echo $dat['idmascotas'] ?></td>
                <td><?php echo $dat['nombre_mascota'] ?></td>
                <td><?php echo $dat['tipo_animal'] ?></td>
                <td><?php echo $dat['raza_animal'] ?></td>
                <td><?php echo $dat['edad_mascota'] ?></td>
                <td><?php echo $dat['fecha_nacimiento'] ?></td>
                <td><?php echo $dat['dueno'] ?></td>  
                <td>
                    <div class='text-center'>
                        <div class='btn-group'>
                            <button class='btn btn-info btnEditar'>Editar</button>
                            <button class='btn btn-danger btnBorrar'>Borrar</button>
                        </div>
                    </div>
                </td>
            </tr>
            <?php
                }
            ?>                                
        </tbody>        
    </table>                    
</div>



<!--Modal para CRUD-->
<div class="modal fade" id="modalCRUD" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
<div class="modal-dialog" role="document">
    <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title">Nueva Mascota</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span>
            </button>
        </div>
    <form id="formMascotas">    
        <div class="modal-body">
            <div class="form-group">
                <label for="nombre_mascota" class="col-form-label">Nombre:</label>
                <input type="text" class="form-control" id="nombre_mascota">
            </div>

            <div class="form-group">
                <label for="tipo_animal" class="col-form-label">Tipo de Animal:</label>
                <input type="text" class="form-control" id="tipo_animal">
            </div>    

            <div class="form-group">
                <label for="raza_animal" class="col-form-label">Raza:</label>
                <input type="text" class="form-control" id="raza_animal">
            </div>  

            <div class="form-group">
                <label for="edad_mascota" class="col-form-label">Edad:</label>
                <input type="number" class="form-control" id="edad_mascota">
            </div>

            <div class="form-group">
                <label for="fecha_nacimiento" class="col-form-label">Fecha de Nacimiento:</label>
                <input type="date" class="form-control" id="fecha_nacimiento">
            </div>  

            <div class="form-group">
                <label for="usuarios_dueno_idusuarios" class="col-form-label">Dueño:</label>
                <select class="form-control" id="usuarios_dueno_idusuarios">
                    <?php
                    // Consulta para obtener los usuarios (dueños)
                    $consultaUsuarios = "SELECT * FROM usuarios";
                    $resultadoUsuarios = $conexion->prepare($consultaUsuarios);
                    $resultadoUsuarios->execute();
                    $usuarios = $resultadoUsuarios->fetchAll(PDO::FETCH_ASSOC);
                    foreach($usuarios as $usuario) {
                        echo '<option value="'.$usuario['idusuarios'].'">'.$usuario['nombre_usuario'].'</option>';
                    }
                    ?>
                </select>
            </div>            
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-light" data-dismiss="modal">Cancelar</button>
            <button type="submit" id="btnGuardar" class="btn btn-dark">Guardar</button>
        </div>
    </form>    
    </div>
</div>
</div>  
  
 

</div>
<!--FIN del cont principal-->

<?php require_once "vistas/parte_inferior.php"?>
