<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalle_PPTO.aspx.cs" Inherits="Cotizador_Copesa.ModuloPresupuesto.View.Detalle_PPTO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
   
    <style type="text/css">
        .font6
        {
            color: red;
            font-size: 10.0pt;
            font-weight: 700;
            font-style: italic;
            text-decoration: none;
            font-family: Arial, sans-serif;
        }
        .font5
        {
            color: windowtext;
            font-size: 10.0pt;
            font-weight: 700;
            font-style: italic;
            text-decoration: none;
            font-family: Arial, sans-serif;
        }
        @media print {
            body
            {
                font-size:10px !important;
            }
        }
        </style>
</head>
<body onload="print();">
    <form id="form1" runat="server">
    <center>
        <div style="max-width: 100%;">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Nombre
                        <asp:Label ID="lblCatalogo" runat="server"></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <asp:Label ID="txtTablaDetalle" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </center>
    </form>
</body>
</html>
