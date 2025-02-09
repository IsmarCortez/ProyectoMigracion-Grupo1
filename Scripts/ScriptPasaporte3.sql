CREATE DATABASE IF NOT EXISTS sistemapasaportes;
USE sistemapasaportes;

-- Tabla Nacionalidad
CREATE TABLE IF NOT EXISTS nacionalidad (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL
);

-- Tabla Usuario
CREATE TABLE IF NOT EXISTS usuario (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL,
  apellido VARCHAR(50) NOT NULL,
  fecha_nacimiento DATE NOT NULL,
  genero ENUM('M', 'F', 'Otro') NOT NULL,
  curp VARCHAR(18) UNIQUE NOT NULL,
  nacionalidad_id INT NOT NULL,
  direccion TEXT NOT NULL,
  telefono VARCHAR(15) NOT NULL,
  correo VARCHAR(100) UNIQUE,
  FOREIGN KEY (nacionalidad_id) REFERENCES nacionalidad(id)
);

-- Tabla Oficina
CREATE TABLE IF NOT EXISTS oficina (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(100) NOT NULL,
  direccion TEXT NOT NULL,
  telefono VARCHAR(15) NOT NULL
);

-- Tabla Empleado
CREATE TABLE IF NOT EXISTS empleado (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL,
  apellido VARCHAR(50) NOT NULL,
  cargo ENUM('Atencion', 'Supervisor', 'Administrador') NOT NULL,
  oficina_id INT NOT NULL,
  FOREIGN KEY (oficina_id) REFERENCES oficina(id)
);

-- Tabla Estado de Solicitud
CREATE TABLE IF NOT EXISTS estado_solicitud (
  id INT AUTO_INCREMENT PRIMARY KEY,
  estado VARCHAR(50) NOT NULL
);

-- Tabla Cita
CREATE TABLE IF NOT EXISTS cita (
  id INT AUTO_INCREMENT PRIMARY KEY,
  usuario_id INT NOT NULL,
  fecha DATETIME NOT NULL,
  oficina_id INT NOT NULL,
  empleado_id INT NOT NULL,
  estado_id INT NOT NULL,
  FOREIGN KEY (usuario_id) REFERENCES usuario(id),
  FOREIGN KEY (oficina_id) REFERENCES oficina(id),
  FOREIGN KEY (empleado_id) REFERENCES empleado(id),
  FOREIGN KEY (estado_id) REFERENCES estado_solicitud(id)
);

-- Tabla Pasaporte
CREATE TABLE IF NOT EXISTS pasaporte (
  id INT AUTO_INCREMENT PRIMARY KEY,
  usuario_id INT NOT NULL,
  numero VARCHAR(15) UNIQUE NOT NULL,
  fecha_emision DATE NOT NULL,
  fecha_expiracion DATE NOT NULL,
  estado_id INT NOT NULL,
  FOREIGN KEY (usuario_id) REFERENCES usuario(id),
  FOREIGN KEY (estado_id) REFERENCES estado_solicitud(id)
);

-- Tabla Pago
CREATE TABLE IF NOT EXISTS pago (
  id INT AUTO_INCREMENT PRIMARY KEY,
  usuario_id INT NOT NULL,
  monto DECIMAL(10,2) NOT NULL,
  metodo ENUM('Tarjeta', 'Efectivo', 'Transferencia') NOT NULL,
  fecha DATETIME NOT NULL,
  FOREIGN KEY (usuario_id) REFERENCES usuario(id)
);
