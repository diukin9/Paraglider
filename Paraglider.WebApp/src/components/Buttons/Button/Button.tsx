import { PropsWithChildren } from "react";

import { ButtonVariant } from "../Buttons.helpers";
import { DefaultButton } from "../Buttons.styles";

interface ButtonProps {
  onClick: () => void;
  variant?: ButtonVariant;
}

export const Button = (props: PropsWithChildren<ButtonProps>) => {
  const { onClick, children, variant = "default" } = props;

  return (
    <DefaultButton variant={variant} onClick={onClick}>
      {children}
    </DefaultButton>
  );
};
