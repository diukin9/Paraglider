import { Button } from "../../Buttons";
import { ControlsGroup, Input, Label } from "../../FormControls";
import { CardRoot, TopRightImage } from "../AuthCard.styles";
import { AuthProviders } from "./AuthProviders";
import Circles from "./images/circles.svg";
import { ButtonWrapper, LoginCardTitle } from "./LoginCard.styles";

interface LoginCardProps {
  onGoToRegistration: () => void;
}

export const LoginCard = ({ onGoToRegistration }: LoginCardProps) => {
  return (
    <CardRoot>
      <TopRightImage src={Circles} />
      <LoginCardTitle>Вход</LoginCardTitle>
      <AuthProviders />

      <ControlsGroup marginBottom={24}>
        <Label>Email</Label>
        <Input type="email" />
      </ControlsGroup>

      <ControlsGroup marginBottom={24}>
        <Label>Пароль</Label>
        <Input type="password" />
      </ControlsGroup>

      <ButtonWrapper>
        <Button variant="default" onClick={() => null}>
          Войти
        </Button>
        <Button variant="ghost" onClick={onGoToRegistration}>
          Зарегистрироваться
        </Button>
      </ButtonWrapper>
    </CardRoot>
  );
};
