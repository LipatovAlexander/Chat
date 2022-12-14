import React, { useState } from 'react'
import { Button, Form, Table, TableProps, Tooltip } from 'antd'
import { PlusOutlined } from '@ant-design/icons'
import { getEditableColumns } from './lib/get-custom-column'
import { EditableTableColumnType } from './types/entity-column-type'
import { getActionsColumn } from './lib/get-actions-column'
import uuid from 'react-uuid'

type EditableTableProps<T> = Omit<TableProps<T>, 'data' | 'columns'> & {
    columns: EditableTableColumnType<T>[]
    uniqueKey: keyof T
    value?: T[]
    onChange?: (value: T[]) => void
}

export const EditableTable = <T extends object>({
    columns,
    onChange = () => {},
    value: data = [],
    uniqueKey,
}: EditableTableProps<T>) => {
    const [editableRow, setEditableRow] = useState<T | null>()
    const [isNewRow, setIsNewRow] = useState(false)
    const [form] = Form.useForm<T | null>()

    const isEditableRow = (row: T) => !!editableRow && editableRow[uniqueKey] === row[uniqueKey]

    const editableColumns = getEditableColumns(columns, isEditableRow)

    const deleteRecord = (record: T) => {
        const index = data.findIndex((row) => row[uniqueKey] === record[uniqueKey])
        data.splice(index, 1)
        onChange([...data])
    }

    editableColumns.push(
        getActionsColumn(
            isEditableRow,
            (record) => {
                setEditableRow(record)
                form.setFieldsValue(record)
            },
            (record) => deleteRecord(record),
            () => {
                form.submit()
            },
            (record) => {
                if (isNewRow) {
                    deleteRecord(record)
                    setIsNewRow(false)
                }

                setEditableRow(null)
            },
        ),
    )

    const onFinish = (value: T | null) => {
        const newData = data.filter((row) => !editableRow || editableRow[uniqueKey] !== row[uniqueKey])
        newData.push({ ...editableRow!, ...value })
        onChange(newData)

        setIsNewRow(false)
        setEditableRow(null)
    }

    const addNewRecord = () => {
        const newRecord = { [uniqueKey]: uuid() } as T
        data.push(newRecord)
        onChange([...data])

        setEditableRow(newRecord)
        setIsNewRow(true)
        form.resetFields()
    }

    return (
        <>
            <Tooltip title={`Добавить`}>
                <Button onClick={addNewRecord}>
                    <PlusOutlined />
                </Button>
            </Tooltip>
            <Form style={{ marginTop: '10px' }} form={form} onFinish={onFinish}>
                <Table<T> columns={editableColumns} dataSource={data} pagination={false} size={'small'} />
            </Form>
        </>
    )
}
