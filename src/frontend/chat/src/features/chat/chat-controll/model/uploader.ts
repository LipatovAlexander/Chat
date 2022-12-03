import { createEvent, createStore, forward } from 'effector'
import { useStore } from 'effector-react'

const $uploaderRef = createStore<any | null>(null)

const updateUploaderRef = createEvent<any | null>()

forward({
    from: updateUploaderRef,
    to: $uploaderRef,
})

export const useUploaderRef = () => useStore($uploaderRef)

export const events = {
    updateUploaderRef,
}
