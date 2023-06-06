
//function () {
//    // Inicjalizacja popup'u
//    $("#loading-popup").dialog({
//        autoOpen: false,
//        modal: true,
//        resizable: false,
//        draggable: false,
//        closeOnEscape: false,
//        open: function (event, ui) {
//            $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
//        }
//    });

//    // Przed wysłaniem żądania AJAX
//    $(document).ajaxSend(function (event, jqXHR, ajaxOptions) {
//        // Sprawdź, czy żądanie jest wywoływane przez $.ajax z odpowiednimi opcjami
//        if (
//            ajaxOptions.url === "/DataAcquisition/Header" &&
//            ajaxOptions.type === "POST"

//        ) {
//            // Pokaż popup
//            $("#loading-popup").dialog("open");
//        }
//    });

//    // Po otrzymaniu odpowiedzi AJAX
//    $(document).ajaxComplete(function (event, jqXHR, ajaxOptions) {
//        // Sprawdź, czy żądanie jest wywoływane przez $.ajax z odpowiednimi opcjami
//        if (
//            ajaxOptions.url === "/DataAcquisition/Header" &&
//            ajaxOptions.type === "POST"
//        ) {
//            // Ukryj popup
//            $("#loading-popup").dialog("close");
//        }
//    });
//}