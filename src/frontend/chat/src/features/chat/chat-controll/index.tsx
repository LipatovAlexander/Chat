import React, { useEffect, useCallback } from 'react'
import styled from 'styled-components'
import { chatModel } from 'entities/chat'
import { userModel } from 'entities/user'
import { Form } from 'antd'
import { MessageForm } from './types/message-from'
import { ChatInput } from './ui/chat-input'
import { ChatButtons } from './ui/chat-buttons'
import { uploaderModel } from './model'

const ChatControll = () => {
    const [form] = Form.useForm<MessageForm>()
    const userIp = userModel.useCurrentUserIp()

    const chatConnecting = chatModel.connection.useIsConnected()
    const userIpLoading = userModel.useUserIpLoading()
    const messageSending = chatModel.connection.useMessageSending()
    const chatLoading = chatConnecting || userIpLoading

    const uploadedFile = uploaderModel.useUploadedFile()

    useEffect(() => {
        chatModel.connection.events.connectToChat()
    }, [])

    const sendMessage = useCallback(
        (newMessage: MessageForm) => {
            if (!messageSending && newMessage.text) {
                console.log(uploadedFile)

                chatModel.connection.events.sendMessage({
                    text: newMessage.text,
                    fileId: uploadedFile?.uid,
                    ip: userIp.ipV4,
                })
                form.resetFields()
                uploaderModel.events.updateFile(null)
            }
        },
        [userIp, form, messageSending, uploadedFile],
    )

    return (
        <ChatControllBlock form={form} onFinish={sendMessage}>
            <ChatInput chatLoading={chatLoading} sendMessage={sendMessage} form={form} />
            <ChatButtons form={form} buttonsDisabled={chatLoading} />
        </ChatControllBlock>
    )
}

const ChatControllBlock = styled(Form<MessageForm>)`
    height: 15%;
    display: flex;
    align-items: center;
    padding: 0 10px 0 10px;
`

export default ChatControll
