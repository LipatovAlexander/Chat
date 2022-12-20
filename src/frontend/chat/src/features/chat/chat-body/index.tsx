import React, { useEffect } from 'react'
import styled from 'styled-components'
import Message from './message'
import { chatModel } from 'entities/chat'
import { Spin } from 'antd'
import { AutoScroll } from 'shared/ui'
import { userModel } from 'entities/user'

const ChatBody = () => {
    const messages = chatModel.messages.useMessages()
    const user = userModel.useUser()

    const messagesLoading = chatModel.messages.useMessagesLoading()

    useEffect(() => {
        if (!user.isAdmin) {
            chatModel.messages.events.loadMessages()
        }
    }, [])

    const isOwn = (senderUsername: string) =>
        (user.isAdmin && senderUsername.startsWith('admin')) || user.username === senderUsername

    return (
        <ChatBodyBlock>
            {messagesLoading && (
                <LoadingArea>
                    <Spin size="large" />
                </LoadingArea>
            )}
            {!messagesLoading &&
                messages.map((message) => (
                    <Message key={message.id} message={message} isOwn={isOwn(message.senderUsername)} />
                ))}
        </ChatBodyBlock>
    )
}

const ChatBodyBlock = styled(AutoScroll)`
    display: flex;
    flex-direction: column;
    width: 100%;
    height: 85%;
    grid-row-gap: 10px;
`

const LoadingArea = styled.div`
    display: flex;
    height: 100%;
    width: 100%;
    align-items: center;
    justify-content: center;
`

export default React.memo(ChatBody)
