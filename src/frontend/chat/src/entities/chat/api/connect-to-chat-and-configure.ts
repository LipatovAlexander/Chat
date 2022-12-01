import * as signalR from '@microsoft/signalr'
import { messages } from '../model'
import { MessageResp } from '../types/message'

const connectToChatAndConfigure = async () => {
    const connection = new signalR.HubConnectionBuilder().withUrl(`${process.env.REACT_APP_API_URL}/api/chat`).build()

    connection.on('ReceiveMessage', (message: MessageResp) => {
        messages.events.addNewMessage(message)
    })

    await connection.start()

    return connection
}

export default connectToChatAndConfigure
