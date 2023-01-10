import { ButtonHTMLAttributes, PropsWithChildren } from "react";

import { ButtonVariant } from "../Buttons.helpers";
import { DefaultButton } from "../Buttons.styles";

interface DefaultButtonProps {
  type?: ButtonHTMLAttributes<HTMLButtonElement>["type"];
}

export interface ButtonProps extends PropsWithChildren<DefaultButtonProps> {
  onClick: () => void;
  variant?: ButtonVariant;
}

export const Button = (props: ButtonProps) => {
  const { children, onClick, type = "button", variant = "default" } = props;

  return (
    <DefaultButton variant={variant} onClick={onClick} type={type}>
      {children}
    </DefaultButton>
  );
};
