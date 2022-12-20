export interface Message {
    id: number
    senderUsername: string
    receiverUsername: string
    text: string
    fileId?: string
    createdAt: Date
}

export type MessageResp = Message & { createdAt: string }
