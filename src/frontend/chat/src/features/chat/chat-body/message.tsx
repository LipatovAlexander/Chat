import React from 'react'
import { Message as MessageType } from 'entities/chat'
import styled from 'styled-components'
import { LinkToFile } from 'shared/ui'
import { urlToFile } from 'shared/consts'

interface MessageProps {
    message: MessageType
    isOwn: boolean
}

const Message = ({ message, isOwn }: MessageProps) => {
    return (
        <MessageBlock isOwn={isOwn}>
            <Content isOwn={isOwn}>
                <Username>{message.senderUsername}</Username>
                <Text>{message.text}</Text>
                {message.fileId && <File link={`${urlToFile}?id=${message.fileId}`} />}
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

const File = styled(LinkToFile)`
    margin-top: 5px;
`

export default React.memo(Message)
