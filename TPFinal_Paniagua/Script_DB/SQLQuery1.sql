USE MASTER
GO
CREATE DATABASE INDUMENTARIA_DB
GO
USE INDUMENTARIA_DB
GO

CREATE TABLE Acceso (
  Id_Acceso INT IDENTITY(1,1) PRIMARY KEY,
  Nombre VARCHAR(100) NOT NULL
);
GO

CREATE TABLE Usuarios (
  Id_Usuario INT IDENTITY(1,1) PRIMARY KEY,
  Nombre VARCHAR(100) NOT NULL,
  Apellido VARCHAR(100) NOT NULL,
  Email VARCHAR(50) NOT NULL UNIQUE,
  Contra VARCHAR(100) NOT NULL,
  Direccion VARCHAR(200),
  Localidad VARCHAR(200),
  Telefono VARCHAR(25),
  FechaNacimiento DATE,
  AccesoId INT,
  Estado BIT DEFAULT 1,
  FOREIGN KEY (AccesoId) REFERENCES Acceso(Id_Acceso)
);
GO
--SELECT* FROM Usuarios
--SELECT Id_Usuario, Nombre, Apellido, Email, Contra, Direccion, Telefono, Localidad, FechaNacimiento, AccesoId, Estado FROM Usuarios
--INSERT INTO Usuarios (AccesoId, Id_Usuario, Nombre, Apellido, Email, Contra, Direccion, Telefono, Localidad, FechaNacimiento, Estado) VALUES (@IdAcceso, @Nombre, @Apellido, @Email, @Pass, @Direccion, @Telefono, @Localidad, @FechaNacimiento, 1)
--UPDATE Usuarios SET AccesoId = @IdAcceso, Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Contra = @Pass, Direccion = @Direccion, Telefono = @Telefono, Localidad = @Localidad, FechaNacimiento = @FechaNacimiento, Estado = @Estado WHERE Id_Usuario = @Id_Usuario
--UPDATE Usuarios SET Estado = 0 WHERE Id_Usuario = @Id_Usuario
--SELECT Id_Usuario, Nombre, Apellido, Email, Contra, Direccion, Telefono, Localidad, FechaNacimiento, Estado, AccesoId FROM Usuarios WHERE Id_Usuario = @Id_Usuario
--SELECT COUNT(*) FROM Usuarios WHERE Email = @Email AND Estado = 1
--SELECT Contra FROM Usuarios WHERE Email = @Email AND Estado = 1
--SELECT Id_Usuario, Nombre, Apellido, Email, Contra, Direccion, Telefono, Localidad, FechaNacimiento, AccesoId, Estado FROM Usuarios WHERE Id_Usuario = @Id_Usuario

CREATE TABLE Categorias (
    Id_Categoria INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Estado BIT DEFAULT 1
);
GO

CREATE TABLE Tipos (
    Id_Tipo INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    CategoriaId INT,
    Estado BIT DEFAULT 1,
    FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id_Categoria)
);
GO

CREATE TABLE Articulos (
    Id_Articulo INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR (255),
    Precio DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL,
    CategoriaId INT NOT NULL,
    TipoId INT NOT NULL,
    Estado BIT DEFAULT 1,
    FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id_Categoria),
    FOREIGN KEY (TipoId) REFERENCES Tipos(Id_Tipo)
);
GO
--SELECT Stock FROM Articulos WHERE Id_Articulo = @Id_Articulo
--SELECT * FROM Articulos

CREATE TABLE Imagenes (
    Id_Imagen INT IDENTITY(1,1) PRIMARY KEY,
    ImagenURL VARCHAR(MAX),
    ArticuloId INT,
    FOREIGN KEY (ArticuloId) REFERENCES Articulos(Id_Articulo)
);
GO
--ALTER TABLE Imagenes
--ALTER COLUMN ImagenURL VARCHAR(MAX);

CREATE TABLE EstadoCompra (
	Id_EstadoCompra INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR (100) NOT NULL,
    Estado BIT DEFAULT 1
);

CREATE TABLE CarritoCompras (
    Id_CarritoCompra INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NULL,
    ImporteTotal DECIMAL (10, 2) NOT NULL DEFAULT 0.00,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id_Usuario)
);
GO
--INSERT INTO CarritoCompras (UsuarioId, ImporteTotal, FechaCreacion) VALUES(@UsuarioId, 0, GETDATE())

CREATE TABLE DetalleCompra (
    Id_DetalleCompra INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT ,
    CarritoCompraId INT NOT NULL,
    ImporteTotal DECIMAL(10, 2),
    DireccionEntregar VARCHAR(255),
    EstadoCompraId INT NOT NULL DEFAULT 1,
    Fecha_Compra DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id_Usuario),
    FOREIGN KEY (CarritoCompraId) REFERENCES CarritoCompras(Id_CarritoCompra)
);
GO

CREATE TABLE Carrito_x_Articulos (
    Id_CarritoArticulo INT IDENTITY(1,1) PRIMARY KEY,
    CarritoCompraId INT NOT NULL,
    ArticuloId INT NOT NULL,
    Cantidad INT NOT NULL,
    FOREIGN KEY (CarritoCompraId) REFERENCES CarritoCompras(Id_CarritoCompra),
    FOREIGN KEY (ArticuloId) REFERENCES Articulos(Id_Articulo) 
);
GO

CREATE TABLE Detalle_x_Articulos (
    Id_DetalleArticulo INT IDENTITY(1,1) PRIMARY KEY,
    DetalleCompraId INT NOT NULL,
    ArticuloId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnidad DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (DetalleCompraId) REFERENCES DetalleCompra(Id_DetalleCompra) ,
    FOREIGN KEY (ArticuloId) REFERENCES Articulos(Id_Articulo) 
);
GO

-------------------------------- INGRESO DE DATOS -----------------------------------
INSERT INTO Acceso (Nombre) VALUES ('Administrador'), ('Empleado'), ('Cliente');
GO

INSERT INTO Usuarios (Nombre, Apellido, Email, Contra, Direccion, Telefono, Localidad, FechaNacimiento, AccesoId)
VALUES 
('Camila', 'Fernandez', 'camifernandez@gmail.com', 'pass123', 'Calle Luna 123', '123456789', 'Lopez Camelo', '1998-04-25', 1),
('Javier', 'Martínez', 'javimartinez@gmail.com', 'pass456', 'Av. Estrella 456', '987654321', 'Rincong de Milberg', '1990-03-18', 2),
('Sofia', 'Ramirez', 'sofiramirez@gmail.com', 'pass789', 'Boulevard Sol 789', '112233445', 'La Paloma', '1985-07-09', 3),
('Lucia', 'Saavedra', 'luciagomez@gmail.com', 'password1', 'Calle del Río 456', '1234567890', 'Tigre', '1995-05-12', 3),
('Pedro', 'Valdebenito', 'pedrolopez@gmail.com', 'password2', 'Av. Siempre Viva 742', '0987654321', 'San Isidro', '1988-11-23', 3);
GO

INSERT INTO Categorias ( Nombre) VALUES ('Ropa de Mujer');
INSERT INTO Categorias ( Nombre) VALUES ('Accesorios');
INSERT INTO Categorias ( Nombre) VALUES ('Calzado');
GO

INSERT INTO Tipos (Nombre, CategoriaId) VALUES 
('Vestidos largos', 1),
('Corset', 1),
('Perfumes florales', 2),
('Bolsos', 2),
('Bucaneras de cuero', 3),
('Bucaneras de gamuza', 3);
GO

INSERT INTO Articulos (Nombre, Descripcion, Precio, Stock, CategoriaId, TipoId) 
VALUES 
('Vestido Rojo Elegante', 'Vestido largo rojo para ocasiones especiales', 120.99, 50, 1, 1), -- Vestidos largos
('Vestido Negro Casual', 'Vestido cómodo y casual para uso diario', 70.50, 80, 1, 1),         -- Vestidos largos
('Corset Negro Clásico', 'Corset negro con detalles elegantes', 85.99, 30, 1, 2),           -- Corset
('Perfume Floral Primavera', 'Perfume fresco con notas florales', 60.00, 150, 2, 3),       -- Perfumes florales
('Perfume Dulce Nocturno', 'Perfume dulce ideal para la noche', 75.99, 90, 2, 3),          -- Perfumes florales
('Bolso de Cuero Marrón', 'Bolso de cuero marrón con gran capacidad', 95.00, 50, 2, 4),    -- Bolsos
('Bucaneras de Cuero Negras', 'Bucaneras elegantes de cuero negro', 150.00, 60, 3, 5),     -- Bucaneras de cuero
('Bucaneras de Gamuza Marrón', 'Bucaneras modernas de gamuza marrón claro', 140.50, 40, 3, 6), -- Bucaneras de gamuza
('Bucaneras de Charol Rojas', 'Bucaneras brillantes de charol rojo intenso', 160.00, 30, 3, 5); --Bucaneras de cuero
GO

INSERT INTO Imagenes (ImagenURL, ArticuloId) 
VALUES 
('https://images.creativefabrica.com/products/previews/2023/10/27/IixqgQnFR/2XLEgvfH3faSaqHGWkMwcGVgC6V-mobile.jpg', 1),     -- Vestido Rojo Elegante
('https://cirilamoda.com/wp-content/uploads/2024/09/vestido-largo-al-cuerpo-en-talle-grande.jpg', 2),     -- Vestido Azul Casual
('https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRHxzRf4pvGxTnrWUL2zH8DW9rL7U77R-uzSw&s', 3),     -- Corset Negro Clásico
('https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSR3N-BLVHeCHw4I-tmupQV90Yv6Hr5ZnMjLQ&s', 4),   -- Perfume Floral Primavera
('https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcREc_B8-b51rq5AfS2CJN6igsTaSpbkkCQGwg&s', 5),    -- Perfume Dulce Nocturno
('https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRnPp5JH71G80YaAjnoN9yMfO2El4jnHuz9Q&s', 6),      -- Bolso de Cuero Marrón
('https://f.fcdn.app/imgs/7a162d/lemon.com.uy/lemouy/5367/original/catalogo/427824_1_1/600x900/bucaneras-cuero-negro.jpg', 7),  -- Bucaneras de Cuero Negras
('https://http2.mlstatic.com/D_NQ_NP_721829-MLA41366892171_042020-O.webp', 8), -- Bucaneras de Gamuza Marrón
('https://http2.mlstatic.com/D_NQ_NP_851818-MLA69611608195_052023-O.webp', 9); --Bucaneras de Charol Rojas
GO

INSERT INTO EstadoCompra (Nombre) VALUES 
('Enviado'),
('En Proceso'),
('Finalizado'),
('Cancelado');
GO

INSERT INTO CarritoCompras ( UsuarioId, ImporteTotal) 
VALUES 
(3, 95.50),  -- Sofia Ramirez
(4, 220.75), -- Lucia Saavedra
(5, 310.50); --Pedro Valdebenito
GO


INSERT INTO DetalleCompra (UsuarioId, CarritoCompraId, ImporteTotal, DireccionEntregar, EstadoCompraId, Fecha_Compra) 
VALUES 
(3, 1, 95.50, 'Uriburu 1931', 1, '2024-12-30'), -- Sofia ramirez
(4, 2, 220.75, 'Humboldt 1225', 2, '2024-12-28'), -- Lucia Saavedra
(5, 3, 310.50, 'Caseros 3008', 2, '2025-01-03'); --Pedro Valdebenito
GO

INSERT INTO Carrito_x_Articulos (CarritoCompraId, ArticuloId, Cantidad) 
VALUES 
(1, 1, 1), -- Vestido Rojo Elegante (Sofia)
(1, 3, 1), -- Corset Negro Clásico (Sofia)
(2, 7, 1), -- Perfume Floral Primavera (Lucia)
(2, 8, 2), -- Vestido Negro Clásico (Lucia)
(3, 5, 2), -- Bucaneras de Cuero Negras (Pedro)
(3, 9, 1); --Bucaneras de Charol(Pedro)
GO

INSERT INTO Detalle_x_Articulos (DetalleCompraId, ArticuloId, Cantidad, PrecioUnidad)
VALUES 
(1, 1, 1, 120.99), -- Sofia
(1, 3, 1, 85.99), -- Sofia
(2, 7, 1, 60.00), -- Lucia
(2, 8, 2, 130.00), -- Lucia
(3, 5, 2, 150.00), -- Pedro
(3, 9, 1, 160.00); --Pedro
GO

------------------ - PROCEDIMIENTOS ALMACENADOS------------------

--AREA ARTICULOS

CREATE PROCEDURE sp_ListarArticulosActivos            
AS
BEGIN
    SELECT 
        A.Id_Articulo,
        A.Nombre AS NombreArticulo,
        A.Descripcion,
        A.Precio,
        A.Stock,
		A.CategoriaId,
        C.Nombre AS NombreCategoria,
		A.TipoId,
        T.Nombre AS NombreTipo,
		I.ImagenURL
    FROM 
        Articulos A
	INNER JOIN 
		Imagenes I ON A.Id_Articulo = I.ArticuloId
    INNER JOIN 
        Categorias C ON A.CategoriaId = C.Id_Categoria
    INNER JOIN 
        Tipos T ON A.TipoId = T.Id_Tipo
	WHERE 
        A.Estado = 1
    ORDER BY 
        A.Nombre
END;
GO
--EXEC sp_ListarArticulosActivos

CREATE PROCEDURE sp_ListarArticulosTodos            
AS
BEGIN
    SELECT 
        A.Id_Articulo,
        A.Nombre AS NombreArticulo,
        A.Descripcion,
        A.Precio,
        A.Stock,
		A.CategoriaId,
        C.Nombre AS NombreCategoria,
		A.TipoId,
        T.Nombre AS NombreTipo,
		I.ImagenURL,
        A.Estado
    FROM 
        Articulos A
	INNER JOIN 
		Imagenes I ON A.Id_Articulo = I.ArticuloId
    INNER JOIN 
        Categorias C ON A.CategoriaId = C.Id_Categoria
    INNER JOIN 
        Tipos T ON A.TipoId = T.Id_Tipo
    ORDER BY 
        A.Nombre
END;
GO

--sp_ListarArticuloxCategoriaytipo
CREATE PROCEDURE sp_ListarArticulosPorCategoria
    @CategoriaId INT
AS
BEGIN
    SELECT 
        A.Id_Articulo,
        A.Nombre AS NombreArticulo,
        A.Descripcion,
        A.Precio,
        I.ImagenURL
    FROM 
        Articulos A
	INNER JOIN 
        Tipos T ON A.TipoId = T.Id_Tipo
    INNER JOIN 
        Categorias C ON A.CategoriaId = C.Id_Categoria
    LEFT JOIN 
        Imagenes I ON A.Id_Articulo = I.ArticuloId
    WHERE 
        A.Estado = 1
        AND A.CategoriaId = @CategoriaId 
    ORDER BY 
        A.Nombre;
END;
GO

CREATE PROCEDURE sp_ListarArticulosPorTipo
    @TipoId INT
AS
BEGIN
    SELECT 
        A.Id_Articulo,
        A.Nombre AS NombreArticulo,
        A.Descripcion,
        A.Precio,
        I.ImagenURL
    FROM 
        Articulos A
    INNER JOIN 
        Categorias C ON A.CategoriaId = C.Id_Categoria
    INNER JOIN 
        Tipos T ON A.TipoId = T.Id_Tipo
    LEFT JOIN 
        Imagenes I ON A.Id_Articulo = I.ArticuloId
    WHERE 
        A.Estado = 1
        AND A.TipoId = @TipoId  
    ORDER BY 
        A.Nombre;
END;
GO

CREATE PROCEDURE sp_AgregarArticulo
	(
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(255),
    @Precio DECIMAL(10, 2),
    @Stock INT,
    @CategoriaId INT,
    @TipoId INT,
    @ImagenesURL VARCHAR(MAX)  --puede ser una cadena separada por comas
	)
AS
BEGIN
    BEGIN TRANSACTION
    BEGIN TRY
        --DECLARE @Id_Articulo INT 

        INSERT INTO Articulos (Nombre, Descripcion, Precio, Stock, CategoriaId, TipoId)
        VALUES (@Nombre, @Descripcion, @Precio, @Stock, @CategoriaId, @TipoId);

		DECLARE @Id_Articulo INT = SCOPE_IDENTITY();
        -- Si se han proporcionado imágenes
        IF @ImagenesURL IS NOT NULL
        BEGIN
            -- Separar las URLs de las imágenes y guardarlas en la tabla Imagenes
            DECLARE @ImagenURL VARCHAR(255);
            DECLARE @Pos INT = 1;

            WHILE @Pos > 0
            BEGIN
                -- Obtener la próxima URL de imagen
                SET @Pos = CHARINDEX(',', @ImagenesURL);
                IF @Pos > 0
                    SET @ImagenURL = SUBSTRING(@ImagenesURL, 1, @Pos - 1);
                ELSE
                    SET @ImagenURL = @ImagenesURL;

                -- Insertar la imagen
                INSERT INTO Imagenes (ImagenURL, ArticuloId)
                VALUES (@ImagenURL, @Id_Articulo);

                -- Eliminar la URL procesada de la cadena
                SET @ImagenesURL = LTRIM(SUBSTRING(@ImagenesURL, @Pos + 1, LEN(@ImagenesURL)));
            END
        END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

--SELECT * FROM Articulos
--SELECT * FROM Imagenes
--DELETE FROM ImagenesWHERE Id_Imagen = 10
--Delete Articulos where Id_Articulo= 11

CREATE PROCEDURE sp_ModificarArticulo
    @Id_Articulo INT, 
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(255),
    @Precio DECIMAL(10, 2),
    @Stock INT,
    @CategoriaId INT,
    @TipoId INT,
    @Estado BIT,
    @ImagenURL VARCHAR(255) -- URL de la imagen (solo una)
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Actualizar el artículo
        UPDATE Articulos
        SET 
            Nombre = @Nombre,
            Descripcion = @Descripcion,
            Precio = @Precio,
            Stock = @Stock,
            CategoriaId = @CategoriaId,
            TipoId = @TipoId,
            Estado = @Estado
        WHERE Id_Articulo = @Id_Articulo;

        -- Si hay una nueva imagen
        IF @ImagenURL IS NOT NULL
        BEGIN
            -- Primero eliminamos todas las imágenes asociadas al artículo
            DELETE FROM Imagenes WHERE ArticuloId = @Id_Articulo;

            -- Insertamos la nueva imagen
            INSERT INTO Imagenes (ImagenURL, ArticuloId)
            VALUES (@ImagenURL, @Id_Articulo);
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

--EXEC sp_help 'Imagenes';

--https://acdn.mitiendanube.com/stores/222/175/products/photoroom-20230417_0739201-b147ae2ae910dd781d16817558702779-1024-1024.png
--SELECT * FROM Imagenes
--EXEC sp_ModificarArticulo 5, 'perfume horrible', 'Perfume dulce de noche', 7889, 60, 2,3,1,'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcREc_B8-b51rq5AfS2CJN6igsTaSpbkkCQGwg&s'

CREATE PROCEDURE sp_DesactivarArticulo
    @Id_Articulo INT 
AS
BEGIN
    BEGIN TRANSACTION
    BEGIN TRY
        
        UPDATE Articulos
        SET Estado = 0
        WHERE Id_Articulo = @Id_Articulo;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_ReactivarArticulo
    @Id_Articulo INT 
AS
BEGIN
    BEGIN TRANSACTION
    BEGIN TRY
        UPDATE Articulos
        SET Estado = 1
        WHERE Id_Articulo = @Id_Articulo;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW;
    END CATCH
END;
GO
--------------------CATEGORIAS----------------------------
--uso para admins
CREATE PROCEDURE sp_ListarCategoriasActivos
AS
BEGIN
    SELECT 
        C.Id_Categoria,
        C.Nombre AS NombreCategoria,
		C.Estado
    FROM 
        Categorias C
	WHERE
		C.Estado = 1
    ORDER BY 
        C.Nombre
END;
GO
	
--para mostrar en la pagina
CREATE PROCEDURE sp_ListarCategoriasTodas
AS
BEGIN
    SELECT 
        C.Id_Categoria,
        C.Nombre AS NombreCategoria,
		C.Estado
    FROM 
        Categorias C
    ORDER BY 
        C.Nombre
END;
GO


CREATE PROCEDURE sp_AgregarCategoria(
    @Nombre VARCHAR(100)  
	)
AS
BEGIN
    BEGIN TRANSACTION
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM Categorias WHERE Nombre = @Nombre)
        BEGIN
            PRINT 'La categoría ya existe.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        INSERT INTO Categorias (Nombre)
        VALUES (@Nombre);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_ModificarCategoria(
    @IdCategoria INT,       
    @Nombre VARCHAR(100)
	)
AS
BEGIN
    BEGIN TRANSACTION
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Categorias WHERE Id_Categoria = @IdCategoria)
        BEGIN
            PRINT 'La categoría no existe.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        UPDATE Categorias
        SET Nombre = @Nombre
        WHERE Id_Categoria = @IdCategoria;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_DesactivarCategoria(
    @IdCategoria INT
	)
AS
BEGIN
    BEGIN TRANSACTION
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Categorias WHERE Id_Categoria = @IdCategoria)
        BEGIN
            PRINT 'La categoría no existe.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        UPDATE Categorias
        SET Estado = 0
        WHERE Id_Categoria = @IdCategoria;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_ReactivarCategoria(
    @IdCategoria INT   
	)
AS
BEGIN
    BEGIN TRANSACTION
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Categorias WHERE Id_Categoria = @IdCategoria)
        BEGIN
            PRINT 'La categoría no existe.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        UPDATE Categorias
        SET Estado = 1
        WHERE Id_Categoria = @IdCategoria;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
--------------------------------

CREATE PROCEDURE sp_ActualizarStock(
    @idArticulo INT,          
    @cantidadVendida INT      
	)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
		UPDATE Articulos
		SET Stock = Stock - @cantidadVendida
		WHERE Id_Articulo = @idArticulo AND Stock >= @cantidadVendida;
		COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE PROCEDURE ObtenerTiposPorCategoria
    @CategoriaId INT
AS
BEGIN
    SELECT Id_Tipo, Nombre
    FROM Tipos
    WHERE CategoriaId = @CategoriaId AND Estado = 1
    ORDER BY Nombre;
END;
GO

------------------------------TIPOS---------------------------------
--para el admin
CREATE PROCEDURE sp_ListarTiposTodas
AS
BEGIN
    SELECT 
        T.Id_Tipo,
        T.Nombre AS NombreTipo,
		T.CategoriaId,
		T.Estado
    FROM 
        Tipos T
    ORDER BY 
        T.Nombre
END;
GO

--para mostrar en la pagina
CREATE PROCEDURE sp_ListarTiposActivos
AS
BEGIN
    SELECT 
        T.Id_Tipo,
        T.Nombre AS NombreTipo,
		T.Estado
    FROM 
        Tipos T
	WHERE
		T.Estado = 1
    ORDER BY 
        T.Nombre
END;
GO

CREATE PROCEDURE sp_AgregarTipo(
    @Nombre VARCHAR(100),   
    @CategoriaId INT        
	)
AS
BEGIN
    BEGIN TRANSACTION
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Categorias WHERE Id_Categoria = @CategoriaId)
        BEGIN
            PRINT 'La categoría no existe.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Tipos WHERE Nombre = @Nombre AND CategoriaId = @CategoriaId)
        BEGIN
            PRINT 'El tipo ya existe en esta categoría.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        INSERT INTO Tipos (Nombre, CategoriaId)
        VALUES (@Nombre, @CategoriaId);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_ModificarTipo(
    @IdTipo INT,  
    @Nombre VARCHAR(100),
    @CategoriaId INT    
	)
AS
BEGIN
    BEGIN TRANSACTION
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tipos WHERE Id_Tipo = @IdTipo)
        BEGIN
            PRINT 'El tipo con Id_Tipo = ' + CAST(@IdTipo AS VARCHAR) + ' no existe.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Categorias WHERE Id_Categoria = @CategoriaId)
        BEGIN
            PRINT 'La categoría con Id_Categoria = ' + CAST(@CategoriaId AS VARCHAR) + ' no existe.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Tipos WHERE Nombre = @Nombre AND CategoriaId = @CategoriaId AND Id_Tipo != @IdTipo)
        BEGIN
            PRINT 'El tipo con el nombre "' + @Nombre + '" ya existe en la categoría con Id_Categoria = ' + CAST(@CategoriaId AS VARCHAR);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        UPDATE Tipos
        SET Nombre = @Nombre,
            CategoriaId = @CategoriaId
        WHERE Id_Tipo = @IdTipo;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_DesactivarTipo(
    @IdTipo INT  
	)
AS
BEGIN
    BEGIN TRANSACTION
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tipos WHERE Id_Tipo = @IdTipo)
        BEGIN
            PRINT 'El tipo no existe.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        UPDATE Tipos
        SET Estado = 0
        WHERE Id_Tipo = @IdTipo;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_ReactivarTipo(
    @IdTipo INT  
	)
AS
BEGIN
    BEGIN TRANSACTION
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tipos WHERE Id_Tipo = @IdTipo)
        BEGIN
            PRINT 'El tipo no existe.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Tipos WHERE Id_Tipo = @IdTipo AND Estado = 1)
        BEGIN
            PRINT 'El tipo ya está activo.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        UPDATE Tipos
        SET Estado = 1
        WHERE Id_Tipo = @IdTipo;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE spObtenerDetalleCompra(
    @IdUsuario INT
	)
AS
BEGIN
    SELECT 
        dc.Id_DetalleCompra,
		dc.UsuarioId,
		dc.CarritoCompraId,
        dc.Fecha_Compra,
        dc.ImporteTotal AS ImporteDetalleCompra,
        dc.DireccionEntregar,
		ec.Id_EstadoCompra,
        ec.Nombre AS EstadoCompra,
		u.Nombre AS NombreUsuario,
		u.Apellido AS ApellidoUsuario,
		u.Email AS EmailUsuario
    FROM 
        DetalleCompra dc
    JOIN 
        EstadoCompra ec ON dc.EstadoCompraId = ec.Id_EstadoCompra
    JOIN 
        Usuarios u ON dc.UsuarioId = u.Id_Usuario
    WHERE 
        dc.UsuarioId = @IdUsuario
    ORDER BY dc.Fecha_Compra DESC;
END;
GO

CREATE VIEW vw_ListarTodasLasCompras 
AS
SELECT 
    dc.Id_DetalleCompra,
    dc.UsuarioId,
    dc.CarritoCompraId,
    dc.Fecha_Compra,
    dc.ImporteTotal AS ImporteDetalleCompra,
    dc.DireccionEntregar,
    ec.Id_EstadoCompra,
    ec.Nombre AS EstadoCompra,
    u.Nombre AS NombreUsuario,
    u.Apellido AS ApellidoUsuario,
    u.Email AS EmailUsuario
FROM 
    DetalleCompra dc
JOIN 
    EstadoCompra ec ON dc.EstadoCompraId = ec.Id_EstadoCompra
JOIN 
    Usuarios u ON dc.UsuarioId = u.Id_Usuario;
GO
--detaSELECT * FROM vw_ListarTodasLasCompras ORDER BY Fecha_Compra DESC;
--SELECT * FROM CarritoCompras
--SELECT * FROM Carrito_x_Articulos

CREATE PROCEDURE sp_CambiarEstadoCompraCiclo(
    @Id_DetalleCompra INT
	)
AS
BEGIN
    BEGIN TRANSACTION
    BEGIN TRY
		DECLARE @EstadoActual INT;
		DECLARE @NuevoEstado INT;

		SELECT @EstadoActual = EstadoCompraId
		FROM DetalleCompra
		WHERE Id_DetalleCompra = @Id_DetalleCompra;

		IF @EstadoActual = 4
		BEGIN
			SET @NuevoEstado = 1;  
		END
		ELSE
		BEGIN
			SET @NuevoEstado = @EstadoActual + 1;  
		END

		UPDATE DetalleCompra
		SET EstadoCompraId = @NuevoEstado
		WHERE Id_DetalleCompra = @Id_DetalleCompra;
		COMMIT TRANSACTION; 
	END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
--SELECT * FROM EstadoCompra
--SELECT * FROM DetalleCompra
--EXEC sp_CambiarEstadoCompraCiclo @Id_DetalleCompra = 3;
--SELECT Id_DetalleCompra, EstadoCompraId FROM DetalleCompra WHERE Id_DetalleCompra = 1;


CREATE PROCEDURE sp_InsertarPedido(
    @UsuarioId INT,          
    @CarritoCompraId INT,     
    @ImporteTotal DECIMAL(18, 2), 
    @DireccionEntregar VARCHAR(255), 
    @EstadoCompraId INT  
)
AS
BEGIN
    BEGIN TRY
	BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM CarritoCompras WHERE Id_CarritoCompra = @CarritoCompraId AND UsuarioId = @UsuarioId)
        BEGIN
            THROW 50000, 'El carrito de compra no existe o no pertenece al usuario.', 1;
        END

        IF NOT EXISTS (SELECT 1 FROM EstadoCompra WHERE Id_EstadoCompra = @EstadoCompraId)
        BEGIN
            THROW 50000, 'El estado de compra no es válido.', 1;
        END

        INSERT INTO DetalleCompra (UsuarioId, CarritoCompraId, ImporteTotal, DireccionEntregar, EstadoCompraId, Fecha_Compra)
        VALUES (@UsuarioId, @CarritoCompraId, @ImporteTotal, @DireccionEntregar, @EstadoCompraId, GETDATE());

		COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

--SELECT COUNT(*) AS Cantidad FROM Usuarios WHERE AccesoId = 3 AND Estado= 1
--SELECT COUNT(*) AS Cantidad FROM Usuarios WHERE AccesoId = 2 AND Estado= 1
--SELECT COUNT(*) AS Cantidad FROM Articulos WHERE Estado= 1
--SELECT SUM(Stock * Precio) AS PrecioTotales FROM Articulos WHERE Estado= 1
--SELECT a.Id_Articulo, a.Nombre, a.Stock from Articulos a WHERE stock < 50
--SELECT a.Id_Articulo, a.Nombre, a.Stock, a.Precio from Articulos a WHERE stock > 40
select * from Usuarios
select * from Articulos
--SELECT * FROM Carrito_x_Articulos
--SELECT * FROM CarritoCompras
--select * from DetalleCompra
--delete DetalleCompra where UsuarioId = 7
--delete CarritoCompras where UsuarioId = 7
--delete Usuarios where Id_Usuario = 7
--https://cdn.v2.tiendanegocio.com/gallery/19678/img_19678_188f3550886.png?class=sm

--------IMAGENES---------------
CREATE PROCEDURE sp_AgregarImagen
    @ArticuloId INT,         
    @ImagenURL VARCHAR(MAX) 
AS
BEGIN
    BEGIN TRY
        -- Insertar la imagen solo si no existe
        IF NOT EXISTS (SELECT 1 FROM Imagenes WHERE ImagenURL = @ImagenURL AND ArticuloId = @ArticuloId)
        BEGIN
            INSERT INTO Imagenes (ImagenURL, ArticuloId)
            VALUES (@ImagenURL, @ArticuloId);
        END
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_EliminarImagen
    @ArticuloId INT,         -- El ID del artículo
    @ImagenURL VARCHAR(255)  -- La URL de la imagen a eliminar
AS
BEGIN
    BEGIN TRY
        -- Eliminar la imagen del artículo
        DELETE FROM Imagenes
        WHERE ArticuloId = @ArticuloId AND ImagenURL = @ImagenURL;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_ObtenerImagenes
    @ArticuloId INT
AS
BEGIN
    BEGIN TRY
        -- Seleccionar todas las imágenes asociadas al artículo
        SELECT ImagenURL
        FROM Imagenes
        WHERE ArticuloId = @ArticuloId;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_EliminarTodasLasImagenes
    @ArticuloId INT
AS
BEGIN
    BEGIN TRY
        -- Eliminar todas las imágenes asociadas al artículo
        DELETE FROM Imagenes
        WHERE ArticuloId = @ArticuloId;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_ListarImagenesPorArticulo
    @Id_Articulo INT  -- El ID del artículo del cual se desean obtener las imágenes
AS
BEGIN
    BEGIN TRY
        -- Seleccionar tanto la URL de la imagen como el ID de la imagen
        SELECT Id_Imagen, ImagenURL
        FROM Imagenes
        WHERE ArticuloId = @Id_Articulo;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO