function cargaEntidades() {
    var modelo = {};
    EnvioGenerico("/Home/ConsultaEstados", modelo, RenderizaEntidades);
}

function cargaMunicipios() {
    var modelo = { EstadoId: $("#selectEstados").val() }
    EnvioGenerico("/Home/ConsultaMunicipios", modelo, RenderizaMunicipios);
}

function cargaLocalidades() {
    var modelo = { MunicipioId: $("#selectMunicipios").val() }
    EnvioGenerico("/Home/ConsultaLocalidades", modelo, RenderizaLocalidades);
}

function RenderizaEntidades(jsonResult) {
    var opciones = RenderizaResultado(jsonResult);
    $("#selectEstados").html(opciones)
}

function RenderizaMunicipios(jsonResult) {
    var opciones = RenderizaResultado(jsonResult);
    $("#selectMunicipios").html(opciones)
}

function RenderizaLocalidades(jsonResult) {
    var opciones = RenderizaResultado(jsonResult);
    $("#selectLocalidades").html(opciones)
}

function ActualizaMunicipios() {
    cargaMunicipios();
    cargaLocalidades();
}

function ActualizaLocalidades() {
    cargaLocalidades();
}

function PresentaDetalle() {
    var modelo = {
        LocalidadId: $("#selectLocalidades").val(),
        EstadoId: $("#selectEstados").val(),
        MunicipioId: $("#selectMunicipios").val(),
        Estado: $("#selectEstados option:selected").text(),
        Municipio: $("#selectMunicipios option:selected").text(),
        Localidad: $("#selectLocalidades option:selected").text(),
    }
    $("#DetalleInformacion").html("");
    if (modelo.LocalidadId != 0) {
        EnvioGenerico("/Home/ConsultaDetalle", modelo, "DetalleInformacion");
    }
}

function ConfiguraDescargaURL() {
    $("select").change(function () {
        CambiosSelector();
    });
    CambiosSelector();
}

function CambiosSelector() {
    LocalidadId = $("#selectLocalidades").val();
    EstadoId = $("#selectEstados").val();
    MunicipioId = $("#selectMunicipios").val();
    var cadena = "LocalidadId=" + LocalidadId + "&EstadoId=" + EstadoId + "&MunicipioId=" + MunicipioId;
    cadena = url + cadena;
    alert(cadena);
    $("#DescargaXLS").attr("href", cadena);
}