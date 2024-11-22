<?php
include_once '../bd/conexion.php';
$objeto = new Conexion();
$conexion = $objeto->Conectar();

// Mostrar errores para depuración (quitar en producción)
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);

// Recepción de los datos enviados mediante POST desde el JS   
$nombre_tipo = (isset($_POST['nombre_tipo'])) ? $_POST['nombre_tipo'] : '';
$precio_tipo = (isset($_POST['precio_tipo'])) ? $_POST['precio_tipo'] : '';
$opcion = (isset($_POST['opcion'])) ? $_POST['opcion'] : '';
$idatencion_o_servicio = (isset($_POST['idatencion_o_servicio'])) ? $_POST['idatencion_o_servicio'] : '';

switch($opcion){
    case 1: //alta
        $consulta = "INSERT INTO tipo_atencion_o_servicio (nombre_tipo, precio_tipo) VALUES(:nombre_tipo, :precio_tipo)";			
        $resultado = $conexion->prepare($consulta);
        $resultado->bindParam(':nombre_tipo', $nombre_tipo);
        $resultado->bindParam(':precio_tipo', $precio_tipo);
        $resultado->execute(); 

        // Verificar si se insertó correctamente
        if($resultado->rowCount() > 0){
            $consulta = "SELECT * FROM tipo_atencion_o_servicio ORDER BY idatencion_o_servicio DESC LIMIT 1";
            $resultado = $conexion->prepare($consulta);
            $resultado->execute();
            $data = $resultado->fetchAll(PDO::FETCH_ASSOC);
        } else {
            $data = array('error' => 'No se pudo insertar el registro.');
        }
        break;

    case 2: //modificación
        $consulta = "UPDATE tipo_atencion_o_servicio SET nombre_tipo=:nombre_tipo, precio_tipo=:precio_tipo WHERE idatencion_o_servicio=:idatencion_o_servicio";		
        $resultado = $conexion->prepare($consulta);
        $resultado->bindParam(':nombre_tipo', $nombre_tipo);
        $resultado->bindParam(':precio_tipo', $precio_tipo);
        $resultado->bindParam(':idatencion_o_servicio', $idatencion_o_servicio);
        $resultado->execute();        

        if($resultado->rowCount() > 0){
            $consulta = "SELECT * FROM tipo_atencion_o_servicio WHERE idatencion_o_servicio=:idatencion_o_servicio";       
            $resultado = $conexion->prepare($consulta);
            $resultado->bindParam(':idatencion_o_servicio', $idatencion_o_servicio);
            $resultado->execute();
            $data = $resultado->fetchAll(PDO::FETCH_ASSOC);
        } else {
            $data = array('error' => 'No se pudo actualizar el registro.');
        }
        break;        

    case 3: //baja
        $consulta = "DELETE FROM tipo_atencion_o_servicio WHERE idatencion_o_servicio=:idatencion_o_servicio";		
        $resultado = $conexion->prepare($consulta);
        $resultado->bindParam(':idatencion_o_servicio', $idatencion_o_servicio);
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
