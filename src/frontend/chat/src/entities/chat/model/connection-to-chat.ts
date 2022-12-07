import { createEffect, createEvent, createStore, forward, sample } from 'effector'
import * as signalR from '@microsoft/signalr'
import { NewMessage } from '../types/new-message'
import { useStore } from 'effector-react'
import { connectToChatAndConfigure } from '../api'
import { UploadFile } from 'antd'
import { RcFile } from 'antd/es/upload'
import { api } from 'shared/api'

type FileMetadata = {
    requestId: string
    file: UploadFile
    metadata: { name: string; value: string }[]
}

const $connection = createStore<signalR.HubConnection | null>(null)

const connectToChat = createEvent()

const connectToChatFx = createEffect(async () => {
    return await connectToChatAndConfigure()
})

const uploadFileWithMetadataFx = createEffect(
    async ({ con, request }: { request: FileMetadata; con: signalR.HubConnection }) => {
        await con.invoke('Upload', request.requestId)
        const formData = new FormData()

        formData.set('file', request.file.originFileObj!)
        formData.append('id', request.requestId)

        console.log(await api.post('File', formData))
        console.log(
            await api.post('Metadata', {
                id: request.requestId,
                metadata: request.metadata,
            }),
        )
    },
)

const fileUploaded = createEvent<string>()

const uploadFile = createEvent<FileMetadata>()

sample({
    clock: uploadFile,
    source: $connection,
    fn: (con, req) => ({
        con: con!,
        request: req,
    }),
    target: uploadFileWithMetadataFx,
})

forward({
    from: connectToChat,
    to: connectToChatFx,
})

forward({
    from: connectToChatFx.doneData,
    to: $connection,
})

const sendMessage = createEvent<NewMessage>()

const sendMessageFx = createEffect(
    async ({ connection, newMessage }: { connection: signalR.HubConnection; newMessage: NewMessage }) => {
        await connection.invoke('SendMessage', newMessage)
    },
)

sample({
    clock: sendMessage,
    source: $connection,
    filter: (connection) => !!connection,
    fn: (connection, newMessage) => ({
        connection: connection!,
        newMessage,
    }),
    target: sendMessageFx,
})

export const useIsConnected = () => useStore(connectToChatFx.pending)
export const useMessageSending = () => useStore(sendMessageFx.pending)

export const events = {
    connectToChat,
    sendMessage,
    uploadFile,
    fileUploaded,
}
