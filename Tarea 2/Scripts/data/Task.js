$(document).ready(function () {
    getTaskList(3);
  
    $("#btnAdd").on("click", function () {

     
        $("#formtask")[0].reset();

        $("#taskId").val(0);
        $("#tituloModaltask").text("Agregar Tarea");
        $("#btnGuardartask").text("Guardar Tarea");

    });



});
$("#statusFilter").on("change", function () {
    const estado = $(this).val();
   
    if (estado == "Pendiente") {
        getTaskList(1);
    } else if (estado == "Completada") {
        getTaskList(2);
    } else {
        getTaskList(3);
    }
  

});
$("#formtask").on("submit", function (e) {

    e.preventDefault();

    const id = parseInt($("#taskId").val(), 10) || 0;

    const task = {
        Id: id,
        Title: $("#title").val(),
        Description: $("#description").val(),
        Status: $("#status").val()
    };

    const url = id > 0
        ? "/Task/UpdateTask"
        : "/Task/AddTask";
    const messageSuccess = id > 0
        ? "La tarea se ha actualizado con éxito"
        : "La tarea ha sido agreada con éxito";
    const boton = $("#btnGuardartask");
    boton.prop("disabled", true);

    $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(task),

        success: function (respuesta) {

            alert(messageSuccess);


            const modal = bootstrap.Modal.getInstance(
                document.getElementById("modaltask")
            );
            modal.hide();


            $("#formtask")[0].reset();

            getTaskList(3);



          

    
        },

        error: function (xhr) {
            console.error(xhr.responseText);
            alert("Error intente nuevamente");
        },

        complete: function () {
            boton.prop("disabled", false);
        }
    });



});
function getTaskList(isFilter) {
    var url;
    if (isFilter == 1) {
        url = `/Task/GetTaskListFilter/${1}`
    } else if (isFilter == 2) {
        url = `/Task/GetTaskListFilter/${0}`
    } else {
        url = '/Task/GetTaskList'
    }


    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        success: function (respuesta) {

            $("#tablatask").empty();

            respuesta.list.forEach(function (task) {

                const fila = $("<tr>").attr("data-id", task.Id);

                $("<td>").text(task.Title).appendTo(fila);
                $("<td>").text(task.Description).appendTo(fila);
                $("<td>").text(task.Status).appendTo(fila);
                const actions = $("<td>");

            
                const btnEdit = $("<button>")
                    .addClass("btn btn-primary btn-sm me-2")
                    .text("Editar")
                    .on("click", function () {
                        updatetask(task.Id);
                    });

             
                const btnDelete = $("<button>")
                    .addClass("btn btn-danger btn-sm")
                    .text("Eliminar")
                    .on("click", function () {
                        deleteTask(task.Id);
                    });

                actions.append(btnEdit, btnDelete);
                fila.append(actions);
                $("#tablatask").append(fila);
            });
        },
        error: function (xhr) {
            console.error("Error al obtener tareas:", xhr);
        }
    });

}

function deleteTask(id) {
    $.ajax({
        url: `/Task/Delete/${id}`,
        type: 'GET',
        dataType: 'json',
        success: function (respuesta) {
            
            getTaskList(3);
            alert("La tarea ha sido eliminada de manera exitosa.");
        },
        error: function (xhr) {
            console.error("Error al obtener tareas:", xhr);
        }
    });
}

function updatetask(id) {
    $.ajax({
        url: `/Task/GetTaskListById/${id}`,
        type: 'GET',
        dataType: 'json',
        success: function (respuesta) {

            const task = respuesta.task;

            if (!task) {
                console.error("No se encontró la tarea");
                return;
            }

          
            $("#taskId").val(task.Id);

        
            $("#title").val(task.Title);
            $("#description").val(task.Description);
            $("#status").val(task.Status);

         
            $("#tituloModaltask").text("Editar Tarea");
            $("#btnGuardartask").text("Actualizar Tarea");

          
            const modal = bootstrap.Modal.getOrCreateInstance(
                document.getElementById("modaltask")
            );

            modal.show();
        },
        error: function (xhr) {
            console.error("Error al obtener tarea:", xhr);
        }
    });
}

