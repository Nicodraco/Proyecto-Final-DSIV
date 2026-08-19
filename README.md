# QuickVentas - Sistema de Gestión para Pequeños Negocios

Aplicación de escritorio desarrollada como **proyecto final de Desarrollo de Software IV**.  
QuickVentas permite gestionar productos, clientes y ventas en una sola interfaz, con persistencia local usando SQLite.

## Objetivo del proyecto

Construir un sistema de gestión simple y funcional para negocios pequeños, aplicando arquitectura por capas, operaciones CRUD y registro de transacciones de venta con actualización de inventario.

## Funcionalidades principales

- **Gestión de productos**: crear, editar, eliminar, listar y buscar productos.
- **Gestión de clientes**: crear, editar, eliminar, listar y buscar clientes.
- **Registro de ventas**:
  - selección de productos y cantidades,
  - validación de stock disponible,
  - cálculo de subtotales y total,
  - asociación opcional a cliente.
- **Procesamiento transaccional**:
  - guarda la venta y sus detalles,
  - descuenta stock automáticamente,
  - usa transacción para mantener consistencia.
- **Reportes básicos**:
  - visualización de ventas recientes,
  - total de ventas registradas,
  - monto acumulado.
- **Inicialización automática de base de datos**:
  - crea `QuickVentas.db` y tablas al iniciar si no existen,
  - inserta datos de prueba iniciales.

## Arquitectura y organización

El proyecto está organizado por capas:

- `Entidades/`: modelos de dominio (`Producto`, `Cliente`, `Venta`, `VentaDetalle`).
- `AccesoDatos/`: conexión y creación de base de datos (`ConexionBD`).
- `LogicaNegocio/`: reglas y operaciones de negocio (`ProductoBL`, `ClienteBL`, `VentaBL`).
- Formularios WinForms (`frmPrincipal`, `frmProductos`, `frmClientes`, `frmVentas`, `frmReportes`): capa de presentación.

## Stack técnico

- **Lenguaje**: C#
- **Framework**: .NET Framework 4.8
- **UI**: Windows Forms
- **Base de datos**: SQLite
- **Acceso a datos**: `System.Data.SQLite`

## Estructura del repositorio

```text
Proyecto-Final-DSIV/
├── QuickVentas/
│   ├── AccesoDatos/
│   ├── Entidades/
│   ├── LogicaNegocio/
│   ├── frm*.cs
│   ├── QuickVentas.csproj
│   └── QuickVentas.sln
└── README.md
```

## Requisitos

- Windows 10/11
- Visual Studio 2019/2022 con carga de trabajo de .NET desktop
- .NET Framework 4.8

## Ejecución del proyecto

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/Nicodraco/Proyecto-Final-DSIV.git
   ```
2. Abrir `QuickVentas/QuickVentas.sln` en Visual Studio.
3. Restaurar paquetes NuGet (si aplica).
4. Ejecutar el proyecto (`F5`).

Al iniciar por primera vez, la aplicación crea automáticamente la base de datos local `QuickVentas.db`.

## Estado académico

Proyecto final universitario completado y publicado como evidencia de experiencia en:

- desarrollo de aplicaciones de escritorio,
- modelado básico de datos,
- operaciones CRUD,
- manejo de transacciones y lógica de negocio.
