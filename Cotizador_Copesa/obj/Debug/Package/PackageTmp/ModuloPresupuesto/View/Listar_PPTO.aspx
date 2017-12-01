<%@ Page Title="" Language="C#" MasterPageFile="~/Estructura/MasterView/MasterEstructura.Master" AutoEventWireup="true" CodeBehind="Listar_PPTO.aspx.cs" Inherits="Cotizador_Copesa.ModuloPresupuesto.View.Listar_PPTO" %>

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
                url: "Listar_PPTO.aspx/ListarPresupuestos",
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
#contPrincipal {
    min-height: 700px !important;
    border: 1px solid #cacaca !important;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="nav nav-tabs">
        <li class="active"><a href="#1" data-toggle="tab">Presupuestos</a> </li>
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
       
    </div>
</asp:Content>
