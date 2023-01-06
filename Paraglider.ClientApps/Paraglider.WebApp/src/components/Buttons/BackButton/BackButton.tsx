import { PropsWithChildren } from "react";

import { SecondaryText } from "../../Text/Common.styles";
import ArrowLeft from "./arrow-left.svg";
import { BackButtonIcon, BackButtonRoot } from "./BackButton.styles";

interface BackButtonProps {
  onClick: () => void;
}

export const BackButton = (props: PropsWithChildren<BackButtonProps>) => {
  const { onClick, children = "Назад" } = props;

  return (
    <BackButtonRoot onClick={onClick}>
      <SecondaryText>
        <BackButtonIcon src={ArrowLeft} />
        <span>{children}</span>
      </SecondaryText>
    </BackButtonRoot>
  );
};
