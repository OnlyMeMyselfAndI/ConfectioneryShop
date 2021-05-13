import React from "react"
import styled from "styled-components"
import PropTypes from "prop-types"

import OrderCard from "../cards/OrderCard"

const OrderGrid = ({ orders }) => (
  <OrderGridStyled className="OrderGrid">
    {orders.length > 0 && (
      <div className="order-grid">
        {orders.reverse().map(order => (<OrderCard order={order} />))}
      </div>
    )}

    {orders.length === 0 && <p>Поки що на сайті немає замовлень :(</p>}
  </OrderGridStyled>
)

OrderGrid.propTypes = {
  orders: PropTypes.arrayOf({
    order: PropTypes.shape({
      email: PropTypes.string.isRequired,
      product: PropTypes.string.isRequired,
			time: PropTypes.string.isRequired,
			amount: PropTypes.number.isRequired,
    }).isRequired,
  }).isRequired,
}

const OrderGridStyled = styled.div`
  .order-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
    justify-items: center;
    grid-gap: 28px;
  }
`

export default OrderGrid
