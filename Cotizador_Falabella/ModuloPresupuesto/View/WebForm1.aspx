<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Cotizador_Falabella.ModuloPresupuesto.View.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet">
    </link>
    <link rel="stylesheet" href="https://rawgit.com/Eonasdan/bootstrap-datetimepicker/master/build/css/bootstrap-datetimepicker.min.css" />
    <script src="//oss.maxcdn.com/momentjs/2.8.2/moment.min.js"></script>
    <script src="https://rawgit.com/Eonasdan/bootstrap-datetimepicker/master/src/js/bootstrap-datetimepicker.js"></script>
    <script>
        $('#myForm').validator()
    </script>
</head>
<body>
    <form role="form" data-toggle="validator">
    <h2>
        Formulario de Registro</h2>
    <div class="form-group">
        <label class="col-lg-3 control-label">
            Nombres</label>
        <div class="col-lg-3">
            <input type="text" class="form-control" name="nombre" required/>
        </div>
    </div>
    <div class="form-group">
        <label class="col-lg-3 control-label">
            Apellidos</label>
        <div class="col-lg-3">
            <input type="text" class="form-control" name="apellido" />
        </div>
    </div>
    <div class="form-group">
        <label class="col-lg-3 control-label">
            Correo Electrónico</label>
        <div class="col-lg-3">
            <input type="text" class="form-control" name="email" />
        </div>
    </div>
    <div class="form-group">
        <label class="col-lg-3 control-label">
            Password</label>
        <div class="col-lg-3">
            <input type="password" class="form-control" name="password" />
        </div>
    </div>
    <div class="form-group">
        <label class="col-lg-3 control-label">
            Fecha de Nacimiento</label>
        <div class="col-lg-3">
            <input type="text" class="form-control" name="datetimepicker" id="datetimepicker"
                data-date-format="YYYY-MM-DD" />
        </div>
    </div>
    <div class="form-group">
        <label class="col-lg-3 control-label">
            Sexo</label>
        <div class="col-lg-9">
            <div class="radio">
                <label>
                    <input type="radio" name="sexo" value="M" />
                    Masculino
                </label>
            </div>
            <div class="radio">
                <label>
                    <input type="radio" name="sexo" value="F" />
                    Femenino
                </label>
            </div>
        </div>
    </div>
    <div class="form-group">
        <label class="col-lg-3 control-label">
            Teléfono</label>
        <div class="col-lg-3">
            <input type="text" class="form-control" name="telefono" />
        </div>
    </div>
    <div class="form-group">
        <label class="col-lg-3 control-label">
            Teléfono Celular</label>
        <div class="col-lg-3">
            <input type="text" class="form-control" name="telefono_cel" />
        </div>
    </div>
    <div class="form-group">
        <div class="col-lg-9 col-lg-offset-3">
            <button type="submit" class="btn btn-success left">
                Registrarse</button>
        </div>
    </div>
    </form>
</body>
</html>
