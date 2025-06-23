USE [master]
GO
/****** Object:  Database [PredictorBDD]    Script Date: 12/6/2025 10:42:43 ******/
CREATE DATABASE [PredictorBDD]
GO

USE [PredictorBDD]

/****** Object:  Table [dbo].[Usuario]    Script Date: 12/6/2025 10:42:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[nombre] [nvarchar](100) NOT NULL,
	[apellido] [nvarchar](100) NOT NULL,
	[tokenconfirmacion] [nvarchar](255),
	[contrasenia] [nvarchar](255) NOT NULL,
	[activo] [bit] NOT NULL,
	[verificado] [bit] NOT NULL,
	[administrador] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((0)) FOR [verificado]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((0)) FOR [administrador]
GO
USE [master]
GO
ALTER DATABASE [PredictorBDD] SET  READ_WRITE 
GO


INSERT INTO [dbo].[Usuario] ([email], [nombre], [apellido], [contrasenia], [activo], [verificado], [administrador])
VALUES 
('admin@pw3.com', 'Admin', 'PW3', 'admin123', 1, 1, 1),
('user@normal.com', 'User', 'Normal', 'user123', 0, 0, 0),
('Loren@pw3.com', 'Loren', 'Noceda', 'admin123', 0, 1, 1),
('Felipe@normal.com', 'Felipe', 'Wigel', 'user123', 0, 1, 0),
('Tomas@pw3.com', 'Tomas', 'Nania', 'admin123', 1, 0, 1),
('Lucas@normal.com', 'Lucas', 'Rios', 'user123', 1, 1, 0),
('Julian@pw3.com', 'Julian', 'Schmuker', 'admin123', 0, 1, 0),
('German@normal.com', 'German', 'Schmuker', 'user123', 1, 1, 0);
GO

SELECT * FROM [dbo].[Usuario]
GO 







/*  ACÁ VA LO DE PROCESAR IMAGEN, la que guarda la imagen y las personas que se detectaron dentro, si se borra el resultado, se borran las personas que estaban detectadas automaticamente por el ON DELETE CASCADE  */
USE PredictorBDD

SELECT * FROM Usuario;

-- Tabla ResultadoImagen
CREATE TABLE ResultadoImagen (
    resultadoImagenId INT PRIMARY KEY IDENTITY(1,1),
    userId INT NOT NULL,
    rutaImg NVARCHAR(255) NOT NULL,
    FOREIGN KEY (userId) REFERENCES Usuario(userId)
);
GO

SELECT * FROM ResultadoImagen;
GO
-- Tabla PersonaDetectada
CREATE TABLE PersonaDetectada (
    personaDetectadaId INT PRIMARY KEY IDENTITY(1,1),
    resultadoImagenId INT NOT NULL,
    descripcionPersona NVARCHAR(100) NOT NULL,
    FOREIGN KEY (resultadoImagenId) REFERENCES ResultadoImagen(resultadoImagenId) ON DELETE CASCADE
);
GO

SELECT * FROM PersonaDetectada;
GO


USE PredictorBDD
GO

CREATE TABLE FraseIdioma (
    fraseIdiomaId INT PRIMARY KEY IDENTITY(1,1),
    fraseEnIdioma NVARCHAR(MAX) NOT NULL,
    idioma NVARCHAR(50) NOT NULL,
    porcentajeDeConfianza FLOAT NOT NULL
);
GO

USE [PredictorBDD]
GO

CREATE TABLE [dbo].[DatoSentimiento] (
    [resultadoId] INT IDENTITY(1,1) PRIMARY KEY,
    [fraseConSentimiento] NVARCHAR(MAX) NOT NULL,
    [sentimiento] NVARCHAR(100) NOT NULL,
    [porcentajeDeConfianza] FLOAT NOT NULL
);
GO

USE [PredictorBDD]
GO

/****** Object:  Table [dbo].[DatoPolaridad]    Script Date: 22/6/2025 21:05:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DatoPolaridad](
	[datoPolaridadId] [int] IDENTITY(1,1) NOT NULL,
	[_textoProcesado] [nvarchar](max) NULL,
	[_resutlado] [nvarchar](50) NULL,
	[_probabilidadNegativa] [float] NULL,
	[_probabilidadPositiva] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[datoPolaridadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/* este lo dejé para probar si se borraban en cascada las personas si borraban las imagenes, ajustar el id del user y el de el resultado img para probar el delete
INSERT INTO ResultadoImagen (userId, rutaImg)
VALUES (2, 'foto1.jpg');
GO

DECLARE @idResultadoImagen INT = SCOPE_IDENTITY();

INSERT INTO PersonaDetectada (resultadoImagenId, descripcionPersona)
VALUES (@idResultadoImagen, 'Persona Detectada 1');
GO


SELECT * FROM ResultadoImagen;
SELECT * FROM PersonaDetectada;


DELETE FROM ResultadoImagen WHERE resultadoImagenId = 1;
GO

SELECT * FROM ResultadoImagen;
SELECT * FROM PersonaDetectada;*/



