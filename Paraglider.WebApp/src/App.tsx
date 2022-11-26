import React from "react";
import { ThemeProvider } from "styled-components";
import { GlobalStyle, theme } from "./styles/theme";

export const App = () => {
  return (
    <ThemeProvider theme={theme}>
      <>App</>
      <GlobalStyle />
    </ThemeProvider>
  );
};
