import React, { memo, useCallback, useMemo } from 'react'
import { Button, Form, Upload, UploadProps } from 'antd'
import { UploadOutlined } from '@ant-design/icons'

const UploaderForm = () => {
    const uploaderConfig: UploadProps = useMemo(
        () => ({
            beforeUpload: () => {
                return false
            },
            maxCount: 1,
        }),
        [],
    )

    const getValueFromUpload = useCallback((e: any) => {
        return e.fileList.at(0)
    }, [])

    return (
        <Form.Item name={'file'} valuePropName={'file'} getValueFromEvent={getValueFromUpload}>
            <Upload {...uploaderConfig}>
                <Button icon={<UploadOutlined />}>Загрузить файл</Button>
            </Upload>
        </Form.Item>
    )
}

export default memo(UploaderForm)
