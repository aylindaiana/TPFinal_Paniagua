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
--SELECT * FROM Usuarios
--SELECT Id_Usuario, Nombre, Apellido, Email, Contra, Direccion, Telefono, Localidad, FechaNacimiento, AccesoId, Estado FROM Usuarios
--INSERT INTO Usuarios (AccesoId, Id_Usuario, Nombre, Apellido, Email, Contra, Direccion, Telefono, Localidad, Fecha_nacimiento, Estado) VALUES (@IdAcceso, @Nombre, @Apellido, @Email, @Pass, @Direccion, @Telefono, @Localidad, @FechaNacimiento, 1)
--UPDATE Usuarios SET Nombre = @Nombre, Email = @Email, Contra = @Pass, Direccion = @Direccion, Telefono = @Telefono, Localidad = @Localidad, FechaNacimiento = @FechaNacimiento, AccesoId = @IdAcceso WHERE Id_Usuario = @Id_Usuario
--UPDATE Usuarios SET Estado = 0 WHERE Id_Usuario = @Id_Usuario
--SELECT Id_Usuario, Nombre, Apellido, Email, Contra, Direccion, Telefono, Localidad, FechaNacimiento, Estado, AccesoId FROM Usuarios WHERE Id_Usuario = @Id_Usuario

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

CREATE TABLE Imagenes (
    Id_Imagen INT IDENTITY(1,1) PRIMARY KEY,
    ImagenURL VARCHAR(255),
    ArticuloId INT,
    FOREIGN KEY (ArticuloId) REFERENCES Articulos(Id_Articulo)
);
GO


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

INSERT INTO Usuarios (Nombre, Apellido, Email, Contra, Direccion, Telefono, Localidad, Fecha_nacimiento, AccesoId)
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
('Bucaneras de Charol Rojas', 'Bucaneras brillantes de charol rojo intenso', 160.00, 30, 3, 5); -- Bucaneras de cuero
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
('https://http2.mlstatic.com/D_NQ_NP_851818-MLA69611608195_052023-O.webp', 9); -- Bucaneras de Charol Rojas
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
(5, 310.50); -- Pedro Valdebenito
GO


INSERT INTO DetalleCompra (UsuarioId, CarritoCompraId, ImporteTotal, DireccionEntregar,EstadoCompraId, Fecha_Compra) 
VALUES 
(3, 1, 95.50, 'Uriburu 1931', 1, '2024-12-30'), -- Sofia ramirez
(4, 2, 220.75, 'Humboldt 1225', 2, '2024-12-28'), -- Lucia Saavedra
(5, 3, 310.50, 'Caseros 3008', 2, '2025-01-03'); -- Pedro Valdebenito
GO

INSERT INTO Carrito_x_Articulos (CarritoCompraId, ArticuloId, Cantidad) 
VALUES 
(1, 1, 1), -- Vestido Rojo Elegante (Sofia)
(1, 3, 1), -- Corset Negro Clásico (Sofia)
(2, 7, 1), -- Perfume Floral Primavera (Lucia)
(2, 8, 2), -- Vestido Negro Clásico (Lucia)
(3, 5, 2), -- Bucaneras de Cuero Negras (Pedro)
(3, 9, 1); -- Bucaneras de Charol (Pedro)
GO

INSERT INTO Detalle_x_Articulos (DetalleCompraId, ArticuloId, Cantidad, PrecioUnidad)
VALUES 
(1, 1, 1, 120.99), -- Sofia
(1, 3, 1, 85.99), -- Sofia
(2, 7, 1, 60.00), -- Lucia
(2, 8, 2, 130.00), -- Lucia
(3, 5, 2, 150.00), -- Pedro
(3, 9, 1, 160.00); -- Pedro
GO

-------------------PROCEDIMIENTOS ALMACENADOS ------------------

--AREA ARTICULOS
CREATE PROCEDURE sp_ListarArticulos            
AS
BEGIN
    SELECT 
        A.Id_Articulo,
        A.Nombre AS NombreArticulo,
        A.Descripcion,
        A.Precio,
        A.Stock,
        C.Nombre AS NombreCategoria,
        T.Nombre AS NombreTipo,
        A.Estado
    FROM 
        Articulos A
    INNER JOIN 
        Categorias C ON A.CategoriaId = C.Id_Categoria
    INNER JOIN 
        Tipos T ON A.TipoId = T.Id_Tipo
    ORDER BY 
        A.Nombre
END;
GO
