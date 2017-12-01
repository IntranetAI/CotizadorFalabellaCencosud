<%@ Page Title="" Language="C#" MasterPageFile="~/Estructura/MasterView/MasterEstructura.Master" AutoEventWireup="true" CodeBehind="Presupuestador.aspx.cs" Inherits="Cotizador_Copesa.ModuloPresupuesto.View.Presupuestador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Estructura/Js/jquery.min.js" type="text/javascript"></script>
    <script src="../../Estructura/Js/jquery-1.11.3.js" type="text/javascript"></script>
    <script src="../../Estructura/Js/jquery-ui-1.11.4.js" type="text/javascript"></script>
    
    <style>
        .input-group
        {
            margin-bottom: 5px;
        }
        option
        {
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
        }
        
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
            width: 76%;
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
    <script type="text/javascript">
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
            var PapelTap = "";
            if (selectPapelTap.options[selectPapelTap.selectedIndex] != null) {
                PapelTap = selectPapelTap.options[selectPapelTap.selectedIndex].value;
            }

            var selectGramajeInterior = document.getElementById("<%= ddlgramaje.ClientID %>");
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
            var Encuadernacion = selectEncuadernacion.options[selectEncuadernacion.selectedIndex].value;
            var Tiraje1 = document.getElementById("<%= txtTiraje1.ClientID%>").value;

            var selectQuintoColor = document.getElementById("<%= ddlQuintocolor.ClientID %>");
            var QuintoColor = selectQuintoColor.options[selectQuintoColor.selectedIndex].text;
            var selectUV = document.getElementById("<%= ddlBarnizUV.ClientID %>");
            var UV = selectUV.options[selectUV.selectedIndex].text;

            var selectLaminado = document.getElementById("<%= ddlLaminado.ClientID %>");
            var Laminado = selectLaminado.options[selectLaminado.selectedIndex].text;
            var selectBarnizacuosoTapa = document.getElementById("<%= ddlBarnizAcuosoTapa.ClientID %>");
            var BarnizacuosoTapa = selectBarnizacuosoTapa.options[selectBarnizacuosoTapa.selectedIndex].text;

            var selectEmbolsado = document.getElementById("<%= ddlEmbolsado.ClientID %>");
            var Embolsado = selectEmbolsado.options[selectEmbolsado.selectedIndex].text;

            var selectAlzadoElementoPlano = document.getElementById("<%= ddlAlzadoElementoPlano.ClientID %>");
            var AlzadoElementoPlano = selectAlzadoElementoPlano.options[selectAlzadoElementoPlano.selectedIndex].text;

            var selectEmbolsadoMailaRev = document.getElementById("<%= ddlEmbolsadoMailaRev.ClientID %>");
            var EmbolsadoMailaRev = selectEmbolsadoMailaRev.options[selectEmbolsadoMailaRev.selectedIndex].text;

            var selectDesembolsadoSimple = document.getElementById("<%= ddlDesembolsadoSimple.ClientID %>");
            var DesembolsadoSimple = selectDesembolsadoSimple.options[selectDesembolsadoSimple.selectedIndex].text;

            var selectAlzado = document.getElementById("<%= ddlAlzado.ClientID %>");
            var Alzado = selectAlzado.options[selectAlzado.selectedIndex].text;

            var selectInsercionManual = document.getElementById("<%= ddlInsercionManual.ClientID %>");
            var InsercionManual = selectInsercionManual.options[selectInsercionManual.selectedIndex].text;

            var selectPegado = document.getElementById("<%= ddlPegado.ClientID %>");
            var Pegado = selectPegado.options[selectPegado.selectedIndex].text;

            var selectFajado = document.getElementById("<%= ddlFajado.ClientID %>");
            var Fajado = selectFajado.options[selectFajado.selectedIndex].text;

            var selectAdhesivo = document.getElementById("<%= ddlAdhesivo.ClientID %>");
            var Adhesivo = selectAdhesivo.options[selectAdhesivo.selectedIndex].text;

            var selectSuministroCajas = document.getElementById("<%= ddlSuministroCajas.ClientID %>");
            var SuministroCajas = selectSuministroCajas.options[selectSuministroCajas.selectedIndex].text;

            var selectInsercionElemCajaSellado = document.getElementById("<%= ddlInsercionElemCajaSellado.ClientID %>");
            var InsercionElemCajaSellado = selectInsercionElemCajaSellado.options[selectInsercionElemCajaSellado.selectedIndex].text;

            var selectEnzunchado = document.getElementById("<%= ddlEnzunchado.ClientID %>");
            var Enzunchado = selectEnzunchado.options[selectEnzunchado.selectedIndex].text;

            var selectPegadoSticker = document.getElementById("<%= ddlPegadoSticker.ClientID %>");
            var PegadoSticker = selectPegadoSticker.options[selectPegadoSticker.selectedIndex].text;

            $.ajax({
                url: "Presupuestador.aspx/PrePrensa",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'PagInterior':'" + eval(PaginasInterior) + "','Pagtapa':'" + eval(PaginasTapa) + "','EntradasxFormato':'" + NroEntradasxPliego +
                    "','Formato':'" + Formato + "','GramajeInt1':'" + GramajeInterior + "','GramajeTapa1':'" + GramajeTapas + "','Papelinterior':'" + PapelInt +
                    "','PapelTapa':'" + PapelTap + "','Encuadernacion':'" + Encuadernacion + "','Tiraje':'" + Tiraje1 +
                    "','QuintoColor':'" + QuintoColor + "','UV':'" + UV + "','Laminado':'" + Laminado + "', 'BarnizAcuosoTapa':'" + BarnizacuosoTapa +
                    "', 'ValorUF':'0','Embolsado':'" + Embolsado + "','AlzadoElementoPlano':'" + AlzadoElementoPlano + "','EmbolsadoMailaRev':'" + EmbolsadoMailaRev +
                    "','DesembolsadoSimple':'" + DesembolsadoSimple + "','Alzado':'" + Alzado + "','InsercionManual':'" + InsercionManual + "','Pegado':'" + Pegado +
                    "','Fajado':'" + Fajado + "','Adhesivo':'" + Adhesivo + "','SuministroCajas':'" + SuministroCajas + "','InsercionElemCajaSellado':'" + InsercionElemCajaSellado +
                    "','Enzunchado':'" + Enzunchado + "','PegadoSticker':'" + PegadoSticker + "'}",
                success: function (msg) {
                    if (document.getElementById("ContentPlaceHolder1_LabelPrimerPrecio") == null) {
                        document.getElementById("ContentPlaceHolder1_Label28").innerHTML = msg.d[1];

                        document.getElementById("ContentPlaceHolder1_Label29").innerHTML = msg.d[2];
                    }
                    else {
                        document.getElementById("ContentPlaceHolder1_LabelPrimerPrecio").innerHTML = msg.d[0];
                        document.getElementById("ContentPlaceHolder1_Label28").innerHTML = msg.d[1];

                        document.getElementById("ContentPlaceHolder1_Label29").innerHTML = msg.d[2];
                    }
                },
                error: function () {
                    alert('¡Ha Ocurrido un Error!');
                }
            });


        }
    </script>
    <script type="text/javascript">
        function GrabarPresupuesto() {
            var NombrePres = document.getElementById("ContentPlaceHolder1_txtNombre").value;
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
            var PapelTap = "";
            if (selectPapelTap.options[selectPapelTap.selectedIndex] != null) {
                PapelTap = selectPapelTap.options[selectPapelTap.selectedIndex].value;
            }
            var selectGramajeInterior = document.getElementById("<%= ddlgramaje.ClientID %>");
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
            var Encuadernacion = selectEncuadernacion.options[selectEncuadernacion.selectedIndex].value;
            var Tiraje1 = document.getElementById("<%= txtTiraje1.ClientID%>").value;

            var selectQuintoColor = document.getElementById("<%= ddlQuintocolor.ClientID %>");
            var QuintoColor = selectQuintoColor.options[selectQuintoColor.selectedIndex].text;
            var selectUV = document.getElementById("<%= ddlBarnizUV.ClientID %>");
            var UV = selectUV.options[selectUV.selectedIndex].text;

            var selectLaminado = document.getElementById("<%= ddlLaminado.ClientID %>");
            var Laminado = selectLaminado.options[selectLaminado.selectedIndex].text;
            var selectBarnizacuosoTapa = document.getElementById("<%= ddlBarnizAcuosoTapa.ClientID %>");
            var BarnizacuosoTapa = selectBarnizacuosoTapa.options[selectBarnizacuosoTapa.selectedIndex].text;

            var selectEmbolsado = document.getElementById("<%= ddlEmbolsado.ClientID %>");
            var Embolsado = selectEmbolsado.options[selectEmbolsado.selectedIndex].text;
            var Empresa = document.getElementById("Image1").alt;

            $.ajax({
                url: "Presupuestador.aspx/GuardarPresupuesto",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'PagInterior':'" + eval(PaginasInterior) + "','Pagtapa':'" + eval(PaginasTapa) + "','EntradasxFormato':'" + NroEntradasxPliego +
                    "','Formato':'" + Formato + "','GramajeInt1':'" + GramajeInterior + "','GramajeTapa1':'" + GramajeTapas + "','Papelinterior':'" + PapelInt +
                    "','PapelTapa':'" + PapelTap + "','Encuadernacion':'" + Encuadernacion + "','Tiraje':'" + Tiraje1 +
                    "','QuintoColor':'" + QuintoColor + "','UV':'" + UV + "','Laminado':'" + Laminado + "', 'BarnizAcuosoTapa':'" + BarnizacuosoTapa +
                    "','NombrePres':'" + NombrePres + "','usuario':'" + Usuario + "','ValorUF':'0','Embolsado':'" + Embolsado + "'}",
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
                        alert(msg.d[0]);
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
                Validador();
            }

            else {
                input.value = input.value.replace(/[^\d\.]*/g, '');
            }
        }

        function pulsarTiraje(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 13 || tecla > 31 && (tecla < 48 || tecla > 57)) return false;
        }
        function CantidadPaginasInteriorEnc() {
            var ddlTerritory = document.getElementById("<%= ddlPaginas.ClientID %>");
            var lengthddlTerritory = ddlTerritory.length - 1;
            for (var i = lengthddlTerritory; i >= 0; i--) {
                ddlTerritory.options[i] = null;
            }

            var select = document.getElementById("<%= ddlEncuadernacion.ClientID %>");
            var seleccionado = select.options[select.selectedIndex].text;

            $.ajax({
                url: "Presupuestador.aspx/CantidadPaginasInterior",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'Encuadernacion':'" + seleccionado + "'}",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    $.each(jsdata, function (key, value) {
                        $('#<%=ddlPaginas.ClientID%>').append($("<option></option>").val(value).html(value));

                    });
                    LimitesPapelEncuadernacion(seleccionado, document.getElementById("Image1").alt);
                },
                error: function () {
                    alert('¡Ha Ocurrido un Error!');
                }
            });
        }

        function LimitesPapelEncuadernacion(Encuadernacion, Empresa) {
            var ddlTerritory = document.getElementById("<%= ddlPapel.ClientID %>");
            var lengthddlTerritory = ddlTerritory.length - 1;
            for (var i = lengthddlTerritory; i >= 0; i--) {
                ddlTerritory.options[i] = null;
            }

            $.ajax({
                url: "Presupuestador.aspx/LimitesPapelEncuadernacion",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'Encuadernacion':'" + Encuadernacion + "','Empresa':'" + Empresa + "'}",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    $.each(jsdata, function (key, value) {
                        $('#<%=ddlPapel.ClientID%>').append($("<option></option>").val(value).html(value));

                    });
                },
                error: function () {
                    alert('¡Ha Ocurrido un Error!');
                }
            });
        }
        function GramajeTapaDifInterior(gramajeint) {
            if (gramajeint != "Seleccione Gramaje Papel...") {
                var Empresa = document.getElementById("Image1").alt;
                var selectEnc = document.getElementById("<%= ddlEncuadernacion.ClientID %>");
                var Encuadernacion = selectEnc.options[selectEnc.selectedIndex].text;
                var ddlTerritory = document.getElementById("<%= ddlPapTapas.ClientID %>");
                var lengthddlTerritory = ddlTerritory.length - 1;
                for (var i = lengthddlTerritory; i >= 0; i--) {
                    ddlTerritory.options[i] = null;
                }
                $.ajax({
                    url: "Presupuestador.aspx/GramajeMinTapa_Papel",
                    type: "post",
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    data: "{'Empresa':'" + Empresa + "','Encuadernacion':'" + Encuadernacion + "','Gramaje':'" + gramajeint + "'}",
                    success: function (data) {
                        var jsdata = JSON.parse(data.d);
                        $.each(jsdata, function (key, value) {

                            $('#<%=ddlPapTapas.ClientID%>').append($("<option></option>").val(value).html(value));


                        });

                    },
                    error: function () {
                        alert('¡Ha Ocurrido un Error!');
                    }
                });
            }
        }

        function Papel_GramajeTapa(Empresa, LimiteGramaje) {
            var selectEnc = document.getElementById("<%= ddlEncuadernacion.ClientID %>");
            var Encuadernacion = selectEnc.options[selectEnc.selectedIndex].text;

            var select = document.getElementById("<%= ddlPapTapas.ClientID %>");
            var answer = "Seleccione Tipo Papel...";
            if (select.options[select.selectedIndex] != null) {
                answer = select.options[select.selectedIndex].text;
            }

            var ddlTerritory = document.getElementById("<%= ddlGraTapas.ClientID %>");
            var lengthddlTerritory = ddlTerritory.length - 1;
            for (var i = lengthddlTerritory; i >= 0; i--) {
                ddlTerritory.options[i] = null;
            }

            $.ajax({
                url: "Presupuestador.aspx/Gramage_Papel",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'Papel':'" + answer + "','Empresa':'" + Empresa + "','Encuadernacion':'" + Encuadernacion + "','Componente':'Tapa'}",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    $.each(jsdata, function (key, value) {
                        if (value.GramajeInterior == "Seleccione Gramaje Papel...") {
                            $('#<%=ddlGraTapas.ClientID%>').append($("<option></option>").val(value.GramajeInterior).html(value.GramajeInterior));
                        }
                        else if (eval(value.GramajeInterior) >= LimiteGramaje) {
                            $('#<%=ddlGraTapas.ClientID%>').append($("<option></option>").val(value.GramajeInterior).html(value.GramajeInterior));
                        }

                    });

                },
                error: function () {
                    alert('¡Ha Ocurrido un Error!');
                }
            });
        }
        function Papel_gramaje(Empresa) {
            var select = "";
            var answer = "";
            var selectEnc = document.getElementById("<%= ddlEncuadernacion.ClientID %>");
            var Encuadernacion = selectEnc.options[selectEnc.selectedIndex].text;

            select = document.getElementById("<%= ddlPapel.ClientID %>");
            answer = select.options[select.selectedIndex].text;
            var ddlTerritory = document.getElementById("<%= ddlgramaje.ClientID %>");
            var lengthddlTerritory = ddlTerritory.length - 1;
            for (var i = lengthddlTerritory; i >= 0; i--) {
                ddlTerritory.options[i] = null;
            }

            $.ajax({
                url: "Presupuestador.aspx/Gramage_Papel",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'Papel':'" + answer + "','Empresa':'" + Empresa + "','Encuadernacion':'" + Encuadernacion + "','Componente':'Interior'}",
                success: function (data) {
                    var jsdata = JSON.parse(data.d);
                    $.each(jsdata, function (key, value) {
                        $('#<%=ddlgramaje.ClientID%>').append($("<option></option>").val(value.GramajeInterior).html(value.GramajeInterior));


                    });

                },
                error: function () {
                    alert('¡Ha Ocurrido un Error!');
                }
            });
        }

        function Validador() {
            var NombrePPTO = document.getElementById("ContentPlaceHolder1_txtNombre").value;
            var selectNroEntradasxPliego = document.getElementById("<%= ddlFormato.ClientID %>");
            var Formato = selectNroEntradasxPliego.options[selectNroEntradasxPliego.selectedIndex].text;
            var Tiraje1 = document.getElementById("<%= txtTiraje1.ClientID%>").value;

            var selectPaginasInterior = document.getElementById("<%= ddlPaginas.ClientID %>");
            var PaginasInterior = (selectPaginasInterior.options[selectPaginasInterior.selectedIndex] != null) ? PaginasInterior = selectPaginasInterior.options[selectPaginasInterior.selectedIndex].text : "";
            
            var selectPapelInt = document.getElementById("<%= ddlPapel.ClientID %>");
            var PapelInt = selectPapelInt.options[selectPapelInt.selectedIndex].value;
            var selectGramajeInterior = document.getElementById("<%= ddlgramaje.ClientID %>");
            var GramajeInterior = 0;
            if (selectGramajeInterior.options[selectGramajeInterior.selectedIndex] != null) {
                GramajeInterior = selectGramajeInterior.options[selectGramajeInterior.selectedIndex].value;
            }


            var selectPaginasTapa = document.getElementById("<%= ddlPagTapas.ClientID %>");
            var PaginasTapa = selectPaginasTapa.options[selectPaginasTapa.selectedIndex].value;
            var selectPapelTap = document.getElementById("<%= ddlPapTapas.ClientID %>");
            var PapelTap = "";
            if (selectPapelTap.options[selectPapelTap.selectedIndex] != null) {
                PapelTap = selectPapelTap.options[selectPapelTap.selectedIndex].value;
            }
            var selectGramajeTapas = document.getElementById("<%= ddlGraTapas.ClientID %>");
            var GramajeTapas = 0;
            if (selectGramajeTapas.options[selectGramajeTapas.selectedIndex] != null) {
                GramajeTapas = selectGramajeTapas.options[selectGramajeTapas.selectedIndex].value;
            }

            if ((NombrePPTO != "") && (NombrePPTO.length > 4) && (Tiraje1 != "") && (Formato != "0") && (PaginasInterior != 0) && (PapelInt != "Seleccione Tipo Papel...")
                    && (GramajeInterior != "0") && (GramajeInterior != "Seleccione Gramaje Papel...")) {

                if (PaginasTapa == "0") {

                    CalcularPreprensa();
                    document.getElementById("btnGuardar").disabled = false;
                    document.getElementById("divAlertaTotal").style.display = "none";

                }
                else if ((PaginasTapa != "0") && (PapelTap != "") && (GramajeTapas != 0) && (GramajeTapas != "Seleccione Gramaje Papel...")) {

                    if (eval(GramajeTapas) >= 170) {
                        var selectQuintoColor = document.getElementById("<%= ddlQuintocolor.ClientID %>");
                        var QuintoColor = selectQuintoColor.options[selectQuintoColor.selectedIndex].text;
                        var selectUV = document.getElementById("<%= ddlBarnizUV.ClientID %>");
                        var UV = selectUV.options[selectUV.selectedIndex].text;
                        var selectLaminado = document.getElementById("<%= ddlLaminado.ClientID %>");
                        var Laminado = selectLaminado.options[selectLaminado.selectedIndex].text;
                        var selectBarnizacuosoTapa = document.getElementById("<%= ddlBarnizAcuosoTapa.ClientID %>");
                        var BarnizacuosoTapa = selectBarnizacuosoTapa.options[selectBarnizacuosoTapa.selectedIndex].text;

                        if (((BarnizacuosoTapa != "No") && (Laminado != "Seleccione Laminado...") || (UV != "Seleccione Barniz UV...")) || ((QuintoColor != 0) && ((BarnizacuosoTapa != "No") || (Laminado != "Seleccione Laminado...") || (UV != "Seleccione Barniz UV...")))) {

                            CalcularPreprensa();
                            document.getElementById("btnGuardar").disabled = false;
                            document.getElementById("divAlertaTotal").style.display = "none";
                        }
                        else {
                            document.getElementById("divAlertaTotal").innerHTML = "<strong>* Se debe seleccionar una Terminación</strong>";
                            document.getElementById("btnGuardar").disabled = true;
                            document.getElementById("divAlertaTotal").style.display = "block";
                        }
                    }
                    else {

                        CalcularPreprensa();
                        document.getElementById("btnGuardar").disabled = false;
                        document.getElementById("divAlertaTotal").style.display = "none";
                    }
                }
                else {

                    document.getElementById("btnGuardar").disabled = true;
                    if (PapelTap == "") {

                        document.getElementById("divAlertaTotal").innerHTML = "<strong>* Se debe seleccionar un Tipo de Papel</strong>";
                        document.getElementById("divAlertaTotal").style.display = "block";
                    }
                    else if ((GramajeTapas == 0) || (GramajeTapas != "Seleccione Gramaje Papel...")) {

                        document.getElementById("divAlertaTotal").innerHTML = "<strong>* Se debe seleccionar un Gramaje Tapa</strong>";
                        document.getElementById("divAlertaTotal").style.display = "block";
                    }

                }
            }
            else {
                document.getElementById("btnGuardar").disabled = true;
            }
        }
        </script>
    <script>
        $(document).ready(function () {
            $("#ContentPlaceHolder1_txtNombre").change(function () {
                if (this.value != "") {
                    Validador();
                }
            });
            $("#ContentPlaceHolder1_ddlFormato").change(function () {
                if (this.value != "0") {
                    Validador();
                }
            });
            $("#ContentPlaceHolder1_ddlEncuadernacion").change(function () {
                CantidadPaginasInteriorEnc();
                Validador();
            });
            $("#ContentPlaceHolder1_txtTiraje1").change(function () {
                Validador();
            });
            $("#ContentPlaceHolder1_ddlPaginas").change(function () {
                if (this.value != "0") {
                    document.getElementById("ContentPlaceHolder1_ddlPapel").disabled = false;
                }
                else {
                    document.getElementById("ContentPlaceHolder1_ddlPapel").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlgramaje").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlgramaje").selectedIndex = 0;
                }
                document.getElementById("ContentPlaceHolder1_ddlPapel").selectedIndex = 0;
                Validador();
            });
            $("#ContentPlaceHolder1_ddlPapel").change(function () {
                if (this.value != "Seleccione Tipo Papel...") {
                    Papel_gramaje(document.getElementById("Image1").alt);
                    document.getElementById("ContentPlaceHolder1_ddlgramaje").disabled = false;
                }
                else {
                    document.getElementById("ContentPlaceHolder1_ddlgramaje").disabled = true;
                }
                document.getElementById("ContentPlaceHolder1_ddlgramaje").selectedIndex = 0;
                Validador();
            });
            $("#ContentPlaceHolder1_ddlgramaje").change(function () {
                //Validador(8);
                if (this.value != "Seleccione Gramaje Papel...") {
                    var Empresa = document.getElementById("Image1").alt;
                    GramajeTapaDifInterior(this.value);
                    Papel_GramajeTapa(Empresa, this.value);

                }
                Validador();
            });
            
            $("#ContentPlaceHolder1_ddlPagTapas").change(function () {
                if (this.value != "0") {
                    document.getElementById("ContentPlaceHolder1_ddlPapTapas").disabled = false;
                    var selectGramajeInterior = document.getElementById("<%= ddlgramaje.ClientID %>");
                    var GramajeInterior = 0;
                    if (selectGramajeInterior.options[selectGramajeInterior.selectedIndex] != null) {
                        GramajeInterior = selectGramajeInterior.options[selectGramajeInterior.selectedIndex].value;
                    }
                    GramajeTapaDifInterior(GramajeInterior);
                    Validador();
                }
                else {
                    document.getElementById("ContentPlaceHolder1_ddlPapTapas").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlGraTapas").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlGraTapas").selectedIndex = 0;
                    document.getElementById("ContentPlaceHolder1_ddlBarnizUV").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlBarnizUV").selectedIndex = 0;
                    document.getElementById("ContentPlaceHolder1_ddlLaminado").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlLaminado").selectedIndex = 0;
                }
                document.getElementById("ContentPlaceHolder1_ddlPapTapas").selectedIndex = 0;
                Validador();
            });
            $("#ContentPlaceHolder1_ddlPapTapas").change(function () {

                var select = document.getElementById("<%= ddlPapTapas.ClientID %>");

                if (select.options[select.selectedIndex].value != "Seleccione Tipo Papel...") {

                    var Empresa = document.getElementById("Image1").alt
                    document.getElementById("ContentPlaceHolder1_ddlGraTapas").disabled = false;

                    var selectGramajeInterior = document.getElementById("<%= ddlgramaje.ClientID %>");
                    var GramajeInterior = 0;
                    if (selectGramajeInterior.options[selectGramajeInterior.selectedIndex] != null) {
                        GramajeInterior = selectGramajeInterior.options[selectGramajeInterior.selectedIndex].value;
                    }

                    Papel_GramajeTapa(Empresa, GramajeInterior);


                }
                else {
                    document.getElementById("ContentPlaceHolder1_ddlGraTapas").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlGraTapas").selectedIndex = 0;
                    document.getElementById("ContentPlaceHolder1_ddlBarnizUV").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlBarnizUV").selectedIndex = 0;
                    document.getElementById("ContentPlaceHolder1_ddlLaminado").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlLaminado").selectedIndex = 0;

                    document.getElementById("ContentPlaceHolder1_lblMaquinaT").innerHTML = "";

                }
                Validador();
            });
            $("#ContentPlaceHolder1_ddlGraTapas").change(function () {
                if (this.value != "Seleccione Gramaje Papel...") {
                    if (eval(this.value) >= 170) {
                        document.getElementById("ContentPlaceHolder1_ddlBarnizUV").disabled = false;
                        document.getElementById("ContentPlaceHolder1_ddlBarnizUV").selectedIndex = 0;
                        document.getElementById("ContentPlaceHolder1_ddlLaminado").disabled = false;
                        document.getElementById("ContentPlaceHolder1_ddlLaminado").selectedIndex = 0;
                    }

                    else {
                        document.getElementById("ContentPlaceHolder1_ddlBarnizUV").disabled = true;
                        document.getElementById("ContentPlaceHolder1_ddlBarnizUV").selectedIndex = 0;
                        document.getElementById("ContentPlaceHolder1_ddlLaminado").disabled = true;
                        document.getElementById("ContentPlaceHolder1_ddlLaminado").selectedIndex = 0;
                    }

                    if (eval(this.value) >= 100) {
                        document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").disabled = false;
                        document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").selectedIndex = 0;
                    }
                    else {
                        document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").disabled = true;
                        document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").selectedIndex = 0;
                    }
                    document.getElementById("ContentPlaceHolder1_ddlQuintocolor").disabled = false;
                    document.getElementById("ContentPlaceHolder1_ddlQuintocolor").selectedIndex = 0;
                }
                else {
                    document.getElementById("ContentPlaceHolder1_ddlBarnizUV").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlBarnizUV").selectedIndex = 0;
                    document.getElementById("ContentPlaceHolder1_ddlLaminado").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlLaminado").selectedIndex = 0;
                    document.getElementById("ContentPlaceHolder1_ddlQuintocolor").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlQuintocolor").selectedIndex = 0;
                }
                Validador();
            });
            $("#ContentPlaceHolder1_ddlQuintocolor").change(function () {
                if (this.text != "Seleccionar") {
                    document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").value = "Tiro";
                }
                else {
                    document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").disabled = false;
                    document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").selectedIndex = 0;
                }
                Validador();
            });
            $("#ContentPlaceHolder1_ddlBarnizAcuosoTapa").change(function () {
                if (this.value != "No") {
                    document.getElementById("ContentPlaceHolder1_ddlLaminado").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlLaminado").selectedIndex = 0;
                }
                else {
                    var selectLaminado = document.getElementById("<%= ddlGraTapas.ClientID %>");
                    var GramajeTapa = selectLaminado.options[selectLaminado.selectedIndex].text;
                    if (eval(GramajeTapa) >= 170) {
                        document.getElementById("ContentPlaceHolder1_ddlLaminado").disabled = false;
                        document.getElementById("ContentPlaceHolder1_ddlLaminado").selectedIndex = 0;
                    }
                    else {
                        document.getElementById("ContentPlaceHolder1_ddlLaminado").disabled = true;
                        document.getElementById("ContentPlaceHolder1_ddlLaminado").selectedIndex = 0;
                    }
                }
                Validador();
            });
            $("#ContentPlaceHolder1_ddlBarnizUV").change(function () {

                if (this.value != "Barniz UV Parejo Brillante") {
                    document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").disabled = false;
                    document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").selectedIndex = 0;
                    document.getElementById("ContentPlaceHolder1_ddlLaminado").disabled = false;
                    document.getElementById("ContentPlaceHolder1_ddlLaminado").selectedIndex = 0;
                }
                else {
                    document.getElementById("ContentPlaceHolder1_ddlLaminado").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlLaminado").selectedIndex = 0;
                    document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").selectedIndex = 0;
                }
                Validador();
            });
            $("#ContentPlaceHolder1_ddlLaminado").change(function () {
                if (this.value != "0") {
                    document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").selectedIndex = 0;
                }
                else {
                    document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").disabled = false;
                    document.getElementById("ContentPlaceHolder1_ddlBarnizAcuosoTapa").selectedIndex = 0;
                }
                Validador();
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
                        <span class="input-group-addon" id="spanNombre">Nombre del Catalogo:</span>
                        <asp:TextBox ID="txtNombre" name="txtNombre" runat="server" class="form-control"
                            placeholder="Nombre del Catalogo" aria-describedby="basic-addon1" data-error="El Nombre del Catalogo no es válido" required/>
                            <div class="help-block with-errors"></div>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span1">Formato (Ancho x Alto):</span>
                        <asp:DropDownList ID="ddlFormato" runat="server" class="form-control" aria-describedby="basic-addon1"
                            data-size="5">
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span2">Encuadernación:</span>
                        <asp:DropDownList ID="ddlEncuadernacion" runat="server" class="form-control" aria-describedby="basic-addon1">
                            <asp:ListItem Value="No">No</asp:ListItem>
                            <asp:ListItem Value="Corchete">2 Corchete al lomo</asp:ListItem>
                            <asp:ListItem Value="Hotmelt">Entapado Hotmelt</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span3">Tiraje:</span>
                        <asp:TextBox ID="txtTiraje1" name="txtNombre" runat="server" class="form-control"
                            placeholder="Tiraje" aria-describedby="basic-addon1" onkeyup="format(this)"
                            onchange="format(this)" />
                    </div>
                    
                </div>
               
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Interior</h3>
                </div>
                <div class="panel-body">
                    <div class="input-group">
                        <span class="input-group-addon" id="span7">Páginas:</span>
                        <asp:DropDownList ID="ddlPaginas" runat="server" class="form-control" aria-describedby="basic-addon1"
                            data-size="5">
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span8">Papel:</span>
                        <asp:DropDownList ID="ddlPapel" runat="server" class="form-control" aria-describedby="basic-addon1" disabled>
                            <asp:ListItem Value="">Seleccione Tipo Papel...</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span9">Gramaje Papel:</span>
                        <asp:DropDownList ID="ddlgramaje" runat="server" class="form-control" aria-describedby="basic-addon1" disabled>
                            <asp:ListItem Value="">Seleccione Gramaje Papel...</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Tapa</h3>
                </div>
                <div class="panel-body">
                    <div class="input-group">
                        <span class="input-group-addon" id="span10">Páginas:</span>
                        <asp:DropDownList ID="ddlPagTapas" runat="server" class="form-control" aria-describedby="basic-addon1"
                            data-size="5">
                            <asp:ListItem Value="0" Selected="True">0</asp:ListItem>
                            <asp:ListItem Value="4">4 Paginas - Díptica</asp:ListItem>
                            <asp:ListItem Value="6">6 Paginas - Tríptica</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span11">Papel:</span>
                        <asp:DropDownList ID="ddlPapTapas" runat="server" class="form-control" aria-describedby="basic-addon1" disabled>
                            <asp:ListItem Value="">Seleccione Tipo Papel...</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span12">Gramaje Papel:</span>
                        <asp:DropDownList ID="ddlGraTapas" runat="server" class="form-control" aria-describedby="basic-addon1" disabled>
                            <asp:ListItem Value="">Seleccione Gramaje Papel...</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span15">Quinto Color:</span>
                        <asp:DropDownList ID="ddlQuintocolor" runat="server" class="form-control" aria-describedby="basic-addon1" disabled>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Terminaciones</h3>
                </div>
                <div class="panel-body">
                    <div class="input-group">
                        <span class="input-group-addon" id="span14">Barniz Acuoso:</span>
                        <asp:DropDownList ID="ddlBarnizAcuosoTapa" runat="server" class="form-control" aria-describedby="basic-addon1" disabled>
                            <asp:ListItem Value="No">No</asp:ListItem>
                            <asp:ListItem Value="Tiro">Tiro</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span16">Barniz UV:</span>
                        <asp:DropDownList ID="ddlBarnizUV" runat="server" class="form-control" aria-describedby="basic-addon1" disabled>
                            <asp:ListItem Value="">Seleccione Barniz UV...</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span18">Laminado:</span>
                        <asp:DropDownList ID="ddlLaminado" runat="server" class="form-control" aria-describedby="basic-addon1" disabled>
                            <asp:ListItem Value="">Seleccione Laminado...</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span6">Embolsado Mecánico:</span>
                        <asp:DropDownList ID="ddlEmbolsado" runat="server" class="form-control" aria-describedby="basic-addon1">
                            <asp:ListItem Value="">Seleccione Laminado...</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Manualidades</h3>
                </div>
                <div class="panel-body">
                    <div class="input-group">
                        <span class="input-group-addon" id="span28">Alzados de elementos planos :</span>
                        <asp:DropDownList ID="ddlAlzadoElementoPlano" runat="server" class="form-control" aria-describedby="basic-addon1">
                            <asp:ListItem Value="">Seleccione</asp:ListItem>
                            <asp:ListItem Value="Si">Si</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span27">Embolsado manual de Revista :</span>
                        <asp:DropDownList ID="ddlEmbolsadoMailaRev" runat="server" class="form-control" aria-describedby="basic-addon1">
                            <asp:ListItem Value="">Seleccione</asp:ListItem>
                            <asp:ListItem Value="Si">Si</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span26">Desembolsado simple:</span>
                        <asp:DropDownList ID="ddlDesembolsadoSimple" runat="server" class="form-control" aria-describedby="basic-addon1">
                            <asp:ListItem Value="">Seleccione</asp:ListItem>
                            <asp:ListItem Value="Si">Si</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span25">Alzado :</span>
                        <asp:DropDownList ID="ddlAlzado" runat="server" class="form-control" aria-describedby="basic-addon1">
                        </asp:DropDownList>
                    </div>
                    
                    <div class="input-group">
                        <span class="input-group-addon" id="span22">Inserción manual :</span>
                        <asp:DropDownList ID="ddlInsercionManual" runat="server" class="form-control" aria-describedby="basic-addon1">
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span21">Pegado :</span>
                        <asp:DropDownList ID="ddlPegado" runat="server" class="form-control" aria-describedby="basic-addon1">
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span20">Fajado :</span>
                        <asp:DropDownList ID="ddlFajado" runat="server" class="form-control" aria-describedby="basic-addon1">
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span19">Adhesivo para sachet :</span>
                        <asp:DropDownList ID="ddlAdhesivo" runat="server" class="form-control" aria-describedby="basic-addon1">
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span4">Suministro de cajas :</span>
                        <asp:DropDownList ID="ddlSuministroCajas" runat="server" class="form-control" aria-describedby="basic-addon1">
                            <asp:ListItem Value="">Seleccione</asp:ListItem>
                            <asp:ListItem Value="Si">Si</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span5">Inserción de elementos en caja y sellado de caja :</span>
                        <asp:DropDownList ID="ddlInsercionElemCajaSellado" runat="server" class="form-control" aria-describedby="basic-addon1">
                            <asp:ListItem Value="">Seleccione</asp:ListItem>
                            <asp:ListItem Value="Si">Si</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span13">Enzunchado x paquete manualidad :</span>
                        <asp:DropDownList ID="ddlEnzunchado" runat="server" class="form-control" aria-describedby="basic-addon1">
                            <asp:ListItem Value="">Seleccione</asp:ListItem>
                            <asp:ListItem Value="Si">Si</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="span17">Pegado de Sticker:</span>
                        <asp:DropDownList ID="ddlPegadoSticker" runat="server" class="form-control" aria-describedby="basic-addon1">
                            <asp:ListItem Value="">Seleccione</asp:ListItem>
                            <asp:ListItem Value="Si">Si</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
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
                        <li class="active"><a href="#1" data-toggle="tab">Tiraje</a> </li>
                    </ul>
                    <div class="tab-content ">
                        <div class="tab-pane active" id="1">
                            <asp:Label ID="LabelPrimerPrecio" runat="server" Text=""></asp:Label>
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
                        Tiraje</h3>
                    <ul>
                        <li>Costo Total<b> $
                            <asp:Label ID="Label28" runat="server" Text="0"></asp:Label></b></li>
                        <li>Costo Unitario<b> $
                            <asp:Label ID="Label29" runat="server" Text="0"></asp:Label></b></li>
                        
                    </ul>
                </div>
            </div>
            <div>UF Hoy: <asp:Label ID="lblCostoQ" runat="server" Text=""></asp:Label></div>
            <div id="divAlertaTotal" style="display:none;" class="alert alert-warning" role="alert">Si seleccionó <strong> Gramaje 170 o mayor</strong> en Tapa debe elegir una <strong>terminación</strong></div>
            <div>
                <input id="btnGuardar" type="button" value="Guardar" onclick="javascript:GrabarPresupuesto();" class="form-control btn-info" disabled/>
                <br />
            </div>
            
        </div>
    </div>
</asp:Content>

        