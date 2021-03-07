import React from 'react'
import ReactDom from 'react-dom'
import { Empty } from './components/Empty'

ReactDom.render(
  <React.StrictMode>
    <Empty />
  </React.StrictMode>,
  document.querySelector('#app'),
)
