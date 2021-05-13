import { USER_LOGGED_IN, USER_LOGGED_OUT } from "../types/applicationUser"

export const applicationUserLoggedIn = function ({
  email,
  token,
	userName,
	isAdmin,
  isEmailConfirmed,
  isAuthenticated,
}) {
  return {
    type: USER_LOGGED_IN,
    applicationUser: {
      email,
      token,
			userName,
			isAdmin,
      isEmailConfirmed,
      isAuthenticated,
    },
  }
}

export const applicationUserLoggedOut = function ({ isAuthenticated }) {
  return {
    type: USER_LOGGED_OUT,
    applicationUser: {
      isAuthenticated,
    },
  }
}
