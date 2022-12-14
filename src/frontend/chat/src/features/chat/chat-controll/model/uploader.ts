import { UploadFile } from 'antd'
import { createEvent, createStore, forward } from 'effector'
import { useStore } from 'effector-react'
import { chatModel } from 'entities/chat'

const $uploadedFileId = createStore<string | null>(null)

forward({
    from: chatModel.connection.events.fileWithMetadataUploaded,
    to: $uploadedFileId,
})

const $uploadedFile = createStore<UploadFile | null>(null)

const updateUploadedFile = createEvent<UploadFile | null>()

forward({
    from: updateUploadedFile,
    to: $uploadedFile,
})

export const useUploadedFileId = () => useStore($uploadedFileId)
export const useUploadedFile = () => useStore($uploadedFile)

export const events = {
    updateFile: updateUploadedFile,
}
