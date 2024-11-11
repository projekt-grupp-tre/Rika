const inputObj = document.getElementById('chat-input')
let inputValue = '';
let groupId

const chatConnection = new signalR.HubConnectionBuilder()
    .withUrl("https://signalprovider-ekadbcdaavg7eyfg.northeurope-01.azurewebsites.net/chatHub")
    .withAutomaticReconnect()
    .build();

class TypingNotifier {
    constructor(chatConnection, delay = 500) {
        this.chatConnection = chatConnection;
        this.delay = delay;
        this.timeoutId = null;
        this.isCurrentlyTyping = false;
    }
    
    handleTyping = async () => {
        if (this.timeoutId) {
            clearTimeout(this.timeoutId);
        }
        try {
            if (!this.isCurrentlyTyping) {
                await this.chatConnection.invoke("UserIsTyping");
                this.isCurrentlyTyping = true;
            }
    
            this.timeoutId = setTimeout(async () => {
                await this.chatConnection.invoke("UserStoppedTyping");
                this.isCurrentlyTyping = false;
            }, this.delay);
    
        } catch (err) {
            console.error("Error in typing notification:", err);
        }
    }
}

const chatList = document.getElementById('chatlist');
const typingNotifier = new TypingNotifier(chatConnection);
const indicatorHtml = `
    <div class="dots">
        <div class="dot"></div>
        <div class="dot"></div>
        <div class="dot"></div>
    </div>
`
const indicator = document.createElement('li')
indicator.classList.add('writing-indicator')
indicator.innerHTML = indicatorHtml

inputObj.addEventListener('keyup',(e) => {
    inputValue = e.target.value;
    typingNotifier.handleTyping();
});

chatConnection.on("MessageReceived", (message) => {
    displayMessage(message)
})

chatConnection.on("AddedToGroup", addedGroupId => {
    groupId = addedGroupId
})

chatConnection.on("UserIsTyping", isTyping => handleWritingIndicator(isTyping))
chatConnection.on("UserStoppedTyping", isTyping => handleWritingIndicator(isTyping))

function handleWritingIndicator(isTyping) {
    const allIndicators = document.getElementsByClassName('writing-indicator')
    Array.from(allIndicators).forEach(el => chatList.removeChild(el))

    if (isTyping) {   
        chatList.appendChild(indicator)
    }
}

async function start() {
    try {
        await chatConnection.start()
        console.log('Connected to SignalR')

    } catch (err) {
        console.error("Error connecting to  hub: ", err)
        setTimeout(startConnection, 5000)
    }

    // Add the new event listener
    window.addEventListener('beforeunload', async () => {
        try {
            await chatConnection.stop()
        } catch (err) {
            console.error("Error disconnecting from SignalR hub: ", err)
        }
    });
}

const submitButton = document.getElementById('chat-submit-btn');
submitButton.addEventListener('click', async (e) => {
    e.preventDefault(); // Förhindra standardbeteendet för knappen
    try {
        if (inputValue)
            await chatConnection.invoke("SendMessage", {
                username: "Levi",
                senderUserId: chatConnection.connectionId,
                groupId: groupId,
                messageContent: inputValue,
                messageSentAt: null,
                attachmentUrls: null
            })
    } catch (err) {
        console.error("Error sending message: ", err)
    }
    inputObj.value = "";
});

async function sendMessage() {
    try {
        console.log(payload)
        if (inputValue)
            await chatConnection.invoke("SendMessage", payload)
    } catch (err) {
        console.error("Error sending message: ", err)
    }
}

function displayMessage(message) {
    const messageBlob = document.createElement('li');
    messageBlob.className = message.senderUserId === chatConnection.connectionId ? 'outgoing-message' : 'incoming-message'; 
    messageBlob.innerHTML = `
        <div class="message-wrapper">
            <div class="message-text">
                ${message.messageContent || 'No content'} 
            </div>
            <div class="message-timestamp">
                ${new Date().toLocaleTimeString('sv-SE', { hour: '2-digit', minute: '2-digit' })}
            </div>
        </div>
    `;
    
    chatList.appendChild(messageBlob);
    chatList.scrollTop = chatList.scrollHeight;
    document.querySelector('#chatList').innerHTML = messageBlob.innerHTML
}

start();

