import React from 'react'
import { Button } from 'react-bootstrap'

function TodoItem(props) {
  const { item, onUpdated } = props

  return (
    <tr>
      {/* <td>{item.id}</td> */}
      <td>{item.description}</td>
      <td>
        <Button data-testid="action" variant="warning" size="sm" onClick={() => onUpdated({...item,isCompleted:!item.isCompleted})}>
          {item.isCompleted ? 'Mark as incomplete' : 'Mark as completed'}
        </Button>
      </td>
    </tr>
  )
}

export default TodoItem
