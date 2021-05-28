import React, { useState } from "react"
import styled from "styled-components"
import validator from "validator"
import PropTypes from "prop-types"

import Form from "../globals/forms/Form"
import FormGroup from "../globals/forms/FormGroup"
import InlineError from "../globals/messages/InlineError"
import AlertError from "../globals/messages/AlertError"
import LoadingSpinner from "../globals/loading/LoadingSpinner"

const isValidString = s => s !== "" && typeof s === "string"
const isValidNumber = n => typeof n === "number" || !isNaN(n)

const INITIAL_ERRORS = {
	title: "",
	image: "",
	price: "",
	info: "",
  global: "",
}

const getFormErrors = (data) => {
	const title = isValidString(data.title) ? "" : "Назва мусить бути строкою"
	const image = isValidString(data.image) ? "" : "Лінк до картинки мусить бути строкою"
	const price = isValidNumber(data.price) ? "" : "Ціна має бути числом"
	const info = isValidString(data.info) ? "" : "Інформація мусить бути строкою"

  return {
		title, image, price, info
  }
}

const isValidForm = ({
	title, image, price, info
}) =>
	title === "" && image === "" && price === "" && info === ""

const ProductCreationForm = ({ onSubmit }) => {
	const [title, setTitle] = useState("")
	const [image, setImage] = useState("")
	const [price, setPrice] = useState(0)
	const [info, setInfo] = useState("")

  const [isLoading, setIsLoading] = useState(false)

  const [errors, setErrors] = useState(INITIAL_ERRORS)

  const handleSubmit = (e) => {
    e.preventDefault()
    const formData = {
			title, image, price, info
		}
    const formErrors = getFormErrors(formData)
    setErrors({ ...errors, ...formErrors })
    if (isValidForm(formErrors)) {
      setIsLoading(true)
      onSubmit({
				title, image, price, info
			}).catch((err) => {
        setIsLoading(false)
        if (err.response) {
          setErrors({ ...errors, global: err.response.data.errors.global })
        } else {
          setErrors({ ...errors, global: "Немає відповіді від сервера" })
        }
      })
    }
  }

  return (
    <ProductCreationFormStyled className="ProductCreationForm">

      {isLoading && <LoadingSpinner text="Пробуємо створити новий продукт..." />}

      {!isLoading && (
        <Form onSubmit={handleSubmit}>
          {errors.global !== "" && <AlertError text={errors.global} />}

          <FormGroup>
            <label>Назва продукту</label>
            <input
              type="text"
              name="title"
              placeholder="Назва продукту"
              value={title}
              onChange={e => setTitle(e.target.value)}
            />
            {errors.title !== "" && <InlineError text={errors.title} />}
          </FormGroup>

          <FormGroup>
            <label>Посилання на картинку</label>
            <input
              type="text"
              name="image"
              placeholder="Посилання на картинку"
              value={image}
              onChange={e => setImage(e.target.value)}
            />
            {errors.image !== "" && <InlineError text={errors.image} />}
          </FormGroup>

          <FormGroup>
            <label>Ціна</label>
            <input
              type="number"
              name="price"
              placeholder="Ціна"
              value={price}
              onChange={e => setPrice(e.target.value)}
            />
            {errors.price !== "" && <InlineError text={errors.price} />}
          </FormGroup>

          <FormGroup>
            <label>Інформація</label>
            <input
              type="text"
              name="info"
              placeholder="Інформація"
              value={info}
              onChange={e => setInfo(e.target.value)}
            />
            {errors.info !== "" && <InlineError text={errors.info} />}
          </FormGroup>

          <FormGroup>
            <button type="submit">Створити продукт</button>
          </FormGroup>
        </Form>
      )}
    </ProductCreationFormStyled>
  )
}

ProductCreationForm.propTypes = {
  onSubmit: PropTypes.func.isRequired,
}

const ProductCreationFormStyled = styled.div`
  .AlertError {
    margin: 10px 0 6px;
  }

  button[type="submit"] {
    margin-top: 10px;
  }

  label {
    padding-top: 2px;
  }
`

export default ProductCreationForm
