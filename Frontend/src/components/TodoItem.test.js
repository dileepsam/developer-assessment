import { render, screen, cleanup, fireEvent } from '@testing-library/react'
import { act } from 'react-dom/test-utils';
import TodoItem from './TodoItem'

describe('todo item tests', () => {
  afterEach(cleanup)

  it('should notify complete', () => {
    const fn = jest.fn()
    const item = { id: 'xy-xy', description: 'description' }
    render(<TodoItem id={item.id} description={item.description} onComplete={fn} />)
    fireEvent.click(screen.getByText(/Mark/))
    expect(fn).toBeCalledWith(item)
    expect(fn).toBeCalledTimes(1)
  })
})
