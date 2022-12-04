import styled from "styled-components";

export const ContentRoot = styled.div`
  padding: 0 ${({ theme }) => theme.layout.padding};
  margin: 0 auto;
  position: relative;
  width: ${({ theme }) => theme.layout.maxWidth};
`;
