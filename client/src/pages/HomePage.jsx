import React, { useState, useEffect } from "react"
import styled from "styled-components"
import api from "../api/api"

import ProductGrid from "../components/containers/ProductGrid"

import PageContainer from "../components/globals/containers/PageContainer"
import ListTitle from "../components/globals/titles/ListTitle"
import LoadingSpinner from "../components/globals/loading/LoadingSpinner"

const HomePage = () => {
  const [products, setProducts] = useState([])
  const [isLoadingProducts, setIsLoadingProducts] = useState(true)

  useEffect(() => {
    api.products.getAll().then((fetchedProducts) => {
      setTimeout(() => {
				fetchedProducts.forEach(p => delete p.orders)
        setProducts(fetchedProducts)
        setIsLoadingProducts(false)
      }, 250)
    })
  }, [])

  return (
    <HomePageStyled className="HomePage">
      <PageContainer>
        {isLoadingProducts && <LoadingSpinner text="Завантажуємо солодке..." />}

        {!isLoadingProducts && (
          <>
            <ListTitle text="Каталог" />
            <ProductGrid products={products} />
          </>
        )}
      </PageContainer>
    </HomePageStyled>
  )
}
const HomePageStyled = styled.div``

export default HomePage
