import * as signalR from '@microsoft/signalr'
import { messages } from '../model'
import { Message } from '../types'

const connectToChatAndConfigure = async () => {
    const connection = new signalR.HubConnectionBuilder().withUrl(`${process.env.REACT_APP_API_URL}/api/chat`).build()

    connection.on('ReceiveMessage', (message: Message) => {
        messages.events.addNewMessage(message)
    })

    await connection.start()

    return connection
}

export default connectToChatAndConfigure
