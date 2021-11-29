import React, { useState, useEffect } from 'react'
import { Button, Table, Toast } from 'react-bootstrap'
import todoItemService from 'services/TodoItemsService'
import TodoItem from 'components/TodoItem'

function TodoItems({ addedItem }) {
  const [items, setItems] = useState([])
  const [showToastMessage, setShowToastMessage] = useState('')

  useEffect(() => {
    getItems()
  }, [])

  useEffect(() => {
    if (addedItem) {
      setItems([...items, addedItem])
    }
    // eslint-disable-next-line
  }, [addedItem])

  async function getItems() {
    try {
      const res = await todoItemService.getAll()
      setItems(res.data)
    } catch {
      setShowToastMessage('Failed to fetch todo list')
    }
  }

  const handleMarkAsComplete = async (item) => {
    try {
      const completedItem = {
        ...item,
        isCompleted: true,
      }
      await todoItemService.update(completedItem.id, completedItem)
      getItems()
    } catch (e) {
      setShowToastMessage('Failed to mark as completed')
    }
  }

  return (
    <>
      <h1>
        Showing {items.length} Item(s)
        <Button variant="primary" className="pull-right" onClick={() => getItems()}>
          Refresh
        </Button>
      </h1>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Id</th>
            <th>Description</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody data-testid="contents">
          {items.map((item) => (
            <TodoItem key={item.id} id={item.id} description={item.description} onComplete={handleMarkAsComplete} />
          ))}
        </tbody>
      </Table>
      <Toast
        className="position-fixed"
        style={{ top: '10px', right: '10px' }}
        onClose={() => setShowToastMessage('')}
        show={!!showToastMessage}
        delay={3000}
        autohide
      >
        <Toast.Body className="text-danger">{showToastMessage}</Toast.Body>
      </Toast>
    </>
  )
}

export default TodoItems
