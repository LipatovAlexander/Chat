import React from 'react'
import { FormInstance } from 'antd'
import { useWatch } from 'antd/es/form/Form'
import { MessageForm } from '../../types/message-from'
import { AttachFileButton } from './attach-file-button'
import { SendButton } from './send-button'

interface ButtonsProps {
    form: FormInstance<MessageForm>
    buttonsDisabled: boolean
}

export const ChatButtons = ({ form, buttonsDisabled }: ButtonsProps) => {
    const text = useWatch('text', form)
    const file = useWatch('file', form)

    return (
        <>
            <SendButton htmlType="submit" disabled={buttonsDisabled || !text} />
            <AttachFileButton disabled={buttonsDisabled || !!file} />
        </>
    )
}
