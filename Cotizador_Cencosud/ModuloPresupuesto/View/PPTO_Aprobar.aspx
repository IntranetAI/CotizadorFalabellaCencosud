<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PPTO_Aprobar.aspx.cs" Inherits="Cotizador_Cencosud.ModuloPresupuesto.View.PPTO_Aprobar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script>
        function Guardar1() {
            var id = document.getElementById("lblIDTiraje1").innerHTML;
            Aprobar(id);
        }
        function Guardar2() {
            var id = document.getElementById("lblIDTiraje2").innerHTML;
            Aprobar(id);
        }
        function Guardar3() {
            var id = document.getElementById("lblIDTiraje3").innerHTML;
            Aprobar(id);
        }

        function Aprobar(id) {
            $.ajax({
                url: "PPTO_Aprobar.aspx/AprobarPPTO",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'id':'" + eval(id) + "'}",
                success: function (msg) {
                    if (msg.d == 'OK') {
                        opener.location.reload(); window.close();
                    }
                    else {
                        alert('¡Ha Ocurrido un Error!');
                    }
                },
                error: function () {
                    alert('¡Ha Ocurrido un Error!');
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div style="max-width: 100%;">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                            Nombre <asp:Label ID="lblCatalogo" runat="server"></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <div class="panel panel-default" style="width: 33%;float: left;margin-right:3px;">
                      <!-- Default panel contents -->
                      <div class="panel-heading">Tiraje 1</div>
                      <div class="panel-body">
                        <table class="table">
                            <tr><td>Cantidad</td><td align="right">
                                <asp:Label ID="lblCantidad1" runat="server"></asp:Label></td></tr>
                            <tr><td>Costo Total</td><td align="right">$<asp:Label ID="lblPrecio1" runat="server"></asp:Label></td></tr>
                        </table>
                        <asp:Label ID="lblIDTiraje1" runat="server" style="display:none;"></asp:Label>
                        <div class="col-xs-12">
                            <button type="button" class="btn btn-primary" onclick="Guardar1()">
                                Aprobar</button>
                        </div>
                      </div>

                    </div>
                    <div id="divT2" runat="server" class="panel panel-default" style="width: 33%;float: left;;">
                      <!-- Default panel contents -->
                      <div class="panel-heading">Tiraje 2</div>
                      <div class="panel-body">
                        <table class="table">
                            <tr><td>Cantidad</td><td align="right">
                                <asp:Label ID="lblCantidad2" runat="server"></asp:Label></td></tr>
                            <tr><td>Costo Total</td><td align="right">$<asp:Label ID="lblPrecio2" runat="server"></asp:Label></td></tr>
                        </table>
                        <asp:Label ID="lblIDTiraje2" runat="server" style="display:none;"></asp:Label>
                        <div class="col-xs-12">
                            <button type="button" class="btn btn-primary" onclick="Guardar2()">
                                Aprobar</button>
                        </div>
                      </div>

                    </div>
                    <div id="divT3" runat="server" class="panel panel-default" style="width: 33%;float: left;margin-left:3px">
                      <!-- Default panel contents -->
                      <div class="panel-heading">Tiraje 3</div>
                      <div class="panel-body">
                        <table class="table">
                            <tr><td>Cantidad</td><td align="right">
                                <asp:Label ID="lblCantidad3" runat="server"></asp:Label></td></tr>
                            <tr><td>Costo Total</td><td align="right">$<asp:Label ID="lblPrecio3" runat="server"></asp:Label></td></tr>
                        </table>
                        <asp:Label ID="lblIDTiraje3" runat="server" style="display:none;"></asp:Label>
                        <div class="col-xs-12">
                            <button type="button" class="btn btn-primary" onclick="Guardar3()">
                                Aprobar</button>
                        </div>
                      </div>

                    </div>
                    
                </div>
            </div>
        </div>
        
    </center>
    </form>
</body>
</html>
