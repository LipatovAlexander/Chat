import React, { useState } from 'react'
import { PaperClipOutlined } from '@ant-design/icons'
import { Button, ButtonProps } from 'antd'
import styled from 'styled-components'
import { FileUploaderModalWindow } from '../file-uploader-modal-window'

type AttachFileButtonProps = ButtonProps

export const AttachFileButton = (props: AttachFileButtonProps) => {
    const [open, setOpen] = useState(false)

    return (
        <>
            <FileUploaderModalWindow open={open} onCancel={() => setOpen(false)} fileUploaded={() => setOpen(false)} />
            <Block>
                <Button icon={<PaperClipOutlined />} onClick={() => setOpen(true)} {...props} />
            </Block>
        </>
    )
}

const Block = styled.div`
    margin-left: 10px;
`
