import React, { useEffect, useState } from 'react'
import styled from 'styled-components'
import TextArea from 'antd/es/input/TextArea'
import { chatModel } from 'entities/chat'
import { userModel } from 'entities/user'
import { SendButton } from './ui/send-button'

const ChatControll = () => {
    const [text, setText] = useState('')
    const userIp = userModel.useCurrentUserIp()

    const chatConnecting = chatModel.connection.useIsConnected()
    const userIpLoading = userModel.useUserIpLoading()

    const chatLoading = chatConnecting || userIpLoading

    const messageSending = chatModel.connection.useMessageSending()

    useEffect(() => {
        chatModel.connection.events.connectToChat()
    }, [])

    const sendMessage = () => {
        if (!messageSending) {
            chatModel.connection.events.sendMessage({ ip: userIp.ipV4, text: text })
            setText('')
        }
    }

    const onPressEnter = (e: React.KeyboardEvent<HTMLTextAreaElement>) => {
        if (!e.shiftKey) {
            e.preventDefault()
            sendMessage()
        }
    }

    return (
        <ChatControllBlock>
            <TextArea
                disabled={chatLoading}
                value={text}
                autoSize={{ minRows: 3, maxRows: 3 }}
                onChange={(e) => setText(e.target.value)}
                onPressEnter={onPressEnter}
            />
            <SendButton onClick={sendMessage} disabled={messageSending || chatLoading} />
        </ChatControllBlock>
    )
}

const ChatControllBlock = styled.div`
    height: 10%;
    display: flex;
    align-items: center;
    padding: 0 10px 0 10px;
`

export default ChatControll
