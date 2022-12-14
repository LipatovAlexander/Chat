import { api } from 'shared/api'
import { FileWithMetadataRequest } from '../types'

export const uploadFile = async (request: Pick<FileWithMetadataRequest, 'file' | 'requestId'>) => {
    const formData = new FormData()
    formData.append('file', request.file)
    formData.append('id', request.requestId)

    return await api.post('File', formData)
}
