<?php
include_once '../bd/conexion.php';
$objeto = new Conexion();
$conexion = $objeto->Conectar();

// Mostrar errores para depuración
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);

// Recepción de los datos enviados mediante POST desde el JS   
$nombre_mascota = (isset($_POST['nombre_mascota'])) ? $_POST['nombre_mascota'] : '';
$tipo_animal = (isset($_POST['tipo_animal'])) ? $_POST['tipo_animal'] : '';
$raza_animal = (isset($_POST['raza_animal'])) ? $_POST['raza_animal'] : '';
$edad_mascota = (isset($_POST['edad_mascota'])) ? $_POST['edad_mascota'] : '';
$fecha_nacimiento = (isset($_POST['fecha_nacimiento'])) ? $_POST['fecha_nacimiento'] : '';
$usuarios_dueno_idusuarios = (isset($_POST['usuarios_dueno_idusuarios'])) ? $_POST['usuarios_dueno_idusuarios'] : '';
$opcion = (isset($_POST['opcion'])) ? $_POST['opcion'] : '';
$idmascotas = (isset($_POST['idmascotas'])) ? $_POST['idmascotas'] : '';

switch($opcion){
    case 1: //alta
        $consulta = "INSERT INTO mascotas (nombre_mascota, tipo_animal, raza_animal, edad_mascota, fecha_nacimiento, usuarios_dueno_idusuarios) VALUES(:nombre_mascota, :tipo_animal, :raza_animal, :edad_mascota, :fecha_nacimiento, :usuarios_dueno_idusuarios)";			
        $resultado = $conexion->prepare($consulta);
        $resultado->bindParam(':nombre_mascota', $nombre_mascota);
        $resultado->bindParam(':tipo_animal', $tipo_animal);
        $resultado->bindParam(':raza_animal', $raza_animal);
        $resultado->bindParam(':edad_mascota', $edad_mascota);
        $resultado->bindParam(':fecha_nacimiento', $fecha_nacimiento);
        $resultado->bindParam(':usuarios_dueno_idusuarios', $usuarios_dueno_idusuarios);
        $resultado->execute(); 

        // Verificar si se insertó correctamente
        if($resultado->rowCount() > 0){
            $consulta = "SELECT mascotas.*, usuarios.nombre_usuario AS dueno FROM mascotas INNER JOIN usuarios ON mascotas.usuarios_dueno_idusuarios = usuarios.idusuarios ORDER BY idmascotas DESC LIMIT 1";
            $resultado = $conexion->prepare($consulta);
            $resultado->execute();
            $data = $resultado->fetchAll(PDO::FETCH_ASSOC);
        } else {
            $data = array('error' => 'No se pudo insertar el registro.');
        }
        break;

    case 2: //modificación
        $consulta = "UPDATE mascotas SET nombre_mascota=:nombre_mascota, tipo_animal=:tipo_animal, raza_animal=:raza_animal, edad_mascota=:edad_mascota, fecha_nacimiento=:fecha_nacimiento, usuarios_dueno_idusuarios=:usuarios_dueno_idusuarios WHERE idmascotas=:idmascotas";		
        $resultado = $conexion->prepare($consulta);
        $resultado->bindParam(':nombre_mascota', $nombre_mascota);
        $resultado->bindParam(':tipo_animal', $tipo_animal);
        $resultado->bindParam(':raza_animal', $raza_animal);
        $resultado->bindParam(':edad_mascota', $edad_mascota);
        $resultado->bindParam(':fecha_nacimiento', $fecha_nacimiento);
        $resultado->bindParam(':usuarios_dueno_idusuarios', $usuarios_dueno_idusuarios);
        $resultado->bindParam(':idmascotas', $idmascotas);
        $resultado->execute();        

        if($resultado->rowCount() > 0){
            $consulta = "SELECT mascotas.*, usuarios.nombre_usuario AS dueno FROM mascotas INNER JOIN usuarios ON mascotas.usuarios_dueno_idusuarios = usuarios.idusuarios WHERE idmascotas=:idmascotas";       
            $resultado = $conexion->prepare($consulta);
            $resultado->bindParam(':idmascotas', $idmascotas);
            $resultado->execute();
            $data = $resultado->fetchAll(PDO::FETCH_ASSOC);
        } else {
            $data = array('error' => 'No se pudo actualizar el registro.');
        }
        break;        

    case 3: //baja
        $consulta = "DELETE FROM mascotas WHERE idmascotas=:idmascotas";		
        $resultado = $conexion->prepare($consulta);
        $resultado->bindParam(':idmascotas', $idmascotas);
        $resultado->execute();                           

        if($resultado->rowCount() > 0){
            $data = array('success' => 'Registro eliminado correctamente.');
        } else {
            $data = array('error' => 'No se pudo eliminar el registro.');
        }
        break;        
}

print json_encode($data, JSON_UNESCAPED_UNICODE); // Enviar el array final en formato JSON a JS
$conexion = NULL;
?>
