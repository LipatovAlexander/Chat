import { FormInstance, Upload, UploadProps } from 'antd'
import { useWatch } from 'antd/es/form/Form'
import FormItem from 'antd/es/form/FormItem'
import TextArea from 'antd/es/input/TextArea'
import React, { useCallback, useEffect, useMemo, useRef } from 'react'
import { urlToFile } from 'shared/consts'
import styled from 'styled-components'
import { uploaderModel } from '../../model'
import { MessageForm } from '../../types/message-from'

interface ChatInputProps {
    sendMessage: (newMessage: MessageForm) => void
    chatLoading: boolean
    form: FormInstance<MessageForm>
}

export const ChatInput = ({ sendMessage, chatLoading, form }: ChatInputProps) => {
    const uploadRef = useRef<any>()
    const file = useWatch('file', form)

    useEffect(() => {
        uploaderModel.events.updateUploaderRef(uploadRef)

        return () => {
            uploaderModel.events.updateUploaderRef(null)
        }
    }, [uploadRef.current])

    const onPressEnter = useCallback(
        (e: React.KeyboardEvent<HTMLTextAreaElement>) => {
            if (!e.shiftKey) {
                e.preventDefault()
                sendMessage(form.getFieldsValue())
            }
        },
        [form, sendMessage],
    )

    const uploadConfig = useMemo<UploadProps>(
        () => ({
            action: urlToFile,
            listType: 'text',
            maxCount: 1,
        }),
        [],
    )

    const getValueFromUpload = useCallback((e: any) => {
        return e.fileList.at(0)
    }, [])

    return (
        <ChatInputBlock>
            <SimpleFormItem name={'text'}>
                <TextArea disabled={chatLoading} autoSize={{ minRows: 3, maxRows: 3 }} onPressEnter={onPressEnter} />
            </SimpleFormItem>
            <UploadForm hasFile={!!file}>
                <SimpleFormItem name={'file'} valuePropName={'file'} getValueFromEvent={getValueFromUpload}>
                    <Upload {...uploadConfig} ref={uploadRef} />
                </SimpleFormItem>
            </UploadForm>
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

const UploadForm = styled.div<{ hasFile: boolean }>`
    height: ${({ hasFile }) => (hasFile ? 'auto' : '0')};
`
