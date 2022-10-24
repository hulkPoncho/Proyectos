function EnvioGenerico(controlador, modelo, callback) {

    $.ajax({
        type: 'POST',
        url: controlador,
        data: modelo,
        async: false,
        cache: false,
        success: function (resultado) {

            //Obtengo el tipo de dato del argumento callback
            tipoCallback = typeof (callback);

            if (tipoCallback == "function") {
                //Ejecuta la funciòn callback teniendo como argumento lo que respondió
                //el servidor (resultado)
                callback(resultado);
            }
            else {
                $("#" + callback).html(resultado);
            }
        },
        error: function (errorHtml) {
            alert(JSON.stringify(errorHtml))
        },
        complete: function () {

        }
    });
}