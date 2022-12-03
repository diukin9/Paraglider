import styled from "styled-components";

export const HeaderRoot = styled.header`
  position: sticky;
  top: 0;
  right: 0;
  left: 0;
  background-color: ${({ theme }) => theme.bgColors.white};
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
