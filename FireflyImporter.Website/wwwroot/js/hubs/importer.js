const connection = new signalR.HubConnectionBuilder().withUrl("/hubs/importer").build();

const IMPORT_MESSAGE_EVENT = "ImportMessageEvent";
const IMPORT_TRANSACTION_EVENT = "ImportTransactionEvent";

const handleImportMessageEvent = (time, message) => {
    let date = new Date(time);
    let li = document.createElement("li");
    li.textContent = `[${date.toLocaleString()}] ${message}`;
    li.className= "list-group-item"
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