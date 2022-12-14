import React, { memo, useMemo } from 'react'
import { Form } from 'antd'
import { EditableTable } from 'shared/ui'
import { MetadataForm as MetadataFormType } from '../../types/file-with-metadata-form'
import { getColumnsMetadataTable } from '../../lib/get-columns-metadata-table'

const MetadataForm = () => {
    const columns = useMemo(() => getColumnsMetadataTable(), [])

    return (
        <Form.Item name={'metadata'}>
            <EditableTable<MetadataFormType> columns={columns} uniqueKey={'id'} />
        </Form.Item>
    )
}

export default memo(MetadataForm)
