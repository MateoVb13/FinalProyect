<?php
include_once '../bd/conexion.php'; // Incluye el archivo de conexión a la base de datos
$objeto = new Conexion();
$conexion = $objeto->Conectar();

// Recibir datos enviados por AJAX
$idcitas = isset($_POST['idcitas']) ? $_POST['idcitas'] : '';
$fecha_apartada = isset($_POST['fecha_apartada']) ? $_POST['fecha_apartada'] : '';
$hora_inicio = isset($_POST['hora_inicio']) ? $_POST['hora_inicio'] : '';
$hora_final = isset($_POST['hora_final']) ? $_POST['hora_final'] : '';
$estado_cita = isset($_POST['estado_cita']) ? $_POST['estado_cita'] : '';
$tipo_atencion = isset($_POST['tipo_atencion']) ? $_POST['tipo_atencion'] : '';
$usuario_veterinario = isset($_POST['usuario_veterinario']) ? $_POST['usuario_veterinario'] : '';
$id_mascota = isset($_POST['id_mascota']) ? $_POST['id_mascota'] : '';
$opcion = isset($_POST['opcion']) ? $_POST['opcion'] : '';

try {
    switch ($opcion) {
        case 1: // Crear nueva cita
            $consulta = "INSERT INTO atenciones_y_servicios 
                        (fecha_apartada, hora_inicio, hora_final, estados_cita_idestados_cita, tipo_atencion_o_servicio_idatencion_o_servicio, usuarios_veterinario_idusuarios, mascotas_idmascotas) 
                        VALUES (?, ?, ?, ?, ?, ?, ?)";
            $stmt = $conexion->prepare($consulta);
            $stmt->execute([$fecha_apartada, $hora_inicio, $hora_final, $estado_cita, $tipo_atencion, $usuario_veterinario, $id_mascota]);

            // Obtener la última cita creada
            $consulta = "SELECT a.idcitas, a.fecha_apartada, a.hora_inicio, a.hora_final, 
                                e.nombre_estado, t.nombre_tipo, u.nombre_usuario, m.nombre_mascota
                         FROM atenciones_y_servicios a
                         JOIN estados_cita e ON a.estados_cita_idestados_cita = e.idestados_cita
                         JOIN tipo_atencion_o_servicio t ON a.tipo_atencion_o_servicio_idatencion_o_servicio = t.idatencion_o_servicio
                         JOIN usuarios u ON a.usuarios_veterinario_idusuarios = u.idusuarios
                         JOIN mascotas m ON a.mascotas_idmascotas = m.idmascotas
                         ORDER BY a.idcitas DESC LIMIT 1";
            $stmt = $conexion->prepare($consulta);
            $stmt->execute();
            $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
            break;

        case 2: // Actualizar cita existente
            $consulta = "UPDATE atenciones_y_servicios SET 
                            fecha_apartada = ?, hora_inicio = ?, hora_final = ?, estados_cita_idestados_cita = ?, 
                            tipo_atencion_o_servicio_idatencion_o_servicio = ?, usuarios_veterinario_idusuarios = ?, mascotas_idmascotas = ? 
                         WHERE idcitas = ?";
            $stmt = $conexion->prepare($consulta);
            $stmt->execute([$fecha_apartada, $hora_inicio, $hora_final, $estado_cita, $tipo_atencion, $usuario_veterinario, $id_mascota, $idcitas]);

            // Obtener la cita actualizada
            $consulta = "SELECT a.idcitas, a.fecha_apartada, a.hora_inicio, a.hora_final, 
                                e.nombre_estado, t.nombre_tipo, u.nombre_usuario, m.nombre_mascota
                         FROM atenciones_y_servicios a
                         JOIN estados_cita e ON a.estados_cita_idestados_cita = e.idestados_cita
                         JOIN tipo_atencion_o_servicio t ON a.tipo_atencion_o_servicio_idatencion_o_servicio = t.idatencion_o_servicio
                         JOIN usuarios u ON a.usuarios_veterinario_idusuarios = u.idusuarios
                         JOIN mascotas m ON a.mascotas_idmascotas = m.idmascotas
                         WHERE a.idcitas = ?";
            $stmt = $conexion->prepare($consulta);
            $stmt->execute([$idcitas]);
            $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
            break;

        case 3: // Eliminar cita
            $consulta = "DELETE FROM atenciones_y_servicios WHERE idcitas = ?";
            $stmt = $conexion->prepare($consulta);
            $stmt->execute([$idcitas]);
            $data = null;
            break;
    }

    echo json_encode($data, JSON_UNESCAPED_UNICODE); // Enviar datos al cliente
} catch (PDOException $e) {
    echo json_encode(["error" => $e->getMessage()]); // Manejo de errores
}

$conexion = null; // Cerrar conexión
?>
