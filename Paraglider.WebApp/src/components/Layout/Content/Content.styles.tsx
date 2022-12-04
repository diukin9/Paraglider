import styled from "styled-components";

export const ContentRoot = styled.div`
  padding: 0 ${({ theme }) => theme.layout.padding};
  margin-top: ${({ theme }) => theme.layout.header.height};
  margin-left: auto;
  margin-right: auto;
  position: relative;
  width: ${({ theme }) => theme.layout.maxWidth};
`;
