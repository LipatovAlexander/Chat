import React from 'react'
import { Message as MessageType } from 'entities/chat'
import styled from 'styled-components'

interface MessageProps {
    message: MessageType
    isOwn: boolean
}

const Message = ({ message, isOwn }: MessageProps) => {
    return (
        <MessageBlock isOwn={isOwn}>
            <Content isOwn={isOwn}>
                <Username>{message.ip}</Username>
                <Text>{message.text}</Text>
            </Content>
        </MessageBlock>
    )
}

const MessageBlock = styled.div<{ isOwn: boolean }>`
    display: flex;
    width: 100%;
    justify-content: ${(props) => (props.isOwn ? 'flex-end' : 'flex-start')};
    text-align: ${({ isOwn }) => (isOwn ? 'end' : 'start')};
`

const Content = styled.div<{ isOwn: boolean }>`
    display: flex;
    flex-direction: column;
    background-color: ${(props) => (props.isOwn ? '#cde3ff' : '#f0f0f0')};
    border-radius: ${(props) => (props.isOwn ? '10px 10px 0px 10px' : '10px 10px 10px 0px')};
    padding: 5px 10px;
    max-width: 50%;
    margin: 0 5px;
`

const Username = styled.div`
    font-size: 12px;
    color: #525252;
`

const Text = styled.div`
    word-wrap: break-word;
`

export default React.memo(Message)
