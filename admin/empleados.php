<?php require_once "vistas/parte_superior.php"?>

<!--INICIO del cont principal-->
<div class="container">
    <h1>Gestión de Empleados</h1>

<?php
include_once 'bd/conexion.php';
$objeto = new Conexion();
$conexion = $objeto->Conectar();

$consulta = "SELECT usuarios.*, roles.nombre_rol FROM usuarios INNER JOIN roles ON usuarios.Roles_idroles = roles.idroles WHERE roles.nombre_rol IN ('Veterinario', 'Administrador');";
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
    <table id="tablaUsuarios" class="table table-striped table-bordered table-condensed" style="width:100%">
        <thead class="text-center">
            <tr>
                <th>Id</th>
                <th>Nombre</th>
                <th>Correo</th>                                
                <th>Teléfono</th>  
                <th>Dirección</th>
                <th>Contraseña</th>
                <th>Rol</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
            <?php                            
            foreach($data as $dat) {                                                        
            ?>
            <tr>
                <td><?php echo $dat['idusuarios'] ?></td>
                <td><?php echo $dat['nombre_usuario'] ?></td>
                <td><?php echo $dat['correo_ususario'] ?></td>
                <td><?php echo $dat['telefono_usuario'] ?></td>
                <td><?php echo $dat['direccion_usuario'] ?></td>
                <td><?php echo $dat['contraseña_usuario'] ?></td>
                <td><?php echo $dat['nombre_rol'] ?></td>  
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
            <h5 class="modal-title">Nuevo Usuario</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span>
            </button>
        </div>
    <form id="formUsuarios">    
        <div class="modal-body">
            <div class="form-group">
                <label for="nombre_usuario" class="col-form-label">Nombre:</label>
                <input type="text" class="form-control" id="nombre_usuario">
            </div>

            <div class="form-group">
                <label for="correo_usuario" class="col-form-label">Correo:</label>
                <input type="email" class="form-control" id="correo_usuario">
            </div>    

            <div class="form-group">
                <label for="telefono_usuario" class="col-form-label">Teléfono:</label>
                <input type="text" class="form-control" id="telefono_usuario">
            </div>  

            <div class="form-group">
                <label for="direccion_usuario" class="col-form-label">Dirección:</label>
                <input type="text" class="form-control" id="direccion_usuario">
            </div>

            <div class="form-group">
                <label for="contraseña_usuario" class="col-form-label">Contraseña:</label>
                <input type="password" class="form-control" id="contraseña_usuario">
            </div>  

            <div class="form-group">
                <label for="Roles_idroles" class="col-form-label">Rol:</label>
                <select class="form-control" id="Roles_idroles">
                    <?php
                    $consultaRoles = "SELECT * FROM roles";
                    $resultadoRoles = $conexion->prepare($consultaRoles);
                    $resultadoRoles->execute();
                    $roles = $resultadoRoles->fetchAll(PDO::FETCH_ASSOC);
                    foreach($roles as $rol) {
                        echo '<option value="'.$rol['idroles'].'">'.$rol['nombre_rol'].'</option>';
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
