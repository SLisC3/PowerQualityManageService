
function createTable(data, id) {
    var table = document.createElement('table');
    table.className = "table align-items-center mb-0";
    // Tworzenie wiersza nagłówka
    var head = table.createTHead().insertRow();
    for (var key in data[0]) {
        var th = document.createElement('th');
        th.className = "text-columnHeader text-xs font-weight-bolder opacity-7 ps-2"
        th.textContent = key;
        head.appendChild(th);
    }
    // Tworzenie wierszy z danymi
    var body = table.createTBody();
    data.forEach(function (val) {
        var row = body.insertRow();
        for (var key in val) {
            var cell = row.insertCell();
            cell.className = "text-dark text-xs"
            cell.textContent = val[key];
        }
    });

    // Dodanie tabeli do elementu HTML
    var tableContainer = document.getElementById(id);
    tableContainer.appendChild(table);
}

