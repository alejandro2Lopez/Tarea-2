function validarTarea() {

    let errores = [];



    if ($("#title").val().trim().length < 6) {
        errores.push("El título debe tener al menos 6 caracteres.");
    }

    if ($("#description").val().trim().length < 11) {
        errores.push("La descripción debe tener al menos 11 caracteres.");
    }
    if (!$("#status").val()) {
        errores.push("Debe seleccionar un estado.");
    }

    if (errores.length > 0) {
        alert("Corrija los siguientes errores:\n\n" +
            errores.join("\n"));

        return false;
    }

    return true;
}