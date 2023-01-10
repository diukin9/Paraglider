import { RouterProvider } from "react-router-dom";
import { ThemeProvider } from "styled-components";

import { ContextStore } from "./contexts";
import { router } from "./routes";
import { GlobalStyle, theme } from "./styles/theme";

export const App = () => {
  return (
    <ThemeProvider theme={theme}>
      <ContextStore>
        <RouterProvider router={router} />
        <GlobalStyle />
      </ContextStore>
    </ThemeProvider>
  );
};
