import { createBrowserRouter } from "react-router-dom";

import { CommonLayout } from "../components/Layout/CommonLayout/CommonLayout";
import { ErrorBoundary } from "../features/ErrorBoundary";
import { Main } from "../features/Main";

export const router = createBrowserRouter([
  {
    path: "/",
    children: [
      {
        element: <CommonLayout />,
        children: [
          {
            index: true,
            element: <Main />,
          },
        ],
      },
    ],
    errorElement: <ErrorBoundary />,
  },
]);
