import styled from "styled-components";

export const SecondaryText = styled.div`
  color: ${({ theme }) => theme.textColors.secondary};
`;

interface PageTitleProps {
  marginBottom?: number;
}

export const PageTitle = styled.h1<PageTitleProps>`
  margin-bottom: ${({ marginBottom }) => marginBottom ?? 0}px;
  font-weight: 400;
  font-size: 40px;
  line-height: 50px;
`;
