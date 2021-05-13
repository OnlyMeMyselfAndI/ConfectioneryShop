import React, { useState, useEffect } from "react"
import styled from "styled-components"
import api from "../../api/api"

import PageContainer from "../../components/globals/containers/PageContainer"
import ListTitle from "../../components/globals/titles/ListTitle"
import LoadingSpinner from "../../components/globals/loading/LoadingSpinner"
import OrderGrid from "../../components/containers/OrderGrid"

const UserProfilePage = () => {
  const [orders, setOrders] = useState([])
  const [isLoadingOrders, setIsLoadingOrders] = useState(true)

  useEffect(() => {
    api.orders.getAll().then((fetchedOrders) => {
      setTimeout(() => {
        setOrders(fetchedOrders)
        setIsLoadingOrders(false)
      }, 250)
    })
  }, [])

  return (
    <UserProfilePageStyled className="UserProfilePage">
      <PageContainer>
        {isLoadingOrders && <LoadingSpinner text="Завантажуємо замовлення..." />}

        {!isLoadingOrders && (
          <>
            <ListTitle text="Замовлення" />
            <OrderGrid orders={orders} />
          </>
        )}
      </PageContainer>
    </UserProfilePageStyled>
  )
}

const UserProfilePageStyled = styled.div``

export default UserProfilePage
