import api from "../../api/api"
import {
  applicationUserLoggedIn,
  applicationUserLoggedOut,
} from "../actions/applicationUser"
import {
  setAuthorizationHeaders,
  deleteAuthorizationHeaders,
} from "../../utils/authorizationHeaders"
import decode from "jwt-decode"

export const signup = function (newUser) {
  return function () {
    return api.applicationUser.signup(newUser).then((applicationUser) => {
      // TODO: Do something here if and only if backend have auto signin on signup
    })
  }
}

export const signin = function (credentials) {
  return function (dispatch) {
    return api.applicationUser.signin(credentials).then((applicationUser) => {
      const { email, token, userName, isAdmin, isEmailConfirmed } = applicationUser
			localStorage.setItem("user", JSON.stringify(applicationUser))
      setAuthorizationHeaders(applicationUser.token)
      dispatch(
        applicationUserLoggedIn({
          email,
          token,
					userName,
					isAdmin,
          isEmailConfirmed,
          isAuthenticated: true,
        }),
      )
    })
  }
}

export const signinWithToken = function (token) {
  return function (dispatch) {
    if (token) {
      const payload = decode(token)
      const { email, isEmailConfirmed, role, userName } = payload
			const isAdmin = role === "Admin"
      setAuthorizationHeaders(token)
      dispatch(
        applicationUserLoggedIn({
          email,
          token,
					userName,
					isAdmin,
          isEmailConfirmed: isEmailConfirmed === "True",
          isAuthenticated: true,
        }),
      )
    }
  }
}

export const signout = function () {
  return function (dispatch) {
    localStorage.removeItem("user")
    deleteAuthorizationHeaders()
    dispatch(
      applicationUserLoggedOut({
        isAuthenticated: false,
      }),
    )
  }
}
