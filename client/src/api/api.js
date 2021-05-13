import axios from "axios"

const host = "http://localhost:5000/api/v1"

export default {
  applicationUser: {
    signup: newUser =>
      axios
        .post(`${host}/application_users`, { ...newUser })
        .then(response => response.data),
    signin: credentials =>
      axios
        .post(`${host}/application_users/signin`, {
          ...credentials,
        })
        .then(response => response.data),
    getAll: () =>
			axios
				.get(`${host}/application_users`)
				.then(response => response.data),
		getByEmail: email =>
				axios
					.get(`${host}/application_users/${email}`)
					.then(response => response.data),
	},
  products: {
    create: (title, image, price, info) =>
      axios
        .post(`${host}/products`, { title, image, price, info })
        .then(response => response.data),
    getAll: () =>
      axios
        .get(`${host}/products`)
        .then(response => response.data),
		getByTitle: title =>
			axios
				.get(`${host}/products/${title}`)
				.then(response => response.data),
		removeByTitle: title =>
			axios
				.get(`${host}/products/remove/${title}`)
				.then(response => response.data),
  },
	orders: {
    create: (amount, userEmail, productTitle) =>
      axios
        .post(`${host}/orders`, { amount, userEmail, productTitle })
        .then(response => response.data),
    getAll: () =>
      axios
        .get(`${host}/orders`)
        .then(response => response.data),
		getById: id =>
			axios
				.get(`${host}/orders/${id}`)
				.then(response => response.data),
		remove: id =>
			axios
				.get(`${host}/orders/remove/${id}`)
				.then(response => response.data),
  },
}
