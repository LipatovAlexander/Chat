export interface Message {
    id: number
    ip: string
    text: string
    createdAt: Date
}

export type MessageResp = Message & { createdAt: string }
