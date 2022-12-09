import React, { useEffect, useMemo } from 'react'
import { Form, Modal, ModalProps, UploadFile } from 'antd'
import { chatModel } from 'entities/chat'
import uuid from 'react-uuid'
import { uploaderModel } from '../../model'
import { FileWithMetadataForm } from '../../types/file-with-metadata-form'
import UploaderForm from './uploader-form'
import MetadataForm from './metadata-form'
import { useWatch } from 'antd/es/form/Form'

type FileModalWindowProps = ModalProps & {
    fileUploaded: () => void
}

export const FileUploaderModalWindow = ({ fileUploaded, ...modalProps }: FileModalWindowProps) => {
    const [form] = Form.useForm<FileWithMetadataForm>()
    const file = useWatch('file', form)

    const fileId = uploaderModel.useUploadedFileId()
    const uploading = chatModel.connection.useUploadingFileWithMetdata()

    useEffect(() => {
        if (fileId) {
            const file = form.getFieldValue('file') as UploadFile
            file.uid = fileId
            uploaderModel.events.updateFile(file)

            fileUploaded()
            form.resetFields()
        }
    }, [fileId])

    const handleOk = () => {
        const request = form.getFieldsValue()
        chatModel.connection.events.uploadFileWithMetdata({
            file: request.file.originFileObj!,
            metadata: request.metadata ?? [],
            requestId: uuid(),
        })
    }

    const okButtonProps: ModalProps['okButtonProps'] = useMemo(() => ({ disabled: !file }), [file])

    return (
        <Modal
            title={<h3>Данные файла</h3>}
            {...modalProps}
            onOk={handleOk}
            confirmLoading={uploading}
            okButtonProps={okButtonProps}
        >
            <Form form={form}>
                <UploaderForm />
                <h3>Метаданные</h3>
                <MetadataForm />
            </Form>
        </Modal>
    )
}
