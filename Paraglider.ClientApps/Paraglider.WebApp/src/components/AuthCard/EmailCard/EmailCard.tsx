import { ReactNode } from "react";

import { Button } from "../../Buttons";
import { CardRoot, TopRightImage } from "../AuthCard.styles";
import {
  ButtonWrapper,
  EmailCardTitle,
  EmailDescriptionContainer,
  MailIconContainer,
} from "./EmailCard.styles";
import Circles from "./images/circles.svg";
import Mail from "./images/mail.png";

interface Props {
  title: ReactNode;
  description: ReactNode;
  onMoveToLogin: () => void;
}

export const EmailCard = ({ title, description, onMoveToLogin }: Props) => {
  return (
    <CardRoot>
      <TopRightImage src={Circles} />

      <EmailCardTitle>{title}</EmailCardTitle>

      <MailIconContainer src={Mail} />

      <EmailDescriptionContainer>{description}</EmailDescriptionContainer>

      <ButtonWrapper>
        <Button variant="default" onClick={onMoveToLogin}>
          Вернуться к входу
        </Button>
      </ButtonWrapper>
    </CardRoot>
  );
};
