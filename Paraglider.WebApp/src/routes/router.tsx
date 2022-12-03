import { createBrowserRouter } from "react-router-dom";
import { Main } from "../features/Main";

export const router = createBrowserRouter([
  {
    path: "/",
    children: [
      {
        index: true,
        element: <Main />,
      },
    ],
  },
]);
