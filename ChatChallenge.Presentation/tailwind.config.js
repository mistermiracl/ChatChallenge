/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./Views/**/*', 'Areas/**/*'],
  theme: {
    extend: {
      fontFamily: {
        'bebas-neue': ['Bebas Neue', 'sans-serif']
      },
      backgroundColor: {
        'primary': '#000000',
        'secondary': '#211d1e',
        'skyblue': '#6ec1e6;'
      },
      textColor: {
        'skyblue': '#6ec1e6;'
      }
    },
  },
  plugins: [],
}
