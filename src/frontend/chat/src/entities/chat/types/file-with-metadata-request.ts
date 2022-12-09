export interface NewMetadata {
    name: string
    value: string
}

export interface FileWithMetadataRequest {
    requestId: string
    file: File
    metadata: NewMetadata[]
}
