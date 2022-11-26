import { createGlobalStyle, DefaultTheme } from "styled-components";

export const GlobalStyle = createGlobalStyle`
  * {
    box-sizing: border-box;
    padding: 0;
    margin: 0;
  }

  body {
    font-family: "Geometria", "Arial", sans-serif;
    font-size: 16px;
  }
`;

export const theme: DefaultTheme = {};
