import React, { useCallback, useEffect, useState } from 'react'
import styled from 'styled-components'
import { SendOutlined } from '@ant-design/icons'
import { Tooltip, Button } from 'antd'
import TextArea from 'antd/es/input/TextArea'
import { chatModel } from 'entities/chat'
import { userModel } from 'entities/user'

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

    const sendMessage = useCallback(
        (text: string) => {
            if (!messageSending) {
                chatModel.connection.events.sendMessage({ ip: userIp.ipV4, text: text })
                setText('')
            }
        },
        [userIp],
    )

    const onPressEnter = (e: React.KeyboardEvent<HTMLTextAreaElement>) => {
        if (!e.shiftKey) {
            e.preventDefault()
            sendMessage(text)
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
            <SendButton>
                <Tooltip title={'Отправить'}>
                    <Button
                        disabled={messageSending || chatLoading}
                        icon={<SendOutlined />}
                        onClick={() => sendMessage(text)}
                    />
                </Tooltip>
            </SendButton>
        </ChatControllBlock>
    )
}

const ChatControllBlock = styled.div`
    height: 10%;
    display: flex;
    align-items: center;
    padding: 0 10px 0 10px;
`

const SendButton = styled.div`
    margin-left: 10px;
`

export default ChatControll
