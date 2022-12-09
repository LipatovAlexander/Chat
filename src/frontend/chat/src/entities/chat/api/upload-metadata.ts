import { api } from 'shared/api'
import { FileWithMetadataRequest } from '../types'

export const uploadMetadata = async (request: Pick<FileWithMetadataRequest, 'metadata' | 'requestId'>) => {
    return await api.post('Metadata', {
        id: request.requestId,
        metadata: request.metadata,
    })
}
