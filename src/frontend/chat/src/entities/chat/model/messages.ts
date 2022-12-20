import { createEffect, createEvent, createStore, forward, sample } from 'effector'
import { useStore } from 'effector-react'
import { getHistory } from '../api'
import { Message } from '../types'

const $messages = createStore<Message[]>([])

const loadMessages = createEvent()

const loadMessagesFx = createEffect(async () => {
    const history = await getHistory()

    return history.data.map((m) => ({
        ...m,
        createdAt: new Date(m.createdAt),
    }))
})

forward({
    from: loadMessages,
    to: loadMessagesFx,
})

sample({
    clock: loadMessagesFx.doneData,
    fn: (messages) => messages.sort((m1, m2) => m1.createdAt.getTime() - m2.createdAt.getTime()),
    target: $messages,
})

const addNewMessage = createEvent<Message>()

sample({
    clock: addNewMessage,
    source: $messages,
    fn: (messages, newMessage) => [...messages, { ...newMessage, createdAt: new Date(newMessage.createdAt) }],
    target: $messages,
})

const clearMessage = createEvent()

sample({
    clock: clearMessage,
    fn: () => [],
    target: $messages,
})

export const useMessages = () => useStore($messages)
export const useMessagesLoading = () => useStore(loadMessagesFx.pending)

export const events = {
    loadMessages,
    addNewMessage,
    clearMessage,
}
