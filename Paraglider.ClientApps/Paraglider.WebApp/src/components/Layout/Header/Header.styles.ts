import styled from "styled-components";

export const HeaderRoot = styled.header`
  padding: 0 ${({ theme }) => theme.layout.padding};
  background-color: ${({ theme }) => theme.bgColors.white};
  z-index: 1000;
`;

export const HeaderWrapper = styled.div`
  width: ${({ theme }) => theme.layout.maxWidth};
  margin: 0 auto;
`;

export const DefaultContentWrapper = styled.div`
  height: 110px;
  display: flex;
  justify-content: space-between;
  align-items: center;
`;
