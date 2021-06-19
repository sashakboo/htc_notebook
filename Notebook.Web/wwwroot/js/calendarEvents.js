var calendar = {};

var calendarEventTypes = {
  All: {
    Id: 0,
  },
  Meeting: {
    Id: 1,
    CreateAction: "/CalendarEvents/CreateMeeting",
    EditAction: "CalendarEvents/EditMeeting",
    DeleteAction: "/CalendarEvents/Delete",
    Color: "blue",
    DoneColor: "gray",
  },
  Memo: {
    Id: 2,
    CreateAction: "/CalendarEvents/CreateMemo",
    EditAction: "/CalendarEvents/EditMemo",
    DeleteAction: "/CalendarEvents/Delete",
    Color: "orange",
    DoneColor: "gray",
  },
  Work: {
    Id: 3,
    CreateAction: "/CalendarEvents/CreateWork",
    EditAction: "/CalendarEvents/EditWork",
    DeleteAction: "/CalendarEvents/Delete",
    Color: "green",
    DoneColor: "gray",
  },
};

function calendarEventConfig(eventType) {
  return Object.keys(calendarEventTypes)
    .map(function (e) {
      return calendarEventTypes[e];
    })
    .filter(function (e) {
      return e.Id === eventType;
    })[0];
};

function eventColor(eventType, done) {
  var config = calendarEventConfig(eventType);
  if (config) {
    return done ? config.DoneColor : config.Color;
  }

  return 'red';
}

function selectEventType(callback) {
  $('#selectEventType').modal('show');
  $("#eventTypeSelected").unbind().click(function () {
    $('#selectEventType').modal('hide');
    callback(Number($("#eventTypeSelect").val()));
  });
}

function showEventEditor(modalContent) {
  document.getElementById("editEventContent").innerHTML = modalContent;
  $('#editEvent').modal('show');
}

function modalFormValid() {
  var modalForm = $("form")
  modalForm.removeData("validator");
  modalForm.removeData("unobtrusiveValidation");
  $.validator.unobtrusive.parse("form");
  return modalForm.valid();
}

function createEvent(selectionInfo) {
  selectEventType(function (eventType) {
    var eventConfig = calendarEventConfig(eventType);
    if (eventConfig && eventConfig.CreateAction) {

      $.get(eventConfig.CreateAction)
        .done(function (html) {
          showEventEditor(html);

          var dateFormatOptions = {
            year: "numeric",
            month: "2-digit",
            day: "2-digit",
            hour: "2-digit",
            minute: "2-digit",
            second: "2-digit"
          };
          $("#DateStart").val(selectionInfo.start.toLocaleString("sv-SE", dateFormatOptions).replace(" ", "T"));
          $("#DateEnd").val(selectionInfo.end.toLocaleString("sv-SE", dateFormatOptions).replace(" ", "T"));

          $("#appointment_update").unbind().click(function () {
            if (modalFormValid()) {
              $.ajax({
                type: 'POST',
                url: eventConfig.CreateAction,
                datatype: "json",
                data: $("form").serialize()
              }).done(function (e) {

                var newEvent = {
                  id: e.id,
                  title: e.subject,
                  start: e.dateStart,
                  end: e.dateEnd,
                  color: eventColor(eventType, e.done),
                  extendedProps: {
                    subject: e.subject,
                    eventType: eventType,
                    place: e.place,
                    done: e.done,
                  },
                };
                calendar.addEvent(newEvent, true);

                $('#editEvent').modal('hide');
                document.getElementById("editEventContent").innerHTML = "";

                showInfo("#calendarEventsContainer", "Создано событие: " + newEvent.title);
              }).fail(function (err) {
                showError("#editEventContent", err)
              });
            }
          });
        })
        .fail(function (err) {
          showError("#calendarEventsContainer", err);
        });
    }
  });
}

// FIXME: отправляется несколько GET запросов после выполнения POST.
function editEvent(selectionInfo) {
  var event = selectionInfo.event;
  var eventConfig = calendarEventConfig(event.extendedProps.eventType);
  if (eventConfig && eventConfig.EditAction) {
    $.get(eventConfig.EditAction + "/" + event.id)
      .done(function (html) {
        // Скрыть всплывающее окно со списком событий.
        var popover = $('.fc-popover');
        if (popover)
          popover.remove();

        showEventEditor(html);

        $("#appointment_update").unbind().click(function () {
          if (modalFormValid()) {
            $.ajax({
              type: 'POST',
              url: eventConfig.EditAction + "/" + event.id,
              datatype: "json",
              data: $("form").serialize()
            }).done(function (result) {
              var title = $("#Subject").val();
              var start = $("#DateStart").val();
              var end = $("#DateEnd").val();
              var place = $("#Place").val();
              var done = $('#Done').is(':checked')

              event.setProp("title", title);
              event.setStart(start);
              event.setEnd(end);
              event.setExtendedProp("subject", title);
              event.setExtendedProp("place", place);
              event.setExtendedProp("done", done);
              event.setProp("color", eventColor(event.extendedProps.eventType, done));

              $('#editEvent').modal('hide');
              document.getElementById("editEventContent").innerHTML = "";

              showInfo("#calendarEventsContainer", "Изменено событие: " + event.title);
            }).fail(function (err) {
              showError("#editEventContent", err);
            });
          }
        });

        $("#appointment_delete").unbind().click(function () {
          $.ajax({
            type: 'POST',
            url: eventConfig.DeleteAction + "/" + event.id
          }).done(function (result) {
            event.remove();

            $('#editEvent').modal('hide');
            document.getElementById("editEventContent").innerHTML = "";
            showInfo("#calendarEventsContainer", "Удалено событие: " + event.title);
          }).fail(function (err) {
            showError("#editEventContent", err);
          });
        });
      }).fail(function (err) {
        showError("#calendarEventsContainer", err)
      });
  }
}

function fetchEvents (fetchInfo, successCallback, failureCallback) {
  var filterEventType = Number($("#filterEventTypeSelect").val());
  var filterText = $("#filterEventText").val();
  var urlGetEvents = "/CalendarEvents/GetFiltered?from=" + fetchInfo.start.toJSON().slice(0, 19) + "&to=" + fetchInfo.end.toJSON().slice(0, 19)
  if (filterEventType)
    urlGetEvents += "&eventType=" + filterEventType;
  if (filterText)
    urlGetEvents += "&text=" + filterText;

  $.get(urlGetEvents)
    .done(function (eventsSource) {
      var events = eventsSource.map(function (e) {

        return {
          id: e.id,
          title: e.subject,
          start: e.dateStart,
          end: e.dateEnd,
          color: eventColor(e.eventType, e.done),
          extendedProps: {
            subject: e.subject,
            eventType: e.eventType,
            place: e.place,
            done: e.done
          },
        }
      });

      successCallback(events);
    })
    .fail(function (err) {
      failureCallback(err);
    });
}

$(document).ready(function () {
  // Добавить перезагрузку событий при подтверждении фильтра.
  $("#filterSubmit").unbind().click(function () {
    calendar.refetchEvents();
  });

  var calendarEl = document.getElementById('calendarEventsContainer');

  calendar = new FullCalendar.Calendar(calendarEl, {
    locale: 'ru',
    initialView: 'timeGridDay',
    initialDate: new Date(),
    dayHeaderFormat: {
      weekday: 'short', month: 'numeric', day: 'numeric', omitCommas: true
    },
    headerToolbar: {
      left: 'prev,next today',
      center: 'title',
      right: 'timeGridDay,timeGridWeek,dayGridMonth,listMonth'
    },
    buttonText: {
      today: 'Сегодня',
      timeGridDay: 'День',
      timeGridWeek: 'Неделя',
      dayGridMonth: 'Месяц',
      listMonth: 'Список'
    },
    navLinks: true,
    dayMaxEvents: true,
    eventMaxStack: 3,
    editable: true,
    selectable: true,
    select: createEvent,
    eventClick: editEvent,
    eventChange: function (changeInfo) {
      editEvent(changeInfo);
      // TODO: пока используется редактор с сервера, менять можно только через него.
      changeInfo.revert();
    },    
    events: fetchEvents
  });

  calendar.render()
});