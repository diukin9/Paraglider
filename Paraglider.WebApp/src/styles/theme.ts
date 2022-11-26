import { createGlobalStyle } from "styled-components";

export const GlobalStyle = createGlobalStyle`
  * {
    box-sizing: border-box;
    padding: 0;
    margin: 0;
  }

  body {
    font-family: "Geometria", "Arial", sans-serif;
    font-size: 16px;
    color: ${({ theme }) => theme.textColors.default};
  }
`;

export const theme = {
  bgColors: {
    primary: "#FF8787",
  },
  textColors: {
    default: "#3A3A3A",
    light: "#FFFEF6",
  },
};
