<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PPTO_Despacho.aspx.cs"
    Inherits="Cotizador_Cencosud.ModuloPresupuesto.View.PPTO_Despacho" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <div style="max-width: 600px;">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Despacho Presupuesto</h3>
            </div>
            <div class="panel-body">
                <div class="form-group">
                    <label for="ddlTirajes" class="control-label col-xs-2">
                        Despacho:
                    </label>
                    <div class="col-xs-10">
                        <asp:TextBox ID="txtTiraje1" runat="server" onkeyup="format(this)" onchange="format(this)"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-offset-2 col-xs-10">
                        <button type="button" class="btn btn-primary" onclick="Guardar()">
                            Guardar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </center>
    </form>
</body>
</html>
