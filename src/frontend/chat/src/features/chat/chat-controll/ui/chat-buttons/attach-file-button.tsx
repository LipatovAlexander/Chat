import { PaperClipOutlined } from '@ant-design/icons'
import { Button, ButtonProps } from 'antd'
import React, { useCallback, useState } from 'react'
import styled from 'styled-components'
import { uploaderModel } from '../../model'
import { FileModalWindow } from '../file-modal-window'

type AttachFileButtonProps = ButtonProps

export const AttachFileButton = (props: AttachFileButtonProps) => {
    const [open, setOpen] = useState(false)
    const uploader = uploaderModel.useUploaderRef()

    const onClick = useCallback(
        (e: React.MouseEvent<HTMLElement, MouseEvent>) => {
            uploader?.current?.upload.uploader.onClick(e)
        },
        [uploader?.current],
    )

    return (
        <>
            <FileModalWindow open={open} onCancel={() => setOpen(false)} fileUploaded={() => setOpen(false)} />
            <Block>
                <Button icon={<PaperClipOutlined />} onClick={() => setOpen(true)} {...props} />
            </Block>
        </>
    )
}

const Block = styled.div`
    margin-left: 10px;
`
