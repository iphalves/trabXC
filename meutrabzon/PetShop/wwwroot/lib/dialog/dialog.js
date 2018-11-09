var dialog = {

    title: "",
    resp: "",
    divContainer: null,

    init: function (title) {
        dialog.title = title;
        dialog.divContainer = document.getElementById("divMsg");
        if (dialog.divContainer == null) {
            dialog.divContainer = document.createElement("div");
            dialog.divContainer.id = "divMsg";
            document.body.appendChild(dialog.divContainer);
        }


    },

    createMsg: function (msg, type, functionYes) {

        var html = "<div class=\"modale opened\" >" +
            "<div class=\"modales-dialog\">" +
            "    <div class=\"modales-header @type\">" +
            "        @title" +
            "    </div>" +
            "    <div class=\"modales-body\">" +
            "      @msg " +
            "    </div>" +
            "    <div class=\"modales-footer\">" +
            "       @btn1 @btn2 " +
            "   </div>" +
            " </div>" +
            " </div>";

        var htmlOK = html.replace("@title", dialog.title).replace("@msg", msg).replace("@type", type);


        if (type == "alert" || type == "error") {
            var btn = "<input id=\"modale-ok\" type=\"button\" value=\"OK\" class=\"btn\" />";

            htmlOK = htmlOK.replace("@btn1", btn).replace("@btn2", " ");
        }
        else if (type == "confirm") {
            var btn1 = "<input id=\"modale-s\" type=\"button\" data-response=\"SIM\" class=\"btn\" />";
            var btn2 = "<input id=\"modale-n\" type=\"button\" data-response=\"NÃO\" class=\"btn\" />";
            htmlOK = htmlOK.replace("@btn1", btn1).replace("@btn2", btn2);
        }

        dialog.divContainer.innerHTML = htmlOK;

        if (type == "alert" || type == "error") {

            dialog.divContainer.querySelector("#modale-ok").onclick = function () {
                console.log('deru ruim');
            }
        }
        else if (type == "confirm") {

            dialog.divContainer.querySelector("#modale-s").onclick = function () {

                functionYes();
                dialog.divContainer.innerHTML = "";
            }


            dialog.divContainer.querySelector("#modale-n").onclick = function () {
                dialog.divContainer.innerHTML = "";
            }
        }

    },

    alert: function (msg, type) {

        dialog.createMsg(msg, type);
    },

    alertSuccess: function (msg) {
        dialog.createMsg(msg, "alert");
    },

    alertError: function (msg) {
        dialog.createMsg(msg, "error");
    },

    confirm: function (msg, functionYes) {

        dialog.createMsg(msg, "confirm", functionYes);
    }



}

document.addEventListener("DOMContentLoaded", function () {
    dialog.init("PrimeWEB");
});