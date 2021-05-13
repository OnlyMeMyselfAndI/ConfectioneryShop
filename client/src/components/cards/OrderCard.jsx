import React from "react"
import styled from "styled-components"
import PropTypes from "prop-types"

const OrderCard = ({ order }) => {
  const {
    email, product, time, amount
  } = order

  return (
    <OrderCardStyled className="OrderCard">
      <div className="card-title">{product}</div>
      <div className="card-info">{email}<br/>{time}</div>
      <div className="card-price">{amount} шт</div>
    </OrderCardStyled>
  )
}

OrderCard.propTypes = {
  order: PropTypes.shape({
		email: PropTypes.string.isRequired,
		product: PropTypes.string.isRequired,
		time: PropTypes.string.isRequired,
		amount: PropTypes.number.isRequired,
  }).isRequired,
}

const OrderCardStyled = styled.div`
  padding: 1.2rem;
  max-width: 450px;
  border: 1px solid var(--grey6);
  border-radius: 0.3em;

  .CardImage {
    width: 50%;
    margin: 0 auto 12px;
  }

  .card-title {
    font-size: 2rem;
    font-weight: 500;
    margin-bottom: 20px;
    height: 50px;
		text-align: center;
  }

	.card-price {
    font-size: 2.1rem;
    font-weight: 700;
    color: var(--mainBlue);
    margin-bottom: 13px;
		float: left;
  }

  .card-info {
    font-size: 0.75rem;
    color: var(--grey4);
    font-weight: 300;
    margin: 40px;
		text-align: center;
  }

  .buttons {
    display: flex;
		float: right;
    justify-content: space-between;
  }

  .TagList {
    margin-top: 6px;
  }
`

export default OrderCard
