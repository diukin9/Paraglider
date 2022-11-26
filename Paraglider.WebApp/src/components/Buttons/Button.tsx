import React, { DetailedHTMLProps, HTMLAttributes } from "react";
import { DefaultButton } from "./Buttons.styles";

type DefaultButtonProps = DetailedHTMLProps<
  HTMLAttributes<HTMLButtonElement>,
  HTMLButtonElement
>;

interface ButtonProps extends DefaultButtonProps {}

export const Button = (props: ButtonProps) => {
  const { onClick, children } = props;

  return <DefaultButton onClick={onClick}>{children}</DefaultButton>;
};
