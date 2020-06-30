# <img src="https://user-images.githubusercontent.com/59380623/82971714-f4417f80-9f98-11ea-917c-3a067a5b6f27.png" width="25"> **Soap**
Servicio web desarrollado con WCF Service .NET Framework 4.8, Cliente web desarrollado con MVC 5 .NET Framework bootstrap, jquery, jquery-validate, fontawesome, sweetalert.

## 1. Instalación
Ejecutar en SQL Server.
```hcl
USE Master
GO

IF DB_ID ('Soap') IS NOT NULL
	DROP DATABASE Soap
GO

CREATE DATABASE Soap
GO

USE Soap
GO

ALTER DATABASE Soap SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE
GO

IF SCHEMA_ID ('Ventas') IS NOT NULL
	DROP SCHEMA Ventas
GO

CREATE SCHEMA Ventas
GO

CREATE TABLE Ventas.Comprobantes
(
	IdentificadorComprobante INT IDENTITY(1,1) NOT NULL,
	TipoComprobante	VARCHAR(100) NOT NULL,
	VendedorComprobante VARCHAR(100) NOT NULL,
	ClienteComprobante VARCHAR(100) NOT NULL,
	FechaComprobante DATETIME NOT NULL,
	DescuentoComprobante MONEY NOT NULL,
	ImpuestoComprobante MONEY NOT NULL,
	SubTotalComprobante MONEY NOT NULL,
	TotalComprobante MONEY NOT NULL
)
GO

ALTER TABLE Ventas.Comprobantes
ADD CONSTRAINT LlavePrimariaComprobantes
PRIMARY KEY (IdentificadorComprobante)
GO

CREATE TABLE Ventas.ComprobantesProductos
(
  IdentificadorComprobanteProducto INT IDENTITY(1,1) NOT NULL,
	IdentificadorComprobante INT NOT NULL,
	NombreComprobanteProducto VARCHAR(100) NOT NULL,
	CantidadComprobanteProducto INT NOT NULL,
	PrecioComprobanteProducto MONEY NOT NULL,
	TotalComprobanteProducto MONEY NOT NULL
)
GO

ALTER TABLE Ventas.ComprobantesProductos
ADD CONSTRAINT LlavePrimariaComprobantesProductos
PRIMARY KEY (IdentificadorComprobanteProducto)
GO

IF OBJECT_ID ('Ventas.ActualizarComprobante', 'P') IS NOT NULL
   DROP PROCEDURE Ventas.ActualizarComprobante
GO

CREATE PROCEDURE Ventas.ActualizarComprobante
	@IdentificadorComprobante INT,
	@TipoComprobante VARCHAR(100),
	@VendedorComprobante VARCHAR(100),
	@ClienteComprobante VARCHAR(100),
	@FechaComprobante DATETIME,
	@DescuentoComprobante MONEY,
	@ImpuestoComprobante MONEY,
	@SubTotalComprobante MONEY,
	@TotalComprobante MONEY,
	@MensajeRespuesta VARCHAR(100) OUTPUT,
	@ErrorRespuesta BIT OUTPUT
AS
	BEGIN TRANSACTION
	BEGIN TRY
		UPDATE Ventas.Comprobantes
		SET
			TipoComprobante = @TipoComprobante,
			VendedorComprobante = @VendedorComprobante,
			ClienteComprobante = @ClienteComprobante,
			FechaComprobante = @FechaComprobante,
			DescuentoComprobante = @DescuentoComprobante,
			ImpuestoComprobante = @ImpuestoComprobante,
			SubTotalComprobante = @SubTotalComprobante,
			TotalComprobante = @TotalComprobante
		WHERE
			IdentificadorComprobante = @IdentificadorComprobante

		SET @MensajeRespuesta = 'Actualizado correctamente.'
		SET @ErrorRespuesta = 0
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		SET @MensajeRespuesta = ERROR_MESSAGE()
		SET @ErrorRespuesta = 1
		ROLLBACK TRANSACTION
	END CATCH
GO

IF OBJECT_ID ('Ventas.RegistrarComprobante', 'P') IS NOT NULL
   DROP PROCEDURE Ventas.RegistrarComprobante
GO

CREATE PROCEDURE Ventas.RegistrarComprobante
	@TipoComprobante VARCHAR(100),
	@VendedorComprobante VARCHAR(100),
	@ClienteComprobante VARCHAR(100),
	@FechaComprobante DATETIME,
	@DescuentoComprobante MONEY,
	@ImpuestoComprobante MONEY,
	@SubTotalComprobante MONEY,
	@TotalComprobante MONEY,
	@IdentificadorRespuesta INT OUTPUT,
	@MensajeRespuesta VARCHAR(100) OUTPUT,
	@ErrorRespuesta BIT OUTPUT
AS
	BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO Ventas.Comprobantes
		(
			TipoComprobante,
			VendedorComprobante,
			ClienteComprobante,
			FechaComprobante,
			DescuentoComprobante,
			ImpuestoComprobante,
			SubTotalComprobante,
			TotalComprobante
		)
		VALUES
		(
			@TipoComprobante,
			@VendedorComprobante,
			@ClienteComprobante,
			@FechaComprobante,
			@DescuentoComprobante,
			@ImpuestoComprobante,
			@SubTotalComprobante,
			@TotalComprobante
		)

		SET @IdentificadorRespuesta = SCOPE_IDENTITY()
		SET @MensajeRespuesta = 'Registrado correctamente'
		SET @ErrorRespuesta = 0
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		SET @IdentificadorRespuesta = 0
		SET @MensajeRespuesta = ERROR_MESSAGE()
		SET @ErrorRespuesta = 1
		ROLLBACK TRANSACTION
	END CATCH
GO

IF OBJECT_ID ('Ventas.EliminarComprobante', 'P') IS NOT NULL
   DROP PROCEDURE Ventas.EliminarComprobante
GO

CREATE PROCEDURE Ventas.EliminarComprobante
	@IdentificadorComprobante INT,
	@MensajeRespuesta VARCHAR(100) OUTPUT,
	@ErrorRespuesta BIT OUTPUT
AS
	BEGIN TRANSACTION
	BEGIN TRY
		DELETE FROM Ventas.Comprobantes
		WHERE
			IdentificadorComprobante = @IdentificadorComprobante

		DELETE FROM Ventas.ComprobantesProductos
		WHERE
			IdentificadorComprobante = @IdentificadorComprobante

		SET @MensajeRespuesta = 'Eliminado correctamente'
		SET @ErrorRespuesta = 0
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		SET @MensajeRespuesta = ERROR_MESSAGE()
		SET @ErrorRespuesta = 1
		ROLLBACK TRANSACTION
	END CATCH
GO

IF OBJECT_ID ('Ventas.ObtenerComprobante', 'P') IS NOT NULL
   DROP PROCEDURE Ventas.ObtenerComprobante
GO

CREATE PROCEDURE Ventas.ObtenerComprobante
	@IdentificadorComprobante INT,
	@MensajeRespuesta VARCHAR(100) OUTPUT,
	@ErrorRespuesta BIT OUTPUT
AS
	BEGIN TRY
		SELECT
			IdentificadorComprobante,
			TipoComprobante,
			VendedorComprobante,
			ClienteComprobante,
			FechaComprobante,
			DescuentoComprobante,
			ImpuestoComprobante,
			SubTotalComprobante,
			TotalComprobante
		FROM Ventas.Comprobantes
		WHERE
			IdentificadorComprobante = @IdentificadorComprobante

		SET @MensajeRespuesta = 'Obtenido correctamente.'
		SET @ErrorRespuesta = 0
	END TRY
	BEGIN CATCH
		SET @MensajeRespuesta = ERROR_MESSAGE()
		SET @ErrorRespuesta = 1
	END CATCH
GO

IF OBJECT_ID ('Ventas.ListarComprobante', 'P') IS NOT NULL
   DROP PROCEDURE Ventas.ListarComprobante
GO

CREATE PROCEDURE Ventas.ListarComprobante
	@MensajeRespuesta VARCHAR(100) OUTPUT,
	@ErrorRespuesta BIT OUTPUT
AS
	BEGIN TRY
		SELECT
			IdentificadorComprobante,
			TipoComprobante,
			VendedorComprobante,
			ClienteComprobante,
			FechaComprobante,
			DescuentoComprobante,
			ImpuestoComprobante,
			SubTotalComprobante,
			TotalComprobante
		FROM Ventas.Comprobantes

		SET @MensajeRespuesta = 'Listado correctamente.'
		SET @ErrorRespuesta = 0
	END TRY
	BEGIN CATCH
		SET @MensajeRespuesta = ERROR_MESSAGE()
		SET @ErrorRespuesta = 1
	END CATCH
GO

IF OBJECT_ID ('Ventas.ActualizarComprobanteProducto', 'P') IS NOT NULL
    DROP PROCEDURE Ventas.ActualizarComprobanteProducto
GO

CREATE PROCEDURE Ventas.ActualizarComprobanteProducto
    @IdentificadorComprobanteProducto INT,
    @IdentificadorComprobante INT,
    @NombreComprobanteProducto VARCHAR(100),
    @CantidadComprobanteProducto INT,
    @PrecioComprobanteProducto MONEY,
    @TotalComprobanteProducto MONEY,
    @MensajeRespuesta VARCHAR(100) OUTPUT,
    @ErrorRespuesta BIT OUTPUT
AS
    BEGIN TRANSACTION
    BEGIN TRY
        UPDATE Ventas.ComprobantesProductos
        SET
            IdentificadorComprobante = @IdentificadorComprobante,
            NombreComprobanteProducto = @NombreComprobanteProducto,
            CantidadComprobanteProducto = @CantidadComprobanteProducto,
            PrecioComprobanteProducto = @PrecioComprobanteProducto,
            TotalComprobanteProducto = @TotalComprobanteProducto
        WHERE
            IdentificadorComprobanteProducto = @IdentificadorComprobanteProducto
        
        SET @MensajeRespuesta = 'Actualizado correctamente.'
        SET @ErrorRespuesta = 0
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        SET @MensajeRespuesta = ERROR_MESSAGE()
        SET @ErrorRespuesta = 1
        ROLLBACK TRANSACTION
    END CATCH
GO

IF OBJECT_ID ('Ventas.RegistrarComprobanteProducto', 'P') IS NOT NULL
    DROP PROCEDURE Ventas.RegistrarComprobanteProducto
GO

CREATE PROCEDURE Ventas.RegistrarComprobanteProducto
    @IdentificadorComprobante INT,
    @NombreComprobanteProducto VARCHAR(100),
    @CantidadComprobanteProducto INT,
    @PrecioComprobanteProducto MONEY,
    @TotalComprobanteProducto MONEY,
    @IdentificadorRespuesta INT OUTPUT,
    @MensajeRespuesta VARCHAR(100) OUTPUT,
    @ErrorRespuesta BIT OUTPUT
AS
    BEGIN TRANSACTION
    BEGIN TRY
        INSERT INTO Ventas.ComprobantesProductos
        (
            IdentificadorComprobante,
            NombreComprobanteProducto,
            CantidadComprobanteProducto,
            PrecioComprobanteProducto,
            TotalComprobanteProducto
        )
        VALUES
        (
            @IdentificadorComprobante,
            @NombreComprobanteProducto,
            @CantidadComprobanteProducto,
            @PrecioComprobanteProducto,
            @TotalComprobanteProducto
        )

        SET @IdentificadorRespuesta = SCOPE_IDENTITY()
        SET @MensajeRespuesta = 'Registrado correctamente.';
        SET @ErrorRespuesta = 0
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        SET @IdentificadorRespuesta = 0
        SET @MensajeRespuesta = ERROR_MESSAGE()
        SET @ErrorRespuesta = 1
        ROLLBACK TRANSACTION
    END CATCH
GO

IF OBJECT_ID ('Ventas.EliminarComprobanteProducto', 'P') IS NOT NULL
    DROP PROCEDURE Ventas.EliminarComprobanteProducto
GO

CREATE PROCEDURE Ventas.EliminarComprobanteProducto
    @IdentificadorComprobanteProducto INT,
    @MensajeRespuesta VARCHAR(100) OUTPUT,
    @ErrorRespuesta BIT OUTPUT
AS
    BEGIN TRANSACTION
    BEGIN TRY
        DELETE FROM Ventas.ComprobantesProductos
        WHERE
            IdentificadorComprobanteProducto = @IdentificadorComprobanteProducto
        
        SET @MensajeRespuesta = 'Eliminado correctamente.'
        SET @ErrorRespuesta = 0
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        SET @MensajeRespuesta = ERROR_MESSAGE()
        SET @ErrorRespuesta = 1
        ROLLBACK TRANSACTION
    END CATCH
GO

IF OBJECT_ID ('Ventas.ObtenerComprobanteProducto', 'P') IS NOT NULL
    DROP PROCEDURE Ventas.ObtenerComprobanteProducto
GO

CREATE PROCEDURE Ventas.ObtenerComprobanteProducto
    @IdentificadorComprobanteProducto INT,
    @MensajeRespuesta VARCHAR(100) OUTPUT,
	@ErrorRespuesta BIT OUTPUT
AS
    BEGIN TRY
        SELECT
            IdentificadorComprobanteProducto,
            IdentificadorComprobante,
            NombreComprobanteProducto,
            CantidadComprobanteProducto,
            PrecioComprobanteProducto,
            TotalComprobanteProducto
        FROM Ventas.ComprobantesProductos
        WHERE
            IdentificadorComprobanteProducto = @IdentificadorComprobanteProducto

        SET @MensajeRespuesta = 'Obtenido correctamente.'
		SET @ErrorRespuesta = 0
    END TRY
    BEGIN CATCH
        SET @MensajeRespuesta = ERROR_MESSAGE()
		SET @ErrorRespuesta = 1
    END CATCH
GO

IF OBJECT_ID ('Ventas.ListarComprobanteProducto', 'P') IS NOT NULL
    DROP PROCEDURE Ventas.ListarComprobanteProducto
GO

CREATE PROCEDURE Ventas.ListarComprobanteProducto
    @MensajeRespuesta VARCHAR(100) OUTPUT,
	@ErrorRespuesta BIT OUTPUT
AS
    BEGIN TRY
        SELECT
            IdentificadorComprobanteProducto,
            IdentificadorComprobante,
            NombreComprobanteProducto,
            CantidadComprobanteProducto,
            PrecioComprobanteProducto,
            TotalComprobanteProducto
        FROM Ventas.ComprobantesProductos

        SET @MensajeRespuesta = 'Listado correctamente.'
		SET @ErrorRespuesta = 0
    END TRY
    BEGIN CATCH
        SET @MensajeRespuesta = ERROR_MESSAGE()
		SET @ErrorRespuesta = 1
    END CATCH
GO

IF OBJECT_ID ('Ventas.ListarComprobanteProductoPorIdentificadorComprobante', 'P') IS NOT NULL
    DROP PROCEDURE Ventas.ListarComprobanteProductoPorIdentificadorComprobante
GO

CREATE PROCEDURE Ventas.ListarComprobanteProductoPorIdentificadorComprobante
    @IdentificadorComprobante INT,
    @MensajeRespuesta VARCHAR(100) OUTPUT,
	@ErrorRespuesta BIT OUTPUT
AS
    BEGIN TRY
        SELECT
            IdentificadorComprobanteProducto,
            IdentificadorComprobante,
            NombreComprobanteProducto,
            CantidadComprobanteProducto,
            PrecioComprobanteProducto,
            TotalComprobanteProducto
        FROM Ventas.ComprobantesProductos
        WHERE
            IdentificadorComprobante = @IdentificadorComprobante

        SET @MensajeRespuesta = 'Listado correctamente.'
		SET @ErrorRespuesta = 0
    END TRY
    BEGIN CATCH
        SET @MensajeRespuesta = ERROR_MESSAGE()
		SET @ErrorRespuesta = 1
    END CATCH
GO
```

Iniciar los siguientes proyectos.

![image](https://user-images.githubusercontent.com/59380623/86083245-4b86b400-ba5f-11ea-821f-3d281c122ace.png)

## 2. Ejemplo
Cliente web consumiendo el servicio WCF.

![image](https://user-images.githubusercontent.com/59380623/86081107-28a5d100-ba5a-11ea-8366-09dfc210c7a8.png)

## 3. Licencia
**[MIT LICENSE](https://github.com/victorpaucas/Soap/blob/master/LICENSE)**

Copyright © 2020 Victor Hugo Paucas Navarro
