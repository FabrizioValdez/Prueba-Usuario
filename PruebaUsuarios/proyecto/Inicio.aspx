<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="PruebaUsuarios.proyecto.Inicio" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Bienvenido</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" rel="stylesheet">
    <link href="Content/Site.css" rel="stylesheet">
</head>
<body class="login-page">
    <form id="form1" runat="server">
    <div class="container">
        <div class="login-container">
            <div class="login-card">
                <div class="welcome-card text-center p-4">
                    <div class="welcome-icon mb-4">
                        <i class="fas fa-handshake"></i>
                    </div>
                    
                    <h1 class="welcome-title mb-3">Bienvenido</h1>
                    
                    <h3 class="text-primary mb-3">
                        <i class="fas fa-user-circle"></i>
                        <asp:Label ID="lblNombreCliente" runat="server"></asp:Label>
                    </h3>
                    
                    <div class="alert alert-success d-inline-block mb-4">
                        <i class="fas fa-check-circle"></i> Su cuenta esta activada
                    </div>
                    
                    <p class="text-muted mb-4">Ya puede iniciar sesion</p>
                    
                    <asp:Button ID="btnIniciarSesion" runat="server" Text="Iniciar Sesion" 
                        CssClass="btn btn-primary btn-lg px-5" OnClick="btnIniciarSesion_Click" />
                </div>
            </div>
        </div>
    </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
