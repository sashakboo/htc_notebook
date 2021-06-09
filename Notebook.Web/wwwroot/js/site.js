function showError(parentSelector, error) {
  if (parentSelector) {
    var errorMessage = error ? error : "";

    html =
      '<div class="alert alert-danger alert-dismissible" role="alert">' +
      'Ошибка: ' + errorMessage +
      '<button type="button" class="close" data-dismiss="alert" aria-label="Close" >' +
      '<span aria-hidden="true">&times;</span>' +
      '</button>'
    '</div >';

    $(parentSelector).prepend(html);
  }
}

function showInfo(parentSelector, info) {
  if (parentSelector && info) {
    html =
      '<div class="alert alert-info alert-dismissible" role="alert" id="alertInfo">' +
      info +
      '<button type="button" class="close" data-dismiss="alert" aria-label="Close" >' +
      '<span aria-hidden="true">&times;</span>' +
      '</button>'
    '</div >';

    $(parentSelector).prepend(html);

    setTimeout(function () {
      $("#alertInfo").alert('close');
    }, 5000)
  }
}

$(document).ready(function () {
  // Ежедневник
  if ($("#calendarEventsContainer").length) {
    $("#calendarEventsTab").addClass("active");
    $.ajax({ url: "~/../js/calendarEvents.js", dataType: "script" });
  }
  else {
  // Контакты
    $("#contactsTab").addClass("active");
  }


});