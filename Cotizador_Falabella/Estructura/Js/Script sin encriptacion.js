document.oncontextmenu = function(){return false}
    function addPoints(input) {
        var str = new String(input.value);
        var amount = str.split('.').join('');
        amount = amount.split("").reverse();

        var output = "";
        for (var i = 0; i <= amount.length - 1; i++) {
            output = amount[i] + output;
            if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = '.' + output;
        }
        input.value = output;
    }

    function pulsarTiraje(e) {
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 13 || tecla > 31 && (tecla < 48 || tecla > 57)) return false;
    }
    function maximo(campo, limite) {
        if (campo.value.length >= limite) {
            campo.value = 100;
        }
    }
    function pulsarNombre(e) {
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 13) return false;
    }
    function CalculadorPredeterminado() {
        var select = document.getElementById("<%= ddlPaginas.ClientID %>");
        var answer = select.options[select.selectedIndex].value;
        var select = document.getElementById("<%= ddlPagTapas.ClientID %>");
        var answer2 = select.options[select.selectedIndex].value;
        CalcularPreprensa(answer, answer2);
    }
    function GrabarPresupuesto() {
        var Nombre = document.getElementById("<%= txtNombre.ClientID%>").value;
        var valor = document.getElementById("ContentPlaceHolder1_lblDescripcion").innerHTML;
        var Maquina = document.getElementById("ContentPlaceHolder1_lblMaquinaI").innerHTML;
        var Maquina2 = document.getElementById("ContentPlaceHolder1_lblMaquinaT").innerHTML;
        var Tiraje = document.getElementById("<%= txtTiraje.ClientID%>").value;
        var select = document.getElementById("<%= ddlEncuadernacion.ClientID %>");
        var Encuadernacion = select.options[select.selectedIndex].text;
        var select2 = document.getElementById("<%= ddlgramage.ClientID %>");
        var GramajeInt = select2.options[select2.selectedIndex].text;
        var select3 = document.getElementById("<%= ddlIntBarniz.ClientID %>");
        var BarnizInt = select3.options[select3.selectedIndex].text;
        var select4 = document.getElementById("<%= ddlGraTapas.ClientID %>");
        var GramajeTap = select4.options[select4.selectedIndex].text;
        var select5 = document.getElementById("<%= ddlBarniz1.ClientID %>");
        var BarnizTap = select5.options[select5.selectedIndex].text;
        var select6 = document.getElementById("<%= ddlFormato.ClientID %>");
        var Formato = select6.options[select6.selectedIndex].text;
        var select7 = document.getElementById("<%= ddlPapel.ClientID %>");
        var PapelInt = select7.options[select7.selectedIndex].text;
        var select8 = document.getElementById("<%= ddlPapTapas.ClientID %>");
        var PapelTap = select8.options[select8.selectedIndex].text;
        var select9 = document.getElementById("<%= ddlPaginas.ClientID %>");
        var answer = select9.options[select9.selectedIndex].value;
        var select0 = document.getElementById("<%= ddlPagTapas.ClientID %>");
        var answer2 = select0.options[select0.selectedIndex].value;
        var Tipo = document.getElementById("ContentPlaceHolder1_lblTipo").innerHTML;
        var Embalaje = document.getElementById("ContentPlaceHolder1_lblEmbalaje").innerHTML;
        //Guardar
        var TotalFijo = document.getElementById("ContentPlaceHolder1_TotalFijo").innerHTML;
        var TotalVari = document.getElementById("ContentPlaceHolder1_TotalVari").innerHTML;
        var CostoTotal = document.getElementById("ContentPlaceHolder1_CostoT").innerHTML;
        var CostoUnit = document.getElementById("ContentPlaceHolder1_CostoU").innerHTML;
        var CostoMill = document.getElementById("ContentPlaceHolder1_lblCostoMillar").innerHTML;
        var loc = document.location.href;
        var valorQ = document.getElementById("ContentPlaceHolder1_lblqID").innerHTML;


        $.ajax({
            url: "Presupuestador.aspx/AgregarPre",
            type: "post",
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            data: "{'PagInterior':'" + eval(answer) + "','Pagtapa':'" + eval(answer2) + "','Desc':'" + valor + "','MaquinaTap':'" + Maquina2 + "','MaquinaInt':'" + Maquina +
                    "','Tiraje':'" + Tiraje + "','Encuadernacion':'" + Encuadernacion + "','GramajeInt':'" + GramajeInt + "','BarnizInt':'" + BarnizInt +
                    "','GramajeTap':'" + GramajeTap + "','BarnizTap':'" + BarnizTap + "','Formato':'" + Formato + "','PapelInt':'" + PapelInt + "','PapelTap':'" + PapelTap +
                    "','Nombre':'" + Nombre + "','Tipo':'" + Tipo + "','Embalaje':'" + Embalaje + "','TotalFijo':'" + TotalFijo + "','TotalVari':'" + TotalVari +
                    "','CostoTotal':'" + CostoTotal + "','CostoUnit':'" + CostoUnit + "','CostoMill':'" + CostoMill + "','loc':'" + loc + "','Usuario':'<%=Session["Usuario"] %>','Empresa':'<%=Session["Empresa"] %>','valorQ':'"+valorQ+"'}",
            success: function (msg) {
                if(msg.d == "Error"){
                    alert("Error al Ingresar Presupuesto");
                }
                else{
                    if (confirm("Presupuesto guardado correctamente. ¿ Desea imprimir el presupuesto ?") == true) {
                        window.open('PresupuestoPDF.aspx?id='+msg.d);
                        setTimeout(function(){window.location = "Historico_PPTO.aspx";},15000);
                        
                    } else {
                        window.location = "Historico_PPTO.aspx";
                    }
                }
            },
            error: function () {
                alert('no funca');
            }
        });
    }
    function CargaTerminacion(estado) {
        if (estado == "0") {
            var ddlTerritory = document.getElementById("<%= ddlTerBarniz.ClientID %>");
            var lengthddlTerritory = ddlTerritory.length - 1;
            for (var i = lengthddlTerritory; i >= 0; i--) {
                ddlTerritory.options[i] = null;
            }
        } else if (estado == "1") {
            var ddlTerritory = document.getElementById("<%= ddlTerLam.ClientID %>");
            var lengthddlTerritory = ddlTerritory.length - 1;
            for (var i = lengthddlTerritory; i >= 0; i--) {
                ddlTerritory.options[i] = null;
            }
        } else if (estado == "2") {
            var ddlTerritory = document.getElementById("<%= ddlTercinco.ClientID %>");
            var lengthddlTerritory = ddlTerritory.length - 1;
            for (var i = lengthddlTerritory; i >= 0; i--) {
                ddlTerritory.options[i] = null;
            }
        }

        var Ter = document.getElementById("ContentPlaceHolder1_lblDescripcion").innerHTML;
        $.ajax({
            url: "Presupuestador.aspx/Carga_Terminacion",
            type: "post",
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            data: "{'Doblez':'" + Ter + "','estado':'" + estado + "'}",
            success: function (data) {
                var jsdata = JSON.parse(data.d);
                $.each(jsdata, function (key, value) {
                    if (estado == "0") {
                        $('#<%=ddlTerBarniz.ClientID%>').append($("<option></option>").val(value.Formato).html(value.Nombre));
                    } else if (estado == "1") {
                        $('#<%=ddlTerLam.ClientID%>').append($("<option></option>").val(value.Formato).html(value.Nombre));
                    } else if (estado == "2") {
                        $('#<%=ddlTercinco.ClientID%>').append($("<option></option>").val(value.Formato).html(value.Nombre));
                    }
                });

            },
            error: function () {
                alert('no funca');
            }
        });
    }        
    function CalcularPreprensa(valor1, valor2) {
        var Nombre = document.getElementById("<%= txtNombre.ClientID%>").value;
        var valor = document.getElementById("ContentPlaceHolder1_lblDescripcion").innerHTML;
        var Maquina = document.getElementById("ContentPlaceHolder1_lblMaquinaI").innerHTML;
        var Maquina2 = document.getElementById("ContentPlaceHolder1_lblMaquinaT").innerHTML;
        var Tiraje = document.getElementById("<%= txtTiraje.ClientID%>").value;
        var select = document.getElementById("<%= ddlEncuadernacion.ClientID %>");
        var Encuadernacion = select.options[select.selectedIndex].text;
        var select2 = document.getElementById("<%= ddlgramage.ClientID %>");
        var GramajeInt = select2.options[select2.selectedIndex].text;
        var select3 = document.getElementById("<%= ddlIntBarniz.ClientID %>");
        var BarnizInt = select3.options[select3.selectedIndex].text;
        var select4 = document.getElementById("<%= ddlGraTapas.ClientID %>");
        var GramajeTap = select4.options[select4.selectedIndex].text;
        var select5 = document.getElementById("<%= ddlBarniz1.ClientID %>");
        var BarnizTap = select5.options[select5.selectedIndex].text;
        var select6 = document.getElementById("<%= ddlFormato.ClientID %>");
        var Formato = select6.options[select6.selectedIndex].text;
        var select7 = document.getElementById("<%= ddlPapel.ClientID %>");
        var PapelInt = select7.options[select7.selectedIndex].text;
        var select8 = document.getElementById("<%= ddlPapTapas.ClientID %>");
        var PapelTap = select8.options[select8.selectedIndex].text;
        var Medios = document.getElementById("<%= txtMedios.ClientID %>").value;
        var select13 = document.getElementById("<%= ddlVersiones.ClientID %>");
        var Versiones = select13.options[select13.selectedIndex].text;
        

        var select12 = document.getElementById("<%= ddlTerBarniz.ClientID %>");
        var TermBarniz = select12.options[select12.selectedIndex].value;
        var select10 = document.getElementById("<%= ddlTerLam.ClientID %>");
        var TermLamina = select10.options[select10.selectedIndex].value;
        var select11 = document.getElementById("<%= ddlTercinco.ClientID %>");
        var TermColor = select11.options[select11.selectedIndex].value;
        var Empresa = '<%=Session["Empresa"] %>';

        $.ajax({
            url: "Presupuestador.aspx/PrePrensa",
            type: "post",
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            data: "{'PagInterior':'" + eval(valor1) + "','Pagtapa':'" + eval(valor2) + "','Desc':'" + valor + "','MaquinaTap':'" + Maquina2 + "','MaquinaInt':'" + Maquina +
                    "','Tiraje':'" + Tiraje + "','Encuadernacion':'" + Encuadernacion + "','Empresa':'" + Empresa + "','GramajeInt':'" + GramajeInt + "','BarnizInt':'" + BarnizInt +
                    "','GramajeTap':'" + GramajeTap + "','BarnizTap':'" + BarnizTap + "','Formato':'" + Formato + "','PapelInt':'" + PapelInt + "','PapelTap':'" + PapelTap +
                    "','Medios':'" + Medios + "','TermBarniz':'" + TermBarniz + "','TermLamina':'" + TermLamina + "','TermColor':'" + TermColor + "','Versiones':'" + Versiones + "'}",
            success: function (msg) {
                document.getElementById("ContentPlaceHolder1_lblPreprensa").innerHTML = msg.d[0];
                document.getElementById("ContentPlaceHolder1_lblTotPrepre").innerHTML = msg.d[0];
                document.getElementById("ContentPlaceHolder1_lblVersiones").innerHTML = msg.d[1];
                document.getElementById("ContentPlaceHolder1_Label2").innerHTML = msg.d[1];
                document.getElementById("ContentPlaceHolder1_lblInsite").innerHTML = msg.d[2];
                document.getElementById("ContentPlaceHolder1_Label4").innerHTML = msg.d[2];
                document.getElementById("ContentPlaceHolder1_lblBase").innerHTML = msg.d[3];
                document.getElementById("ContentPlaceHolder1_Label6").innerHTML = msg.d[3];
                document.getElementById("ContentPlaceHolder1_Versiones").innerHTML = msg.d[4];
                document.getElementById("ContentPlaceHolder1_Label7").innerHTML = msg.d[4];
                document.getElementById("ContentPlaceHolder1_lblPrueba").innerHTML = msg.d[5];
                document.getElementById("ContentPlaceHolder1_Label9").innerHTML = msg.d[5];
                document.getElementById("ContentPlaceHolder1_lblGastos").innerHTML = msg.d[6];
                document.getElementById("ContentPlaceHolder1_Label10").innerHTML = msg.d[6];
                document.getElementById("ContentPlaceHolder1_lblCatalogo").innerHTML = msg.d[7];
                document.getElementById("ContentPlaceHolder1_Label11").innerHTML = msg.d[7];
                document.getElementById("ContentPlaceHolder1_ImpInterior").innerHTML = msg.d[8];
                document.getElementById("ContentPlaceHolder1_ImpInterior2").innerHTML = msg.d[9];
                document.getElementById("ContentPlaceHolder1_ImpInteriorFin").innerHTML = msg.d[27];
                document.getElementById("ContentPlaceHolder1_lblEnc").innerHTML = msg.d[10];
                document.getElementById("ContentPlaceHolder1_lblEnc2").innerHTML = msg.d[11];
                document.getElementById("ContentPlaceHolder1_Label14").innerHTML = msg.d[24];
                document.getElementById("ContentPlaceHolder1_lblDespacho").innerHTML = msg.d[12];
                document.getElementById("ContentPlaceHolder1_Label15").innerHTML = msg.d[12];
                document.getElementById("ContentPlaceHolder1_lblCMC").innerHTML = msg.d[13];
                document.getElementById("ContentPlaceHolder1_Label16").innerHTML = msg.d[13];
                document.getElementById("ContentPlaceHolder1_lblCaja").innerHTML = msg.d[14];
                document.getElementById("ContentPlaceHolder1_Label17").innerHTML = msg.d[14];
                document.getElementById("ContentPlaceHolder1_lblPalletM").innerHTML = msg.d[15];
                document.getElementById("ContentPlaceHolder1_Label18").innerHTML = msg.d[15];
                document.getElementById("ContentPlaceHolder1_lblPalletT").innerHTML = msg.d[16];
                document.getElementById("ContentPlaceHolder1_Label19").innerHTML = msg.d[16];
                document.getElementById("ContentPlaceHolder1_lblDespacho2").innerHTML = msg.d[17];
                document.getElementById("ContentPlaceHolder1_ImpTapas").innerHTML = msg.d[18];
                document.getElementById("ContentPlaceHolder1_ImpTapas2").innerHTML = msg.d[19];
                document.getElementById("ContentPlaceHolder1_Label21").innerHTML = msg.d[28];
                document.getElementById("ContentPlaceHolder1_lblPapelInteriorFijo").innerHTML = msg.d[20];
                document.getElementById("ContentPlaceHolder1_lblPapelInteriorVari").innerHTML = msg.d[21];
                document.getElementById("ContentPlaceHolder1_lblPapelTapaFijo").innerHTML = msg.d[22];
                document.getElementById("ContentPlaceHolder1_lblPapelTapaVari").innerHTML = msg.d[23];
                document.getElementById("ContentPlaceHolder1_Label20").innerHTML = msg.d[25];
                document.getElementById("ContentPlaceHolder1_Label21").innerHTML = msg.d[26];
                document.getElementById("ContentPlaceHolder1_ImpTapasFin").innerHTML = msg.d[28];
                document.getElementById("ContentPlaceHolder1_ImpIntBarnizF").innerHTML = msg.d[29];
                document.getElementById("ContentPlaceHolder1_ImpIntBarnizV").innerHTML = msg.d[31];
                document.getElementById("ContentPlaceHolder1_ImpIntBarnizT").innerHTML = msg.d[36];
                document.getElementById("ContentPlaceHolder1_ImpTapBarnizF").innerHTML = msg.d[30];
                document.getElementById("ContentPlaceHolder1_ImpTapBarnizV").innerHTML = msg.d[32];
                document.getElementById("ContentPlaceHolder1_ImpTapBarnizT").innerHTML = msg.d[37];
                document.getElementById("ContentPlaceHolder1_BarnizUVF").innerHTML = msg.d[33];
                document.getElementById("ContentPlaceHolder1_BarnizUVV").innerHTML = msg.d[38];
                document.getElementById("ContentPlaceHolder1_BarnizUVT").innerHTML = msg.d[41];
                document.getElementById("ContentPlaceHolder1_LaminadoF").innerHTML = msg.d[34];
                document.getElementById("ContentPlaceHolder1_LaminadoV").innerHTML = msg.d[39];
                document.getElementById("ContentPlaceHolder1_LaminadoT").innerHTML = msg.d[42];
                document.getElementById("ContentPlaceHolder1_ColorF").innerHTML = msg.d[35];
                document.getElementById("ContentPlaceHolder1_ColorV").innerHTML = msg.d[40];
                document.getElementById("ContentPlaceHolder1_ColorT").innerHTML = msg.d[43]; //numero max

                Totales();

            },
            error: function () {
                alert('no funca');
            }
        });
    }
    function Totales() {
        var preprensa = document.getElementById("ContentPlaceHolder1_lblPreprensa").innerHTML;
        var versiones = document.getElementById("ContentPlaceHolder1_lblVersiones").innerHTML;
        var insite = document.getElementById("ContentPlaceHolder1_lblInsite").innerHTML;
        var base1 = document.getElementById("ContentPlaceHolder1_lblBase").innerHTML;
        var Iversion = document.getElementById("ContentPlaceHolder1_Versiones").innerHTML;
        var Color = document.getElementById("ContentPlaceHolder1_lblPrueba").innerHTML;
        var Gastos = document.getElementById("ContentPlaceHolder1_lblGastos").innerHTML;
        var Catalog = document.getElementById("ContentPlaceHolder1_lblCatalogo").innerHTML;

        var ImpInte = document.getElementById("ContentPlaceHolder1_ImpInterior").innerHTML;
        var ImpInte2 = document.getElementById("ContentPlaceHolder1_ImpInterior2").innerHTML;
        var ImpIntBarF = document.getElementById("ContentPlaceHolder1_ImpIntBarnizF").innerHTML; 
        var ImpIntBarV = document.getElementById("ContentPlaceHolder1_ImpIntBarnizV").innerHTML; 
        var Encua = document.getElementById("ContentPlaceHolder1_lblEnc").innerHTML;
        var Encua2 = document.getElementById("ContentPlaceHolder1_lblEnc2").innerHTML;
        var Despa = document.getElementById("ContentPlaceHolder1_lblDespacho").innerHTML;
        var Despa2 = document.getElementById("ContentPlaceHolder1_lblDespacho2").innerHTML;
        var ImpTap = document.getElementById("ContentPlaceHolder1_ImpTapas").innerHTML;
        var ImpTap2 = document.getElementById("ContentPlaceHolder1_ImpTapas2").innerHTML;
        var ImptapBarF = document.getElementById("ContentPlaceHolder1_ImpTapBarnizF").innerHTML;
        var ImptapBarV = document.getElementById("ContentPlaceHolder1_ImpTapBarnizV").innerHTML;

        var TermBarF = document.getElementById("ContentPlaceHolder1_BarnizUVF").innerHTML;
        var TermBarV = document.getElementById("ContentPlaceHolder1_BarnizUVV").innerHTML; 
        var TermLamF = document.getElementById("ContentPlaceHolder1_LaminadoF").innerHTML;
        var TermLamV = document.getElementById("ContentPlaceHolder1_LaminadoV").innerHTML;
        var TermColF = document.getElementById("ContentPlaceHolder1_ColorF").innerHTML; 
        var TermColV = document.getElementById("ContentPlaceHolder1_ColorV").innerHTML; 

        var PapIntFijo = document.getElementById("ContentPlaceHolder1_lblPapelInteriorFijo").innerHTML;
        var PapIntVari = document.getElementById("ContentPlaceHolder1_lblPapelInteriorVari").innerHTML;
        var PapTapFijo = document.getElementById("ContentPlaceHolder1_lblPapelTapaFijo").innerHTML;
        var PapTapVari = document.getElementById("ContentPlaceHolder1_lblPapelTapaVari").innerHTML;
        var Tiraje = 0;
        if (document.getElementById("<%= txtTiraje.ClientID%>").value != "") {
            Tiraje = document.getElementById("<%= txtTiraje.ClientID%>").value;
        }

        $.ajax({
            url: "Presupuestador.aspx/Totales",
            type: "post",
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            data: "{'preprensa':'" + preprensa + "','versiones':'" + versiones + "','insite':'" + insite + "','base1':'" + base1 +
                  "','Iversion':'" + Iversion + "','Color':'" + Color + "','Gastos':'" + Gastos + "','Catalog':'" + Catalog +
                  "','ImpInte':'" + ImpInte + "','Encua':'" + Encua + "','Despa':'" + Despa + "','ImpInte2':'" + ImpInte2 +
                  "','Encua2':'" + Encua2 + "','Despa2':'" + Despa2 + "','Tiraje':'" + Tiraje + "','ImpTap':'" + ImpTap +
                  "','ImpTap2':'" + ImpTap2 + "','PapIntFijo':'" + PapIntFijo + "','PapIntVari':'" + PapIntVari +
                  "','PapTapFijo':'" + PapTapFijo + "','PapTapVari':'" + PapTapVari + "','ImpIntBarF':'" + ImpIntBarF +
                  "','ImpIntBarV':'" + ImpIntBarV + "','ImptapBarF':'" + ImptapBarF + "','ImptapBarV':'" + ImptapBarV +
                  "','TermBarF':'" + TermBarF + "','TermBarV':'" + TermBarV + "','TermLamF':'" + TermLamF + "','TermLamV':'" + TermLamV +
                  "','TermColF':'" + TermColF + "','TermColV':'" + TermColV + "'}",
            success: function (msg) {
                document.getElementById("ContentPlaceHolder1_TotalFijo").innerHTML = msg.d[0];
                document.getElementById("ContentPlaceHolder1_TotalVari").innerHTML = msg.d[1];
                document.getElementById("ContentPlaceHolder1_CostoT").innerHTML = msg.d[2];
                document.getElementById("ContentPlaceHolder1_CostoU").innerHTML = msg.d[3];
                document.getElementById("ContentPlaceHolder1_lblCostoMillar").innerHTML = msg.d[4];

                
                document.getElementById("ContentPlaceHolder1_Label28").innerHTML = msg.d[2];
                document.getElementById("ContentPlaceHolder1_Label29").innerHTML = msg.d[3];
                document.getElementById("ContentPlaceHolder1_Label30").innerHTML = msg.d[4];
                //div especial Inicio
                //termino del div especial

            },
            error: function () {
                alert('no funca');
            }
        });
    }
    function Validador(TipValid) {
        if(TipValid==1){
            var caracter = document.getElementById("<%= txtNombre.ClientID%>").value;
            if (caracter == "") {
                document.getElementById("ContentPlaceHolder1_lblErrorNombre").innerHTML = "* Debe ingresar un Nombre";
                document.getElementById ('ContentPlaceHolder1_lblErrorNombre').style.color = "rgb(255, 0, 0)"; 
                document.getElementById("<%= txtNombre.ClientID%>").style.borderColor = "rgb(255, 0, 0)"; 
            }
            else if(caracter.length<5){
                document.getElementById("ContentPlaceHolder1_lblErrorNombre").innerHTML = "* Nombre muy corto";
                document.getElementById ('ContentPlaceHolder1_lblErrorNombre').style.color = "rgb(255, 0, 0)"; 
                document.getElementById("<%= txtNombre.ClientID%>").style.borderColor = "rgb(255, 0, 0)"; 
            }
            else{
                document.getElementById("ContentPlaceHolder1_lblErrorNombre").innerHTML = "";
                document.getElementById("<%= txtNombre.ClientID%>").style.borderColor = "#959595";
            }
        }
        else if(TipValid==2){
            var select1 = document.getElementById("<%= ddlFormato.ClientID %>");
            var Formato = select1.options[select1.selectedIndex].text;
            if(Formato=="Seleccionar"){
                document.getElementById("ContentPlaceHolder1_lblDescripcion").innerHTML = "* Debe seleccionar un Formato";
                document.getElementById ('ContentPlaceHolder1_lblDescripcion').style.color = "rgb(255, 0, 0)"; 
                document.getElementById("<%= ddlFormato.ClientID%>").style.borderColor = "rgb(255, 0, 0)"; 
            }
            else{
                document.getElementById("ContentPlaceHolder1_lblDescripcion").style.color = "black"; 
                document.getElementById("<%= ddlFormato.ClientID%>").style.borderColor = "#959595";
            }
            Validador(1);
        }
        else if(TipValid==3){
            var select2 = document.getElementById("<%= ddlEncuadernacion.ClientID %>");
            var Encuadernacion = select2.options[select2.selectedIndex].text;
            if(Encuadernacion=="Seleccione Encuadernación..."){
                document.getElementById("ContentPlaceHolder1_lblErrorEnc").innerHTML = "* Debe seleccionar un Tipo Encuadernación";
                document.getElementById ('ContentPlaceHolder1_lblErrorEnc').style.color = "rgb(255, 0, 0)"; 
                document.getElementById("<%= ddlEncuadernacion.ClientID%>").style.borderColor = "rgb(255, 0, 0)"; 
            }
            else{
                document.getElementById("ContentPlaceHolder1_lblErrorEnc").innerHTML = ""; 
                document.getElementById("<%= ddlEncuadernacion.ClientID%>").style.borderColor = "#959595";
            }
            Validador(2);
        }
        else if(TipValid==4){
            var Tiraje = document.getElementById("<%= txtTiraje.ClientID%>").value;
            if(Tiraje==""){
                document.getElementById("ContentPlaceHolder1_lblErrorTiraje").innerHTML = "* Debe ingresar un Tiraje";
                document.getElementById ('ContentPlaceHolder1_lblErrorTiraje').style.color = "rgb(255, 0, 0)"; 
                document.getElementById("<%= txtTiraje.ClientID%>").style.borderColor = "rgb(255, 0, 0)"; 
            }
            else{
                document.getElementById("ContentPlaceHolder1_lblErrorTiraje").innerHTML = "";
                document.getElementById("<%= txtTiraje.ClientID%>").style.borderColor = "#959595";
            }
            Validador(3);
        }
        else if(TipValid==5){
            var Tiraje = document.getElementById("<%= txtMedios.ClientID%>").value;
            if(Tiraje==""){
                document.getElementById("ContentPlaceHolder1_lblErrorMedios").innerHTML = "* Debe ingresar un porcentaje de medios";
                document.getElementById ('ContentPlaceHolder1_lblErrorMedios').style.color = "rgb(255, 0, 0)"; 
                document.getElementById("<%= txtMedios.ClientID%>").style.borderColor = "rgb(255, 0, 0)"; 
            }
            else{
                document.getElementById("ContentPlaceHolder1_lblErrorMedios").innerHTML = "";
                document.getElementById("<%= txtMedios.ClientID%>").style.borderColor = "#959595";
            }
            Validador(4);
        }
        else if(TipValid==6){
            var select1 = document.getElementById("<%= ddlPaginas.ClientID %>");
            var CantPaginas = select1.options[select1.selectedIndex].text;
            if(CantPaginas=="0"){
                document.getElementById("ContentPlaceHolder1_lblErrorPagInt").innerHTML = "* Debe seleccionar cantidad de paginas";
                document.getElementById ('ContentPlaceHolder1_lblErrorPagInt').style.color = "rgb(255, 0, 0)"; 
                document.getElementById("<%= ddlPaginas.ClientID%>").style.borderColor = "rgb(255, 0, 0)"; 
            }
            else{
                document.getElementById("ContentPlaceHolder1_lblErrorPagInt").innerHTML = "";
                document.getElementById("<%= ddlPaginas.ClientID%>").style.borderColor = "#959595";
            }
            Validador(5);
        }
        else if(TipValid==7){
            var select1 = document.getElementById("<%= ddlPapel.ClientID %>");
            var TipPapel = select1.options[select1.selectedIndex].text;
            if(TipPapel=="Seleccione Tipo Papel..."){
                document.getElementById("ContentPlaceHolder1_lblErrorPapint").innerHTML = "* Debe seleccionar un tipo de papel";
                document.getElementById ('ContentPlaceHolder1_lblErrorPapint').style.color = "rgb(255, 0, 0)"; 
                document.getElementById("<%= ddlPapel.ClientID%>").style.borderColor = "rgb(255, 0, 0)"; 
            }
            else{
                document.getElementById("ContentPlaceHolder1_lblErrorPapint").innerHTML = "";
                document.getElementById("<%= ddlPapel.ClientID%>").style.borderColor = "#959595";
            }
            Validador(6);
        }
        else if(TipValid==8){
            var select1 = document.getElementById("<%= ddlgramage.ClientID %>");
            var grPaginas = select1.options[select1.selectedIndex].text;
            if(grPaginas=="Seleccione Gramaje Papel..."){
                document.getElementById("ContentPlaceHolder1_lblMaquinaI").innerHTML = "* Debe seleccionar gramaje de las paginas";
                document.getElementById ('ContentPlaceHolder1_lblMaquinaI').style.color = "rgb(255, 0, 0)"; 
                document.getElementById("<%= ddlgramage.ClientID%>").style.borderColor = "rgb(255, 0, 0)"; 
            }
            else{
                document.getElementById("ContentPlaceHolder1_lblMaquinaI").style.color = "black";
                document.getElementById("<%= ddlgramage.ClientID%>").style.borderColor = "#959595";
            }
            Validador(7);
        }

    }
    function Papel_gramaje(Div) {
        var Usuario = '<%=Session["Empresa"] %>';
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
        else if("Tapa") {
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
            data: "{'Papel':'" + answer + "','Div':'" + Div + "','Usuario':'" + Usuario + "'}",
            success: function (data) {
                var jsdata = JSON.parse(data.d);
                $.each(jsdata, function (key, value) {
                    if (Div == "Interior") {
                        $('#<%=ddlgramage.ClientID%>').append($("<option></option>").val(value.Maquina).html(value.Gramage));
                        document.getElementById("ContentPlaceHolder1_lblMaquinaI").innerHTML = "";
                        document.getElementById("lblBarniz").style.visibility = "hidden";
                        document.getElementById("Barniz").style.visibility = "hidden";
                    }
                    else if ("Tapa") {
                        $('#<%=ddlGraTapas.ClientID%>').append($("<option></option>").val(value.Maquina).html(value.Gramage));
                        document.getElementById("ContentPlaceHolder1_lblMaquinaT").innerHTML = "";
                        document.getElementById("lblBarniz1").style.visibility = "hidden";
                        document.getElementById("Barniz1").style.visibility = "hidden";
                    }
                });
                
            },
            error: function () {
                alert('no funca');
            }
        });
    }
    function Editar(){
        var select1 = document.getElementById("ContentPlaceHolder1_lblMaquinaI").innerHTML;
        if(select1=="Tipo Impresión: Plana"){
            document.getElementById("lblBarniz").style.visibility = "visible";
            document.getElementById("Barniz").style.visibility = "visible";
        }
        else{
            document.getElementById("lblBarniz").style.visibility = "hidden";
            document.getElementById("Barniz").style.visibility = "hidden";
        }
        var select2 = document.getElementById("ContentPlaceHolder1_lblMaquinaT").innerHTML;
        if(select2=="Tipo Impresión: Plana"){
            document.getElementById("lblBarniz1").style.visibility = "visible";
            document.getElementById("Barniz1").style.visibility = "visible";
        }
        else{
            document.getElementById("lblBarniz1").style.visibility = "hidden";
            document.getElementById("Barniz1").style.visibility = "hidden";
        }
    }

    $(document).ready(function () {
        
        $("#ContentPlaceHolder1_txtNombre").change(function () {
            Validador(1);
        });
        $("#ContentPlaceHolder1_ddlFormato").change(function () {
            Validador(2);
            var select = document.getElementById("<%= ddlFormato.ClientID %>");
            var answer = select.options[select.selectedIndex].text;
            $.ajax({
                url: "Presupuestador.aspx/FormatoSelec",
                type: "post",
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                data: "{'Formato':'" + answer + "'}",
                success: function (msg) {
                    var x = document.getElementById("ContentPlaceHolder1_lblDescripcion").innerHTML;
                    if(msg.d!=""){
                        document.getElementById("ContentPlaceHolder1_lblDescripcion").innerHTML = msg.d;
                    }
                    CalculadorPredeterminado();
                    if (x != msg.d) {
                        CargaTerminacion("0");
                        CargaTerminacion("1");
                        CargaTerminacion("2");
                    }
                },
                error: function () {
                    alert('no funca');
                }
            });
        });

        $("#ContentPlaceHolder1_ddlTerBarniz").change(function () {
            CalculadorPredeterminado();
        });

        $("#ContentPlaceHolder1_ddlTerLam").change(function () {
            CalculadorPredeterminado();
        });

        $("#ContentPlaceHolder1_ddlTercinco").change(function () {
            CalculadorPredeterminado();
        });

        $("#ContentPlaceHolder1_ddlEncuadernacion").change(function () {
            Validador(3);
            CalculadorPredeterminado();
        });

        $("#ContentPlaceHolder1_txtTiraje").change(function () {
            Validador(4);
            CalculadorPredeterminado();
        });

        $("#ContentPlaceHolder1_txtMedios").change(function () {
            Validador(5);
            CalculadorPredeterminado();
        });

        $("#ContentPlaceHolder1_ddlPaginas").change(function () {
            Validador(6);
            CalculadorPredeterminado();
        });

        $("#ContentPlaceHolder1_ddlPapel").change(function () {
            Validador(7);
            Papel_gramaje("Interior");
            document.getElementById("<%= ddlgramage.ClientID%>").style.borderColor = "#959595";
            CalculadorPredeterminado();
        });

        $("#ContentPlaceHolder1_ddlgramage").change(function () {
            Validador(8);
            var select = document.getElementById("<%= ddlgramage.ClientID %>");
            var answer = select.options[select.selectedIndex].value;
            if (answer == "") {
                document.getElementById("lblBarniz").style.visibility = "hidden";
                document.getElementById("Barniz").style.visibility = "hidden";
                CalculadorPredeterminado();
            }
            else {
                document.getElementById("ContentPlaceHolder1_lblMaquinaI").innerHTML = answer;
                if (answer == "Tipo Impresión: Rotativa") {
                    document.getElementById("lblBarniz").style.visibility = "hidden";
                    document.getElementById("Barniz").style.visibility = "hidden";
                    CalculadorPredeterminado();
                }
                else {
                    document.getElementById("lblBarniz").style.visibility = "visible";
                    document.getElementById("Barniz").style.visibility = "visible";
                    var select3 = document.getElementById("<%= ddlIntBarniz.ClientID %>");
                    var BarnizInt = select3.options[select3.selectedIndex].text;
                    if (BarnizInt == "Si") {
                        document.getElementById("ContentPlaceHolder1_Label32").innerHTML = "Considera la Aplicación de barniz acuoso por tiro y retiro";
                    }
                    else {
                        document.getElementById("ContentPlaceHolder1_Label32").innerHTML = "No considera la Aplicación de barniz acuoso";
                    }
                    CalculadorPredeterminado();
                }
            }
        });

        $("#ContentPlaceHolder1_ddlIntBarniz").change(function () {
            var select3 = document.getElementById("<%= ddlIntBarniz.ClientID %>");
            var BarnizInt = select3.options[select3.selectedIndex].text;
            if (BarnizInt == "Si") {
                document.getElementById("ContentPlaceHolder1_Label32").innerHTML = "Considera la Aplicación de barniz acuoso por tiro y retiro";
            }
            else {
                document.getElementById("ContentPlaceHolder1_Label32").innerHTML = "No considera la Aplicación de barniz acuoso";
            }
            CalculadorPredeterminado();
        });
        
        $("#ContentPlaceHolder1_ddlPagTapas").change(function () {
            var select = document.getElementById("<%= ddlPagTapas.ClientID %>");
            if(select.options[select.selectedIndex].text!="0"){
                document.getElementById("ContentPlaceHolder1_ddlPapTapas").disabled = false;
                CalculadorPredeterminado();
                var select1 = document.getElementById("<%= ddlPapTapas.ClientID %>");
                if(select1.options[select1.selectedIndex].value!="Seleccione Tipo Papel..."){
                    document.getElementById("ContentPlaceHolder1_ddlGraTapas").disabled = false;
                    CalculadorPredeterminado();
                    var select2 = document.getElementById("<%= ddlGraTapas.ClientID %>");
                    if(select2.options[select2.selectedIndex].value!="Seleccione Gramaje Papel..."){
                        document.getElementById("lblBarniz1").style.visibility = "visible";
                        document.getElementById("Barniz1").style.visibility = "visible";
                        document.getElementById("ContentPlaceHolder1_ddlTerBarniz").disabled = false;
                        document.getElementById("ContentPlaceHolder1_ddlTerLam").disabled = false;
                        document.getElementById("ContentPlaceHolder1_ddlTercinco").disabled = false;
                        document.getElementById("ContentPlaceHolder1_lblMaquinaT").innerHTML = select2.options[select2.selectedIndex].value;
                        CalculadorPredeterminado();
                        CargaTerminacion("0");
                        CargaTerminacion("1");
                        CargaTerminacion("2");
                    }
                    else{
                        document.getElementById("lblBarniz1").style.visibility = "hidden";
                        document.getElementById("Barniz1").style.visibility = "hidden";
                        document.getElementById("ContentPlaceHolder1_ddlTerBarniz").disabled = true;
                        document.getElementById("ContentPlaceHolder1_ddlTerBarniz").selectedIndex = 0;
                        document.getElementById("ContentPlaceHolder1_ddlTerLam").disabled = true;
                        document.getElementById("ContentPlaceHolder1_ddlTerLam").selectedIndex = 0;
                        document.getElementById("ContentPlaceHolder1_ddlTercinco").disabled = true;
                        document.getElementById("ContentPlaceHolder1_ddlTercinco").selectedIndex = 0;
                        document.getElementById("ContentPlaceHolder1_ddlBarniz1").selectedIndex = 0;
                        CalculadorPredeterminado();
                    }
                }
                else{
                    document.getElementById("ContentPlaceHolder1_ddlGraTapas").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlGraTapas").selectedIndex = 0;
                    document.getElementById("lblBarniz1").style.visibility = "hidden";
                    document.getElementById("Barniz1").style.visibility = "hidden";
                    document.getElementById("ContentPlaceHolder1_ddlTerBarniz").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlTerBarniz").selectedIndex = 0;
                    document.getElementById("ContentPlaceHolder1_ddlTerLam").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlTerLam").selectedIndex = 0;
                    document.getElementById("ContentPlaceHolder1_ddlTercinco").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlTercinco").selectedIndex = 0;
                    document.getElementById("ContentPlaceHolder1_ddlBarniz1").selectedIndex = 0;
                    CalculadorPredeterminado();
                }
            }
            else{
                document.getElementById("ContentPlaceHolder1_ddlPapTapas").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlPapTapas").selectedIndex = 0;
                document.getElementById("ContentPlaceHolder1_ddlGraTapas").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlGraTapas").selectedIndex = 0;
                document.getElementById("ContentPlaceHolder1_lblMaquinaT").innerHTML = "";
                document.getElementById("lblBarniz1").style.visibility = "hidden";
                document.getElementById("Barniz1").style.visibility = "hidden";
                document.getElementById("ContentPlaceHolder1_ddlTerBarniz").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlTerBarniz").selectedIndex = 0;
                document.getElementById("ContentPlaceHolder1_ddlTerLam").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlTerLam").selectedIndex = 0;
                document.getElementById("ContentPlaceHolder1_ddlTercinco").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlTercinco").selectedIndex = 0;
                document.getElementById("ContentPlaceHolder1_ddlBarniz1").selectedIndex = 0;
                CalculadorPredeterminado();
            }

        });
        
        $("#ContentPlaceHolder1_ddlPapTapas").change(function () {
            var select = document.getElementById("<%= ddlPapTapas.ClientID %>");
            if(select.options[select.selectedIndex].value!="Seleccione Tipo Papel..."){
                document.getElementById("ContentPlaceHolder1_ddlGraTapas").disabled = false;
                var select1 = document.getElementById("<%= ddlGraTapas.ClientID %>");
                if(select1.options[select1.selectedIndex].value!="Seleccione Gramaje Papel..."){
                    Papel_gramaje("Tapa");
                    document.getElementById("ContentPlaceHolder1_ddlBarniz1").selectedIndex = 0;
                }
                else{
                    Papel_gramaje("Tapa");
                    document.getElementById("ContentPlaceHolder1_ddlBarniz1").selectedIndex = 0;
                }
                CalculadorPredeterminado();
            }
            else{
                document.getElementById("ContentPlaceHolder1_ddlGraTapas").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlGraTapas").selectedIndex = 0;
                document.getElementById("lblBarniz1").style.visibility = "hidden";
                document.getElementById("Barniz1").style.visibility = "hidden";
                document.getElementById("ContentPlaceHolder1_ddlTerBarniz").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlTerBarniz").selectedIndex = 0;
                document.getElementById("ContentPlaceHolder1_ddlTerLam").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlTerLam").selectedIndex = 0;
                document.getElementById("ContentPlaceHolder1_ddlTercinco").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlTercinco").selectedIndex = 0;
                document.getElementById("ContentPlaceHolder1_ddlBarniz1").selectedIndex = 0;
                document.getElementById("ContentPlaceHolder1_lblMaquinaT").innerHTML = "";
                CalculadorPredeterminado();
            }
        });
        $("#ContentPlaceHolder1_ddlGraTapas").change(function () {
            var select = document.getElementById("<%= ddlGraTapas.ClientID %>");
            var answer = select.options[select.selectedIndex].value;
            document.getElementById("ContentPlaceHolder1_lblMaquinaT").innerHTML = answer;
            if (answer == "") {
                document.getElementById("lblBarniz1").style.visibility = "hidden";
                document.getElementById("Barniz1").style.visibility = "hidden";
                document.getElementById("ContentPlaceHolder1_ddlTerBarniz").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlTerLam").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlTercinco").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlBarniz1").selectedIndex = 0;
                CalculadorPredeterminado();
            }
            else {
                if (answer == "Tipo Impresión: Rotativa") {
                    document.getElementById("lblBarniz1").style.visibility = "hidden";
                    document.getElementById("Barniz1").style.visibility = "hidden";
                    document.getElementById("ContentPlaceHolder1_ddlTerBarniz").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlTerLam").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlTercinco").disabled = true;
                    document.getElementById("ContentPlaceHolder1_ddlBarniz1").selectedIndex = 0;
                    CalculadorPredeterminado();
                }
                else if(answer=="Tipo Impresión: Plana"){
                    document.getElementById("lblBarniz1").style.visibility = "visible";
                    document.getElementById("Barniz1").style.visibility = "visible";
                    document.getElementById("ContentPlaceHolder1_ddlTerBarniz").disabled = false;
                    document.getElementById("ContentPlaceHolder1_ddlTerLam").disabled = false;
                    document.getElementById("ContentPlaceHolder1_ddlBarniz1").selectedIndex = 0;
                    CalculadorPredeterminado();
                    CargaTerminacion("0");
                    CargaTerminacion("1");
                    CargaTerminacion("2");
                    
                }
            }
        });

        $("#ContentPlaceHolder1_ddlBarniz1").change(function () {
            CalculadorPredeterminado();
            var select = document.getElementById("<%= ddlBarniz1.ClientID %>");
            var answer = select.options[select.selectedIndex].value;
            if (answer == "Tiro") {
                document.getElementById("ContentPlaceHolder1_ddlTercinco").disabled = false;
            }
            else {
                document.getElementById("ContentPlaceHolder1_ddlTercinco").disabled = true;
            }
        });

        //validacion de terminaciones
        $("#ContentPlaceHolder1_ddlTerBarniz").change(function () {
            var select = document.getElementById("<%= ddlTerBarniz.ClientID %>");
            var answer = select.options[select.selectedIndex].text;
            if (answer == "Parejo") {
                document.getElementById("ContentPlaceHolder1_ddlTerLam").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlTercinco").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlTerLam").selectedIndex = 0;
                document.getElementById("ContentPlaceHolder1_ddlTercinco").selectedIndex = 0;
            } else if (answer == "Selectivo") {
                document.getElementById("ContentPlaceHolder1_ddlTerLam").disabled = false;
                document.getElementById("ContentPlaceHolder1_ddlTercinco").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlTercinco").selectedIndex = 0;
            } else {
                document.getElementById("ContentPlaceHolder1_ddlTerLam").disabled = false;
                var select = document.getElementById("<%= ddlBarniz1.ClientID %>");
                var answer = select.options[select.selectedIndex].value;
                if (answer == "Tiro") {
                    document.getElementById("ContentPlaceHolder1_ddlTercinco").disabled = false;
                }
                else {
                    document.getElementById("ContentPlaceHolder1_ddlTercinco").disabled = true;
                }
                document.getElementById("ContentPlaceHolder1_ddlTerLam").selectedIndex = 0;
                document.getElementById("ContentPlaceHolder1_ddlTercinco").selectedIndex = 0;
            }
        });

        $("#ContentPlaceHolder1_ddlTerLam").change(function () {
            document.getElementById("ContentPlaceHolder1_ddlTerLam").disabled = false;
            document.getElementById("ContentPlaceHolder1_ddlTercinco").disabled = true;
            document.getElementById("ContentPlaceHolder1_ddlTercinco").selectedIndex = 0;
        });

        $("#ContentPlaceHolder1_ddlTercinco").change(function () {
            var select = document.getElementById("<%= ddlTercinco.ClientID %>");
            var answer = select.options[select.selectedIndex].text;

            if (answer == "No") {
                document.getElementById("ContentPlaceHolder1_ddlTerBarniz").disabled = false;
                document.getElementById("ContentPlaceHolder1_ddlTerLam").disabled = false;
            } else {
                document.getElementById("ContentPlaceHolder1_ddlTerBarniz").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlTerLam").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlTerLam").selectedIndex = 0;
                document.getElementById("ContentPlaceHolder1_ddlTerBarniz").selectedIndex = 0;
            }
        });
    });