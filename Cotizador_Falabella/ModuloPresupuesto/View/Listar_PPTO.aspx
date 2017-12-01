<%@ Page Title="" Language="C#" MasterPageFile="~/Estructura/MasterView/MasterEstructura.Master" AutoEventWireup="true" CodeBehind="Listar_PPTO.aspx.cs" Inherits="Cotizador_Falabella.ModuloPresupuesto.View.Listar_PPTO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Estructura/Js/jquery-1.12.4.js" type="text/javascript"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.13/js/jquery.dataTables.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css">
    <script language="javascript" type="text/javascript">
        function openGame(OT) {
            var respuesta = PageMethods.Delete(OT);
            alert("Eliminado Correctamente");
            window.location = "Historico_PPTO.aspx";
        }
        function openEdit(id, Estado, Empresa) {
            window.location = "Presupuestador.aspx";
        }
        function openDuplic(id, Estado, Empresa) {
            window.location = "Presupuestador.aspx";
        }
        function openPDF(id) {
            var Nombre = "";
            $.ajax({
                url: "Listar_PPTO.aspx/NombrePresupuesto",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'id':'" + id + "'}",
                success: function (data) {
                    if (data.d[0] != "" && data.d[0] != null) {
                        if (data.d[1] == 2) {
                            window.open('Oferta_Comercial.aspx?id=' + id, 'Detalle OT', 'left=160,top=100,width=1020 ,height=770,scrollbars=no,dependent=no,toolbar=no,location=no,status=no,directories=no,menubar=no,status=no,resizable=yes');
                        }
                        else {
                            window.open('../../PDF/' + id + '.- ' + data.d[0] + '.pdf', 'Detalle OT', 'left=160,top=100,width=1020 ,height=770,scrollbars=no,dependent=no,toolbar=no,location=no,status=no,directories=no,menubar=no,status=no,resizable=yes');
                        }
                    }


                },
                error: function () {
                    alert('¡Ha Ocurrido un Error!');
                }
            });

        }
        function format(input) {
            var num = input.value.replace(/\./g, '');
            if (!isNaN(num)) {
                num = num.toString().split('').reverse().join('').replace(/(?=\d*\.?)(\d{3})/g, '$1.');
                num = num.split('').reverse().join('').replace(/^[\.]/, '');
                input.value = num;
                Validador(4);
                CalcularPreprensa();
            }

            else {
                input.value = input.value.replace(/[^\d\.]*/g, '');
            }
        }
        function muestraPPTOPendientes() {
            var Usuario = document.getElementById("lblUsuario").innerHTML;
            $.ajax({
                url: "Listar_PPTO.aspx/ListarPPTOPendientes",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'EstadoPPTO':'1','Usuario':'" + Usuario + "'}",
                success: function (data) {
                    var algo = data.d.replace(/_/g, '"');
                    $('#tblPresupuestosPendientes').DataTable({
                        "searching": false,
                        "scrollY": "550px",
                        "scrollCollapse": true,
                        "paging": false,
                        "bDestroy": true,
                        "aaData": JSON.parse(algo),
                        "aoColumns": [

                                {
                                    "mDataProp": "idPresupuestos"
                                },
                                {
                                    "mDataProp": "NombrePresupuesto"
                                },
                                {
                                    "mDataProp": "Formato"
                                },
                                {
                                    "mDataProp": "Encuadernacion"
                                },
                                {
                                    "mDataProp": "PaginasInt"
                                },
                                {
                                    "mDataProp": "PapelInt"
                                },
                                {
                                    "mDataProp": "PaginasTap"
                                },
                                {
                                    "mDataProp": "PapelTap"
                                },
                                {
                                    "mDataProp": "Editar"
                                }]
                                ,
                        "aoColumnDefs": [{ "bVisible": true, "aTargets": [8]}]

                    });
                },
                error: function () {
                    alert('¡Ha Ocurrido un Error!');
                }
            });
        }

        function muestraPPTOAprobados() {
            var Usuario = document.getElementById("lblUsuario").innerHTML;
            $.ajax({
                url: "Listar_PPTO.aspx/ListarPPTOAprobados",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'EstadoPPTO':'2','Usuario':'" + Usuario + "'}",
                success: function (data) {
                    var algo = data.d.replace(/_/g, '"');
                    $('#tblPresupuestosAprobados').DataTable({
                        "searching": false,
                        "scrollY": "550px",
                        "scrollCollapse": true,
                        "paging": false,
                        "bDestroy": true,
                        "aaData": JSON.parse(algo),
                        "aoColumns": [

                                {
                                    "mDataProp": "idPresupuestos"
                                },
                                {
                                    "mDataProp": "NombrePresupuesto"
                                },
                                {
                                    "mDataProp": "Formato"
                                },
                                {
                                    "mDataProp": "Encuadernacion"
                                },
                                {
                                    "mDataProp": "PaginasInt"
                                },
                                {
                                    "mDataProp": "PapelInt"
                                },
                                {
                                    "mDataProp": "PaginasTap"
                                },
                                {
                                    "mDataProp": "PapelTap"
                                },
                                {
                                    "mDataProp": "Editar"
                                }]
                                ,
                        "aoColumnDefs": [{ "bVisible": true, "aTargets": [8]}]

                    });
                },
                error: function () {
                    alert('¡Ha Ocurrido un Error!');
                }
            });
        }

        function levantarPopupAprobar(id) {
            window.open('PPTO_Aprobar.aspx?id=' + id, 'Detalle PPTO', 'left=160,top=100,width=800 ,height=312,scrollbars=no,dependent=no,toolbar=no,location=no,status=no,directories=no,menubar=no,status=no,resizable=yes');

        }
        function levantarPopupDespacho(id) {
            window.open('PPTO_Despacho.aspx?id=' + id, 'Despacho PPTO', 'left=160,top=100,width=800 ,height=200,scrollbars=no,dependent=no,toolbar=no,location=no,status=no,directories=no,menubar=no,status=no,resizable=yes');

        }
        function levantarPopupDetalle(id) {
            window.open('Detalle_PPTO.aspx?id=' + id, 'Despacho PPTO', 'left=160,top=100,width=900 ,height=800,scrollbars=no,dependent=no,toolbar=no,location=no,status=no,directories=no,menubar=no,status=no,resizable=yes');

        }
        
    </script>
    <style>
    .table{
     margin: 0 auto;
     width: 100% !important;
     clear: both;
     border-collapse: collapse;
     word-wrap:break-word;
}
.dataTables_scrollHeadInner
{
    width: 100% !important;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="nav nav-tabs">
        <li class="active"><a href="#1" data-toggle="tab">PPTO Pendientes</a> </li>
        <li><a href="#2" data-toggle="tab">PPTO Aprobados</a> </li>
    </ul>
    <div class="tab-content ">
        <div class="tab-pane active" id="1">
            <table id="tblPresupuestosPendientes" class="table table-striped table-bordered"
                style="width: 100%; margin-bottom: 0px;" cellspacing="0">
                <thead>
                    <tr>
                        <th data-field="idPresupuestos" data-align="right" data-sortable="true">
                            Nº Ppto
                        </th>
                        <th data-field="NombrePresupuesto" data-align="right" data-sortable="true">
                            Nombre Catalogo
                        </th>
                        <th data-field="Formato" data-align="right" data-sortable="true">
                            Formato
                        </th>
                        <th data-field="Encuadernacion" data-align="right" data-sortable="true">
                            Encuadernacion
                        </th>
                        <th data-field="PaginasInt" data-align="right" data-sortable="true">
                            N° Páginas
                        </th>
                        <th data-field="PapelInt" data-align="right" data-sortable="true">
                            Papel Interior
                        </th>
                        <th data-field="PaginasTap" data-align="right" data-sortable="true">
                            N° Tapas
                        </th>
                        <th data-field="PapelTap" data-align="right" data-sortable="true">
                            Papel Tapas
                        </th>
                        <th data-field="Editar" data-align="right" data-sortable="true">
                            Acciones
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
        <div class="tab-pane" id="2">
            <table id="tblPresupuestosAprobados" class="table table-striped table-bordered" style="width: 100%;margin-bottom: 0px;" cellspacing="0">
                <thead>
                    <tr>
                        <th data-field="idPresupuestos" data-align="right" data-sortable="true">
                            Nº Ppto
                        </th>
                        <th data-field="NombrePresupuesto" data-align="right" data-sortable="true">
                            Nombre Catalogo
                        </th>
                        <th data-field="Formato" data-align="right" data-sortable="true">
                            Formato
                        </th>
                        <th data-field="Encuadernacion" data-align="right" data-sortable="true">
                            Encuadernacion
                        </th>
                        <th data-field="PaginasInt" data-align="right" data-sortable="true">
                            N° Páginas
                        </th>
                        <th data-field="PapelInt" data-align="right" data-sortable="true">
                            Papel Interior
                        </th>
                        <th data-field="PaginasTap" data-align="right" data-sortable="true">
                            N° Tapas
                        </th>
                        <th data-field="PapelTap" data-align="right" data-sortable="true">
                            Papel Tapas
                        </th>
                        <th data-field="Editar" data-align="right" data-sortable="true">
                            Acciones
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#AprobarPPTOModal"
        id="PopupAprobar2" style="display: none;">
        <span class="glyphicon glyphicon-plus"></span>Aprobar PPTO</button>
    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#DespachoModal"
        id="PopupDespacho" style="display: none;">
        <span class="glyphicon glyphicon-plus"></span>Aprobar PPTO</button>
    <div class="modal fade bs-example-modal-lg" id="AprobarPPTOModal" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 920px;">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Aprobar Presupuesto</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="ddlTirajes" class="control-label col-xs-2">
                            Tirajes:</label>
                        <div class="col-xs-10">
                            <asp:DropDownList ID="ddlTirajes" runat="server" CssClass="form-control" Style="margin-top: -10px;">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" id="DivFooterRequerimiento">
                    <asp:Label ID="lblUsuario" runat="server" Text="Label" Style="display: none;"></asp:Label>
                    <asp:Label ID="lblTipoDireccion" runat="server" Text="0" Style="display: none;"></asp:Label>
                    <button type="button" class="btn btn-primary" onclick="javascript:GuardarTIraje();"
                        data-dismiss="modal" id="btnGuardarDirecc">
                        Guardar Direccion</button>
                    <button type="button" class="btn btn-default" onclick="javascript:LimpiarFormulario('Tiraje');"
                        data-dismiss="modal">
                        Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bs-example-modal-lg" id="DespachoModal" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 920px;">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Despacho Presupuesto</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="ddlTirajes" class="control-label col-xs-2">
                            Despacho: $</label>
                        <div class="col-xs-10">
                            <asp:TextBox ID="txtTiraje1" runat="server" onkeyup="format(this)" onchange="format(this)"
                                CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" id="Div2">
                    <asp:Label ID="Label1" runat="server" Text="Label" Style="display: none;"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text="0" Style="display: none;"></asp:Label>
                    <button type="button" class="btn btn-primary" onclick="javascript:GuardarTIraje();"
                        data-dismiss="modal" id="Button1">
                        Guardar Direccion</button>
                    <button type="button" class="btn btn-default" onclick="javascript:LimpiarFormulario('Tiraje');"
                        data-dismiss="modal">
                        Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
