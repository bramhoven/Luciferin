const connection = new signalR.HubConnectionBuilder().withUrl("/hubs/importer").build();

const IMPORT_MESSAGE_EVENT = "ImportMessageEvent";
const IMPORT_TRANSACTION_EVENT = "ImportTransactionEvent";

const handleImportMessageEvent = (time, message) => {
    let date = new Date(time);
    let li = document.createElement("li");
    li.textContent = `[${formatDateString(date)} ${formatTimeString(date)}] ${message}`;
    li.className = "list-group-item"
    let messageList = document.getElementById("message-list");
    messageList.prepend(li);
};

const handleImportTransactionEvent = (transaction, successful, fireflyUrl) => {
    let transactionList = document.getElementById("transaction-list")

    let card = document.createElement("div");
    card.className = transactionList.childElementCount === 0 ? "card" : "card mb-4";

    let cardBody = document.createElement("div");
    cardBody.className = "card-body";

    let cardTitle = document.createElement("h5");
    cardTitle.className = "card-title";
    cardTitle.textContent = transaction.description;

    let cardSubTitle = document.createElement("h6");
    cardSubTitle.className = "card-subtitle mb-2 text-muted";

    let date = new Date(transaction.date);
    cardSubTitle.textContent = formatDateString(date);

    let converter = new showdown.Converter();
    let cardText = document.createElement("p");
    cardText.className = "card-text";
    cardText.innerHTML = converter.makeHtml(transaction.notes.replaceAll("#", "####"));

    cardBody.appendChild(cardTitle);
    cardBody.appendChild(cardSubTitle);
    cardBody.appendChild(cardText);
    card.appendChild(cardBody);

    if (successful) {
        let fireflyLink = document.createElement("a");
        fireflyLink.className = "card-link";
        fireflyLink.href = fireflyUrl;
        fireflyLink.textContent = "View in Firefly III";
        cardBody.appendChild(fireflyLink);
    }

    transactionList.prepend(card);
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

const formatTimeString = (date) => {
    let hour = date.getHours().toString().padStart(2, '0');
    let minute = date.getMinutes().toString().padStart(2, '0');
    let seconds = date.getSeconds().toString().padStart(2, '0');

    return `${hour}:${minute}:${seconds}`
}

const formatDateString = (date) => {
    let day = date.getDate().toString().padStart(2, '0');
    let month = (date.getMonth() + 1).toString().padStart(2, '0');
    let year = date.getFullYear().toString().padStart(2, '0');

    return `${day}-${month}-${year}`
}