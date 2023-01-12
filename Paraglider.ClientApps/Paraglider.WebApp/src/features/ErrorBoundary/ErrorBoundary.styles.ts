import styled from "styled-components";

export const ErrorBoundaryRoot = styled.div`
  padding: 20px;

  width: 100vw;
  height: 100vh;

  display: flex;
  justify-content: center;
  align-items: center;
`;

export const ErrorBoundaryCard = styled.div`
  padding: 24px;
  max-width: 560px;

  border-radius: 12px;
  background-color: ${({ theme }) => theme.bgColors.white};
  box-shadow: 0px 4px 30px rgba(136, 135, 135, 0.15);
`;

export const ErrorBoundaryTitle = styled.h2`
  margin-bottom: 8px;
`;

export const ErrorDescription = styled.div`
  margin-bottom: 32px;
`;
