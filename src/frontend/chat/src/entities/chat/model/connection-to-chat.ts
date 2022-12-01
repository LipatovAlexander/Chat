import { createEffect, createEvent, createStore, forward, sample } from 'effector'
import * as signalR from '@microsoft/signalr'
import { NewMessage } from '../types/new-message'
import { useStore } from 'effector-react'
import { connectToChatAndConfigure } from '../api'

const $connection = createStore<signalR.HubConnection | null>(null)

const connectToChat = createEvent()

const connectToChatFx = createEffect(async () => {
    return await connectToChatAndConfigure()
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
}
