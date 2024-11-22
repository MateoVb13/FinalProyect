<?php require_once "vistas/parte_superior.php"; ?>

<!--INICIO del cont principal-->
<div class="container">
    <h1>GESTION DE CITAS</h1>
    
    <?php
    include_once 'bd/conexion.php';
    $objeto = new Conexion();
    $conexion = $objeto->Conectar();

    // Consulta para cargar la tabla principal
    $consulta = "SELECT
        a.idcitas,
        a.fecha_apartada,
        a.hora_inicio,
        a.hora_final,
        e.nombre_estado,
        t.nombre_tipo,
        u.nombre_usuario,
        m.nombre_mascota 
    FROM
        atenciones_y_servicios a
    JOIN estados_cita e ON a.estados_cita_idestados_cita = e.idestados_cita
    JOIN tipo_atencion_o_servicio t ON a.tipo_atencion_o_servicio_idatencion_o_servicio = t.idatencion_o_servicio
    JOIN usuarios u ON a.usuarios_veterinario_idusuarios = u.idusuarios
    JOIN mascotas m ON a.mascotas_idmascotas = m.idmascotas;";
    $resultado = $conexion->prepare($consulta);
    $resultado->execute();
    $data = $resultado->fetchAll(PDO::FETCH_ASSOC);

    // Consultas para los select dinámicos
    $consulta_estados = "SELECT idestados_cita, nombre_estado FROM estados_cita";
    $resultado_estados = $conexion->prepare($consulta_estados);
    $resultado_estados->execute();
    $estados = $resultado_estados->fetchAll(PDO::FETCH_ASSOC);

    $consulta_tipos = "SELECT idatencion_o_servicio, nombre_tipo FROM tipo_atencion_o_servicio";
    $resultado_tipos = $conexion->prepare($consulta_tipos);
    $resultado_tipos->execute();
    $tipos = $resultado_tipos->fetchAll(PDO::FETCH_ASSOC);

    $consulta_veterinarios = "SELECT idusuarios, nombre_usuario FROM usuarios WHERE Roles_idroles = 2";
    $resultado_veterinarios = $conexion->prepare($consulta_veterinarios);
    $resultado_veterinarios->execute();
    $veterinarios = $resultado_veterinarios->fetchAll(PDO::FETCH_ASSOC);

    $consulta_mascotas = "SELECT idmascotas, nombre_mascota FROM mascotas";
    $resultado_mascotas = $conexion->prepare($consulta_mascotas);
    $resultado_mascotas->execute();
    $mascotas = $resultado_mascotas->fetchAll(PDO::FETCH_ASSOC);
    ?>

    <div class="container">
        <div class="row">
            <div class="col-lg-12">            
                <button id="btnNuevo" type="button" class="btn btn-success" data-toggle="modal">Nuevo</button>    
            </div>    
        </div>    
    </div>    
    <br>  
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <div class="table-responsive">        
                    <table id="tablaCitas" class="table table-striped table-bordered table-condensed" style="width:100%">
                        <thead class="text-center">
                            <tr>
                                <th>Id</th>
                                <th>Fecha Apartada</th>
                                <th>Hora Inicio</th>                                
                                <th>Hora Final</th>  
                                <th>Estado</th>  
                                <th>Tipo</th>  
                                <th>Veterinario</th>  
                                <th>Mascota</th>  
                                <th>Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            <?php                             
                            foreach($data as $dat) {                                                        
                            ?>
                            <tr>
                                <td><?php echo $dat['idcitas'] ?></td>
                                <td><?php echo $dat['fecha_apartada'] ?></td>
                                <td><?php echo $dat['hora_inicio'] ?></td>
                                <td><?php echo $dat['hora_final'] ?></td>
                                <td><?php echo $dat['nombre_estado'] ?></td>
                                <td><?php echo $dat['nombre_tipo'] ?></td>
                                <td><?php echo $dat['nombre_usuario'] ?></td>
                                <td><?php echo $dat['nombre_mascota'] ?></td>
                                <td>
                                    <button class="btn btn-info btnEditar">Editar</button>
                                    <button class="btn btn-danger btnBorrar">Borrar</button>
                                </td>
                            </tr>
                            <?php
                                }
                            ?>                                
                        </tbody>        
                    </table>                    
                </div>
            </div>
        </div>  
    </div>    

    <!-- Modal para CRUD -->
    <div class="modal fade" id="modalCRUD" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <form id="formCitas">    
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="fecha_apartada" class="col-form-label">Fecha Apartada:</label>
                            <input type="date" class="form-control" id="fecha_apartada">
                        </div>

                        <div class="form-group">
                            <label for="hora_inicio" class="col-form-label">Hora Inicio:</label>
                            <input type="time" class="form-control" id="hora_inicio">
                        </div>    

                        <div class="form-group">
                            <label for="hora_final" class="col-form-label">Hora Final:</label>
                            <input type="time" class="form-control" id="hora_final">
                        </div>  

                        <div class="form-group">
                            <label for="estado_cita" class="col-form-label">Estado:</label>
                            <select class="form-control" id="estado_cita">
                                <?php foreach ($estados as $estado) { ?>
                                    <option value="<?php echo $estado['idestados_cita']; ?>">
                                        <?php echo $estado['nombre_estado']; ?>
                                    </option>
                                <?php } ?>
                            </select>
                        </div>  

                        <div class="form-group">
                            <label for="tipo_atencion" class="col-form-label">Tipo Atención:</label>
                            <select class="form-control" id="tipo_atencion">
                                <?php foreach ($tipos as $tipo) { ?>
                                    <option value="<?php echo $tipo['idatencion_o_servicio']; ?>">
                                        <?php echo $tipo['nombre_tipo']; ?>
                                    </option>
                                <?php } ?>
                            </select>
                        </div>  

                        <div class="form-group">
                            <label for="usuario_veterinario" class="col-form-label">Veterinario:</label>
                            <select class="form-control" id="usuario_veterinario">
                                <?php foreach ($veterinarios as $veterinario) { ?>
                                    <option value="<?php echo $veterinario['idusuarios']; ?>">
                                        <?php echo $veterinario['nombre_usuario']; ?>
                                    </option>
                                <?php } ?>
                            </select>
                        </div>  

                        <div class="form-group">
                            <label for="id_mascota" class="col-form-label">Mascota:</label>
                            <select class="form-control" id="id_mascota">
                                <?php foreach ($mascotas as $mascota) { ?>
                                    <option value="<?php echo $mascota['idmascotas']; ?>">
                                        <?php echo $mascota['nombre_mascota']; ?>
                                    </option>
                                <?php } ?>
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
<script src="citas.js"></script>

<?php require_once "vistas/parte_inferior.php"; ?>
