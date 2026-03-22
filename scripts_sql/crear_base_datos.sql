-- =============================================
-- Script para crear la base de datos y tabla
-- =============================================

-- Crear base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'UsuariosDB')
BEGIN
    CREATE DATABASE UsuariosDB;
END
GO

USE UsuariosDB;
GO

-- Crear tabla de usuarios
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND type in (N'U'))
BEGIN
    CREATE TABLE Usuarios (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Username NVARCHAR(50) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(255) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        Nombre NVARCHAR(100) NOT NULL,
        Apellido NVARCHAR(100) NOT NULL,
        Telefono NVARCHAR(20),
        FechaNacimiento DATE,
        Activo BIT DEFAULT 1,
        Bloqueado BIT DEFAULT 0,
        IntentosFallidos INT DEFAULT 0,
        UltimoBloqueo DATETIME NULL,
        FechaCreacion DATETIME DEFAULT GETDATE(),
        UltimoAcceso DATETIME NULL
    );
END
GO