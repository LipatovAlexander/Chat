import { UploadFile } from 'antd'
import { createEvent, createStore, forward } from 'effector'
import { useStore } from 'effector-react'
import { chatModel } from 'entities/chat'

const $uploaderRef = createStore<any | null>(null)
const $uploadedFileId = createStore<string | null>(null)
const $uploadedFile = createStore<UploadFile | null>(null)

const updateFile = createEvent<UploadFile | null>()

forward({
    from: updateFile,
    to: $uploadedFile,
})

forward({
    from: chatModel.connection.events.fileUploaded,
    to: $uploadedFileId,
})

forward({
    from: chatModel.connection.events.fileUploaded,
    to: $uploadedFileId,
})

const updateUploaderRef = createEvent<any | null>()

forward({
    from: updateUploaderRef,
    to: $uploaderRef,
})

export const useUploaderRef = () => useStore($uploaderRef)
export const useUploadedFileId = () => useStore($uploadedFileId)
export const useUploadedFile = () => useStore($uploadedFile)

export const events = {
    updateUploaderRef,
    updateFile,
}
