import { createEffect, createEvent, createStore, forward, sample } from 'effector'
import * as signalR from '@microsoft/signalr'
import { NewMessage } from '../types/new-message'
import { useStore } from 'effector-react'
import { connectToChatAndConfigure } from '../api'
import { FileWithMetadataRequest } from '../types'
import { uploadFile } from '../api/upload-file'
import { uploadMetadata } from '../api/upload-metadata'

const $connection = createStore<signalR.HubConnection | null>(null)
const $hasInterlocutor = createStore<boolean>(false)

const connectToChat = createEvent()

const connectToChatFx = createEffect(async () => {
    return await connectToChatAndConfigure()
})

const restartConnectionFx = createEffect(async (con: signalR.HubConnection) => {
    await con.start()
})

sample({
    clock: connectToChat,
    source: $connection,
    filter: (con) => !con,
    target: connectToChatFx,
})

sample({
    clock: connectToChat,
    source: $connection,
    filter: (con) => !!con,
    fn: (con) => con!,
    target: restartConnectionFx,
})

forward({
    from: connectToChatFx.doneData,
    to: $connection,
})

const uploadFileWithMetadataFx = createEffect(
    async ({ con, request }: { request: FileWithMetadataRequest; con: signalR.HubConnection }) => {
        await con.invoke('Upload', request.requestId)
        await uploadFile(request)
        await uploadMetadata(request)
    },
)

const uploadFileWithMetdata = createEvent<FileWithMetadataRequest>()

sample({
    clock: uploadFileWithMetdata,
    source: $connection,
    fn: (con, req) => ({
        con: con!,
        request: req,
    }),
    target: uploadFileWithMetadataFx,
})

const fileWithMetadataUploaded = createEvent<string>()

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

const newUserJoinedToRoom = createEvent()

sample({
    clock: newUserJoinedToRoom,
    fn: () => true,
    target: $hasInterlocutor,
})

const interlocutorLeftFromRoom = createEvent()

sample({
    clock: interlocutorLeftFromRoom,
    fn: () => false,
    target: $hasInterlocutor,
})

const disconnect = createEvent()

const disconnectFx = createEffect(async (connection: signalR.HubConnection) => {
    await connection.stop()
})

sample({
    clock: disconnect,
    source: $connection,
    filter: (connection) => !!connection,
    fn: (connection) => connection!,
    target: disconnectFx,
})

export const useIsConnected = () => useStore(connectToChatFx.pending)
export const useMessageSending = () => useStore(sendMessageFx.pending)
export const useUploadingFileWithMetdata = () => useStore(uploadFileWithMetadataFx.pending)

export const useHasInterlocutor = () => useStore($hasInterlocutor)

export const events = {
    connectToChat,
    sendMessage,
    uploadFileWithMetdata,
    fileWithMetadataUploaded,
    newUserJoinedToRoom,
    interlocutorLeftFromRoom,
    disconnect,
}
