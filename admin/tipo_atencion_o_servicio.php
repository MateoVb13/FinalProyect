<?php require_once "vistas/parte_superior.php"?>

<!--INICIO del cont principal-->
<div class="container">
    <h1>Gestión de Tipos de Atención o Servicio</h1>

<?php
include_once 'bd/conexion.php';
$objeto = new Conexion();
$conexion = $objeto->Conectar();

// Consulta para obtener los tipos de atención o servicio
$consulta = "SELECT * FROM tipo_atencion_o_servicio;";
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
    <table id="tablaTipoAtencion" class="table table-striped table-bordered table-condensed" style="width:100%">
        <thead class="text-center">
            <tr>
                <th>ID</th>
                <th>Nombre del Tipo</th>
                <th>Precio</th>                                
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
            <?php                            
            foreach($data as $dat) {                                                        
            ?>
            <tr>
                <td><?php echo $dat['idatencion_o_servicio'] ?></td>
                <td><?php echo $dat['nombre_tipo'] ?></td>
                <td><?php echo $dat['precio_tipo'] ?></td>
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
<div class="modal fade" id="modalCRUD" tabindex="-1" role="dialog" aria-labelledby="modalCRUDLabel" aria-hidden="true">
<div class="modal-dialog" role="document">
    <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title">Nuevo Tipo de Atención o Servicio</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span>
            </button>
        </div>
    <form id="formTipoAtencion">    
        <div class="modal-body">
            <div class="form-group">
                <label for="nombre_tipo" class="col-form-label">Nombre del Tipo:</label>
                <input type="text" class="form-control" id="nombre_tipo">
            </div>

            <div class="form-group">
                <label for="precio_tipo" class="col-form-label">Precio:</label>
                <input type="number" class="form-control" id="precio_tipo" step="0.01">
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
