import styled from "styled-components";

export const HeaderRoot = styled.header`
  position: fixed;
  height: ${({ theme }) => theme.layout.header.height};
  width: 100%;
  top: 0;
  left: 0;
  background-color: ${({ theme }) => theme.bgColors.white};
  z-index: 100;
`;

export const HeaderWrapper = styled.div`
  position: relative;
  padding: 0 ${({ theme }) => theme.layout.padding};
  margin: 0 auto;
  width: ${({ theme }) => theme.layout.maxWidth};
  height: inherit;
`;

export const DefaultContentWrapper = styled.div`
  height: inherit;
  display: flex;
  justify-content: space-between;
  align-items: center;
`;
