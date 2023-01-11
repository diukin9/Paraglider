import styled from "styled-components";

import { CardTitle } from "../AuthCard.styles";

export const EmailCardTitle = styled(CardTitle)`
  margin-bottom: 60px;
`;

export const MailIconContainer = styled.img`
  display: block;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 40px;
`;

export const EmailDescriptionContainer = styled.p`
  margin-bottom: 44px;
  color: ${({ theme }) => theme.textColors.secondary};
`;

export const EmailContainer = styled.span`
  color: ${({ theme }) => theme.textColors.default};
`;

export const ButtonWrapper = styled.div`
  display: flex;
  justify-content: center;
`;
