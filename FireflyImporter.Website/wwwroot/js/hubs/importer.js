const connection = new signalR.HubConnectionBuilder().withUrl("/hubs/importer").build();

const IMPORT_MESSAGE_EVENT = "ImportMessageEvent";
const IMPORT_TRANSACTION_EVENT = "ImportTransactionEvent";

const handleImportMessageEvent = (time, message) => {
    let date = new Date(time);
    let li = document.createElement("li");
    li.textContent = `[${formatDateString(date)}] ${message}`;
    li.className = "list-group-item"
    let messageList = document.getElementById("message-list");
    messageList.prepend(li);
};

const handleImportTransactionEvent = (importTransactionEvent) => {
};

const setupEvents = (connection) => {
    connection.on(IMPORT_MESSAGE_EVENT, handleImportMessageEvent);
    connection.on(IMPORT_TRANSACTION_EVENT, handleImportTransactionEvent);
};

connection.start()
    .then(() => {
        document.getElementById("connection-status").textContent = "Connected";
        document.getElementById("connection-status").className = "text-success";
        setupEvents(connection);
    })
    .catch(function (err) {
        document.getElementById("connection-status").textContent = "Failed";
        document.getElementById("connection-status").className = "text-danger";
        document.getElementById("connection-help").style = "display: block;"
        return console.error(err.toString());
    });

const formatDateString = (date) => {
    let day = date.getDate().toString().padStart(2, '0');
    let month = (date.getMonth()+1).toString().padStart(2, '0');
    let year = date.getFullYear().toString().padStart(2, '0');
    let hour = date.getHours().toString().padStart(2, '0');
    let minute = date.getMinutes().toString().padStart(2, '0');
    let seconds = date.getSeconds().toString().padStart(2, '0');

    return `${day}-${month}-${year} ${hour}:${minute}:${seconds}`
}