# Sistema de Autenticación y Manejo de Sesiones - Prueba Técnica

Sistema de autenticación y manejo sesiones de usuarios con ASP.NET Web Forms, Bootstrap 5 y SQL Server.

## Requisitos

- Visual Studio 2022
- SQL Server (LocalDB o Express)
- .NET Framework 4.7.2

## Instalación

### 1. Base de datos

Ejecutar `scripts_sql/crear_base_datos.sql` en SQL Server Management Studio.

### 2. Configurar conexión

Modificar `Web.config` líneas 8-10:

```xml
<connectionStrings>
    <add name="UsuariosDB" 
         connectionString="Data Source=TU_SERVIDOR;Initial Catalog=UsuariosDB;Integrated Security=True;" 
         providerName="System.Data.SqlClient"/>
</connectionStrings>
```

### 3. Configurar correo (opcional)

Modificar `appSettings` en `Web.config`:

```xml
<appSettings>
    <add key="SmtpHost" value="smtp.gmail.com"/>
    <add key="SmtpPort" value="587"/>
    <add key="SmtpUser" value="tuemail@gmail.com"/>
    <add key="SmtpPass" value="tu_password_de_aplicacion"/>
    <add key="SmtpFrom" value="noreply@tusistema.com"/>
</appSettings>
```

**Nota para Gmail**: Necesitas crear una "App Password" en tu cuenta de Google:
1. Ve a Mi Cuenta > Seguridad
2. Habilita Verificación en 2 pasos
3. Ve a Contraseñas de aplicaciones
4. Genera una nueva contraseña para la aplicación

### 4. Ejecutar

1. Abrir solución en Visual Studio 2022
2. `F5` para ejecutar
3. Se abre `Default.aspx` → redirige automáticamente a `Inicio.aspx`
4. El **Seeder** crea automáticamente el usuario admin si la BD está vacía

## Credenciales de prueba

- **Usuario:** admin
- **Contraseña:** Admin123

*(El usuario se crea automáticamente con el Seeder si la base de datos está vacía)*

## Estructura del Proyecto

```
PruebaUsuarios/
├── App_Code/
│   ├── Usuario.cs          # Modelo de Usuario
│   ├── UsuarioDB.cs       # Acceso a datos
│   ├── CorreoService.cs   # Envío de correos SMTP
│   ├── SessionManager.cs  # Manejo de sesiones
│   └── Seeder.cs          # Crear usuario de prueba automáticamente
├── proyecto/
│   ├── Inicio.aspx        # Página principal
│   ├── Login.aspx         # Inicio de sesión
│   ├── Registrar.aspx     # Registro de usuarios
│   ├── Dashboard.aspx     # Panel del usuario
│   └── Default.aspx       # Redirección automática
├── scripts_sql/
│   └── crear_base_datos.sql
└── Web.config
```

## Flujo de la aplicación

1. `Default.aspx` → Redirige a Inicio.aspx
2. `Inicio.aspx` → Seeder crea usuario admin si no existe
3. `Login.aspx` → Autenticación con BCrypt
4. `Dashboard.aspx` → Panel principal del usuario

## Funcionalidades

- [x] Login con hashing BCrypt
- [x] Bloqueo por 5 intentos fallidos (15 minutos)
- [x] Notificación por correo al bloquear cuenta
- [x] Timeout de sesión (20 min de inactividad)
- [x] Advertencia de sesión con cuenta regresiva (60 seg)
- [x] Extensión de sesión
- [x] Registro de usuarios
- [x] Diseño responsive con Bootstrap 5
- [x] Iconos Font Awesome

## Paquetes NuGet

- Bootstrap (5.3.2)
- BCrypt.Net-Next
- System.Data.SqlClient
- Modernizr
- jQuery (3.7.0)
- MailKit (4.15.1) - preparado para futuro uso
