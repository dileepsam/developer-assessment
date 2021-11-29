import { render, screen, cleanup, fireEvent } from '@testing-library/react'
import { act } from 'react-dom/test-utils'
import TodoItem from './TodoItem'

describe('todo item tests', () => {
  afterEach(cleanup)

  it('should notify complete', () => {
    const fn = jest.fn()
    let item = { id: 'xy-xy', description: 'description', isCompleted: false }
    act(() => { render(<TodoItem item={item} onUpdated={fn} />) })
    act(() => {fireEvent.click(screen.getByTestId('action'))})
    item.isCompleted = !item.isCompleted
    expect(fn).toBeCalledWith(item)
    expect(fn).toBeCalledTimes(1)
  })
})
