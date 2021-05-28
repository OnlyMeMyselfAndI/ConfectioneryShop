import React from "react"
import { connect } from "react-redux"
import * as middleware from "../store/middleware/applicationUser"
import styled from "styled-components"

import PageContainer from "../components/globals/containers/PageContainer"

import ProductCreationBottons from "../components/containers/ProductCreationBottons"
import ProductCreationForm from "../components/forms/ProductCreationForm"

import api from "../api/api"

const ProductCreation = () => {
  const handleSubmit = product => api.products.create(product).then(() => window.location.href = "/")

  return (
    <ProductCreationStyled className="productCreation">
      <PageContainer>
        <h1>Створити продукт</h1>
        <div className="columns">
          <div className="column-form">
            <ProductCreationForm onSubmit={handleSubmit}/>
          </div>
          <div className="column-buttons">
            <ProductCreationBottons/>
          </div>
        </div>
      </PageContainer>
    </ProductCreationStyled>
  )
}

ProductCreation.propTypes = {}

const ProductCreationStyled = styled.div`
  .columns {
    display: block;
  }

  .column-form {
    width: 100%;
  }

  .column-buttons {
    width: 100%;
  }

  @media (min-width: 768px) {
    .columns {
      display: flex;
      justify-content: center;
    }
  }
`

export default connect(null)(ProductCreation)
