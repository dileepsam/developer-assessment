import { render, screen, cleanup } from '@testing-library/react'
import AddTodoItem from './AddTodoItem'
import todoItemService from 'services/TodoItemsService'

jest.mock('../services/TodoItemsService')

describe('add todo items', () => {
  it('should render input field', async () => {
    const fn = jest.fn()
    render(<AddTodoItem onItemAdded={fn} />)
    todoItemService.create.mockResolvedValue({})

    const input = screen.getByPlaceholderText(/Enter description/)
    expect(input).toBeInTheDocument()

    const button = screen.getByTestId('add-button')
    expect(button).toBeInTheDocument()

    const clearButton = screen.getByText('Clear')
    expect(clearButton).toBeInTheDocument()

    cleanup()
  })
  it('should render all items', () => {})
})
