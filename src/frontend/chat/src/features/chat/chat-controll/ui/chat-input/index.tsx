import { FormInstance, Upload } from 'antd'
import FormItem from 'antd/es/form/FormItem'
import TextArea from 'antd/es/input/TextArea'
import React, { useCallback, useMemo } from 'react'
import styled from 'styled-components'
import { uploaderModel } from '../../model'
import { MessageForm } from '../../types/message-from'

interface ChatInputProps {
    sendMessage: (newMessage: MessageForm) => void
    disabled: boolean
    form: FormInstance<MessageForm>
}

export const ChatInput = ({ sendMessage, disabled, form }: ChatInputProps) => {
    const uploadedFile = uploaderModel.useUploadedFile()
    const fileList = useMemo(() => (uploadedFile ? [uploadedFile] : []), [uploadedFile])

    const onPressEnter = useCallback(
        (e: React.KeyboardEvent<HTMLTextAreaElement>) => {
            if (!e.shiftKey) {
                e.preventDefault()
                sendMessage(form.getFieldsValue())
            }
        },
        [form, sendMessage],
    )

    const removeFile = useMemo(
        () => () => {
            uploaderModel.events.updateFile(null)
        },
        [],
    )

    return (
        <ChatInputBlock>
            <SimpleFormItem name={'text'}>
                <TextArea disabled={disabled} autoSize={{ minRows: 3, maxRows: 3 }} onPressEnter={onPressEnter} />
            </SimpleFormItem>
            <Upload fileList={fileList} onRemove={removeFile} />
        </ChatInputBlock>
    )
}

const ChatInputBlock = styled.div`
    display: flex;
    flex-direction: column;
    flex: 1;
`

const SimpleFormItem = styled(FormItem)`
    margin: 0;
`
