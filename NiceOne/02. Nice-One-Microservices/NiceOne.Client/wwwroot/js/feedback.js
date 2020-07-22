const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5010/notifications")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("ReceiveNotification", (message) => {
    const encodedMsg = `New feedback just received: ${message}!`;
    
    const li = document.createElement("li");
    li.textContent = encodedMsg;

    document.getElementById("messagesList").appendChild(li);
});

async function start() {
    try {
        await connection.start();
        console.log("connected");
    } catch (err) {
        console.log(err);
        setTimeout(() => start(), 5000);
    }
};

connection.onclose(async () => {
    await start();
});

// Start the connection.
start();