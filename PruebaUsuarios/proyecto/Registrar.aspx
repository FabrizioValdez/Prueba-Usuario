<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registrar.aspx.cs" Inherits="PruebaUsuarios.proyecto.Registrar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Registro de Usuario</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" rel="stylesheet">
    <link href="Content/Site.css" rel="stylesheet">
</head>
<body class="login-page">
    <form id="form1" runat="server">
    <div class="container">
        <div class="login-container">
            <div class="login-card" style="max-width: 600px;">
                <div class="login-header">
                    <div class="login-icon">
                        <i class="fas fa-user-plus"></i>
                    </div>
                    <h2>Crear Cuenta</h2>
                    <p>Complete todos los campos para registrarse</p>
                </div>

                <div class="login-body">
                    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger">
                        <asp:Label ID="lblError" runat="server"></asp:Label>
                    </asp:Panel>

                    <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success">
                        <i class="fas fa-check-circle"></i> Usuario registrado exitosamente! 
                        <a href="Inicio.aspx" class="alert-link">Volver al inicio</a>
                    </asp:Panel>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label><i class="fas fa-user"></i> Nombre(s)</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese su nombre"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label><i class="fas fa-user"></i> Apellido(s)</label>
                                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ingrese su apellido"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <label><i class="fas fa-at"></i> Nombre de Usuario</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Ingrese un nombre de usuario"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label><i class="fas fa-envelope"></i> Correo Electronico</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="correo@ejemplo.com" TextMode="Email"></asp:TextBox>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group mb-3">
                                <label class="form-label"><i class="fas fa-key me-2"></i>Contrase&ntilde;a</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Minimo 6 caracteres" TextMode="Password"></asp:TextBox>
                                    <button class="btn btn-outline-secondary" type="button" onclick="togglePassword('txtPassword', this)">
                                        <i class="fas fa-eye"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group mb-3">
                                <label class="form-label"><i class="fas fa-key me-2"></i>Confirmar Contrase&ntilde;a</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" placeholder="Repita la contrasena" TextMode="Password"></asp:TextBox>
                                    <button class="btn btn-outline-secondary" type="button" onclick="togglePassword('txtConfirmPassword', this)">
                                        <i class="fas fa-eye"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <label><i class="fas fa-phone"></i> Telefono (Opcional)</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="+1234567890"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label><i class="fas fa-calendar"></i> Fecha de Nacimiento (Opcional)</label>
                        <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>

                    <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" CssClass="btn btn-primary btn-login" OnClick="btnRegistrar_Click" />

                    <div class="form-links">
                        <a href="Login.aspx" class="link-olvido"><i class="fas fa-arrow-left"></i> Ya tengo cuenta, ir al login</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

    <script>
    function togglePassword(controlId, btn) {
        // En ASP.NET, el ID del cliente puede variar, por eso buscamos que termine en el ID proporcionado
        var input = document.querySelector('[id$="' + controlId + '"]');
        var icon = btn.querySelector('i');

        if (input.type === "password") {
            input.type = "text";
            icon.classList.remove('fa-eye');
            icon.classList.add('fa-eye-slash');
        } else {
            input.type = "password";
            icon.classList.remove('fa-eye-slash');
            icon.classList.add('fa-eye');
        }
    }
    </script>
</body>
</html>
