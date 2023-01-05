import React from "react";
import { Decorator } from "@storybook/react";
import { ThemeProvider } from "styled-components";
import { GlobalStyle, theme } from "../src/styles/theme";

export const ThemeDecorator: Decorator = (Story) => (
  <ThemeProvider theme={theme}>
    <Story />
    <GlobalStyle />
  </ThemeProvider>
);
