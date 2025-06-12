USE [master]
GO
/****** Object:  Database [PredictorBDD]    Script Date: 12/6/2025 10:42:43 ******/
CREATE DATABASE [PredictorBDD]
GO

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
	[contraseña] [nvarchar](255) NOT NULL,
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


INSERT INTO [dbo].[Usuario] ([email], [nombre], [apellido], [contraseña], [activo], [verificado], [administrador])
VALUES 
('admin@pw3.com', 'Admin', 'PW3', 'admin123', 1, 1, 1),
('user@normal.com', 'User', 'Normal', 'user123', 1, 1, 0);
GO

SELECT * FROM [dbo].[Usuario]
GO