import styled from "styled-components";

import { ButtonVariant, defaultButtonStyles, getButtonStyles } from "./Buttons.helpers";

export const DefaultButton = styled.button<{ variant: ButtonVariant }>`
  ${defaultButtonStyles}

  padding: 16px 48px;

  border-radius: 12px;
  font-size: 16px;

  ${({ variant }) => getButtonStyles(variant)}
`;
