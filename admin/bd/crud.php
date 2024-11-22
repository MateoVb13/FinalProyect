<?php
include_once '../bd/conexion.php';
$objeto = new Conexion();
$conexion = $objeto->Conectar();

// Recepción de los datos enviados mediante POST desde el JS   
$nombre_usuario = (isset($_POST['nombre_usuario'])) ? $_POST['nombre_usuario'] : '';
$correo_usuario = (isset($_POST['correo_usuario'])) ? $_POST['correo_usuario'] : '';
$telefono_usuario = (isset($_POST['telefono_usuario'])) ? $_POST['telefono_usuario'] : '';
$direccion_usuario = (isset($_POST['direccion_usuario'])) ? $_POST['direccion_usuario'] : '';
$contraseña_usuario = (isset($_POST['contraseña_usuario'])) ? $_POST['contraseña_usuario'] : '';
$Roles_idroles = (isset($_POST['Roles_idroles'])) ? $_POST['Roles_idroles'] : '';
$opcion = (isset($_POST['opcion'])) ? $_POST['opcion'] : '';
$idusuarios = (isset($_POST['idusuarios'])) ? $_POST['idusuarios'] : '';

switch($opcion){
    case 1: //alta
        $consulta = "INSERT INTO usuarios (nombre_usuario, correo_ususario, telefono_usuario, direccion_usuario, contraseña_usuario, Roles_idroles) VALUES('$nombre_usuario', '$correo_usuario', '$telefono_usuario', '$direccion_usuario', '$contraseña_usuario', '$Roles_idroles') ";			
        $resultado = $conexion->prepare($consulta);
        $resultado->execute(); 

        $consulta = "SELECT usuarios.*, roles.nombre_rol FROM usuarios INNER JOIN roles ON usuarios.Roles_idroles = roles.idroles ORDER BY idusuarios DESC LIMIT 1";
        $resultado = $conexion->prepare($consulta);
        $resultado->execute();
        $data = $resultado->fetchAll(PDO::FETCH_ASSOC);
        break;

    case 2: //modificación
        $consulta = "UPDATE usuarios SET nombre_usuario='$nombre_usuario', correo_ususario='$correo_usuario', telefono_usuario='$telefono_usuario', direccion_usuario='$direccion_usuario', contraseña_usuario='$contraseña_usuario', Roles_idroles='$Roles_idroles' WHERE idusuarios='$idusuarios' ";		
        $resultado = $conexion->prepare($consulta);
        $resultado->execute();        
        
        $consulta = "SELECT usuarios.*, roles.nombre_rol FROM usuarios INNER JOIN roles ON usuarios.Roles_idroles = roles.idroles WHERE idusuarios='$idusuarios' ";       
        $resultado = $conexion->prepare($consulta);
        $resultado->execute();
        $data = $resultado->fetchAll(PDO::FETCH_ASSOC);
        break;        

    case 3: //baja
        $consulta = "DELETE FROM usuarios WHERE idusuarios='$idusuarios' ";		
        $resultado = $conexion->prepare($consulta);
        $resultado->execute();                           
        $data = null;
        break;        
}

print json_encode($data, JSON_UNESCAPED_UNICODE);
$conexion = NULL;
?>
