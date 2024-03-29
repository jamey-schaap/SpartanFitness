/** @type {import("tailwindcss").Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        blue: "#2f81f7",
        "light-gray": "#7D8590",
        "hover-gray": "#8b949e",
        gray: "#30363d",
        "semi-black": "#161b22",
        black: "#0d1117",
        "light-green": "#2EA043",
        "dark-green": "#238636",
        red: "#da3633",
      },
      gridTemplateColumns: {
        13: "repeat(13, minmax(0, 1fr))",
        14: "repeat(14, minmax(0, 1fr))",
      },
      rotate: {
        8: "8deg",
      },
    },
  },
  plugins: [
    require("tailwindcss/plugin")(({ addVariant }) => {
      addVariant("search-cancel", "&::-webkit-search-cancel-button");
    }),
  ],
};
