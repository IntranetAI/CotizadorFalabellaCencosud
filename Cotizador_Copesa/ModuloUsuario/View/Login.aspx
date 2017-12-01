<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Cotizador_Copesa.ModuloUsuario.View.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="../../Estructura/Image/Logos color central.ico" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="../../Estructura/Js/validator.js" type="text/javascript"></script>
   
    <title>A Impresores Chile</title>
    <style type="text/css">
        body
        {
            background: #fff url(../../Estructura/Image/bg-sitio.jpg) top center;
        }
        .style1
        {
            width: 2000px;
            height: 699px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var txt = "AImpresores Presupuestador GrupoCopesa ";
        var espera = 1000;
        var refresca = null;
        function titulo() {
            document.title = txt;
            txt = txt.substring(1, txt.length) + txt.charAt(0);
            refresca = setTimeout("titulo()", espera);
        }
        titulo();
    </script>
    <script type="text/javascript">
        function IniciarSesion() {
            var Email = document.getElementById("inputEmail").value;
            var Contraseña = document.getElementById("inputPassword").value;
            if (Email != "" && Email != null && Contraseña != "") {
                $.ajax({
                    url: "Login.aspx/Iniciar",
                    type: "post",
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    data: "{'password':'" + Contraseña + "','Correo':'" + Email + "'}",
                    success: function (msg) {
                        if (msg.d != 'Error') {
                            window.location = "../../ModuloPresupuesto/View/Presupuestador.aspx?id=" + msg.d;
                        }
                        else {
                            alert(msg.d);
                        }
                    },
                    error: function () {
                        alert('¡Ha Ocurrido un Error!');
                    }
                });
            }
        }


    </script>
   
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal" data-toggle="validator">
    <center>
        <div style="max-width: 600px;margin-top:10%;">
            <div class="panel panel-default">
                <div class="panel-heading">
                <img src="../../Estructura/Image/LOGO%20A.png" />
                    <h3 class="panel-title">
                    
                         Presupuestador </h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label for="inputEmail" class="control-label col-xs-2">
                            Correo</label>
                        <div class="col-xs-10">
                            <input type="email" class="form-control" id="inputEmail" placeholder="Correo" data-error="La dirección de correo electrónico no es válida" required/>
                            <div class="help-block with-errors"></div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputPassword" class="control-label col-xs-2">
                            Contraseña</label>
                        <div class="col-xs-10">
                            <input type="password" class="form-control" id="inputPassword" placeholder="Contraseña" data-error="Debe ingresar contraseña" required/>
                            <div class="help-block with-errors"></div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-xs-12">
                            <button type="button" class="btn btn-primary" onclick="IniciarSesion()">
                                Iniciar sesión</button>
                        </div>
                    </div>
                    <div align="center">
                        
                        <img src="../../Estructura/Image/logo_copesa.jpg" width="150px"/>
                        
                        
                </div>
            </div>
        </div>
    </center>
    </form>
</body>
</html>
