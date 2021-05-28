import React, { useState } from "react"
import styled from "styled-components"
import validator from "validator"
import PropTypes from "prop-types"

import Form from "../globals/forms/Form"
import FormGroup from "../globals/forms/FormGroup"
import InlineError from "../globals/messages/InlineError"
import AlertError from "../globals/messages/AlertError"
import LoadingSpinner from "../globals/loading/LoadingSpinner"

const isValidUsername = username =>
  username !== ""
  && typeof username === "string"
  && username.length > 2
  && username.length <= 20

const isValidFullname = fullname =>
  fullname !== ""
  && typeof fullname === "string"
  && fullname.length > 2
  && fullname.length <= 60

const isValidEmail = email =>
  email !== "" && typeof email === "string" && validator.isEmail(email)

const isValidPassword = password =>
  password !== ""
  && typeof password === "string"
  && password.length > 3
  && password.length <= 20

const doPasswordsMatch = (password1, password2) => password1 === password2

const INITIAL_ERRORS = {
  username: "",
  fullname: "",
  email: "",
  password: "",
  confirmPassword: "",
  global: "",
}

const getFormErrors = (data) => {
  const username = isValidUsername(data.username) ? "" : "Username is invalid"
  const fullname = isValidFullname(data.fullname) ? "" : "Fullname is invalid"
  const email = isValidEmail(data.email) ? "" : "E-mail Address is invalid"
  const password = isValidPassword(data.password) ? "" : "Password is invalid"
  const confirmPassword = !isValidPassword(data.confirmPassword)
    ? "Confirm Password is invalid"
    : doPasswordsMatch(data.password, data.confirmPassword)
      ? ""
      : "Password and confirm password do not match"

  return {
    username,
    fullname,
    email,
    password,
    confirmPassword,
  }
}

const isValidForm = ({
  username,
  fullname,
  email,
  password,
  confirmPassword,
}) =>
  username === ""
  && fullname === ""
  && email === ""
  && password === ""
  && confirmPassword === ""

const SignUpForm = ({ onSubmit }) => {
  const [username, setUsername] = useState("")
  const [fullname, setFullname] = useState("")
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [confirmPassword, setConfirPassword] = useState("")

  const [isLoading, setIsLoading] = useState(false)

  const [errors, setErrors] = useState(INITIAL_ERRORS)

  const handleSubmit = (e) => {
    e.preventDefault()
    const formData = {
      username,
      fullname,
      email,
      password,
      confirmPassword,
    }
    const formErrors = getFormErrors(formData)
    setErrors({ ...errors, ...formErrors })
    if (isValidForm(formErrors)) {
      setIsLoading(true)
      onSubmit({
        username,
        fullname,
        email,
        password,
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
    <SignUpFormStyled className="SignUpForm">

      {isLoading && <LoadingSpinner text="Реєструємо нового користувача..." />}

      {!isLoading && (
        <Form onSubmit={handleSubmit}>
          {errors.global !== "" && <AlertError text={errors.global} />}

          <FormGroup>
            <label>Ім'я кристувача</label>
            <input
              type="text"
              name="username"
              placeholder="Ім'я користувача"
              value={username}
              onChange={e => setUsername(e.target.value)}
            />
            {errors.username !== "" && <InlineError text={errors.username} />}
          </FormGroup>

          <FormGroup>
            <label>Повне ім'я</label>
            <input
              type="text"
              name="fullname"
              placeholder="Повне ім'я"
              value={fullname}
              onChange={e => setFullname(e.target.value)}
            />
            {errors.fullname !== "" && <InlineError text={errors.fullname} />}
          </FormGroup>

          <FormGroup>
            <label>Імейл</label>
            <input
              type="text"
              name="email"
              placeholder="Імейл адреса"
              value={email}
              onChange={e => setEmail(e.target.value)}
            />
            {errors.email !== "" && <InlineError text={errors.email} />}
          </FormGroup>

          <FormGroup>
            <label>Пароль</label>
            <input
              type="password"
              name="password"
              placeholder="Пароль"
              value={password}
              onChange={e => setPassword(e.target.value)}
            />
            {errors.password !== "" && <InlineError text={errors.password} />}
          </FormGroup>

          <FormGroup>
            <label>Підтвердження паролю</label>
            <input
              type="password"
              name="confirmPassword"
              placeholder="Підтвердження паролю"
              value={confirmPassword}
              onChange={e => setConfirPassword(e.target.value)}
            />
            {errors.confirmPassword !== "" && (
              <InlineError text={errors.confirmPassword} />
            )}
          </FormGroup>

          <FormGroup>
            <button type="submit">Зареєструватись</button>
          </FormGroup>
        </Form>
      )}
    </SignUpFormStyled>
  )
}

SignUpForm.propTypes = {
  onSubmit: PropTypes.func.isRequired,
}

const SignUpFormStyled = styled.div`
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

export default SignUpForm
