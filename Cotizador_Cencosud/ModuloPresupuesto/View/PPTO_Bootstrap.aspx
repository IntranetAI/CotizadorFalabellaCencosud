<%@ Page Title="" Language="C#" MasterPageFile="~/Estructura/MasterView/MasterEstructura.Master" AutoEventWireup="true" CodeBehind="PPTO_Bootstrap.aspx.cs" Inherits="Cotizador_Cencosud.ModuloPresupuesto.View.PPTO_Bootstrap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Estructura/Js/jquery.min.js" type="text/javascript"></script>
    <script src="../../Estructura/Js/jquery-1.11.3.js" type="text/javascript"></script>
    <script src="../../Estructura/Js/jquery-ui-1.11.4.js" type="text/javascript"></script>
    <style>
        #content
        {
            width: 100%;
            margin: 0px auto;
        }
        
        #column-right
        {
         
            float: right;
            position: fixed;
            left: 74%;
            min-height: 170px;
            margin-bottom: 10px;
            margin-right: 10px;
            overflow: hidden;
            text-align: center;
            width: 20%;
        }
        
        #central
        {
            
            border-radius: 8px 8px 8px 8px;
            float: left;
            margin-bottom: 10px;
            width: 77%;
        }
       .panel-heading
       {
           font-size: 16px !important;
           font-weight : bold;
       }
    </style>
    <script>
        function GrabarPresupuesto() {
            window.open('Oferta_Comercial.aspx');
            window.location = 'Listar_PPTO.aspx';
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

        function pulsarTiraje(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 13 || tecla > 31 && (tecla < 48 || tecla > 57)) return false;
        }

        function Papel_gramaje(Div, Empresa) {
            var select = "";
            var answer = "";
            if (Div == "Interior") {
                select = document.getElementById("<%= ddlPapel.ClientID %>");
                answer = select.options[select.selectedIndex].text;
                var ddlTerritory = document.getElementById("<%= ddlgramage.ClientID %>");
                var lengthddlTerritory = ddlTerritory.length - 1;
                for (var i = lengthddlTerritory; i >= 0; i--) {
                    ddlTerritory.options[i] = null;
                }
            }
            else if ("Tapa") {
                select = document.getElementById("<%= ddlPapTapas.ClientID %>");
                answer = select.options[select.selectedIndex].text;
                var ddlTerritory = document.getElementById("<%= ddlGraTapas.ClientID %>");
                var lengthddlTerritory = ddlTerritory.length - 1;
                for (var i = lengthddlTerritory; i >= 0; i--) {
                    ddlTerritory.options[i] = null;
                }
            }
            $.ajax({
                url: "Presupuestador.aspx/Gramage_Papel",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'Papel':'" + answer + "','Div':'" + Div + "','Empresa':'" + Empresa + "'}",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    $.each(jsdata, function (key, value) {
                        if (Div == "Interior") {
                            $('#<%=ddlgramage.ClientID%>').append($("<option></option>").val(value.Gramage).html(value.Gramage));
                            document.getElementById("ContentPlaceHolder1_lblMaquinaI").innerHTML = "";

                        }
                        else if ("Tapa") {
                            $('#<%=ddlGraTapas.ClientID%>').append($("<option></option>").val(value.Gramage).html(value.Gramage));
                            document.getElementById("ContentPlaceHolder1_lblMaquinaT").innerHTML = "";

                        }
                    });

                },
                error: function () {
                    alert('¡Ha Ocurrido un Error!');
                }
            });
        }



        function CalcularPreprensa() {
            var selectPaginasInterior = document.getElementById("<%= ddlPaginas.ClientID %>");
            var PaginasInterior = selectPaginasInterior.options[selectPaginasInterior.selectedIndex].text;
            var selectPaginasTapa = document.getElementById("<%= ddlPagTapas.ClientID %>");
            var PaginasTapa = selectPaginasTapa.options[selectPaginasTapa.selectedIndex].value;

            var NroEntradasxPliego = "8";
            var Formato = "23,0 x 30,0";
            var selectPapelInt = document.getElementById("<%= ddlPapel.ClientID %>");
            var PapelInt = selectPapelInt.options[selectPapelInt.selectedIndex].value;
            var selectPapelTap = document.getElementById("<%= ddlPapTapas.ClientID %>");
            var PapelTap = selectPapelTap.options[selectPapelTap.selectedIndex].value;
            var selectGramajeInterior = document.getElementById("<%= ddlgramage.ClientID %>");
            var GramajeInterior = 0;
            if (selectGramajeInterior.options[selectGramajeInterior.selectedIndex] != null) {
                GramajeInterior = selectGramajeInterior.options[selectGramajeInterior.selectedIndex].value;
            }
            var selectGramajeTapas = document.getElementById("<%= ddlGraTapas.ClientID %>");
            var GramajeTapas = 0;
            if (selectGramajeTapas.options[selectGramajeTapas.selectedIndex] != null) {
                GramajeTapas = selectGramajeTapas.options[selectGramajeTapas.selectedIndex].value;
            }
            var selectEncuadernacion = document.getElementById("<%= ddlEncuadernacion.ClientID %>");
            var Encuadernacion = selectEncuadernacion.options[selectEncuadernacion.selectedIndex].text;
            var Tiraje = document.getElementById("<%= txtTiraje.ClientID%>").value;

            $.ajax({
                url: "Presupuestador.aspx/PrePrensa",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'PagInterior':'" + eval(PaginasInterior) + "','Pagtapa':'" + eval(PaginasTapa) + "','EntradasxFormato':'" + NroEntradasxPliego +
                    "','Formato':'" + Formato + "','GramajeInt1':'" + GramajeInterior + "','GramajeTapa1':'" + GramajeTapas + "','Papelinterior':'" + PapelInt +
                    "','PapelTapa':'" + PapelTap + "','Encuadernacion':'" + Encuadernacion + "','Tiraje':'" + Tiraje
                //                     +  + "','Empresa':'" + Empresa + "'
                   + "'}",
                success: function (msg) {
                    document.getElementById("ContentPlaceHolder1_ImpInterior").innerHTML = msg.d[0];
                    document.getElementById("ContentPlaceHolder1_ImpInterior2").innerHTML = msg.d[1];
                    document.getElementById("ContentPlaceHolder1_ImpInteriorFin").innerHTML = msg.d[2];
                    document.getElementById("ContentPlaceHolder1_lblPapelInteriorFijo").innerHTML = msg.d[3];
                    document.getElementById("ContentPlaceHolder1_lblPapelInteriorVari").innerHTML = msg.d[4];
                    document.getElementById("ContentPlaceHolder1_lblPrecioPapel1").innerHTML = msg.d[5];
                    document.getElementById("ContentPlaceHolder1_lblEnc").innerHTML = msg.d[6];
                    document.getElementById("ContentPlaceHolder1_lblEnc2").innerHTML = msg.d[7];
                    document.getElementById("ContentPlaceHolder1_Label14").innerHTML = msg.d[8];
                    document.getElementById("ContentPlaceHolder1_ImpTapas").innerHTML = msg.d[9];
                    document.getElementById("ContentPlaceHolder1_ImpTapas2").innerHTML = msg.d[10];
                    document.getElementById("ContentPlaceHolder1_ImpTapasFin").innerHTML = msg.d[11];
                    document.getElementById("ContentPlaceHolder1_lblPapelTapaFijo").innerHTML = msg.d[12];
                    document.getElementById("ContentPlaceHolder1_lblPapelTapaVari").innerHTML = msg.d[13];
                    document.getElementById("ContentPlaceHolder1_lblPrecioPapel2").innerHTML = msg.d[14];
                    document.getElementById("ContentPlaceHolder1_Label38").innerHTML = msg.d[15];
                    document.getElementById("ContentPlaceHolder1_Label39").innerHTML = msg.d[15];
                    document.getElementById("ContentPlaceHolder1_Label20").innerHTML = msg.d[16];
                    document.getElementById("ContentPlaceHolder1_lbl_TotalCostoPapelTota").innerHTML = msg.d[16];
                    document.getElementById("ContentPlaceHolder1_Label21").innerHTML = msg.d[17];
                    document.getElementById("ContentPlaceHolder1_Label60").innerHTML = msg.d[17];
                    document.getElementById("ContentPlaceHolder1_Label58").innerHTML = msg.d[12];
                    document.getElementById("ContentPlaceHolder1_Label59").innerHTML = msg.d[13];
                    document.getElementById("ContentPlaceHolder1_lbl_TotalCostoPapelFijo").innerHTML = msg.d[3];
                    document.getElementById("ContentPlaceHolder1_lbl_TotalCostoPapelVari").innerHTML = msg.d[4];
                    document.getElementById("ContentPlaceHolder1_Label12").innerHTML = msg.d[18];
                    document.getElementById("ContentPlaceHolder1_Label13").innerHTML = msg.d[19];
                    document.getElementById("ContentPlaceHolder1_Label82").innerHTML = msg.d[20];
                    document.getElementById("ContentPlaceHolder1_lbl_ImpTapplisado_Fijo").innerHTML = msg.d[21];
                    document.getElementById("ContentPlaceHolder1_lbl_ImpTapplisado_Vari").innerHTML = msg.d[22];
                    document.getElementById("ContentPlaceHolder1_lbl_ImpTapplisado_Tota").innerHTML = msg.d[23];
                    document.getElementById("ContentPlaceHolder1_lbl_TotalTapa_Fijo").innerHTML = msg.d[24];
                    document.getElementById("ContentPlaceHolder1_lbl_TotalTapa_Vari").innerHTML = msg.d[25];
                    document.getElementById("ContentPlaceHolder1_lbl_TotalTapa_Tota").innerHTML = msg.d[26];
                    document.getElementById("ContentPlaceHolder1_lbl_TotalImpresionInterFijo").innerHTML = msg.d[0];
                    document.getElementById("ContentPlaceHolder1_lbl_TotalImpresionInterVari").innerHTML = msg.d[1];
                    document.getElementById("ContentPlaceHolder1_lbl_TotalImpresionInterTota").innerHTML = msg.d[2];
                    document.getElementById("ContentPlaceHolder1_Label23").innerHTML = msg.d[27];
                    document.getElementById("ContentPlaceHolder1_lbl_TotalManuFijo").innerHTML = msg.d[28];
                    document.getElementById("ContentPlaceHolder1_lbl_TotalManuVari").innerHTML = msg.d[29];
                    document.getElementById("ContentPlaceHolder1_lbl_TotalManuTota").innerHTML = msg.d[30];
                    document.getElementById("ContentPlaceHolder1_Label25").innerHTML = msg.d[31];
                    document.getElementById("ContentPlaceHolder1_TotalFijo").innerHTML = msg.d[32];
                    document.getElementById("ContentPlaceHolder1_TotalVari").innerHTML = msg.d[33];
                    document.getElementById("ContentPlaceHolder1_CostoT").innerHTML = msg.d[34];
                    document.getElementById("ContentPlaceHolder1_CostoU").innerHTML = msg.d[35];
                    document.getElementById("ContentPlaceHolder1_lblCostoMillar").innerHTML = msg.d[36];
                    document.getElementById("ContentPlaceHolder1_Label28").innerHTML = msg.d[34];
                    document.getElementById("ContentPlaceHolder1_Label29").innerHTML = msg.d[35];
                    document.getElementById("ContentPlaceHolder1_Label30").innerHTML = msg.d[36];

                },
                error: function () {
                    alert('¡Ha Ocurrido un Error!');
                }
            });


        }

        $(document).ready(function () {
            $("#ContentPlaceHolder1_txtNombre").change(function () {
                Validador(1);
            });
            $("#ContentPlaceHolder1_ddlFormato").change(function () {
                Validador(2);
            });
            $("#ContentPlaceHolder1_ddlEncuadernacion").change(function () {
                Validador(3);
                var select = document.getElementById("<%= ddlEncuadernacion.ClientID %>");
                var answer = select.options[select.selectedIndex].text;
                document.getElementById("ContentPlaceHolder1_Label31").innerHTML = answer;
                CalcularPreprensa();
            });
            $("#ContentPlaceHolder1_ddlPapel").change(function () {
                Validador(7);

                var Empresa = "Cencosud";
                Papel_gramaje("Interior", Empresa);
                document.getElementById("<%= ddlgramage.ClientID%>").style.borderColor = "#959595";
                CalcularPreprensa();
            });
            $("#ContentPlaceHolder1_ddlgramage").change(function () {
                Validador(8);
                CalcularPreprensa();
            });
            $("#ContentPlaceHolder1_ddlPapTapas").change(function () {
                var select = document.getElementById("<%= ddlPapTapas.ClientID %>");

                if (select.options[select.selectedIndex].value != "Seleccione Tipo Papel...") {

                    var Empresa = "Cencosud";
                    document.getElementById("ContentPlaceHolder1_ddlGraTapas").disabled = false;
                    var select1 = document.getElementById("<%= ddlGraTapas.ClientID %>");
                    if (select1.options[select1.selectedIndex].value != "Seleccione Gramaje Papel...") {
                        Papel_gramaje("Tapa", Empresa);
                    }
                    else {
                        Papel_gramaje("Tapa", Empresa);
                    }
                }
                else {
                    document.getElementById("ContentPlaceHolder1_ddlGraTapas").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlGraTapas").selectedIndex = 0;


                    document.getElementById("ContentPlaceHolder1_lblMaquinaT").innerHTML = "";

                }
                CalcularPreprensa();
            });
            $("#ContentPlaceHolder1_ddlGraTapas").change(function () {
                CalcularPreprensa();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">
        <div id="central" style="text-align: left;">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Presupuesto</h3>
                </div>
                <div class="panel-body">
                    <div class="input-group">
                        <span class="input-group-addon" id="spanNombre">Nombre del Catalogo</span>
                        <input id="txtNombre" type="text" class="form-control" placeholder="Nombre del Catalogo"
                            aria-describedby="basic-addon1" />
                    </div>
                    <br />
                    <div class="input-group">
                        <span class="input-group-addon" id="Span1">Formato (Ancho x Alto)</span>
                        <asp:DropDownList ID="ddlFormato" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                        <%--<select id="ddlFormato" class="selectpicker" placeholder="Formato" aria-describedby="basic-addon1" />--%>
                    </div>
                    <br />
                    <div class="input-group">
                        <span class="input-group-addon" id="Span2">Encuadernación</span>
                        <asp:DropDownList ID="ddlEncuadernacion" CssClass="form-control dropdown-header"
                            runat="server">
                            <asp:ListItem Value="No">No</asp:ListItem>
                            <asp:ListItem Value="Hotmelt">Hotmelt</asp:ListItem>
                            <asp:ListItem Value="Corchete">Corchete</asp:ListItem>
                        </asp:DropDownList>
                        <%--<select id="ddlFormato" class="selectpicker" placeholder="Formato" aria-describedby="basic-addon1" />--%>
                    </div>
                    <table style="width: 100%;" id="presupuestaForm" runat="server">
                        <%--  <tr>
                            <td style="width: 169px;">
                                Nombre Catalogo:
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombre" name="txtNombre" runat="server" Width="417px" MaxLength="200"></asp:TextBox><asp:Label
                                    ID="lblErrorNombre" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>--%>
                        <%-- <tr>
                            <td>
                                Formato (Ancho x Alto):
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFormato" runat="server">
                                </asp:DropDownList>
                                &nbsp;
                                <asp:Label ID="lblDescripcion" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>--%>
                        <%--<tr>
                        <td>
                            Tipo Preprensa:
                        </td>
                        <td>
                            <asp:Label ID="lblTipo" runat="server" Text="Con Preprensa, con Improof"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                        <tr>
                            <td>
                                Encuadernación:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEncuadernacion" runat="server">
                                    <asp:ListItem Value="No">No</asp:ListItem>
                                    <asp:ListItem Value="Hotmelt">Hotmelt</asp:ListItem>
                                    <asp:ListItem Value="Corchete">Corchete</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblErrorEnc" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                Tiraje:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTiraje" runat="server" onkeyup="format(this)" onchange="format(this)"></asp:TextBox>
                                <asp:Label ID="lblErrorTiraje" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <%--<tr>
                        <td>
                            Embalaje:
                        </td>
                        <td>
                            <asp:Label ID="lblEmbalaje" runat="server" Text="Las revistas se entregan en cajas de cartón. No considera el uso de paquetes de polietileno retráctil."></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>--%>
                    </table>
                </div>
            </div>
            <%--<div id="atender" style="width: 100%;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 169px;">
                                <asp:Label ID="Label26" runat="server" Text="Empresa"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEmpresa" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 169px;">
                                <asp:Label ID="Label27" runat="server" Text="Antender a"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAtender" runat="server">
                                    <asp:ListItem>Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>--%>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Interior</h3>
                </div>
                <div class="panel-body">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 168px;">
                                Páginas:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPaginas" runat="server">
                                    <asp:ListItem Value="0">0</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblErrorPagInt" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 168px;">
                                Papel:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPapel" runat="server">
                                    <asp:ListItem Value="">Seleccione Tipo Papel...</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblErrorPapint" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 168px;">
                                Gramaje Papel:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlgramage" runat="server">
                                    <asp:ListItem>Seleccione Gramaje Papel...</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;<asp:Label ID="lblMaquinaI" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <%-- <tr>
                        <td>
                            <div id="lblBarniz" style="visibility: hidden;">
                                Barniz Acuoso:</div>
                        </td>
                        <td>
                            <div id="Barniz" style="visibility: hidden;">
                                <asp:DropDownList ID="ddlIntBarniz" runat="server">
                                    <asp:ListItem>No</asp:ListItem>
                                    <asp:ListItem>Si</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="Label32" runat="server" Text=""></asp:Label>
                            </div>
                        </td>
                    </tr>--%>
                    </table>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Tapa</h3>
                </div>
                <div class="panel-body">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 168px;">
                                Páginas:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPagTapas" runat="server">
                                    <asp:ListItem Value="0" Selected="True">0</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <%--	<asp:ListItem value="6">6</asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 168px;">
                                Papel:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPapTapas" runat="server">
                                    <asp:ListItem Value="">Seleccione Tipo Papel...</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 168px;">
                                Gramaje Papel:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGraTapas" runat="server">
                                    <asp:ListItem>Seleccione Gramaje Papel...</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;<asp:Label ID="lblMaquinaT" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <%--<tr>
                        <td>
                            <div id="lblBarniz1" style="visibility: hidden;">
                                Barniz Acuoso:</div>
                        </td>
                        <td>
                            <div id="Barniz1" style="visibility: hidden;">
                                <asp:DropDownList ID="ddlBarniz1" runat="server">
                                    <asp:ListItem>No</asp:ListItem>
                                    <asp:ListItem>Tiro</asp:ListItem>
                                    <asp:ListItem>Tiro/Retiro</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>--%>
                    </table>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Precios</h3>
                </div>
                <div class="panel-body">
                    <div id="divImprimir">
                        <div align="center">
                            <asp:Label ID="Label24" runat="server" Text=""></asp:Label></div>
                        <table id="data">
                            <tbody>
                                <tr>
                                    <th>
                                        Proceso
                                    </th>
                                    <th>
                                        Costo Fijo
                                    </th>
                                    <th>
                                        Costo Variable
                                    </th>
                                    <th>
                                        Totales
                                    </th>
                                </tr>
                                <%-- <tr>
                                <td class="text-left">
                                    PREPRENSA
                                </td>
                                <td class="text-right">
                                </td>
                                <td class="text-right">
                                </td>
                                <td class="text-right">
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Preprensa Páginas
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lblPreprensa" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    0
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lblTotPrepre" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Preprensa Versiones
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lblVersiones" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    0
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label2" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Improof via Insite
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lblInsite" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    0
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label4" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Improof Impresos - Version Base (Hasta 2 Copias)
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lblBase" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    0
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label6" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Improof Impresos - Versiones (hasta 3 copias)
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Versiones" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    0
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label8" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Pruebas de Color Impresas
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lblPrueba" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    0
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label9" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Gastos de envío
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lblGastos" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    0
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label10" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Archivos para Catalogo Virtual
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lblCatalogo" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    0
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label11" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr><td colspan="4" >________________________________________________________________________________________________________</td></tr>
                            <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Total Preprensa
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lbl_TotalPreprensa" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    0
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label86" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr><td colspan="4">&nbsp;&nbsp;&nbsp;</td></tr>--%>
                                <tr>
                                    <td class="text-left">
                                        IMPRESIÓN
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Impresión Páginas Interiores
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="ImpInterior" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="ImpInterior2" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="ImpInteriorFin" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Barniz Acuoso Interior
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="ImpIntBarnizF" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="ImpIntBarnizV" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="ImpIntBarnizT" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>--%>
                                <%-- <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Doblez Interior
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblImpresionDoblezFijo" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblImpresionDoblezVari" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblImpresionDoblezTota" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Plisado Interior (Papeles de mas de 170 grs.)
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_ImpresionPlisadoFijo" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_ImpresionPlisadoVari" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_ImpresionPlisadoTota" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>--%>
                                <%--  <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Versiones Interior
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lbl_Impresion_VersionFijo" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label35" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lbl_Impresion_VersionTota" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>--%>
                                <tr>
                                    <td colspan="4">
                                        ________________________________________________________________________________________________________
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Total Impresión Interior
                                    </td>
                                    <%--<td class="text-left">Impresión Interior en Prensa</td>--%>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_TotalImpresionInterFijo" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_TotalImpresionInterVari" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_TotalImpresionInterTota" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Impresión de Tapas
                                        <td class="text-right">
                                            <asp:Label ID="ImpTapas" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td class="text-right">
                                            <asp:Label ID="ImpTapas2" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td class="text-right">
                                            <asp:Label ID="ImpTapasFin" runat="server" Text="0"></asp:Label>
                                        </td>
                                </tr>
                                <%--<tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Barniz Acuoso Tapas
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="ImpTapBarnizF" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="ImpTapBarnizV" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="ImpTapBarnizT" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Doblez Tapa
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label37" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label38" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label39" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Plisado Tapa (Papeles de mas de 140 grs.)
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_ImpTapplisado_Fijo" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_ImpTapplisado_Vari" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_ImpTapplisado_Tota" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <%-- <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Versiones Tapa
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lbl_ImpTapVersionesFijo" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label44" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lbl_ImpTapVersionesTota" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>--%>
                                <%--<tr>
                                <td class="text-left">
                                    TERMINACION TAPA
                                </td>
                                <td class="text-right">
                                </td>
                                <td class="text-right">
                                </td>
                                <td class="text-right">
                                </td>
                            </tr>--%>
                                <%--      <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Barniz UV
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="BarnizUVF" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="BarnizUVV" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="BarnizUVT" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Laminado
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="LaminadoF" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="LaminadoV" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="LaminadoT" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;5° Color
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="ColorF" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="ColorV" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="ColorT" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="4">
                                        ________________________________________________________________________________________________________
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Total Impresión Tapa
                                    </td>
                                    <%--<td class="text-left">Impresión Interior en Prensa</td>--%>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_TotalTapa_Fijo" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_TotalTapa_Vari" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_TotalTapa_Tota" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left">
                                        ENCUADERNACION
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label31" runat="server" Text="Encuadernacion"></asp:Label>
                                    </td>
                                    <%--<td class="text-left">Encuadernacion</td>--%>
                                    <td class="text-right">
                                        <asp:Label ID="lblEnc" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblEnc2" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label14" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <%-- <tr>
                                    <td class="text-left">
                                        EMBALAJE
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Despacho
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblDespacho" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <div style="visibility: hidden; width: 100px;">
                                            <asp:Label ID="lblqID" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lblDespacho2" runat="server" Text="0" Visible="true"></asp:Label></div>
                                        <div style="margin-top: -12px;">
                                            <asp:Label ID="Label22" runat="server" Text="0"></asp:Label></div>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label15" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;CMC para Medios
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                        <asp:Label ID="Label1" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                        <asp:Label ID="lblCMC" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label16" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Cajas para Tiendas
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                        <asp:Label ID="Label3" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                        <asp:Label ID="lblCaja" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label17" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Pallets Medios
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                        <asp:Label ID="Label5" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                        <asp:Label ID="lblPalletM" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label18" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Pallets Tiendas
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                        <asp:Label ID="Label7" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                        <asp:Label ID="lblPalletT" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label19" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="4">
                                        ________________________________________________________________________________________________________
                                    </td>
                                </tr>
                                <%-- <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Total Embalaje & Despacho
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                        <asp:Label ID="lbl_TotalEmbalajeFijo" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                        <asp:Label ID="lbl_TotalEmbalajeVari" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_TotalEmbalajeTota" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;COSTO TOTAL MANUFACTURA
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                        <asp:Label ID="lbl_TotalManuFijo" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                        <asp:Label ID="lbl_TotalManuVari" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_TotalManuTota" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label25" runat="server" Text="0 %"></asp:Label>
                                    </td>
                                </tr>
                                <%--  <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label87" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                        <asp:Label ID="Label79" runat="server" Text=" "></asp:Label>
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                        <asp:Label ID="Label80" runat="server" Text=" "></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label81" runat="server" Text=" "></asp:Label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="text-left">
                                        PAPEL
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td class="text-left">Papel Interior</td>--%>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Papel Interior
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblPapelInteriorFijo" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblPapelInteriorVari" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label20" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <%--  <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Papel versiones Interior
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lbl_PapelVersionInt_Fijo" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lbl_PapelVersionInt_Vari" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lbl_PapelVersionInt_Tota" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>--%>
                                <tr>
                                    <td colspan="4">
                                        ________________________________________________________________________________________________________
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Total Costo Papel
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_TotalCostoPapelFijo" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_TotalCostoPapelVari" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lbl_TotalCostoPapelTota" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Consumo (Kg.) Interior
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblKilosConsumo1" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblKilosConsumo1v" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblKilosConsumo1t" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <%-- <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Consumo (Kg.) Versiones Interior
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lbl_ConsumoVersionIntFijo" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lbl_ConsumoVersionIntVari" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="lbl_ConsumoVersionIntTota" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>--%>
                                <%--<tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Total Consumo (Kg.) Papel Interior
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label70" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label71" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label72" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Precio Papel Interior
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblPrecioPapel1" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td class="text-left">Papel Tapa</td>--%>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Papel Tapa
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblPapelTapaFijo" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblPapelTapaVari" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label21" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <%-- <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Papel versiones Tapa
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label64" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label65" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label66" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>--%>
                                <tr>
                                    <td colspan="4">
                                        ________________________________________________________________________________________________________
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Total Costo Papel Tapa
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label58" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label59" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label60" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Consumo (Kg.) Tapa
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblKilosConsumo2" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblKilosConsumo2v" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblKilosConsumo2t" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <%-- <tr>
                                <td class="text-left" style="font-size: 0.80em;">
                                    &nbsp;&nbsp;&nbsp;Consumo (Kg.) versiones Tapa
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label67" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label68" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="text-right">
                                    <asp:Label ID="Label69" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>--%>
                                <%--<tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Total Consumo (Kg.) Papel Tapa
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label73" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label74" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label75" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;Precio Papel Tapa
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblPrecioPapel2" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        ________________________________________________________________________________________________________
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;COSTO TOTAL PAPEL
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label12" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label13" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label82" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                    </td>
                                    <td class="text-right" style="font-size: 0.80em;">
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label23" runat="server" Text="0 %"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label83" runat="server" Text=" "></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="font-size: 0.80em;">
                                        &nbsp;&nbsp;&nbsp;COSTO TOTAL CATALOGO
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                    </td>
                                    <td class="text-right">
                                        <asp:Label ID="Label84" runat="server" Text=" "></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <th class="text-right">
                                        Totales:
                                    </th>
                                    <th class="text-right">
                                        <asp:Label ID="TotalFijo" runat="server" Text="0"></asp:Label>
                                    </th>
                                    <th class="text-right">
                                        <asp:Label ID="TotalVari" runat="server" Text="0"></asp:Label>
                                    </th>
                                    <th class="text-right">
                                        <asp:Label ID="CostoT" runat="server" Text="0"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <th class="text-right">
                                        Costo Unitario
                                    </th>
                                    <th colspan="3" class="text-right">
                                        <asp:Label ID="CostoU" runat="server" Text="0"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <th class="text-right">
                                        Costo Variable x Millar
                                    </th>
                                    <th colspan="3" class="text-right">
                                        <asp:Label ID="lblCostoMillar" runat="server" Text="0"></asp:Label>
                                    </th>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div id="column-right" class="panel panel-success">
           
                <div class="panel-heading success">
                    Nuestra Oferta</div>
                <div class="panel-footer">
                   
                <%--</div>
            </div>--%>
            
            <%--<div style="padding: 16px 10px 10px 10px;">--%>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr style="font-weight: bold; line-height: 18px;">
                        <td style="text-align: left;">
                            Costo total
                        </td>
                        <td style="text-align: right;">
                            $<asp:Label ID="Label28" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr style="line-height: 18px;">
                        <td style="text-align: left;">
                            Costo unitario
                        </td>
                        <td style="text-align: right;">
                            $<asp:Label ID="Label29" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr style="font-weight: bold; line-height: 18px;">
                        <td style="text-align: left;">
                            Millar adicional
                        </td>
                        <td style="text-align: right;">
                            $<asp:Label ID="Label30" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="DIVCostoAnt" style="width: 100%; display: none;">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr style="font-weight: bold; line-height: 18px;">
                            <td style="text-align: left; width: 129px;">
                                Costo Anterior
                            </td>
                            <td style="text-align: right; width: 101px;">
                                $<asp:Label ID="lblCostoAnt" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <br />
                    &nbsp;<br />
                    <input id="btnGuardar" type="button" value="Guardar" onclick="javascript:GrabarPresupuesto();" />
                    <br />
                </div>
                <div class="tooltip" style="font-size: 9px; line-height: 11px; margin-top: 10px;">
                    <span class="tooltiptext">El valor del papel en pesos chilenos será determinado como
                        el producto entre la cotizacion del dolar observado promedio informado por el Banco
                        central de Chile correspondiente al mes anterior al trimestre en que se realiza
                        el ajuste y valor del papel expresado en dolares. </span>
                    <img alt="" src="../../Estructura/Image/sprite-masterpage.png" style="border-radius: 20px;" />
                    <asp:Label ID="lblCostoQ" runat="server" Text=""></asp:Label></div>
            </div>
        
    </div>
</asp:Content>
