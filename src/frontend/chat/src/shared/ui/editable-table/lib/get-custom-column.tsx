import React from 'react'
import { EditableTableColumnType } from '../types/entity-column-type'
import { Form, TableColumnType } from 'antd'
import styled from 'styled-components'

export const getEditableColumns = <T,>(
    columns: EditableTableColumnType<T>[],
    isEditableRow: (record: T) => boolean,
): TableColumnType<T>[] =>
    columns.map((col) => ({
        ...col,
        dataIndex: col.dataField as string,
        render: (value: any, record: T, index) => {
            if (isEditableRow(record)) {
                const input = col.formField

                return (
                    <FormItem
                        name={input.name}
                        key={input.name}
                        id={input.name}
                        valuePropName={input.valuePropName}
                        rules={[{ required: input.required }]}
                    >
                        {React.isValidElement(input.input) && React.cloneElement(input.input)}
                    </FormItem>
                )
            }

            return col.render ? col.render(value, record, index) : value
        },
    }))

const FormItem = styled(Form.Item)`
    margin-bottom: 0;
    width: 100%;
`
