import { Button } from "../../../Buttons";
import { BackButton } from "../../../Buttons/BackButton";
import { ControlsGroup, Input, Label } from "../../../FormControls";
import { CardBackButtonWrapper, CardRoot, CardTitle, TopRightImage } from "../../AuthCard.styles";
import { GirlIcon } from "../GirlIcon";
import Circles from "./images/circles.svg";
import { ButtonWrapper } from "./LoginData.styles";

interface LoginDataProps {
  onGoBack: () => void;
  onGoNext: () => void;
}

export const LoginData = ({ onGoBack, onGoNext }: LoginDataProps) => {
  return (
    <CardRoot>
      <TopRightImage src={Circles} />

      <CardBackButtonWrapper>
        <BackButton onClick={onGoBack}>Вход</BackButton>
      </CardBackButtonWrapper>

      <CardTitle>Регистрация</CardTitle>

      <GirlIcon />

      <ControlsGroup marginBottom={24}>
        <Label>Email</Label>
        <Input type="email" />
      </ControlsGroup>

      <ControlsGroup marginBottom={24}>
        <Label>Пароль</Label>
        <Input type="password" />
      </ControlsGroup>

      <ControlsGroup>
        <Label>Повторите пароль</Label>
        <Input type="password" />
      </ControlsGroup>

      <ButtonWrapper>
        <Button variant="default" onClick={onGoNext}>
          Продолжить
        </Button>
      </ButtonWrapper>
    </CardRoot>
  );
};
