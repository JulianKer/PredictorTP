USE [master]
GO
/****** Object:  Database [PredictorBDD]    Script Date: 23/6/2025 11:10:50 ******/
CREATE DATABASE [PredictorBDD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PredictorBDD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\PredictorBDD.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PredictorBDD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\PredictorBDD_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PredictorBDD] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PredictorBDD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PredictorBDD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PredictorBDD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PredictorBDD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PredictorBDD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PredictorBDD] SET ARITHABORT OFF 
GO
ALTER DATABASE [PredictorBDD] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PredictorBDD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PredictorBDD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PredictorBDD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PredictorBDD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PredictorBDD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PredictorBDD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PredictorBDD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PredictorBDD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PredictorBDD] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PredictorBDD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PredictorBDD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PredictorBDD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PredictorBDD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PredictorBDD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PredictorBDD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PredictorBDD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PredictorBDD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PredictorBDD] SET  MULTI_USER 
GO
ALTER DATABASE [PredictorBDD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PredictorBDD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PredictorBDD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PredictorBDD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PredictorBDD] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PredictorBDD] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PredictorBDD] SET QUERY_STORE = ON
GO
ALTER DATABASE [PredictorBDD] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PredictorBDD]
GO
/****** Object:  Table [dbo].[DatoIdioma]    Script Date: 23/6/2025 11:10:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DatoIdioma](
	[fraseIdiomaId] [int] IDENTITY(1,1) NOT NULL,
	[fraseEnIdioma] [nvarchar](max) NOT NULL,
	[idioma] [nvarchar](50) NOT NULL,
	[porcentajeDeConfianza] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[fraseIdiomaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DatoPolaridad]    Script Date: 23/6/2025 11:10:51 ******/
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
/****** Object:  Table [dbo].[DatoSentimiento]    Script Date: 23/6/2025 11:10:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DatoSentimiento](
	[resultadoId] [int] IDENTITY(1,1) NOT NULL,
	[fraseConSentimiento] [nvarchar](max) NOT NULL,
	[sentimiento] [nvarchar](100) NOT NULL,
	[porcentajeDeConfianza] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[resultadoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonaDetectada]    Script Date: 23/6/2025 11:10:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonaDetectada](
	[personaDetectadaId] [int] IDENTITY(1,1) NOT NULL,
	[resultadoImagenId] [int] NOT NULL,
	[descripcionPersona] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[personaDetectadaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResultadoImagen]    Script Date: 23/6/2025 11:10:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResultadoImagen](
	[resultadoImagenId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[rutaImg] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[resultadoImagenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 23/6/2025 11:10:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[nombre] [nvarchar](100) NOT NULL,
	[apellido] [nvarchar](100) NOT NULL,
	[tokenconfirmacion] [nvarchar](255) NULL,
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
ALTER TABLE [dbo].[PersonaDetectada]  WITH CHECK ADD FOREIGN KEY([resultadoImagenId])
REFERENCES [dbo].[ResultadoImagen] ([resultadoImagenId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ResultadoImagen]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[Usuario] ([userId])
GO
USE [master]
GO
ALTER DATABASE [PredictorBDD] SET  READ_WRITE 
GO






/*INSERTS ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/






/*+++++++++++++++++++++++++++++++   USUARIOS  ++++++++++++++++++++++++++++++++++++++++++++++*/
/* (NO USAR ESTOS USUARIOS PQ NO TIENEN LA PASS HASHEADA, registrar uno nuevo para entrar a la app y si quiere ser un admin, hacer un update de su ussuario con "administrador = 1")------------------------------------------------------*/

INSERT INTO [dbo].[Usuario] (email, nombre, apellido, tokenconfirmacion, contrasenia, activo, verificado, administrador) VALUES
-- Administradores (4)
('admin1@predictor.com', 'Carlos', 'Ramirez', NULL, 'admin123', 1, 1, 1),
('admin2@predictor.com', 'Lucía', 'Fernandez', NULL, 'admin123', 1, 1, 1),
('admin3@predictor.com', 'Miguel', 'Sanchez', NULL, 'admin123', 1, 0, 1),
('admin4@predictor.com', 'Ana', 'Lopez', NULL, 'admin123', 0, 0, 1),

-- No verificados (5)
('nover1@predictor.com', 'Jorge', 'Martinez', NULL, 'pass123', 1, 0, 0),
('nover2@predictor.com', 'Laura', 'Gutierrez', NULL, 'pass123', 1, 0, 0),
('nover3@predictor.com', 'Pedro', 'Silva', NULL, 'pass123', 0, 0, 0),
('nover4@predictor.com', 'Sofia', 'Mendez', NULL, 'pass123', 1, 0, 0),
('nover5@predictor.com', 'Diego', 'Rojas', NULL, 'pass123', 0, 0, 0),

-- Verificados, mitad activos (3 activos, 3 no activos)
('user1@predictor.com', 'Valentina', 'Nuñez', NULL, 'user123', 1, 1, 0),
('user2@predictor.com', 'Marcos', 'Perez', NULL, 'user123', 1, 1, 0),
('user3@predictor.com', 'Agustina', 'Diaz', NULL, 'user123', 1, 1, 0),
('user4@predictor.com', 'Nicolas', 'Bravo', NULL, 'user123', 0, 1, 0),
('user5@predictor.com', 'Camila', 'Reyes', NULL, 'user123', 0, 1, 0),
('user6@predictor.com', 'Ezequiel', 'Torres', NULL, 'user123', 0, 1, 0);

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/



/*---------------------------- RESULTADO IMAGEN------------------------------------------------------------------------------------------------------------------------------------------*/
INSERT INTO [dbo].[ResultadoImagen] (userId, rutaImg) VALUES
(1, 'foto1.jpeg'),
(2, 'foto2.jpeg'),
(3, 'foto3.jpeg'),
(4, 'foto4.jpeg'),
(5, 'foto5.jpeg'),
(6, 'foto6.jpeg'),
(7, 'foto7.jpeg'),
(8, 'foto8.jpeg'),
(9, 'foto9.jpeg'),
(10, 'foto10.jpeg');
/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/




/*---------------------------- Personas detectadas -------------------------------------------------------------------------------------------------------------------------------*/
INSERT INTO [dbo].[PersonaDetectada] (resultadoImagenId, descripcionPersona) VALUES
-- Imagen 1
(1, '?? Persona 1 = Hombre: 81% | Edad: 16 | Emoción: Feliz'),
(1, '?? Persona 2 = Mujer: 76% | Edad: 34 | Emoción: Triste'),

-- Imagen 2
(2, '?? Persona 1 = Hombre: 89% | Edad: 25 | Emoción: Enojado'),
(2, '?? Persona 2 = Mujer: 92% | Edad: 42 | Emoción: Sorprendido'),
(2, '?? Persona 3 = Hombre: 67% | Edad: 60 | Emoción: Miedo'),

-- Imagen 3
(3, '?? Persona 1 = Mujer: 73% | Edad: 19 | Emoción: Feliz'),

-- Imagen 5
(5, '?? Persona 1 = Mujer: 85% | Edad: 28 | Emoción: Triste'),
(5, '?? Persona 2 = Hombre: 78% | Edad: 36 | Emoción: Enojado'),

-- Imagen 6
(6, '?? Persona 1 = Niño: 90% | Edad: 10 | Emoción: Sorprendido'),

-- Imagen 8
(8, '?? Persona 1 = Hombre: 83% | Edad: 30 | Emoción: Feliz'),

-- Imagen 9
(9, '?? Persona 1 = Hombre: 88% | Edad: 70 | Emoción: Miedo'),
(9, '?? Persona 2 = Mujer: 80% | Edad: 22 | Emoción: Sorprendido'),

-- Imagen 10
(10, '?? Persona 1 = Mujer: 86% | Edad: 40 | Emoción: Triste');
/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/



/*--------------- DATO IDIOMA -----------------------------------------------------------------------------------------------------------------------------------------------------*/
INSERT INTO [dbo].[DatoIdioma] (fraseEnIdioma, idioma, porcentajeDeConfianza) VALUES
('Hola, ¿cómo estás?', 'Español', 99.8761),
('Good morning!', 'Inglés', 99.4325),
('Vamos al cine esta noche', 'Español', 99.9213),
('What are you doing?', 'Inglés', 98.7654),
('Me encanta la programación', 'Español', 99.9832),
('See you tomorrow', 'Inglés', 99.5547),
('Estoy aprendiendo SQL', 'Español', 99.7310),
('I love learning new things', 'Inglés', 99.8244),
('La inteligencia artificial es fascinante', 'Español', 99.9981),
('Can we meet later?', 'Inglés', 98.9342);


/*----------------- Dato Polaridad -------------------------------------------------------------------------------------------------------------------------------------------------*/

INSERT INTO [dbo].[DatoPolaridad] (_textoProcesado, _resutlado, _probabilidadNegativa, _probabilidadPositiva) VALUES
('Me encanta este lugar', 'Positiva', 0.35, 99.65),
('Este día ha sido horrible', 'Negativa', 98.80, 1.20),
('Qué alegría verte', 'Positiva', 0.91, 99.09),
('No soporto más esta situación', 'Negativa', 97.12, 2.88),
('La comida estuvo deliciosa', 'Positiva', 1.45, 98.55),
('Estoy muy decepcionado', 'Negativa', 96.47, 3.53),
('Todo salió perfecto', 'Positiva', 0.76, 99.24),
('Estoy cansado de intentarlo', 'Negativa', 95.33, 4.67),
('Hoy fue un buen día', 'Positiva', 1.11, 98.89),
('Nada tiene sentido ya', 'Negativa', 99.21, 0.79);


/*----------------- DatoSentimiento -------------------------------------------------------------------------------------------------------------------------------------------------*/
INSERT INTO [dbo].[DatoSentimiento] (fraseConSentimiento, sentimiento, porcentajeDeConfianza) VALUES
('Me siento invisible', 'Tristeza', 88.0159),
('Hoy no me puede pasar nada', 'Tristeza', 54.3633),
('Amo la vida', 'Felicidad', 56.7243),
('Me equivoqué frente a todos', 'Vergüenza', 73.1198),
('Tengo miedo de lo que va a pasar', 'Miedo', 92.3301),
('Estoy tan feliz por la noticia', 'Felicidad', 87.5426),
('Siempre arruino todo', 'Frustración', 81.4572),
('No puedo dejar de pensar en el error', 'Vergüenza', 66.8394),
('Estoy furioso con lo que hicieron', 'Enojo', 95.2135),
('No quiero salir de la cama hoy', 'Tristeza', 79.0041),
('Esto me supera por completo', 'Frustración', 84.6792),
('Siento que algo malo va a pasar', 'Miedo', 90.2205);
/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/





/*
Use PredictorBDD
DELETE from Usuario
DELETE from DatoIdioma
DELETE from DatoPolaridad
DELETE from DatoSentimiento
DELETE from ResultadoImagen
DELETE from PersonaDetectada

DBCC CHECKIDENT ('PersonaDetectada', RESEED, 0);
DBCC CHECKIDENT ('ResultadoImagen', RESEED, 0);
DBCC CHECKIDENT ('DatoSentimiento', RESEED, 0);
DBCC CHECKIDENT ('DatoPolaridad', RESEED, 0);
DBCC CHECKIDENT ('DatoIdioma', RESEED, 0);
DBCC CHECKIDENT ('Usuario', RESEED, 0);
*/