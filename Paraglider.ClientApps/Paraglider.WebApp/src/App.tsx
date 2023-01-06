import { RouterProvider } from "react-router-dom";
import { ThemeProvider } from "styled-components";

import { ContextStore } from "./contexts";
import { router } from "./routes";
import { GlobalStyle, theme } from "./styles/theme";

export const App = () => {
  return (
    <ContextStore>
      <ThemeProvider theme={theme}>
        <RouterProvider router={router} />
        <GlobalStyle />
      </ThemeProvider>
    </ContextStore>
  );
};
