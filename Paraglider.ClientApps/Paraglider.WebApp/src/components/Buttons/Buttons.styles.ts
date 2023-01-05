import styled, { css } from "styled-components";
import { ButtonVariant } from "./Buttons.helpers";

export const DefaultButton = styled.button<{ variant: ButtonVariant }>`
  padding: 16px 48px;

  border: none;
  border-radius: 12px;

  font-size: 16px;
  font-weight: 700;

  ${({ variant }) => getButtonStyles(variant)}

  cursor: pointer;
`;

const getButtonStyles = (variant: ButtonVariant) => {
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
        background-color: ${({ theme }) => theme.bgColors.light};

        :hover {
          box-shadow: 0px 12px 20px -8px rgba(255, 135, 135, 0.85);
          transition: box-shadow 0.2s;
        }
      `;
  }
};
