CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `AspNetRoles` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUsers` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `UserName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `Email` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 NULL,
    `EmailConfirmed` tinyint(1) NOT NULL,
    `PasswordHash` longtext CHARACTER SET utf8mb4 NULL,
    `SecurityStamp` longtext CHARACTER SET utf8mb4 NULL,
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
    `PhoneNumber` longtext CHARACTER SET utf8mb4 NULL,
    `PhoneNumberConfirmed` tinyint(1) NOT NULL,
    `TwoFactorEnabled` tinyint(1) NOT NULL,
    `LockoutEnd` datetime(6) NULL,
    `LockoutEnabled` tinyint(1) NOT NULL,
    `AccessFailedCount` int NOT NULL,
    CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TipoModificacion` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nombre` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CodigoInterno` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Activo` tinyint(1) NOT NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NULL,
    CONSTRAINT `PK_TipoModificacion` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetRoleClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUserClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `ProviderKey` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `ProviderDisplayName` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUserRoles` (
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`UserId`, `RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUserTokens` (
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `LoginProvider` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `Value` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `DepartamentoEmpresa` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nombre` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Activo` tinyint(1) NOT NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NULL,
    CONSTRAINT `PK_DepartamentoEmpresa` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_DepartamentoEmpresa_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EstadosFactura` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nombre` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NULL,
    CONSTRAINT `PK_EstadosFactura` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_EstadosFactura_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Generos` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nombre` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NULL,
    CONSTRAINT `PK_Generos` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Generos_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ParametrosSistema` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `EmpresaId` int NULL,
    `Nombre` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Activo` tinyint(1) NOT NULL,
    `ValorNumerico` decimal(65,30) NULL,
    `ValorString` varchar(500) CHARACTER SET utf8mb4 NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NULL,
    CONSTRAINT `PK_ParametrosSistema` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ParametrosSistema_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PuestoLaboral` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nombre` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Activo` tinyint(1) NOT NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NULL,
    CONSTRAINT `PK_PuestoLaboral` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PuestoLaboral_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `RegimenEmpresarial` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nombre` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NULL,
    CONSTRAINT `PK_RegimenEmpresarial` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_RegimenEmpresarial_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TipoIdentificacion` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `EmpresaId` int NULL,
    `Nombre` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Activo` tinyint(1) NOT NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NOT NULL,
    CONSTRAINT `PK_TipoIdentificacion` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TipoIdentificacion_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Empresa` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `NombreComercial` longtext CHARACTER SET utf8mb4 NOT NULL,
    `RazonSocial` longtext CHARACTER SET utf8mb4 NOT NULL,
    `NumeroRuc` longtext CHARACTER SET utf8mb4 NOT NULL,
    `RegimenEmpresarialId` int NOT NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NULL,
    CONSTRAINT `PK_Empresa` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Empresa_RegimenEmpresarial_RegimenEmpresarialId` FOREIGN KEY (`RegimenEmpresarialId`) REFERENCES `RegimenEmpresarial` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Empresa_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Clientes` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Nombre` longtext CHARACTER SET utf8mb4 NOT NULL,
    `NumeroIdentificacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `TipoIdentificacionId` int NOT NULL,
    `Activo` tinyint(1) NOT NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NULL,
    CONSTRAINT `PK_Clientes` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Clientes_TipoIdentificacion_TipoIdentificacionId` FOREIGN KEY (`TipoIdentificacionId`) REFERENCES `TipoIdentificacion` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Clientes_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategoriaProductos` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `EmpresaId` int NULL,
    `Nombre` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Activo` tinyint(1) NOT NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NULL,
    CONSTRAINT `PK_CategoriaProductos` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CategoriaProductos_Empresa_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresa` (`Id`),
    CONSTRAINT `FK_CategoriaProductos_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Sucursales` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `EmpresaId` int NOT NULL,
    `Nombre` longtext CHARACTER SET utf8mb4 NOT NULL,
    `DireccionFisica` longtext CHARACTER SET utf8mb4 NOT NULL,
    `NumeroTelefono` longtext CHARACTER SET utf8mb4 NOT NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NULL,
    CONSTRAINT `PK_Sucursales` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Sucursales_Empresa_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresa` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Sucursales_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SubCategoriaProductos` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `EmpresaId` int NULL,
    `CategoriaProductosId` bigint NULL,
    `Nombre` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Activo` tinyint(1) NOT NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NULL,
    CONSTRAINT `PK_SubCategoriaProductos` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_SubCategoriaProductos_CategoriaProductos_CategoriaProductosId` FOREIGN KEY (`CategoriaProductosId`) REFERENCES `CategoriaProductos` (`Id`),
    CONSTRAINT `FK_SubCategoriaProductos_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Empleados` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `EmpresaId` int NOT NULL,
    `SucursalId` int NOT NULL,
    `Nombres` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Apellidos` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Email` longtext CHARACTER SET utf8mb4 NOT NULL,
    `EmailConfirmado` longtext CHARACTER SET utf8mb4 NOT NULL,
    `TipoIdentificacionId` int NOT NULL,
    `NumeroIdentificacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `PuestoLaboralId` int NOT NULL,
    `FechaNacimiento` datetime(6) NOT NULL,
    `GeneroId` int NOT NULL,
    `DepartamentoEmpresaId` int NOT NULL,
    `FotoPath` longtext CHARACTER SET utf8mb4 NOT NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NULL,
    CONSTRAINT `PK_Empleados` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Empleados_DepartamentoEmpresa_DepartamentoEmpresaId` FOREIGN KEY (`DepartamentoEmpresaId`) REFERENCES `DepartamentoEmpresa` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Empleados_Empresa_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresa` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Empleados_Generos_GeneroId` FOREIGN KEY (`GeneroId`) REFERENCES `Generos` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Empleados_PuestoLaboral_PuestoLaboralId` FOREIGN KEY (`PuestoLaboralId`) REFERENCES `PuestoLaboral` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Empleados_Sucursales_SucursalId` FOREIGN KEY (`SucursalId`) REFERENCES `Sucursales` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Empleados_TipoIdentificacion_TipoIdentificacionId` FOREIGN KEY (`TipoIdentificacionId`) REFERENCES `TipoIdentificacion` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Empleados_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Facturas` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `EmpresaID` int NOT NULL,
    `SucursalId` int NOT NULL,
    `NumeroFactura` longtext CHARACTER SET utf8mb4 NULL,
    `Fecha` datetime(6) NOT NULL,
    `EstadoFacturaId` int NOT NULL,
    `ClienteId` bigint NOT NULL,
    `Subtotal` decimal(65,30) NOT NULL,
    `Impuestos` decimal(65,30) NOT NULL,
    `DescuentoValor` decimal(65,30) NULL,
    `PromocionId` int NULL,
    `Total` decimal(65,30) NOT NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NOT NULL,
    CONSTRAINT `PK_Facturas` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Facturas_Clientes_ClienteId` FOREIGN KEY (`ClienteId`) REFERENCES `Clientes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Facturas_Empresa_EmpresaID` FOREIGN KEY (`EmpresaID`) REFERENCES `Empresa` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Facturas_EstadosFactura_EstadoFacturaId` FOREIGN KEY (`EstadoFacturaId`) REFERENCES `EstadosFactura` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Facturas_Sucursales_SucursalId` FOREIGN KEY (`SucursalId`) REFERENCES `Sucursales` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Facturas_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Productos` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `EmpresaId` int NOT NULL,
    `Nombre` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `PrecioVenta` decimal(65,30) NOT NULL,
    `PrecioCosto` decimal(65,30) NOT NULL,
    `EsServicio` tinyint(1) NOT NULL,
    `AplicaIVA` tinyint(1) NOT NULL,
    `Activo` tinyint(1) NOT NULL,
    `CategoriaProductosId` bigint NULL,
    `SubCategoriaProductosId` bigint NULL,
    `FotoPath` longtext CHARACTER SET utf8mb4 NULL,
    `UsuarioCreacion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `FechaModificacion` datetime(6) NULL,
    `UsuarioModificacion` longtext CHARACTER SET utf8mb4 NULL,
    `TipoModificacionId` int NULL,
    CONSTRAINT `PK_Productos` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Productos_CategoriaProductos_CategoriaProductosId` FOREIGN KEY (`CategoriaProductosId`) REFERENCES `CategoriaProductos` (`Id`),
    CONSTRAINT `FK_Productos_Empresa_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `Empresa` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Productos_SubCategoriaProductos_SubCategoriaProductosId` FOREIGN KEY (`SubCategoriaProductosId`) REFERENCES `SubCategoriaProductos` (`Id`),
    CONSTRAINT `FK_Productos_TipoModificacion_TipoModificacionId` FOREIGN KEY (`TipoModificacionId`) REFERENCES `TipoModificacion` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

CREATE INDEX `IX_CategoriaProductos_EmpresaId` ON `CategoriaProductos` (`EmpresaId`);

CREATE INDEX `IX_CategoriaProductos_TipoModificacionId` ON `CategoriaProductos` (`TipoModificacionId`);

CREATE INDEX `IX_Clientes_TipoIdentificacionId` ON `Clientes` (`TipoIdentificacionId`);

CREATE INDEX `IX_Clientes_TipoModificacionId` ON `Clientes` (`TipoModificacionId`);

CREATE INDEX `IX_DepartamentoEmpresa_TipoModificacionId` ON `DepartamentoEmpresa` (`TipoModificacionId`);

CREATE INDEX `IX_Empleados_DepartamentoEmpresaId` ON `Empleados` (`DepartamentoEmpresaId`);

CREATE INDEX `IX_Empleados_EmpresaId` ON `Empleados` (`EmpresaId`);

CREATE INDEX `IX_Empleados_GeneroId` ON `Empleados` (`GeneroId`);

CREATE INDEX `IX_Empleados_PuestoLaboralId` ON `Empleados` (`PuestoLaboralId`);

CREATE INDEX `IX_Empleados_SucursalId` ON `Empleados` (`SucursalId`);

CREATE INDEX `IX_Empleados_TipoIdentificacionId` ON `Empleados` (`TipoIdentificacionId`);

CREATE INDEX `IX_Empleados_TipoModificacionId` ON `Empleados` (`TipoModificacionId`);

CREATE INDEX `IX_Empresa_RegimenEmpresarialId` ON `Empresa` (`RegimenEmpresarialId`);

CREATE INDEX `IX_Empresa_TipoModificacionId` ON `Empresa` (`TipoModificacionId`);

CREATE INDEX `IX_EstadosFactura_TipoModificacionId` ON `EstadosFactura` (`TipoModificacionId`);

CREATE INDEX `IX_Facturas_ClienteId` ON `Facturas` (`ClienteId`);

CREATE INDEX `IX_Facturas_EmpresaID` ON `Facturas` (`EmpresaID`);

CREATE INDEX `IX_Facturas_EstadoFacturaId` ON `Facturas` (`EstadoFacturaId`);

CREATE INDEX `IX_Facturas_SucursalId` ON `Facturas` (`SucursalId`);

CREATE INDEX `IX_Facturas_TipoModificacionId` ON `Facturas` (`TipoModificacionId`);

CREATE INDEX `IX_Generos_TipoModificacionId` ON `Generos` (`TipoModificacionId`);

CREATE INDEX `IX_ParametrosSistema_TipoModificacionId` ON `ParametrosSistema` (`TipoModificacionId`);

CREATE INDEX `IX_Productos_CategoriaProductosId` ON `Productos` (`CategoriaProductosId`);

CREATE INDEX `IX_Productos_EmpresaId` ON `Productos` (`EmpresaId`);

CREATE INDEX `IX_Productos_SubCategoriaProductosId` ON `Productos` (`SubCategoriaProductosId`);

CREATE INDEX `IX_Productos_TipoModificacionId` ON `Productos` (`TipoModificacionId`);

CREATE INDEX `IX_PuestoLaboral_TipoModificacionId` ON `PuestoLaboral` (`TipoModificacionId`);

CREATE INDEX `IX_RegimenEmpresarial_TipoModificacionId` ON `RegimenEmpresarial` (`TipoModificacionId`);

CREATE INDEX `IX_SubCategoriaProductos_CategoriaProductosId` ON `SubCategoriaProductos` (`CategoriaProductosId`);

CREATE INDEX `IX_SubCategoriaProductos_TipoModificacionId` ON `SubCategoriaProductos` (`TipoModificacionId`);

CREATE INDEX `IX_Sucursales_EmpresaId` ON `Sucursales` (`EmpresaId`);

CREATE INDEX `IX_Sucursales_TipoModificacionId` ON `Sucursales` (`TipoModificacionId`);

CREATE INDEX `IX_TipoIdentificacion_TipoModificacionId` ON `TipoIdentificacion` (`TipoModificacionId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230620200841_ResetMigrations', '7.0.5');

COMMIT;

