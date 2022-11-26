import styled from "styled-components";

export const HeaderRoot = styled.header`
  position: fixed;
  top: 0;
  right: 0;
  left: 0;
  background-color: ${({ theme }) => theme.bgColors.white};
`;

export const HeaderWrapper = styled.div`
  max-width: ${({ theme }) => theme.layout.maxWidth};
  margin: 0 auto;
`;

export const DefaultContentWrapper = styled.div`
  height: 140px;
  display: flex;
  justify-content: space-between;
  align-items: center;
`;
