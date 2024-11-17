-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: veterinaria_atencion_mascotas_db
-- ------------------------------------------------------
-- Server version	8.1.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `atenciones_y_servicios`
--

DROP TABLE IF EXISTS `atenciones_y_servicios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `atenciones_y_servicios` (
  `idcitas` int NOT NULL AUTO_INCREMENT,
  `fecha_apartada` date NOT NULL,
  `hora_inicio` time NOT NULL,
  `hora_final` time NOT NULL,
  `estados_cita_idestados_cita` int NOT NULL,
  `tipo_atencion_o_servicio_idatencion_o_servicio` int NOT NULL,
  `usuarios_veterinario_idusuarios` int NOT NULL,
  `mascotas_idmascotas` int NOT NULL,
  PRIMARY KEY (`idcitas`),
  KEY `fk_citas_estados_cita1_idx` (`estados_cita_idestados_cita`),
  KEY `fk_atenciones_y_servicios_tipo_atencion_o_servicio1_idx` (`tipo_atencion_o_servicio_idatencion_o_servicio`),
  KEY `fk_atenciones_y_servicios_usuarios1_idx` (`usuarios_veterinario_idusuarios`),
  KEY `fk_atenciones_y_servicios_mascotas1_idx` (`mascotas_idmascotas`),
  CONSTRAINT `fk_atenciones_y_servicios_mascotas1` FOREIGN KEY (`mascotas_idmascotas`) REFERENCES `mascotas` (`idmascotas`),
  CONSTRAINT `fk_atenciones_y_servicios_tipo_atencion_o_servicio1` FOREIGN KEY (`tipo_atencion_o_servicio_idatencion_o_servicio`) REFERENCES `tipo_atencion_o_servicio` (`idatencion_o_servicio`),
  CONSTRAINT `fk_atenciones_y_servicios_usuarios1` FOREIGN KEY (`usuarios_veterinario_idusuarios`) REFERENCES `usuarios` (`idusuarios`),
  CONSTRAINT `fk_citas_estados_cita1` FOREIGN KEY (`estados_cita_idestados_cita`) REFERENCES `estados_cita` (`idestados_cita`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `atenciones_y_servicios`
--

LOCK TABLES `atenciones_y_servicios` WRITE;
/*!40000 ALTER TABLE `atenciones_y_servicios` DISABLE KEYS */;
INSERT INTO `atenciones_y_servicios` VALUES (1,'2024-11-10','10:00:00','11:00:00',1,1,1,1),(2,'2024-11-11','14:00:00','15:00:00',2,2,2,2);
/*!40000 ALTER TABLE `atenciones_y_servicios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `carnet_vacunaciones`
--

DROP TABLE IF EXISTS `carnet_vacunaciones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `carnet_vacunaciones` (
  `vacunas_idvacunas` int NOT NULL,
  `fecha_hora_vacunacion` datetime NOT NULL,
  `mascotas_idmascotas` int NOT NULL,
  `usuarios_veterinario_idusuarios` int NOT NULL,
  KEY `fk_mascotas_has_vacunas_vacunas1_idx` (`vacunas_idvacunas`),
  KEY `fk_carnet_vacunaciones_mascotas1_idx` (`mascotas_idmascotas`),
  KEY `fk_carnet_vacunaciones_usuarios1_idx` (`usuarios_veterinario_idusuarios`),
  CONSTRAINT `fk_carnet_vacunaciones_mascotas1` FOREIGN KEY (`mascotas_idmascotas`) REFERENCES `mascotas` (`idmascotas`),
  CONSTRAINT `fk_carnet_vacunaciones_usuarios1` FOREIGN KEY (`usuarios_veterinario_idusuarios`) REFERENCES `usuarios` (`idusuarios`),
  CONSTRAINT `fk_mascotas_has_vacunas_vacunas1` FOREIGN KEY (`vacunas_idvacunas`) REFERENCES `vacunas` (`idvacunas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `carnet_vacunaciones`
--

LOCK TABLES `carnet_vacunaciones` WRITE;
/*!40000 ALTER TABLE `carnet_vacunaciones` DISABLE KEYS */;
/*!40000 ALTER TABLE `carnet_vacunaciones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `detalle_pagos`
--

DROP TABLE IF EXISTS `detalle_pagos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `detalle_pagos` (
  `iddetalle_pagos` int NOT NULL AUTO_INCREMENT,
  `fecha_hora_pago` datetime NOT NULL,
  `monto_pago` decimal(10,0) NOT NULL,
  `detalle_pagoscol` varchar(45) DEFAULT NULL,
  `estados_pago_idestados_pago` int NOT NULL,
  `tipos_pago_idtipos_pago` int NOT NULL,
  `facturas_idfacturas` int NOT NULL,
  PRIMARY KEY (`iddetalle_pagos`),
  KEY `fk_detalle_pagos_estados_pago1_idx` (`estados_pago_idestados_pago`),
  KEY `fk_detalle_pagos_tipos_pago1_idx` (`tipos_pago_idtipos_pago`),
  KEY `fk_detalle_pagos_facturas1_idx` (`facturas_idfacturas`),
  CONSTRAINT `fk_detalle_pagos_estados_pago1` FOREIGN KEY (`estados_pago_idestados_pago`) REFERENCES `estados_pago` (`idestados_pago`),
  CONSTRAINT `fk_detalle_pagos_facturas1` FOREIGN KEY (`facturas_idfacturas`) REFERENCES `facturas` (`idfacturas`),
  CONSTRAINT `fk_detalle_pagos_tipos_pago1` FOREIGN KEY (`tipos_pago_idtipos_pago`) REFERENCES `tipos_pago` (`idtipos_pago`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `detalle_pagos`
--

LOCK TABLES `detalle_pagos` WRITE;
/*!40000 ALTER TABLE `detalle_pagos` DISABLE KEYS */;
/*!40000 ALTER TABLE `detalle_pagos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `diagnosticos`
--

DROP TABLE IF EXISTS `diagnosticos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `diagnosticos` (
  `iddiagnosticos` int NOT NULL AUTO_INCREMENT,
  `descripcion_diagnostico` varchar(100) NOT NULL,
  `atenciones_y_servicios_idcitas` int NOT NULL,
  PRIMARY KEY (`iddiagnosticos`),
  KEY `fk_diagnosticos_atenciones_y_servicios1_idx` (`atenciones_y_servicios_idcitas`),
  CONSTRAINT `fk_diagnosticos_atenciones_y_servicios1` FOREIGN KEY (`atenciones_y_servicios_idcitas`) REFERENCES `atenciones_y_servicios` (`idcitas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `diagnosticos`
--

LOCK TABLES `diagnosticos` WRITE;
/*!40000 ALTER TABLE `diagnosticos` DISABLE KEYS */;
/*!40000 ALTER TABLE `diagnosticos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `estados_cita`
--

DROP TABLE IF EXISTS `estados_cita`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `estados_cita` (
  `idestados_cita` int NOT NULL,
  `nombre_estado` varchar(45) NOT NULL,
  PRIMARY KEY (`idestados_cita`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estados_cita`
--

LOCK TABLES `estados_cita` WRITE;
/*!40000 ALTER TABLE `estados_cita` DISABLE KEYS */;
INSERT INTO `estados_cita` VALUES (1,'Pendiente'),(2,'Completada');
/*!40000 ALTER TABLE `estados_cita` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `estados_pago`
--

DROP TABLE IF EXISTS `estados_pago`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `estados_pago` (
  `idestados_pago` int NOT NULL AUTO_INCREMENT,
  `nombre_pago` varchar(45) NOT NULL,
  PRIMARY KEY (`idestados_pago`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estados_pago`
--

LOCK TABLES `estados_pago` WRITE;
/*!40000 ALTER TABLE `estados_pago` DISABLE KEYS */;
/*!40000 ALTER TABLE `estados_pago` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `facturas`
--

DROP TABLE IF EXISTS `facturas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `facturas` (
  `idfacturas` int NOT NULL AUTO_INCREMENT,
  `usuarios_idusuarios` int NOT NULL,
  PRIMARY KEY (`idfacturas`),
  KEY `fk_facturas_usuarios1_idx` (`usuarios_idusuarios`),
  CONSTRAINT `fk_facturas_usuarios1` FOREIGN KEY (`usuarios_idusuarios`) REFERENCES `usuarios` (`idusuarios`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `facturas`
--

LOCK TABLES `facturas` WRITE;
/*!40000 ALTER TABLE `facturas` DISABLE KEYS */;
/*!40000 ALTER TABLE `facturas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `facturas_has_atenciones_y_servicios`
--

DROP TABLE IF EXISTS `facturas_has_atenciones_y_servicios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `facturas_has_atenciones_y_servicios` (
  `facturas_idfacturas` int NOT NULL,
  `atenciones_y_servicios_idcitas` int NOT NULL,
  PRIMARY KEY (`facturas_idfacturas`,`atenciones_y_servicios_idcitas`),
  KEY `fk_facturas_has_atenciones_y_servicios_atenciones_y_servici_idx` (`atenciones_y_servicios_idcitas`),
  KEY `fk_facturas_has_atenciones_y_servicios_facturas1_idx` (`facturas_idfacturas`),
  CONSTRAINT `fk_facturas_has_atenciones_y_servicios_atenciones_y_servicios1` FOREIGN KEY (`atenciones_y_servicios_idcitas`) REFERENCES `atenciones_y_servicios` (`idcitas`),
  CONSTRAINT `fk_facturas_has_atenciones_y_servicios_facturas1` FOREIGN KEY (`facturas_idfacturas`) REFERENCES `facturas` (`idfacturas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `facturas_has_atenciones_y_servicios`
--

LOCK TABLES `facturas_has_atenciones_y_servicios` WRITE;
/*!40000 ALTER TABLE `facturas_has_atenciones_y_servicios` DISABLE KEYS */;
/*!40000 ALTER TABLE `facturas_has_atenciones_y_servicios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `horarios_atencion`
--

DROP TABLE IF EXISTS `horarios_atencion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `horarios_atencion` (
  `idhorarios_atencion` int NOT NULL,
  `hora_inicio` time NOT NULL,
  `hora_fin` time NOT NULL,
  `fecha_inicio` date NOT NULL,
  `fecha_fin` date NOT NULL,
  PRIMARY KEY (`idhorarios_atencion`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `horarios_atencion`
--

LOCK TABLES `horarios_atencion` WRITE;
/*!40000 ALTER TABLE `horarios_atencion` DISABLE KEYS */;
/*!40000 ALTER TABLE `horarios_atencion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mascotas`
--

DROP TABLE IF EXISTS `mascotas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mascotas` (
  `idmascotas` int NOT NULL AUTO_INCREMENT,
  `nombre_mascota` varchar(20) NOT NULL,
  `edad_mascota` int NOT NULL,
  `fecha_nacimiento` date NOT NULL,
  `tipo_animal` varchar(45) NOT NULL,
  `raza_animal` varchar(60) NOT NULL,
  `usuarios_dueno_idusuarios` int NOT NULL,
  PRIMARY KEY (`idmascotas`),
  KEY `fk_mascotas_usuarios_idx` (`usuarios_dueno_idusuarios`),
  CONSTRAINT `fk_mascotas_usuarios` FOREIGN KEY (`usuarios_dueno_idusuarios`) REFERENCES `usuarios` (`idusuarios`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mascotas`
--

LOCK TABLES `mascotas` WRITE;
/*!40000 ALTER TABLE `mascotas` DISABLE KEYS */;
INSERT INTO `mascotas` VALUES (1,'Fido',3,'2020-05-15','Perro','Labrador',1),(2,'Mia',5,'2018-03-22','Gato','Siames',2);
/*!40000 ALTER TABLE `mascotas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `idroles` int NOT NULL AUTO_INCREMENT,
  `nombre_rol` varchar(45) NOT NULL,
  PRIMARY KEY (`idroles`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'Cliente'),(2,'Veterinario');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tipo_atencion_o_servicio`
--

DROP TABLE IF EXISTS `tipo_atencion_o_servicio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tipo_atencion_o_servicio` (
  `idatencion_o_servicio` int NOT NULL AUTO_INCREMENT,
  `nombre_tipo` varchar(30) NOT NULL,
  `precio_tipo` decimal(10,0) NOT NULL,
  PRIMARY KEY (`idatencion_o_servicio`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tipo_atencion_o_servicio`
--

LOCK TABLES `tipo_atencion_o_servicio` WRITE;
/*!40000 ALTER TABLE `tipo_atencion_o_servicio` DISABLE KEYS */;
INSERT INTO `tipo_atencion_o_servicio` VALUES (1,'Consulta General',50000),(2,'Vacunación',30000);
/*!40000 ALTER TABLE `tipo_atencion_o_servicio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tipos_pago`
--

DROP TABLE IF EXISTS `tipos_pago`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tipos_pago` (
  `idtipos_pago` int NOT NULL,
  `nombre_tipo_pago` varchar(45) NOT NULL,
  PRIMARY KEY (`idtipos_pago`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tipos_pago`
--

LOCK TABLES `tipos_pago` WRITE;
/*!40000 ALTER TABLE `tipos_pago` DISABLE KEYS */;
/*!40000 ALTER TABLE `tipos_pago` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `idusuarios` int NOT NULL AUTO_INCREMENT,
  `nombre_usuario` varchar(45) NOT NULL,
  `correo_ususario` varchar(45) NOT NULL,
  `telefono_usuario` varchar(10) NOT NULL,
  `direccion_usuario` varchar(30) NOT NULL,
  `contraseña_usuario` varchar(45) NOT NULL,
  `Roles_idroles` int NOT NULL,
  PRIMARY KEY (`idusuarios`),
  KEY `fk_usuarios_Roles1_idx` (`Roles_idroles`),
  CONSTRAINT `fk_usuarios_Roles1` FOREIGN KEY (`Roles_idroles`) REFERENCES `roles` (`idroles`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'Juan Perez','juan.perez@example.com','3001234567','Calle 123 #45-67','password123',1),(2,'Maria Gomez','maria.gomez@example.com','3109876543','Carrera 45 #67-89','password456',2);
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios_has_horarios_atencion`
--

DROP TABLE IF EXISTS `usuarios_has_horarios_atencion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios_has_horarios_atencion` (
  `usuarios_idusuarios` int NOT NULL,
  `horarios_atencion_idhorarios_atencion` int NOT NULL,
  PRIMARY KEY (`usuarios_idusuarios`,`horarios_atencion_idhorarios_atencion`),
  KEY `fk_usuarios_has_horarios_atencion_horarios_atencion1_idx` (`horarios_atencion_idhorarios_atencion`),
  KEY `fk_usuarios_has_horarios_atencion_usuarios1_idx` (`usuarios_idusuarios`),
  CONSTRAINT `fk_usuarios_has_horarios_atencion_horarios_atencion1` FOREIGN KEY (`horarios_atencion_idhorarios_atencion`) REFERENCES `horarios_atencion` (`idhorarios_atencion`),
  CONSTRAINT `fk_usuarios_has_horarios_atencion_usuarios1` FOREIGN KEY (`usuarios_idusuarios`) REFERENCES `usuarios` (`idusuarios`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios_has_horarios_atencion`
--

LOCK TABLES `usuarios_has_horarios_atencion` WRITE;
/*!40000 ALTER TABLE `usuarios_has_horarios_atencion` DISABLE KEYS */;
/*!40000 ALTER TABLE `usuarios_has_horarios_atencion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vacunas`
--

DROP TABLE IF EXISTS `vacunas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vacunas` (
  `idvacunas` int NOT NULL AUTO_INCREMENT,
  `nombre_vacuna` varchar(45) NOT NULL,
  `dosis_vacuna` varchar(45) NOT NULL,
  `descripcion_vacuna` varchar(45) NOT NULL,
  PRIMARY KEY (`idvacunas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vacunas`
--

LOCK TABLES `vacunas` WRITE;
/*!40000 ALTER TABLE `vacunas` DISABLE KEYS */;
/*!40000 ALTER TABLE `vacunas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-11-17 16:50:33
