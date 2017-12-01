<%@ Page Title="" Language="C#" MasterPageFile="~/Estructura/MasterView/MasterEstructura.Master"
    AutoEventWireup="true" CodeBehind="Presupuestador.aspx.cs" Inherits="Cotizador_Cencosud.ModuloPresupuesto.View.Presupuestador" %>

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
            font-weight: bold;
        }
    </style>
    <style>
        /*Estilo de Tabla Totales de Todo los Tirajes*/
        
        #pricing-table .plan
        {
            font: 12px 'Lucida Sans' , 'trebuchet MS' , Arial, Helvetica;
            text-shadow: 0 1px rgba(255,255,255,.8);
            background: #fff;
            border: 1px solid #ddd;
            color: #333;
            padding: 15px;
            width: 100%; /* plan width = 180 + 20 + 20 + 1 + 1 = 222px */
            float: left;
            position: relative;
            margin-bottom: 20px;
        }
        
        #pricing-table .plan:nth-child(1)
        {
            border-radius: 5px 0 0 5px;
        }
        
        #pricing-table .plan:nth-child(4)
        {
            border-radius: 0 5px 5px 0;
        }
        
        /* --------------- */
        
        #pricing-table h3
        {
            font-size: 20px;
            font-weight: normal;
            padding: 15px;
            margin: -15px -15px 0px -15px;
            background-color: #eee;
            background-image: linear-gradient(#fff, #eee);
        }
        
        
        #pricing-table .plan:nth-child(1) h3
        {
            border-radius: 5px 0 0 0;
        }
        
        #pricing-table .plan:nth-child(4) h3
        {
            border-radius: 0 5px 0 0;
        }
        
        #pricing-table h3 span
        {
            display: block;
            font: bold 25px/100px Georgia, Serif;
            color: #777;
            background: #fff;
            border: 5px solid #fff;
            height: 100px;
            width: 100px;
            margin: 10px auto -65px;
            border-radius: 100px;
            box-shadow: 0 5px 20px #ddd inset, 0 3px 0 #999 inset;
        }
        
        /* --------------- */
        
        #pricing-table ul
        {
            margin: 0 0 0 0;
            padding: 0;
            list-style: none;
        }
        
        #pricing-table li
        {
            border-top: 1px solid #ddd;
            padding: 10px 0;
        }
        
        /* --------------- */
        
        #pricing-table .signup
        {
            position: relative;
            padding: 8px 20px;
            margin: 20px 0 0 0;
            color: #fff;
            font: bold 14px Arial, Helvetica;
            text-transform: uppercase;
            text-decoration: none;
            display: inline-block;
            background-color: #72ce3f;
            background-image: linear-gradient(#72ce3f, #62bc30);
            border-radius: 3px;
            text-shadow: 0 1px 0 rgba(0,0,0,.3);
            box-shadow: 0 1px 0 rgba(255, 255, 255, .5), 0 2px 0 rgba(0, 0, 0, .7);
        }
        
        #pricing-table .signup:hover
        {
            background-color: #62bc30;
            background-image: linear-gradient(#62bc30, #72ce3f);
        }
        
        #pricing-table .signup:active, #pricing-table .signup:focus
        {
            background: #62bc30;
            top: 2px;
            box-shadow: 0 0 3px rgba(0, 0, 0, .7) inset;
        }
        
        /* --------------- */
        
        .clear:before, .clear:after
        {
            content: "";
            display: table;
        }
        
        .clear:after
        {
            clear: both;
        }
        
        .clear
        {
            zoom: 1;
        }
    </style>
    <script>
        function GrabarPresupuesto() {
            var NombrePres = document.getElementById("ContentPlaceHolder1_txtNombre").value;
            var Empresa = document.getElementById("Image1").alt;
            var Usuario = document.getElementById("lblUsuario").innerHTML;

            var selectPaginasInterior = document.getElementById("<%= ddlPaginas.ClientID %>");
            var PaginasInterior = selectPaginasInterior.options[selectPaginasInterior.selectedIndex].text;
            var selectPaginasTapa = document.getElementById("<%= ddlPagTapas.ClientID %>");
            var PaginasTapa = selectPaginasTapa.options[selectPaginasTapa.selectedIndex].value;
            var selectNroEntradasxPliego = document.getElementById("<%= ddlFormato.ClientID %>");
            var NroEntradasxPliego = selectNroEntradasxPliego.options[selectNroEntradasxPliego.selectedIndex].value;
            var Formato = selectNroEntradasxPliego.options[selectNroEntradasxPliego.selectedIndex].text;
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
            var Tiraje1 = document.getElementById("<%= txtTiraje1.ClientID%>").value;
            var Tiraje2 = document.getElementById("<%= txtTiraje2.ClientID%>").value;
            var Tiraje3 = document.getElementById("<%= txtTiraje3.ClientID%>").value;

            $.ajax({
                url: "Presupuestador.aspx/GuardarPresupuesto",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'PagInterior':'" + eval(PaginasInterior) + "','Pagtapa':'" + eval(PaginasTapa) + "','EntradasxFormato':'" + NroEntradasxPliego +
                    "','Formato':'" + Formato + "','GramajeInt1':'" + GramajeInterior + "','GramajeTapa1':'" + GramajeTapas + "','Papelinterior':'" + PapelInt +
                    "','PapelTapa':'" + PapelTap + "','Encuadernacion':'" + Encuadernacion + "','Tiraje1':'" + Tiraje1 + "','Tiraje2':'" + Tiraje2 + "','Tiraje3':'" + Tiraje3 +
                    "','Empresa':'" + Empresa + "','NombrePres':'" + NombrePres + "','Usuario':'" + Usuario + "'}",
                success: function (msg) {
                    if (msg.d[0] == 'OK') {
                        if (!window.open('Oferta_Comercial.aspx?id=' + msg.d[1])) {
                            alert('Por favor deshabilita el bloqueador de ventanas emergentes ');
                        }
                        else {
                            window.location.reload();
                        }

                    }
                    else {
                        alert(msg);
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
                            $('#<%=ddlgramage.ClientID%>').append($("<option></option>").val(value.GramajeInterior).html(value.GramajeInterior));
                            document.getElementById("ContentPlaceHolder1_lblMaquinaI").innerHTML = "";

                        }
                        else if ("Tapa") {
                            $('#<%=ddlGraTapas.ClientID%>').append($("<option></option>").val(value.GramajeInterior).html(value.GramajeInterior));
                            document.getElementById("ContentPlaceHolder1_lblMaquinaT").innerHTML = "";

                        }
                    });

                },
                error: function () {
                    alert('¡Ha Ocurrido un Error!');
                }
            });
        }

        function Validador(TipValid) {
            if (TipValid == 1) {
                var caracter = document.getElementById("<%= txtNombre.ClientID%>").value;
                if (caracter == "") {
                    document.getElementById("ContentPlaceHolder1_lblErrorNombre").innerHTML = "* Debe ingresar un Nombre";
                    document.getElementById('ContentPlaceHolder1_lblErrorNombre').style.color = "rgb(255, 0, 0)";
                    document.getElementById("<%= txtNombre.ClientID%>").style.borderColor = "rgb(255, 0, 0)";
                }
                else if (caracter.length < 5) {
                    document.getElementById("ContentPlaceHolder1_lblErrorNombre").innerHTML = "* Nombre muy corto";
                    document.getElementById('ContentPlaceHolder1_lblErrorNombre').style.color = "rgb(255, 0, 0)";
                    document.getElementById("<%= txtNombre.ClientID%>").style.borderColor = "rgb(255, 0, 0)";
                }
                else {
                    document.getElementById("ContentPlaceHolder1_lblErrorNombre").innerHTML = "";
                    document.getElementById("<%= txtNombre.ClientID%>").style.borderColor = "#959595";
                }
            }
            else if (TipValid == 2) {
                var select1 = document.getElementById("<%= ddlFormato.ClientID %>");
                var Formato = select1.options[select1.selectedIndex].text;
                if (Formato == "Seleccionar") {
                    document.getElementById("ContentPlaceHolder1_lblDescripcion").innerHTML = "* Debe seleccionar un Formato";
                    document.getElementById('ContentPlaceHolder1_lblDescripcion').style.color = "rgb(255, 0, 0)";
                    document.getElementById("<%= ddlFormato.ClientID%>").style.borderColor = "rgb(255, 0, 0)";
                }
                else {
                    document.getElementById("ContentPlaceHolder1_lblDescripcion").innerHTML = "";
                    document.getElementById("ContentPlaceHolder1_lblDescripcion").style.color = "black";
                    document.getElementById("<%= ddlFormato.ClientID%>").style.borderColor = "#959595";
                }
                Validador(1);
            }
            else if (TipValid == 3) {
                var select2 = document.getElementById("<%= ddlEncuadernacion.ClientID %>");
                var Encuadernacion = select2.options[select2.selectedIndex].text;
                if (Encuadernacion == "Seleccione Encuadernación...") {
                    document.getElementById("ContentPlaceHolder1_lblErrorEnc").innerHTML = "* Debe seleccionar un Tipo Encuadernación";
                    document.getElementById('ContentPlaceHolder1_lblErrorEnc').style.color = "rgb(255, 0, 0)";
                    document.getElementById("<%= ddlEncuadernacion.ClientID%>").style.borderColor = "rgb(255, 0, 0)";
                }
                else {
                    document.getElementById("ContentPlaceHolder1_lblErrorEnc").innerHTML = "";
                    document.getElementById("<%= ddlEncuadernacion.ClientID%>").style.borderColor = "#959595";
                }
                Validador(2);
            }
            else if (TipValid == 4) {
                var Tiraje = document.getElementById("<%= txtTiraje1.ClientID%>").value;
                if (Tiraje == "") {
                    document.getElementById("ContentPlaceHolder1_lblErrorTiraje1").innerHTML = "* Debe ingresar un Tiraje";
                    document.getElementById('ContentPlaceHolder1_lblErrorTiraje1').style.color = "rgb(255, 0, 0)";
                    document.getElementById("<%= txtTiraje1.ClientID%>").style.borderColor = "rgb(255, 0, 0)";
                }
                else {
                    document.getElementById("ContentPlaceHolder1_lblErrorTiraje1").innerHTML = "";
                    document.getElementById("<%= txtTiraje1.ClientID%>").style.borderColor = "#959595";
                }
                Validador(3);
            }

            else if (TipValid == 6) {
                var select1 = document.getElementById("<%= ddlPaginas.ClientID %>");
                var CantPaginas = select1.options[select1.selectedIndex].text;
                if (CantPaginas == "0") {
                    document.getElementById("ContentPlaceHolder1_lblErrorPagInt").innerHTML = "* Debe seleccionar cantidad de paginas";
                    document.getElementById('ContentPlaceHolder1_lblErrorPagInt').style.color = "rgb(255, 0, 0)";
                    document.getElementById("<%= ddlPaginas.ClientID%>").style.borderColor = "rgb(255, 0, 0)";
                }
                else {
                    document.getElementById("ContentPlaceHolder1_lblErrorPagInt").innerHTML = "";
                    document.getElementById("<%= ddlPaginas.ClientID%>").style.borderColor = "#959595";
                }
                Validador(4);
            }
            if (TipValid == 7) {
                var select1 = document.getElementById("<%= ddlPapel.ClientID %>");
                var TipPapel = select1.options[select1.selectedIndex].text;
                if (TipPapel == "Seleccione Tipo Papel...") {
                    document.getElementById("ContentPlaceHolder1_lblErrorPapint").innerHTML = "* Debe seleccionar un tipo de papel";
                    document.getElementById('ContentPlaceHolder1_lblErrorPapint').style.color = "rgb(255, 0, 0)";
                    document.getElementById("<%= ddlPapel.ClientID%>").style.borderColor = "rgb(255, 0, 0)";
                }
                else {
                    document.getElementById("ContentPlaceHolder1_lblErrorPapint").innerHTML = "";
                    document.getElementById("<%= ddlPapel.ClientID%>").style.borderColor = "#959595";
                }
                Validador(6);
            }
            else if (TipValid == 8) {
                var select1 = document.getElementById("<%= ddlgramage.ClientID %>");
                var grPaginas = select1.options[select1.selectedIndex].text;
                if (grPaginas == "Seleccione Gramaje Papel...") {
                    document.getElementById("ContentPlaceHolder1_lblMaquinaI").innerHTML = "* Debe seleccionar gramaje de las paginas";
                    document.getElementById('ContentPlaceHolder1_lblMaquinaI').style.color = "rgb(255, 0, 0)";
                    document.getElementById("<%= ddlgramage.ClientID%>").style.borderColor = "rgb(255, 0, 0)";
                }
                else {
                    document.getElementById("ContentPlaceHolder1_lblMaquinaI").innerHTML = "";
                    document.getElementById("ContentPlaceHolder1_lblMaquinaI").style.color = "black";
                    document.getElementById("<%= ddlgramage.ClientID%>").style.borderColor = "#959595";
                }
                Validador(7);
            }
        }

        function CalcularPreprensa() {
            var selectPaginasInterior = document.getElementById("<%= ddlPaginas.ClientID %>");
            var PaginasInterior = selectPaginasInterior.options[selectPaginasInterior.selectedIndex].text;
            var selectPaginasTapa = document.getElementById("<%= ddlPagTapas.ClientID %>");
            var PaginasTapa = selectPaginasTapa.options[selectPaginasTapa.selectedIndex].value;
            var selectNroEntradasxPliego = document.getElementById("<%= ddlFormato.ClientID %>");
            var NroEntradasxPliego = selectNroEntradasxPliego.options[selectNroEntradasxPliego.selectedIndex].value;
            var Formato = selectNroEntradasxPliego.options[selectNroEntradasxPliego.selectedIndex].text;
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
            var Tiraje1 = document.getElementById("<%= txtTiraje1.ClientID%>").value;
            var Tiraje2 = document.getElementById("<%= txtTiraje2.ClientID%>").value;
            var Tiraje3 = document.getElementById("<%= txtTiraje3.ClientID%>").value;

            $.ajax({
                url: "Presupuestador.aspx/PrePrensa",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'PagInterior':'" + eval(PaginasInterior) + "','Pagtapa':'" + eval(PaginasTapa) + "','EntradasxFormato':'" + NroEntradasxPliego +
                    "','Formato':'" + Formato + "','GramajeInt1':'" + GramajeInterior + "','GramajeTapa1':'" + GramajeTapas + "','Papelinterior':'" + PapelInt +
                    "','PapelTapa':'" + PapelTap + "','Encuadernacion':'" + Encuadernacion + "','Tiraje1':'" + Tiraje1 + "','Tiraje2':'" + Tiraje2 + "','Tiraje3':'" + Tiraje3
                //                     +  + "','Empresa':'" + Empresa + "'
                   + "'}",
                success: function (msg) {
                    if (document.getElementById("ContentPlaceHolder1_LabelPrimerPrecio") == null) {
                        document.getElementById("ContentPlaceHolder1_Label28").innerHTML = msg.d[1];
                        document.getElementById("ContentPlaceHolder1_Label29").innerHTML = msg.d[2];
                        document.getElementById("ContentPlaceHolder1_Label30").innerHTML = msg.d[3];
                        document.getElementById("ContentPlaceHolder1_TotalOpcion2CT").innerHTML = msg.d[5];
                        document.getElementById("ContentPlaceHolder1_TotalOpcion2CU").innerHTML = msg.d[6];
                        document.getElementById("ContentPlaceHolder1_TotalOpcion2MA").innerHTML = msg.d[7];
                        document.getElementById("ContentPlaceHolder1_TotalOpcion3CT").innerHTML = msg.d[9];
                        document.getElementById("ContentPlaceHolder1_TotalOpcion3CU").innerHTML = msg.d[10];
                        document.getElementById("ContentPlaceHolder1_TotalOpcion3MA").innerHTML = msg.d[11];
                    }
                    else {
                        document.getElementById("ContentPlaceHolder1_LabelPrimerPrecio").innerHTML = msg.d[0];
                        document.getElementById("ContentPlaceHolder1_Label28").innerHTML = msg.d[1];
                        document.getElementById("ContentPlaceHolder1_Label29").innerHTML = msg.d[2];
                        document.getElementById("ContentPlaceHolder1_Label30").innerHTML = msg.d[3];
                        document.getElementById("ContentPlaceHolder1_LabelSegundoPrecio").innerHTML = msg.d[4];
                        document.getElementById("ContentPlaceHolder1_TotalOpcion2CT").innerHTML = msg.d[5];
                        document.getElementById("ContentPlaceHolder1_TotalOpcion2CU").innerHTML = msg.d[6];
                        document.getElementById("ContentPlaceHolder1_TotalOpcion2MA").innerHTML = msg.d[7];
                        document.getElementById("ContentPlaceHolder1_LabelTercerPrecio").innerHTML = msg.d[8];
                        document.getElementById("ContentPlaceHolder1_TotalOpcion3CT").innerHTML = msg.d[9];
                        document.getElementById("ContentPlaceHolder1_TotalOpcion3CU").innerHTML = msg.d[10];
                        document.getElementById("ContentPlaceHolder1_TotalOpcion3MA").innerHTML = msg.d[11];
                    }
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
                    <table style="width: 100%;" id="presupuestaForm" runat="server">
                        <tr>
                            <td style="width: 169px;">
                                Nombre Catalogo:
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombre" name="txtNombre" runat="server" Width="417px" MaxLength="200"></asp:TextBox><asp:Label
                                    ID="lblErrorNombre" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
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
                        </tr>
                        <tr>
                            <td>
                                Tiraje Opción 1:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTiraje1" runat="server" onkeyup="format(this)" onchange="format(this)"></asp:TextBox>
                                <asp:Label ID="lblErrorTiraje1" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tiraje Opción 2:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTiraje2" runat="server" onkeyup="format(this)" onchange="format(this)"></asp:TextBox>
                                <asp:Label ID="lblErrorTiraje2" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tiraje Opción 3:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTiraje3" runat="server" onkeyup="format(this)" onchange="format(this)"></asp:TextBox>
                                <asp:Label ID="lblErrorTiraje3" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha de Circulación:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaCirculacion" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                      
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
            <div id="DIVPrecio" runat="server" class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Precios</h3>
                </div>
                <div class="panel-body">
                    <asp:Label ID="Label24" runat="server" Text=""></asp:Label>
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#1" data-toggle="tab">Tiraje Opción 1</a> </li>
                        <li><a href="#2" data-toggle="tab">Tiraje Opción 2</a> </li>
                        <li><a href="#3" data-toggle="tab">Tiraje Opción 3</a> </li>
                    </ul>
                    <div class="tab-content ">
                        <div class="tab-pane active" id="1">
                            <asp:Label ID="LabelPrimerPrecio" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="tab-pane" id="2">
                            <asp:Label ID="LabelSegundoPrecio" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="tab-pane" id="3">
                            <asp:Label ID="LabelTercerPrecio" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="column-right" class="panel panel-success">
        <div class="panel-heading success">
            Nuestra Oferta</div>
        <div class="panel-footer">
            <div id="pricing-table" class="clear">
                <div class="plan">
                    <h3>
                        Tiraje Opción 1</h3>
                    <ul>
                        <li>Costo Total<b> $
                            <asp:Label ID="Label28" runat="server" Text="0"></asp:Label></b></li>
                        <li style="display:none;">Costo Unitario<b> $
                            <asp:Label ID="Label29" runat="server" Text="0"></asp:Label></b></li>
                        <li style="display:none;">>Millar Adicional<b> $
                            <asp:Label ID="Label30" runat="server" Text="0"></asp:Label></b></li>
                    </ul>
                </div>
                <div class="plan">
                    <h3>
                        Tiraje Opción 2</h3>
                    <ul>
                        <li>Costo Total<b> $
                            <asp:Label ID="TotalOpcion2CT" runat="server" Text="0"></asp:Label></b></li>
                        <li style="display:none;">>Costo Unitario<b> $
                            <asp:Label ID="TotalOpcion2CU" runat="server" Text="0"></asp:Label></b></li>
                        <li style="display:none;">>Millar Adicional<b> $
                            <asp:Label ID="TotalOpcion2MA" runat="server" Text="0"></asp:Label></b></li>
                    </ul>
                </div>
                <div class="plan">
                    <h3>
                        Tiraje Opción 3</h3>
                    <ul>
                        <li>Costo Total<b> $
                            <asp:Label ID="TotalOpcion3CT" runat="server" Text="0"></asp:Label></b></li>
                        <li style="display:none;">>Costo Unitario<b> $
                            <asp:Label ID="TotalOpcion3CU" runat="server" Text="0"></asp:Label></b></li>
                        <li style="display:none;">>Millar Adicional<b> $
                            <asp:Label ID="TotalOpcion3MA" runat="server" Text="0"></asp:Label></b></li>
                    </ul>
                </div>
            </div>
            <div>
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
