import { PaperClipOutlined } from '@ant-design/icons'
import { Button, ButtonProps } from 'antd'
import React, { useCallback } from 'react'
import styled from 'styled-components'
import { uploaderModel } from '../../model'

type AttachFileButtonProps = ButtonProps

export const AttachFileButton = (props: AttachFileButtonProps) => {
    const uploader = uploaderModel.useUploaderRef()

    const onClick = useCallback(
        (e: React.MouseEvent<HTMLElement, MouseEvent>) => {
            uploader?.current?.upload.uploader.onClick(e)
        },
        [uploader?.current],
    )

    return (
        <Block>
            <Button icon={<PaperClipOutlined />} onClick={onClick} {...props} />
        </Block>
    )
}

const Block = styled.div`
    margin-left: 10px;
`
