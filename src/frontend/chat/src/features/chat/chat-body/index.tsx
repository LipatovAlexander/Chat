import React, { useCallback, useEffect } from 'react'
import styled from 'styled-components'
import Message from './message'
import { chatModel } from 'entities/chat'
import { userModel } from 'entities/user'
import { Spin } from 'antd'
import { AutoScroll } from 'shared/ui'

const ChatBody = () => {
    const messages = chatModel.messages.useMessages()
    const userIp = userModel.useCurrentUserIp()

    const messagesLoading = chatModel.messages.useMessagesLoading()
    const userIpLoading = userModel.useUserIpLoading()

    const chatLoading = messagesLoading || userIpLoading

    const isOwn = useCallback((ipFromMessage: string) => ipFromMessage === userIp.ipV4, [userIp])

    useEffect(() => {
        chatModel.messages.events.loadMessages()
    }, [])

    return (
        <ChatBodyBlock>
            {chatLoading && (
                <LoadingArea>
                    <Spin size="large" />
                </LoadingArea>
            )}
            {!chatLoading &&
                messages.map((message) => <Message key={message.id} message={message} isOwn={isOwn(message.ip)} />)}
        </ChatBodyBlock>
    )
}

const ChatBodyBlock = styled(AutoScroll)`
    display: flex;
    flex-direction: column;
    width: 100%;
    height: 90%;
    grid-row-gap: 10px;
`

const LoadingArea = styled.div`
    display: flex;
    height: 100%;
    width: 100%;
    align-items: center;
    justify-content: center;
`

export default ChatBody
