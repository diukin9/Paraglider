import styled from "styled-components";

import { SecondaryText } from "../../../Text/Common.styles";

export const AuthProvidersRoot = styled.div`
  margin-bottom: 56px;
`;

export const AuthProvidersTitle = styled(SecondaryText)`
  margin-bottom: 16px;
  line-height: 20px;
`;

export const AuthProvidersContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 30px;
`;

export const AuthProviderBlockRoot = styled.a`
  flex-grow: 1;
  padding: 12px;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 8px;

  border-radius: 12px;
  color: ${({ theme }) => theme.textColors.default};
  background: ${({ theme }) => theme.bgColors.white};
  box-shadow: 0px 2px 8px rgba(64, 64, 64, 0.1);

  cursor: pointer;
  text-decoration: none;
  user-select: none;

  :hover {
    outline: 2px solid ${({ theme }) => theme.bgColors.primary};
  }
`;

export const AuthProviderIcon = styled.img`
  height: 28px;
  width: 28px;
`;

export const AuthProviderName = styled.div`
  font-size: 16px;
  line-height: 20px;
`;
