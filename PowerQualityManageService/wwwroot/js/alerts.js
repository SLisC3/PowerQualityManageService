
function showDangerAlert(message, id) {
    var alertElement = document.createElement('div');
    alertElement.className = 'alert alert-danger';
    alertElement.textContent = message;

    var alerts = document.getElementById(id);
    alerts.parentNode.replaceChild(alertElement, alerts);

    setTimeout(function () {
        alertElement.parentNode.replaceChild(alerts, alertElement);
    }, 3000);
}

function showSuccessAlert(message, id) {
    var alertElement = document.createElement('div');
    alertElement.className = 'alert alert-success';
    alertElement.textContent = message;

    var alerts = document.getElementById(id);
    alerts.parentNode.replaceChild(alertElement, alerts);

    setTimeout(function () {
        alertElement.parentNode.replaceChild(alerts, alertElement);
    }, 3000);
}