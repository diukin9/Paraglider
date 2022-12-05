import styled from "styled-components";

export const ContentRoot = styled.div`
  padding-top: 60px;
  padding-right: ${({ theme }) => theme.layout.padding};
  padding-left: ${({ theme }) => theme.layout.padding};
`;

export const ContentWrapper = styled.div`
  position: relative;
  width: ${({ theme }) => theme.layout.maxWidth};
  margin: 0 auto;
`;
