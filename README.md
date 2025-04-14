# Propuesta Ecommerce de Indumentaria

## Índice
- [Descripción del Proyecto](#descripción-del-proyecto)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Tecnologías Utilizadas](#tecnologías-utilizadas)
- [Desarrollador](#desarrollador)
- [Demostración](#demostración)
- [Instalación y Ejecución](#instalacion--y-ejecución)
  
![Image](https://github.com/user-attachments/assets/95424ee3-7539-4bda-acb0-c31b043097c9)
![Image](https://github.com/user-attachments/assets/3194af93-f69c-40cb-945b-a54c662b7231)
![Image](https://github.com/user-attachments/assets/66a2fd45-4525-45b3-ad40-95d77c51d7bc)

## Descripción del Proyecto
Venta de indumentaria y accesorios, permite ver todo tipo de ropa junto con accesorios, permitiendo elegir cuántas cantidades estén disponibles según su talle para agregar en un carrito de compras. Para avanzar con el pago se requiere hacer un registro para tener el usuario. El sistema tendrá un ingreso de autenticación para el comprador (Cliente), el vendedor (Empleado) que maneje el estado de las prendas y Jefe quien tendrá el acceso a todo (Administrador).
- Función Cliente
Podrá visualizar la ropa general con los precios organizando por tipos y categorías. Podrá buscar por nombre o filtrar. Una vez lleno el carrito y en caso de querer comprar se le dará la opción de registrarse si no es cliente (puede hacerlo antes si lo desea), en la cual se descontará el stock una vez que se compruebe que está logueado. Podrá agregar, modificar o eliminar los productos del Carrito. Una vez realizada la compra se descuenta el stock correspondiente a cada producto y generará un reporte con la factura que se generará con un número único para ser descargado.
- Función Empleado
La indumentaria será filtrada por marca y categoría, de las cuales serán administrables para crear, editar y eliminar. Habrá una configuración para visualizar las prendas, poder sumar stock, darlas de baja/alta, modificar o crear. También permite ver el estado del carrito una vez confirmado por el cliente y gestionar su estado.
- Función Administrador
Tendrá las mismas funciones que el Empleado, agregando que podrá visualizar y gestionar información de todos los usuarios.

## Estructura del Proyecto
- **/TPFinal_Paniagua**: Archivos principales del frontend y backend (ASP.NET, JavaScript, CSS).  
- **/Dominio**: Clases de dominio para la lógica de negocio (C#).  
- **/Manager**: Gestión de operaciones (ej. manejo de stock, usuarios).

## Tecnologías Utilizadas
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white)
- **Otros:** Autenticación y gestión de sesiones

## Instalación y Ejecución
1. Clona el repositorio: `git clone https://github.com/aylindalana/TPFinal_Paniagua.git`
2. Configura la base de datos en SQL Server usando los scripts en la carpeta `/TPFinal_Paniagua`.
3. Abre el proyecto en Visual Studio.
4. Configura la cadena de conexión en `web.config` con tu servidor SQL.
5. Ejecuta el proyecto: `F5` o `dotnet run`.
**Requisitos:** Visual Studio, SQL Server, .NET Framework.

## Desarrollador

| [<img src="https://avatars.githubusercontent.com/u/80922222?s=400&u=50f1d5ee252321889f3f5133baed02ee7143f103&v=4" width=115><br><sub>Aylin Paniagua</sub>](https://github.com/aylindaiana) |
| :---: |

## Demostración en Youtube
[![Demostración del proyecto](https://img.youtube.com/vi/fU_DuxQ8WNY/0.jpg)](https://www.youtube.com/watch?v=fU_DuxQ8WNY)  
