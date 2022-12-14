import { UploadFile } from 'antd'
import { NewMetadata } from 'entities/chat'

export type MetadataForm = NewMetadata & {
    id: string
}

export interface FileWithMetadataForm {
    file: UploadFile
    metadata?: MetadataForm[]
}
