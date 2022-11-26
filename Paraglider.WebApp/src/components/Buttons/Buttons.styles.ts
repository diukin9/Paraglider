import styled from "styled-components";

export const DefaultButton = styled.button`
  padding: 16px 48px;

  border: none;
  border-radius: 12px;

  font-size: 16px;
  font-weight: 700;

  color: ${({ theme }) => theme.textColors.light};
  background-color: ${({ theme }) => theme.bgColors.primary};

  cursor: pointer;

  :hover {
    box-shadow: 0px 12px 20px -8px rgba(255, 135, 135, 0.85);
    transition: box-shadow 0.2s;
  }
`;
