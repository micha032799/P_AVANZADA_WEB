USE [proyecto_web]
GO

/****** Object:  Table [dbo].[tReserva]    Script Date: 24-Apr-24 23:42:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tReserva](
	[IdReserva] [bigint] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [bigint] NOT NULL,
	[CantidadPersonas] [int] NOT NULL,
	[FechaReserva] [datetime] NOT NULL,
	[Estado] [bit] NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_tReserva] PRIMARY KEY CLUSTERED 
(
	[IdReserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tReserva]  WITH CHECK ADD  CONSTRAINT [FK_tReserva_tUsuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[tUsuario] ([IdUsuario])
GO

ALTER TABLE [dbo].[tReserva] CHECK CONSTRAINT [FK_tReserva_tUsuario]
GO


CREATE PROCEDURE [dbo].[ActualizarReserva]
	@IdReserva			BIGINT,
	@IdUsuario			BIGINT,
	@CantidadPersonas	INT,
	@FechaReserva		DATETIME,
	@Precio				DECIMAL(18,2)
AS
BEGIN

	IF NOT EXISTS(	SELECT	1 FROM tReserva 
					WHERE	IdUsuario = @IdUsuario 
						AND CONVERT(DATE,FechaReserva) = CONVERT(DATE,@FechaReserva))
	BEGIN

		UPDATE dbo.tReserva
		   SET IdUsuario = @IdUsuario,
			   CantidadPersonas = @CantidadPersonas,
			   FechaReserva = @FechaReserva,
			   Precio = @Precio
		 WHERE IdReserva = @IdReserva

	END

END
GO


CREATE PROCEDURE [dbo].[ConsultarReserva]
	@IdReserva BIGINT
AS
BEGIN

	SELECT	R.IdReserva,
			R.IdUsuario,
			U.Nombre,
			R.CantidadPersonas,
			R.FechaReserva,
			DATEADD(HOUR,2, R.FechaReserva) 'FechaFinReserva',
			R.Estado,
			Precio,
			(CASE	WHEN 	R.Estado = 1 AND FechaReserva <= GETDATE() THEN 'Finalizada'
					WHEN	R.Estado = 1 THEN 'Activa' 
					ELSE 'Cancelada' END) 'EstadoReserva'
	  FROM dbo.tReserva R
	  INNER JOIN dbo.tUsuario U ON U.IdUsuario = R.IdUsuario
	  WHERE R.IdReserva = @IdReserva

END
GO


CREATE PROCEDURE [dbo].[ConsultarReservas]
	@IdUsuario BIGINT
AS
BEGIN
	
	DECLARE @Rol INT
	SELECT  @Rol = IdRol
	FROM	tUsuario
	WHERE	IdUsuario = @IdUsuario

	SELECT	R.IdReserva,
			R.IdUsuario,
			U.Nombre,
			R.CantidadPersonas,
			R.FechaReserva,
			DATEADD(HOUR,2, R.FechaReserva) 'FechaFinReserva',
			R.Estado,
			Precio,
			(CASE	WHEN 	R.Estado = 1 AND FechaReserva <= GETDATE() THEN 'Finalizada'
					WHEN	R.Estado = 1 THEN 'Activa' 
					ELSE 'Cancelada' END) 'EstadoReserva'
	  FROM dbo.tReserva R
	  INNER JOIN dbo.tUsuario U ON U.IdUsuario = R.IdUsuario
	  WHERE R.IdUsuario = (CASE WHEN @Rol = 1 THEN @IdUsuario ELSE R.IdUsuario END)

END
GO


CREATE PROCEDURE [dbo].[EliminarReserva]
	@IdReserva BIGINT
AS
BEGIN

	DELETE FROM tReserva
	WHERE IdReserva = @IdReserva

END
GO


CREATE PROCEDURE [dbo].[RegistrarReserva]
	@IdUsuario			BIGINT,
	@CantidadPersonas	INT,
	@FechaReserva		DATETIME,
	@Precio				DECIMAL(18,2)
AS
BEGIN

	IF NOT EXISTS(	SELECT	1 FROM tReserva 
					WHERE	IdUsuario = @IdUsuario 
						AND CONVERT(DATE,FechaReserva) = CONVERT(DATE,@FechaReserva))
	BEGIN

		INSERT INTO dbo.tReserva (IdUsuario,CantidadPersonas,FechaReserva,Estado,Precio)
		VALUES (@IdUsuario,@CantidadPersonas,@FechaReserva,1,@Precio)

		UPDATE	tProducto
		SET		Cantidad = Cantidad - @CantidadPersonas

	END

END
GO

