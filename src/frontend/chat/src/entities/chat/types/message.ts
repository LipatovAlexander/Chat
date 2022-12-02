export interface Message {
    id: number
    ip: string
    text: string
    fileId?: string
    createdAt: Date
}

export type MessageResp = Message & { createdAt: string }
