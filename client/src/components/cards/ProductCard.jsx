import React from "react"
import styled from "styled-components"
import PropTypes from "prop-types"
import { faPlus } from "@fortawesome/free-solid-svg-icons"

import CardImage from "../globals/images/CardImage"
import IconButton from "../globals/buttons/IconButton"
import api from "../../api/api"

const ProductCard = ({ product }) => {
  const {
    title, image, price, info,
  } = product

  const handleOrder = () => {
		let user = localStorage.getItem("user");
		if (!user) window.location.href = "/signin";
		else {
			user = JSON.parse(user)
			const amount = parseInt(prompt(`Введіть кількість "${title}": `, "1"))
			api.orders.create(amount, user["email"], title);
		}
  }

  return (
    <ProductCardStyled className="ProductCard">
      <CardImage src={image} />
      <div className="card-title">{title}</div>
      <div className="card-info">{info}</div>
      <div className="card-price">{price} грн</div>
      <div className="buttons">
        <IconButton icon={faPlus} onClick={handleOrder} text="Замовити" />
      </div>
    </ProductCardStyled>
  )
}

ProductCard.propTypes = {
  product: PropTypes.shape({
    id: PropTypes.number.isRequired,
    title: PropTypes.string.isRequired,
    image: PropTypes.string.isRequired,
    price: PropTypes.number.isRequired,
    info: PropTypes.string.isRequired,
  }).isRequired,
}

const ProductCardStyled = styled.div`
  padding: 1.2rem;
  max-width: 450px;
  border: 1px solid var(--grey6);
  border-radius: 0.3em;

  .CardImage {
    width: 50%;
    margin: 0 auto 12px;
  }

  .card-title {
    font-size: 3rem;
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
    text-align: justify;
    margin: 40px;
    overflow-y: hidden;
		text-align: center;
  }

  .CardStars {
    margin-bottom: 3px;
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

export default ProductCard
