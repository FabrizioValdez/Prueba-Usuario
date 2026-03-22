<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PruebaUsuarios.proyecto.Login" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Iniciar Sesión</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" rel="stylesheet">
    <link href="Content/Site.css" rel="stylesheet">
</head>
<body class="login-page">
    <form id="form1" runat="server">
    <div class="container">
        <div class="login-container">
            <div class="login-card">
                <div class="login-header">
                    <div class="login-icon">
                        <i class="fas fa-lock"></i>
                    </div>
                    <h2>Iniciar Sesión</h2>
                    <p>Ingrese sus credenciales para continuar</p>
                </div>

                <div class="login-body">
                    <asp:Panel ID="pnlBloqueo" runat="server" Visible="false" CssClass="alert alert-danger text-center">
                        <i class="fas fa-exclamation-triangle"></i>
                        <br />
                        <strong>Cuenta Bloqueada</strong>
                        <br />
                        <asp:Label ID="lblTiempoBloqueo" runat="server"></asp:Label>
                    </asp:Panel>

                    <asp:Panel ID="pnlSesionExpirada" runat="server" Visible="false" CssClass="alert alert-warning text-center">
                        <i class="fas fa-clock"></i>
                        <br />
                        <strong>Sesión expirada por inactividad</strong>
                        <br />
                        <small>Por favor, inicie sesión nuevamente</small>
                    </asp:Panel>

                    <div class="form-group">
                        <label for="txtUsuario">
                            <i class="fas fa-user"></i> Usuario
                        </label>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" 
                            placeholder="Ingrese su usuario" required></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="txtContrasena">
                            <i class="fas fa-key"></i> Contraseña
                        </label>
                        <div class="input-group">
                            <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" 
                                placeholder="Ingrese su contraseña" TextMode="Password" required></asp:TextBox>
                            <button type="button" class="btn btn-outline-secondary" id="btnTogglePassword">
                                <i class="fas fa-eye" id="iconToggle"></i>
                            </button>
                        </div>
                    </div>

                    <div class="form-links">
                        <a href="#" class="link-olvido">
                            <i class="fas fa-question-circle"></i>¿Olvido su contraseña?
                        </a>
                    </div>

                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" 
                        CssClass="btn btn-primary btn-login" OnClick="btnIngresar_Click" />

                    <div class="form-links text-center mt-3">
                        <span>No tiene cuenta? </span>
                        <a href="Registrar.aspx" class="link-olvido">
                            <i class="fas fa-user-plus"></i> Registrarse
                        </a>
                    </div>

                    <div class="form-links support-link">
                        <a href="mailto:soporte@tusistema.com" class="link-soporte">
                            <i class="fas fa-headset"></i> ¿Necesita ayuda? Contacte con el área de soporte
                        </a>
                    </div>

                    <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-danger mt-3">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </asp:Panel>
                </div>
            </div>

            <div class="login-footer">
                <p>&copy; 2024 Sistema de Usuarios. Todos los derechos reservados.</p>
            </div>
        </div>
    </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        document.getElementById('btnTogglePassword').addEventListener('click', function() {
            var passwordInput = document.getElementById('<%= txtContrasena.ClientID %>');
            var icon = document.getElementById('iconToggle');
            
            if (passwordInput.type === 'password') {
                passwordInput.type = 'text';
                icon.classList.remove('fa-eye');
                icon.classList.add('fa-eye-slash');
            } else {
                passwordInput.type = 'password';
                icon.classList.remove('fa-eye-slash');
                icon.classList.add('fa-eye');
            }
        });
    </script>
</body>
</html>
