<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="PruebaUsuarios.proyecto.Dashboard" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Dashboard - Perfil de Usuario</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" rel="stylesheet">
    <link href="Content/Site.css" rel="stylesheet">
</head>
<body class="dashboard-page">
    <form id="form1" runat="server">
    <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
        <div class="container">
            <a class="navbar-brand" href="#">
                <i class="fas fa-user-circle"></i> Panel de Usuario
            </a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ms-auto">
                    <li class="nav-item">
                        <span class="nav-link">
                            <i class="fas fa-user"></i> 
                            <asp:Label ID="lblNombreUsuario" runat="server"></asp:Label>
                        </span>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton ID="btnCerrarSesion" runat="server" CssClass="nav-link" 
                            OnClick="btnCerrarSesion_Click">
                            <i class="fas fa-sign-out-alt"></i> Cerrar Sesión
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </div>
    </nav>

    <div class="container mt-4">
        <div class="row">
            <div class="col-md-4">
                <div class="profile-card">
                    <div class="profile-header">
                        <div class="profile-avatar">
                            <asp:Label ID="lblIniciales" runat="server"></asp:Label>
                        </div>
                        <h3>
                            <asp:Label ID="lblNombreCompleto" runat="server"></asp:Label>
                        </h3>
                        <p class="profile-username">
                            <i class="fas fa-at"></i> 
                            <asp:Label ID="lblUsername" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="profile-menu">
                        <a href="#" class="menu-item active">
                            <i class="fas fa-user"></i> Mi Perfil
                        </a>
                        <a href="#" class="menu-item">
                            <i class="fas fa-cog"></i> Configuración
                        </a>
                        <a href="#" class="menu-item">
                            <i class="fas fa-shield-alt"></i> Seguridad
                        </a>
                    </div>
                </div>
            </div>

            <div class="col-md-8">
                <div class="content-card">
                    <h4 class="card-title">
                        <i class="fas fa-id-card"></i> Información Personal
                    </h4>
                    
                    <div class="row mt-4">
                        <div class="col-md-6">
                            <div class="info-group">
                                <label>
                                    <i class="fas fa-user"></i> Nombre(s)
                                </label>
                                <p><asp:Label ID="lblNombre" runat="server"></asp:Label></p>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="info-group">
                                <label>
                                    <i class="fas fa-user"></i> Apellido(s)
                                </label>
                                <p><asp:Label ID="lblApellido" runat="server"></asp:Label></p>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="info-group">
                                <label>
                                    <i class="fas fa-envelope"></i> Correo Electrónico
                                </label>
                                <p><asp:Label ID="lblEmail" runat="server"></asp:Label></p>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="info-group">
                                <label>
                                    <i class="fas fa-phone"></i> Teléfono
                                </label>
                                <p><asp:Label ID="lblTelefono" runat="server"></asp:Label></p>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="info-group">
                                <label>
                                    <i class="fas fa-calendar"></i> Fecha de Nacimiento
                                </label>
                                <p><asp:Label ID="lblFechaNacimiento" runat="server"></asp:Label></p>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="info-group">
                                <label>
                                    <i class="fas fa-calendar-check"></i> Último Acceso
                                </label>
                                <p><asp:Label ID="lblUltimoAcceso" runat="server"></asp:Label></p>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="content-card mt-3">
                    <h4 class="card-title">
                        <i class="fas fa-chart-line"></i> Actividad de la Sesión
                    </h4>
                    <div class="row mt-4">
                        <div class="col-md-4">
                            <div class="stat-card">
                                <div class="stat-icon bg-success">
                                    <i class="fas fa-check-circle"></i>
                                </div>
                                <div class="stat-info">
                                    <h5>Sesión Activa</h5>
                                    <p>Conectado exitosamente</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="stat-card">
                                <div class="stat-icon bg-info">
                                    <i class="fas fa-clock"></i>
                                </div>
                                <div class="stat-info">
                                    <h5>Tiempo de Inactividad</h5>
                                    <p><span id="lblTiempoInactividad">0</span> min</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="stat-card">
                                <div class="stat-icon bg-warning">
                                    <i class="fas fa-sync"></i>
                                </div>
                                <div class="stat-info">
                                    <h5>Extensiones</h5>
                                    <p><span id="lblExtensiones">0</span> veces</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="sessionWarningModal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header bg-warning">
                    <h5 class="modal-title">
                        <i class="fas fa-exclamation-triangle"></i> Advertencia de Sesión
                    </h5>
                </div>
                <div class="modal-body text-center">
                    <div class="warning-icon">
                        <i class="fas fa-clock"></i>
                    </div>
                    <h4>Su sesión está a punto de expirar</h4>
                    <p>Debido a su inactividad, la sesión se cerrará en:</p>
                    <div class="countdown-display">
                        <span id="countdown">60</span> segundos
                    </div>
                    <p class="text-muted">¿Desea extender su sesión?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" id="btnCerrarSesionModal">
                        <i class="fas fa-times"></i> Cerrar Sesión
                    </button>
                    <button type="button" class="btn btn-primary" id="btnExtenderSesion">
                        <i class="fas fa-sync"></i> Extender Sesión
                    </button>
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="Scripts/custom.js"></script>

    <script>
        let countdownInterval;
        let warningShown = false;
        let inactivityTime = 0;
        let modalActivo = false;
        const warningMinutes = 20;
        const warningSeconds = 60;
        let extensionesCount = 0;

        function updateInactivity() {
            if (!modalActivo) {
                inactivityTime++;
                document.getElementById('lblTiempoInactividad').textContent = inactivityTime;
                console.log('Inactividad: ' + inactivityTime + ' minutos');
            }
        }

        function resetInactivity() {
            if (!modalActivo) {
                inactivityTime = 0;
            }
        }

        document.addEventListener('mousemove', resetInactivity);
        document.addEventListener('keypress', resetInactivity);
        document.addEventListener('click', resetInactivity);
        document.addEventListener('scroll', resetInactivity);

        setInterval(updateInactivity, 60000);

        function checkSession() {
            if (!warningShown && inactivityTime >= warningMinutes) {
                showWarning();
            }
        }

        setInterval(checkSession, 1000);

        function showWarning() {
            warningShown = true;
            modalActivo = true;
            let seconds = warningSeconds;

            $('#sessionWarningModal').modal({
                backdrop: 'static',
                keyboard: false
            });
            $('#sessionWarningModal').modal('show');

            countdownInterval = setInterval(function () {
                seconds--;
                document.getElementById('countdown').textContent = seconds;

                if (seconds <= 0) {
                    clearInterval(countdownInterval);
                    $('#sessionWarningModal').modal('hide');
                    document.cookie = "ASP.NET_SessionId=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
                    window.location.href = 'Login.aspx?expired=1';
                }
            }, 1000);
        }

        document.getElementById('btnExtenderSesion').addEventListener('click', function () {
            clearInterval(countdownInterval);
            $('#sessionWarningModal').modal('hide');
            warningShown = false;
            modalActivo = false;
            inactivityTime = 0;
            document.getElementById('lblTiempoInactividad').textContent = '0';

            extensionesCount++;
            document.getElementById('lblExtensiones').textContent = extensionesCount;

            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/ExtenderSesion",
                contentType: "application/json",
                dataType: "json",
                success: function (response) {
                    console.log("Sesión extendida");
                }
            });
        });

        document.getElementById('btnCerrarSesionModal').addEventListener('click', function () {
            clearInterval(countdownInterval);
            $('#sessionWarningModal').modal('hide');
            document.cookie = "ASP.NET_SessionId=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
            window.location.href = 'Login.aspx';
        });
    </script>
    </form>
</body>
</html>
