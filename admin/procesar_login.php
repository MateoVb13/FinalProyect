<?php
session_start();
include_once 'bd/conexion.php'; // Incluye la conexión a la base de datos

$objeto = new Conexion();
$conexion = $objeto->Conectar();

// Recibir los datos enviados por POST
$correo = isset($_POST['correo']) ? $_POST['correo'] : '';
$contrasena = isset($_POST['contrasena']) ? $_POST['contrasena'] : '';

// Consulta para verificar el usuario y el rol
$consulta = "SELECT idusuarios, nombre_usuario, Roles_idroles 
             FROM usuarios 
             WHERE correo_ususario = ? AND contraseña_usuario = ? AND Roles_idroles = 3";
$stmt = $conexion->prepare($consulta);
$stmt->execute([$correo, $contrasena]); // Aquí estamos pasando los dos parámetros correctamente

$resultado = $stmt->fetch(PDO::FETCH_ASSOC);

if ($resultado) {
    // Usuario válido y con rol de administrador
    $_SESSION['idUsuario'] = $resultado['idusuarios'];
    $_SESSION['nombreUsuario'] = $resultado['nombre_usuario'];
    $_SESSION['rolUsuario'] = $resultado['Roles_idroles'];
    
    // Redirigir al panel principal
    header("Location: usuarios.php");
} else {
    // Usuario inválido o sin permisos
    $_SESSION['error'] = "Credenciales inválidas o no tienes permisos para acceder.";
    header("Location: index.php");
}

$conexion = null; // Cerrar la conexión
?>
