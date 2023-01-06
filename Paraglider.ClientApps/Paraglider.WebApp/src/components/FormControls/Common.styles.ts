import styled from "styled-components";

interface CommonControlsProps {
  width?: string;
}

export const Input = styled.input<CommonControlsProps>`
  outline: none;
  border: none;

  display: block;
  width: ${({ width }) => width || "100%"};
  padding: 5px 0;

  font-size: 16px;
  line-height: 24px;

  border-bottom: 2px solid ${({ theme }) => theme.bgColors.gray};
  background-color: transparent;

  :focus {
    border-color: ${({ theme }) => theme.bgColors.primary};
  }
`;

export const Label = styled.label`
  display: block;

  font-weight: 700;
  font-size: 16px;
  line-height: 24px;
`;

interface ControlsGroupProps {
  marginBottom?: number;
}

export const ControlsGroup = styled.div<ControlsGroupProps>`
  margin-bottom: ${({ marginBottom }) => marginBottom ?? 0}px;
`;
