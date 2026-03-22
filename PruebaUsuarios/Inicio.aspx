<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="PruebaUsuarios.Inicio" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Bienvenido</title>
    <link href="~/Content/bootstrap.css" rel="stylesheet" type="text/css" />
    <style>
        body {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            margin: 0;
        }
        .welcome-card {
            background: white;
            border-radius: 20px;
            padding: 50px;
            text-align: center;
            box-shadow: 0 15px 35px rgba(0,0,0,0.2);
            max-width: 500px;
        }
        .welcome-card h1 {
            color: #667eea;
            font-size: 28px;
            margin-bottom: 20px;
        }
        .user-name {
            color: #333;
            font-size: 36px;
            font-weight: bold;
            margin: 20px 0;
        }
        .btn-login {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            border: none;
            padding: 15px 40px;
            font-size: 18px;
            border-radius: 50px;
            cursor: pointer;
            text-decoration: none;
            display: inline-block;
            margin-top: 30px;
            transition: transform 0.3s;
        }
        .btn-login:hover {
            transform: scale(1.05);
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="welcome-card">
            <h1>Bienvenido a nuestra aplicacion</h1>
            <p>Hola,</p>
            <div class="user-name">
                <asp:Label ID="lblNombreUsuario" runat="server"></asp:Label>
            </div>
            <p>Estamos encantados de tenerte aqui!</p>
            <a href="proyecto/Login.aspx" class="btn-login">Iniciar Sesion</a>
        </div>
    </form>
</body>
</html>
