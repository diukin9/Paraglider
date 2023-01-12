import { Link as RouterLink } from "react-router-dom";
import styled from "styled-components";

export const Link = styled(RouterLink)`
  color: ${({ theme }) => theme.textColors.primary};
  font-weight: bold;
  text-decoration: none;

  :visited {
    color: ${({ theme }) => theme.textColors.primary};
  }

  :hover {
    color: ${({ theme }) => theme.textColors.primary};
    text-decoration: underline;
  }
`;
