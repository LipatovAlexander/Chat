import { UploadOutlined } from '@ant-design/icons'
import { Button, Form, Input, Modal, ModalProps, Upload, UploadFile, UploadProps } from 'antd'
import FormItem from 'antd/es/form/FormItem'
import { chatModel } from 'entities/chat'
import React, { useCallback, useEffect } from 'react'
import { EditableTable, EditableTableColumnType } from 'shared/ui'
import uuid from 'react-uuid'
import { uploaderModel } from '../../model'

type FileModalWindowProps = ModalProps & {
    fileUploaded: () => void
}

interface Metadata {
    id: string
    name: string
    value: string
}

interface FileMetadataForm {
    file: UploadFile
    metadata: Metadata[]
}

export const FileModalWindow = ({ fileUploaded, ...modalProps }: FileModalWindowProps) => {
    const [form] = Form.useForm<FileMetadataForm>()
    const fileId = uploaderModel.useUploadedFileId()

    useEffect(() => {
        if (fileId) {
            console.log(fileId)

            const file = form.getFieldValue('file') as UploadFile
            file.uid = fileId
            uploaderModel.events.updateFile(file)
            form.resetFields()
            fileUploaded()
        }
    }, [fileId])

    const handleOk = () => {
        const req = form.getFieldsValue()
        console.log(req)
        chatModel.connection.events.uploadFile({
            requestId: uuid(),
            file: req.file,
            metadata: req.metadata,
        })
    }
    const handleCancel = () => {}

    const props: UploadProps = {
        beforeUpload: () => {
            return false
        },
    }

    const getValueFromUpload = useCallback((e: any) => {
        return e.fileList.at(0)
    }, [])

    const columns: EditableTableColumnType<Metadata>[] = [
        {
            title: 'Название',
            dataField: 'name',
            formField: {
                input: <Input />,
                name: 'name',
                required: true,
            },
        },
        {
            title: 'Значение',
            dataField: 'value',
            formField: {
                input: <Input />,
                name: 'value',
                required: true,
            },
        },
    ]

    return (
        <Modal title="Basic Modal" {...modalProps} onOk={handleOk}>
            <Form form={form}>
                <FormItem name={'file'} valuePropName={'file'} getValueFromEvent={getValueFromUpload}>
                    <Upload {...props}>
                        <Button icon={<UploadOutlined />}>Загрузить файл</Button>
                    </Upload>
                </FormItem>
                <h3>Метаданные</h3>
                <FormItem name={'metadata'}>
                    <EditableTable<Metadata> columns={columns} uniqueKey={'id'} />
                </FormItem>
            </Form>
        </Modal>
    )
}
