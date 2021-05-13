import React from "react"
import styled from "styled-components"
import PropTypes from "prop-types"

import ProductCard from "../cards/ProductCard"

const ProductGrid = ({ products }) => (
  <ProductGridStyled className="ProductGrid">
    {products.length > 0 && (
      <div className="product-grid">
        {products.map(product => (
          <ProductCard key={product.id} product={product} />
        ))}
      </div>
    )}

    {products.length === 0 && <p>Поки що на сайті немає продуктів.</p>}
  </ProductGridStyled>
)

ProductGrid.propTypes = {
  products: PropTypes.arrayOf(
		PropTypes.shape({
      id: PropTypes.number.isRequired,
      title: PropTypes.string.isRequired,
      image: PropTypes.string.isRequired,
      price: PropTypes.number.isRequired,
      info: PropTypes.string.isRequired,
    }).isRequired,
  ).isRequired,
}

const ProductGridStyled = styled.div`
  .product-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
    justify-items: center;
    grid-gap: 28px;
  }
`

export default ProductGrid
