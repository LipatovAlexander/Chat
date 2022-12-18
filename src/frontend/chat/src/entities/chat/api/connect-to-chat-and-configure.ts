import * as signalR from '@microsoft/signalr'
import { getToken } from 'shared/local-storage'
import { messages } from '../model'
import { connection as connectionModel } from '../model'
import { MessageResp } from '../types/message'

const connectToChatAndConfigure = async () => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl(`${process.env.REACT_APP_API_URL}/api/chat`, {
            headers: {
                Authorization: `Bearer ${getToken()}`,
            },
        })
        .build()

    connection.on('ReceiveMessage', (message: MessageResp) => {
        messages.events.addNewMessage(message)
    })

    connection.on('UploadFinished', ({ fileId }: { fileId: string }) => {
        connectionModel.events.fileWithMetadataUploaded(fileId)
    })

    await connection.start()

    return connection
}

export default connectToChatAndConfigure
