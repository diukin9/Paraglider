import { css } from "styled-components";

export type ButtonVariant = "default" | "outlined" | "ghost";

export const defaultButtonStyles = css`
  display: block;

  outline: none;
  border: none;

  font-family: "Geometria", sans-serif;
  font-size: 16px;

  background: none;
  user-select: none;
  cursor: pointer;
`;

export const getButtonStyles = (variant: ButtonVariant) => {
  switch (variant) {
    case "default":
      return css`
        color: ${({ theme }) => theme.textColors.light};
        background-color: ${({ theme }) => theme.bgColors.primary};

        box-shadow: 0px 12px 20px -8px rgba(255, 135, 135, 0.85);
      `;
    case "outlined":
      return css`
        padding: 14px 46px;

        color: ${({ theme }) => theme.textColors.primary};
        border: 2px solid ${({ theme }) => theme.bgColors.primary};
        background-color: inherit;

        :hover {
          box-shadow: 0px 12px 20px -8px rgba(255, 135, 135, 0.85);
          transition: box-shadow 0.2s;
        }
      `;
    case "ghost":
      return css``;
    default:
      return null;
  }
};
