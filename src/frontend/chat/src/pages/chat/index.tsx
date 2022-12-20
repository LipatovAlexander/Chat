import React from 'react'
import { ChatBody, ChatControll } from 'features/chat'
import styled from 'styled-components'

const ChatPage = () => {
    return (
        <Block>
            <ChatWindow>
                <ChatBody />
                <ChatControll />
            </ChatWindow>
        </Block>
    )
}

const Block = styled.div`
    display: flex;
    align-items: center;
    justify-content: center;
`

const ChatWindow = styled.div`
    border: 1px black solid;
    border-radius: 5px;
    width: 80vw;
    height: 90vh;
`

export default React.memo(ChatPage)
