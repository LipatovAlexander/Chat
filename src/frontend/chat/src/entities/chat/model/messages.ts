import { createEffect, createEvent, createStore, forward, sample } from 'effector'
import { useStore } from 'effector-react'
import { getHistory } from '../api'
import { Message } from '../types'

const $messages = createStore<Message[]>([])

const loadMessages = createEvent()

const loadMessagesFx = createEffect(async () => {
    const history = await getHistory()
    return history.data
})

forward({
    from: loadMessages,
    to: loadMessagesFx,
})

forward({
    from: loadMessagesFx.doneData,
    to: $messages,
})

const addNewMessage = createEvent<Message>()

sample({
    clock: addNewMessage,
    source: $messages,
    fn: (messages, newMessage) => [...messages, newMessage],
    target: $messages,
})

export const useMessages = () => useStore($messages)
export const useMessagesLoading = () => useStore(loadMessagesFx.pending)

export const events = {
    loadMessages,
    addNewMessage,
}
