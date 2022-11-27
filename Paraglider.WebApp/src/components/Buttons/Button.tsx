import React, { DetailedHTMLProps, HTMLAttributes } from "react";
import { ButtonVariant } from "./Buttons.helpers";
import { DefaultButton } from "./Buttons.styles";

type DefaultButtonProps = DetailedHTMLProps<
  HTMLAttributes<HTMLButtonElement>,
  HTMLButtonElement
>;

interface ButtonProps extends DefaultButtonProps {
  variant?: ButtonVariant;
}

export const Button = (props: ButtonProps) => {
  const { variant = "default", onClick, children } = props;

  return (
    <DefaultButton variant={variant} onClick={onClick}>
      {children}
    </DefaultButton>
  );
};
