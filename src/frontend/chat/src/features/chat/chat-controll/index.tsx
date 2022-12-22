import React, { useEffect, useCallback } from 'react'
import styled from 'styled-components'
import { chatModel } from 'entities/chat'
import { Form } from 'antd'
import { MessageForm } from './types/message-from'
import { ChatInput } from './ui/chat-input'
import { ChatButtons } from './ui/chat-buttons'
import { uploaderModel } from './model'
import { userModel } from 'entities/user'

const ChatControll = () => {
    const [form] = Form.useForm<MessageForm>()
    const uploadedFile = uploaderModel.useUploadedFile()

    const chatConnecting = chatModel.connection.useIsConnected()
    const messageSending = chatModel.connection.useMessageSending()

    const hasInterlocutor = chatModel.connection.useHasInterlocutor()
    const { isAdmin } = userModel.useUser()

    const chatDisabled = chatConnecting || (isAdmin && !hasInterlocutor)

    useEffect(() => {
        // Какая-то хрень с маунтом и анмаунтом компоненты, из-за чего эффект отрабатывает 2 раза, поэтому навесил задержку на коннект
        const connectTimeout = setTimeout(() => chatModel.connection.events.connectToChat(), 2000)

        return () => {
            clearTimeout(connectTimeout)

            chatModel.connection.events.disconnect()
        }
    }, [])

    const sendMessage = useCallback(
        (newMessage: MessageForm) => {
            if (!messageSending && newMessage.text) {
                chatModel.connection.events.sendMessage({
                    text: newMessage.text,
                    fileId: uploadedFile?.uid,
                })

                form.resetFields()
                uploaderModel.events.updateFile(null)
            }
        },
        [form, messageSending, uploadedFile],
    )

    return (
        <ChatControllBlock form={form} onFinish={sendMessage}>
            <ChatInput disabled={chatDisabled} sendMessage={sendMessage} form={form} />
            <ChatButtons form={form} buttonsDisabled={chatDisabled} />
        </ChatControllBlock>
    )
}

const ChatControllBlock = styled(Form<MessageForm>)`
    height: 15%;
    display: flex;
    align-items: center;
    padding: 0 10px 0 10px;
`

export default React.memo(ChatControll)
